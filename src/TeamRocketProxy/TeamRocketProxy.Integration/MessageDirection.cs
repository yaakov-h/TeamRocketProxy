using System;

namespace TeamRocketProxy.Integration
{
    [Flags]
    public enum MessageDirection
    {
        None = 0,
        Inbound,
        Outbound,

        Any = Inbound | Outbound
    }
}
