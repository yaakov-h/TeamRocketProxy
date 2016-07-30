using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy.PluginManagement
{
    internal sealed class PluginReader
    {
        public PluginReader(string directoryName)
        {
            this.directoryName = directoryName;
        }

        readonly string directoryName;

        public IEnumerable<IRocketPlugin> LoadPlugins()
        {
            var plugins = new List<IRocketPlugin>();
            var directory = new DirectoryInfo(directoryName);

            AppDomain.CurrentDomain.AssemblyResolve += OnAppDomainAssemblyResolve;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += OnAppDomainReflectionOnlyAssemblyResolve;

            if (directory.Exists)
            {
                var pluginInterfaceType = typeof(IRocketPlugin);
                var reflectionOnlyPluginInterfaceType = GetReflectionOnlyTypeFromType(pluginInterfaceType);

                foreach (var pluginDirectory in directory.EnumerateDirectories())
                {
                    foreach (var assemblyFileCandidate in pluginDirectory.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            var fullPath = assemblyFileCandidate.FullName;
                            var assemblyFile = Assembly.ReflectionOnlyLoadFrom(fullPath);
                            var hasAnyPlugins = assemblyFile.ExportedTypes.Any(type => reflectionOnlyPluginInterfaceType.IsAssignableFrom(type));
                            if (!hasAnyPlugins)
                            {
                                continue;
                            }

                            var assembly = Assembly.LoadFrom(fullPath);
                            foreach (var pluginType in assembly.ExportedTypes.Where(type => pluginInterfaceType.IsAssignableFrom(type)))
                            {
                                var instance = (IRocketPlugin)Activator.CreateInstance(pluginType);
                                plugins.Add(instance);
                            }
                        }
                        catch (BadImageFormatException)
                        {
                        }
                        catch (FileLoadException)
                        {
                        }
                    }
                }
            }

            return plugins;
        }

        static Type GetReflectionOnlyTypeFromType(Type type)
            => Assembly.ReflectionOnlyLoad(type.Assembly.FullName).GetType(type.FullName);

        static Assembly OnAppDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                return Assembly.Load(args.Name);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (FileLoadException)
            {
                return null;
            }
            catch (BadImageFormatException)
            {
                return null;
            }
        }

        static Assembly OnAppDomainReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                return Assembly.ReflectionOnlyLoad(args.Name);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (FileLoadException)
            {
                return null;
            }
            catch (BadImageFormatException)
            {
                return null;
            }
        }
    }
}
