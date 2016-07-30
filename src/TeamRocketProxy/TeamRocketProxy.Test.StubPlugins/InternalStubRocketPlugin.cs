using System;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy.Test.StubPlugins
{
    sealed class InternalStubRocketPlugin : IRocketPlugin
    {
        public PluginDescriptor GetDescriptor()
            => new PluginDescriptor("Internal Stub", "This plugin should never be visible because it is not public.");

        public PluginCapabilities Capabilities => PluginCapabilities.None;

        public IInterceptionContext GetInterceptionContext(IServiceProvider serviceProvider)
            => null;
    }
}