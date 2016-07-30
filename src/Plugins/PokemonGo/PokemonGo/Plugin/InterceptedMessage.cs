using System;
using System.Globalization;
using Google.Protobuf;
using TeamRocketProxy.Integration;

namespace PokemonGo
{
    sealed class InterceptedMessage : IInterceptedMessage
    {
        public InterceptedMessage(ulong requestID, int index, Type messageType, MessageDirection direction, byte[] data)
        {
            this.requestID = requestID;
            this.index = index;
            this.messageType = messageType;
            this.direction = direction;
            this.data = data;
        }

        readonly ulong requestID;
        readonly int index;
        readonly Type messageType;
        readonly MessageDirection direction;
        readonly byte[] data;

        public MessageDirection Direction => direction;

        public string MessageID
             => string.Format(CultureInfo.InvariantCulture, "{0}_{1}", requestID, index);

        public string MessageType => messageType.Name;

        public Type ObjectType => messageType;

        public CodedInputStream GetInputStream() => new CodedInputStream(data);
    }
}
