using System;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy.Test.StubPlugins
{
    public sealed class StubRocketPlugin : IRocketPlugin
    {
        public PluginDescriptor GetDescriptor()
            => new PluginDescriptor("Stub", "A basic mockup used for testing.");

        public PluginCapabilities Capabilities => PluginCapabilities.SupportsLiveCapture;

        public IInterceptionContext GetInterceptionContext(IServiceProvider serviceProvider)
            => new StubInterceptionContext();
    }
}
