namespace TeamRocketProxy.Integration
{
    public interface IInterceptedMessage
    {
        string MessageID { get; }
        MessageDirection Direction { get; }
        string MessageType { get; }
    }
}
