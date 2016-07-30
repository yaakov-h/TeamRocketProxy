using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using TeamRocketProxy.Integration;
using TeamRocketProxy.Integration.Http;

namespace PokemonGo
{
    public sealed class PokemonGoPlugin : IRocketPlugin
    {
        public PluginCapabilities Capabilities
            => PluginCapabilities.SupportsLiveCapture;
        
        public PluginDescriptor GetDescriptor()
            => new PluginDescriptor("Pokemon Go", "MITM proxy for Pokemon Go");
            

        public IInterceptionContext GetInterceptionContext(IServiceProvider serviceProvider)
        {
            var proxy = serviceProvider.GetService<IHttpProxy>();
            return new PokemonGoInterceptionContext(proxy);
        }

        public void Describe(IMessageExplorer explorer, IInterceptedMessage message)
        {
            var interceptedMessage = (InterceptedMessage)message;
            var messageObject = (IMessage)Activator.CreateInstance(interceptedMessage.ObjectType);

            try
            {
                messageObject.MergeFrom(interceptedMessage.GetInputStream());
            }
            catch (InvalidProtocolBufferException ex)
            {
                explorer.AddError(string.Format(CultureInfo.InvariantCulture, "An error occured whilst parsing this message: {0}", ex.Message));
                return;
            }

            var messageExplorer = explorer.AddChildObject(interceptedMessage.MessageType);
            DescribeMessageRecursive(messageExplorer, messageObject);
        }

        void DescribeMessageRecursive(IMessageExplorer explorer, IMessage message)
        {
            var descriptor = message.Descriptor;

            foreach (var field in descriptor.Fields.InDeclarationOrder())
            {
                if (field.FieldType == FieldType.Message && field.IsRepeated)
                {
                    var messageListExplorer = explorer.AddChildObject(field.Name);

                    var values = ((IEnumerable)field.Accessor.GetValue(message)).Cast<IMessage>().ToArray();
                    for (int i = 0; i < values.Length; i++)
                    {
                        DescribeMessageRecursive(messageListExplorer, values[i]);
                    }
                }
                else if (field.FieldType == FieldType.Message)
                {
                    var messageValue = (IMessage)field.Accessor.GetValue(message);
                    if (messageValue == null)
                    {
                        explorer.AddKeyValuePair(field.Name, GetClrType(field.FieldType), null);
                    }
                    else
                    {
                        var messageExplorer = explorer.AddChildObject(field.Name);
                        DescribeMessageRecursive(messageExplorer, messageValue);
                    }
                }
                else if (field.IsRepeated)
                {
                    var collectionExplorer = explorer.AddChildObject(field.Name);
                    var type = GetClrType(field.FieldType);

                    var values = ((IEnumerable)field.Accessor.GetValue(message)).Cast<object>().ToArray();
                    for (int i = 0; i < values.Length; i++)
                    {
                        collectionExplorer.AddKeyValuePair(i, type, values[i]);
                    }
                }
                else
                {
                    explorer.AddKeyValuePair(field.Name, GetClrType(field.FieldType), field.Accessor.GetValue(message));
                }
            }
        }

        static Type GetClrType(FieldType type)
        {
            switch (type)
            {
                case FieldType.Bool:
                    return typeof(bool);

                case FieldType.Bytes:
                    return typeof(byte[]);

                case FieldType.Double:
                    return typeof(double);

                case FieldType.Enum:
                    return typeof(Enum);

                case FieldType.Fixed32:
                case FieldType.Int32:
                case FieldType.SInt32:
                    return typeof(int);

                case FieldType.Fixed64:
                case FieldType.Int64:
                case FieldType.SInt64:
                    return typeof(long);

                case FieldType.Float:
                    return typeof(float);

                case FieldType.String:
                    return typeof(string);

                case FieldType.UInt32:
                    return typeof(uint);

                case FieldType.UInt64:
                    return typeof(ulong);

                default:
                    return typeof(object);
            }
        }
    }
}
