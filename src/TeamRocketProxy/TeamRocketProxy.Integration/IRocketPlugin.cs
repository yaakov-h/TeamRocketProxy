using System;

namespace TeamRocketProxy.Integration
{
    public interface IRocketPlugin
    {
        PluginDescriptor GetDescriptor();
        PluginCapabilities Capabilities { get; }

        IInterceptionContext GetInterceptionContext(IServiceProvider serviceProvider);

        void Describe(IMessageExplorer explorer, IInterceptedMessage message);
    }
}
