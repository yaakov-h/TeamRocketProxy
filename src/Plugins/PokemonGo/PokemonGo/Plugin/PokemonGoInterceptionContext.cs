using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Protobuf;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;
using TeamRocketProxy.Integration;
using TeamRocketProxy.Integration.Http;

namespace PokemonGo
{
    sealed class PokemonGoInterceptionContext : IInterceptionContext
    {
        public PokemonGoInterceptionContext(IHttpProxy proxy)
        {
            messages = new List<IInterceptedMessage>();
            this.proxy = proxy;
            proxy.OnSessionComplete += OnProxySessionComplete;
        }

        readonly object messagesLock = new object();
        readonly List<IInterceptedMessage> messages;
        readonly IHttpProxy proxy;

        FilterOptions currentFilterOptions;
        
        public event EventHandler<MessageInterceptionEventArgs> OnNewMessageIntercepted;

        public void Initialize()
        {
            var configuration = new HttpProxyConfiguration(15100);
            configuration.AllowRemoteConnections = true;
            configuration.DecryptSecureConnections = true;

            configuration.InterceptHost("pgorelease.nianticlabs.com");

            proxy.Start(configuration);
        }

        public IEnumerable<IInterceptedMessage> GetMessages()
        {
            IEnumerable<IInterceptedMessage> filteredMessages = this.messages;
            if (currentFilterOptions != null)
            {
                filteredMessages = filteredMessages.Where(m => (m.Direction & currentFilterOptions.Direction) != 0);

                if (!string.IsNullOrEmpty(currentFilterOptions.UserFilterText))
                {
                    filteredMessages = filteredMessages.Where(m => m.MessageType.IndexOf(currentFilterOptions.UserFilterText, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            return filteredMessages.ToList();
        }

        public void SetFilterOptions(FilterOptions options)
        {
            currentFilterOptions = options;
        }

        void OnProxySessionComplete(object sender, HttpSessionCompleteEventArgs e)
        {
            RequestEnvelope requestEnvelope;
            if (TryReadProtobuf(e.Session.GetRequestBody(), out requestEnvelope))
            {
                for (var i = 0; i < requestEnvelope.Requests.Count; i++)
                {
                    var request = requestEnvelope.Requests[i];

                    Type requestMessageType;
                    if (!TryFindRequestMessageType(request.RequestType, out requestMessageType))
                    {
                        continue;
                    }

                    var message = new InterceptedMessage(requestEnvelope.RequestId, i, requestMessageType, MessageDirection.Outbound, request.RequestMessage.ToByteArray());
                    NotifyInterceptedMessage(message);
                }

                ResponseEnvelope responseEnvelope;
                if (TryReadProtobuf(e.Session.GetResponseBody(), out responseEnvelope) && responseEnvelope.Returns.Count == requestEnvelope.Requests.Count)
                {
                    for (var i = 0; i < responseEnvelope.Returns.Count; i++)
                    {
                        var responseData = responseEnvelope.Returns[i];
                        var request = requestEnvelope.Requests[i];

                        Type responseMessageType;
                        if (!TryFindResponseMessageType(request.RequestType, out responseMessageType))
                        {
                            continue;
                        }

                        var message = new InterceptedMessage(responseEnvelope.RequestId, i, responseMessageType, MessageDirection.Inbound, responseData.ToByteArray());
                        NotifyInterceptedMessage(message);
                    }
                }
            }
        }

        void NotifyInterceptedMessage(IInterceptedMessage message)
            => OnNewMessageIntercepted?.Invoke(this, new MessageInterceptionEventArgs(message));

        public void Dispose()
            => proxy?.Stop();

        static bool TryReadProtobuf<TProto>(Stream stream, out TProto proto)
            where TProto : IMessage, new()
        {
            proto = new TProto();
            try
            {
                proto.MergeFrom(stream);
                return true;
            }
            catch (InvalidProtocolBufferException)
            {
                return false;
            }
        }

        static bool TryFindRequestMessageType(RequestType requestType, out Type messageType)
        {
            foreach (var type in typeof(RequestType).Assembly.GetTypes())
            {
                if (!typeof(IMessage).IsAssignableFrom(type))
                {
                    continue;
                }

                if (!type.Name.StartsWith(Enum.GetName(typeof(RequestType), requestType)))
                {
                    continue;
                }

                if (!type.Name.EndsWith("Response"))
                {
                    messageType = type;
                    return true;
                }
            }

            messageType = null;
            return false;
        }


        static bool TryFindResponseMessageType(RequestType requestType, out Type messageType)
        {
            foreach (var type in typeof(RequestType).Assembly.GetTypes())
            {
                if (!typeof(IMessage).IsAssignableFrom(type))
                {
                    continue;
                }

                if (type.Name == Enum.GetName(typeof(RequestType), requestType) + "Response")
                {
                    messageType = type;
                    return true;
                }
            }

            messageType = null;
            return false;
        }
    }
}
