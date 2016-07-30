namespace TeamRocketProxy.Integration
{
    public static class RocketPluginExtensions
    {
        public static bool HasCapabilities(this IRocketPlugin plugin, PluginCapabilities capabilities)
            => (plugin.Capabilities & capabilities) != 0;
    }
}
