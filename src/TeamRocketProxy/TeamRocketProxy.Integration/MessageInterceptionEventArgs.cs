using System;

namespace TeamRocketProxy.Integration
{
    public sealed class MessageInterceptionEventArgs : EventArgs
    {
        public MessageInterceptionEventArgs(IInterceptedMessage message)
        {
            Message = message;
        }

        IInterceptedMessage Message { get; }
    }
}
