using System;

namespace TeamRocketProxy.Integration
{
    [Flags]
    public enum PluginCapabilities
    {
        None = 0,
        SupportsLiveCapture,
        SupportsSessionPersistence
    }
}
