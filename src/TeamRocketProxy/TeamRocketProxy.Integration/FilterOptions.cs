namespace TeamRocketProxy.Integration
{
    public sealed class FilterOptions
    {
        public FilterOptions(MessageDirection direction, string userFilterText)
        {
            Direction = direction;
            UserFilterText = userFilterText;
        }

        public MessageDirection Direction { get; }
        public string UserFilterText { get; }
    }
}
