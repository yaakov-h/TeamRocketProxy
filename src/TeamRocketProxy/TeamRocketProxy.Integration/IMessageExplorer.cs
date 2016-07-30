using System;

namespace TeamRocketProxy.Integration
{
    public interface IMessageExplorer
    {
        // For properties
        void AddKeyValuePair(string key, Type valueType, object value);
        IMessageExplorer AddChildObject(string key);

        // For lists/collections
        void AddKeyValuePair(int index, Type valueType, object value);
        IMessageExplorer AddChildObject(int index, string key);

        void AddError(string errorText);
    }
}
