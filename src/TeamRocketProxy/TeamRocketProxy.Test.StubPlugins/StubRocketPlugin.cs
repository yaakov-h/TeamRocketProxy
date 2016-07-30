using TeamRocketProxy.Integration;

namespace TeamRocketProxy.Test.StubPlugins
{
    public sealed class StubRocketPlugin : IRocketPlugin
    {
        public PluginDescriptor GetDescriptor()
            => new PluginDescriptor("Stub", "A basic mockup used for testing.");
    }
}
