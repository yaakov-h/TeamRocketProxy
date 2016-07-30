using System.IO;
using System.Linq;
using NUnit.Framework;
using TeamRocketProxy.PluginManagement;

namespace TeamRocketProxy.Test.PluginManagement
{
    class PluginReaderAssemblyLoadTestFixture
    {
        [Test]
        public void ReturnsNonNullValue()
        {
            Assert.That(reader.LoadPlugins(), Is.Not.Null);
        }

        [Test]
        public void HasOnePlugin()
        {
            Assert.That(reader.LoadPlugins(), Has.Count.EqualTo(1));
        }
        
        [Test]
        public void HasStubPlugin()
        {
            var plugin = reader.LoadPlugins().First();
            Assert.That(plugin, Is.Not.Null);
            Assert.That(plugin.GetType().FullName, Is.EqualTo("TeamRocketProxy.Test.StubPlugins.StubRocketPlugin"));

            var descriptor = plugin.GetDescriptor();
            Assert.That(descriptor.Name, Is.EqualTo("Stub"));
            Assert.That(descriptor.Description, Is.EqualTo("A basic mockup used for testing."));
            Assert.That(descriptor.Icon, Is.Null);
        }

        PluginReader reader;
        string directory;

        [OneTimeSetUp]
        public void SetUpDirectory()
        {
            directory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(Path.Combine(directory, "StubPlugin"));
            File.Copy(Path.Combine(TestContext.CurrentContext.TestDirectory, "Plugins", "Stubs", "TeamRocketProxy.Test.StubPlugins.dll"), Path.Combine(directory, "StubPlugin", "TeamRocketProxy.Test.StubPlugins.dll"));

            reader = new PluginReader(directory);
        }

        [OneTimeTearDown]
        public void TearDownDirectory()
        {
            try
            {
                Directory.Delete(directory, recursive: true);
            }
            catch (DirectoryNotFoundException)
            {
            }
        }
    }
}
