using System.Globalization;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy.Test.StubPlugins
{
    sealed class StubInterceptedMessage : IInterceptedMessage
    {
        public StubInterceptedMessage(long messageID, MessageDirection direction, string name)
        {
            MessageID = messageID.ToString(CultureInfo.InvariantCulture);
            Direction = direction;
            MessageType = name;
        }
        
        public string MessageID { get; }

        public MessageDirection Direction { get; }

        public string MessageType { get; }
    }
}
