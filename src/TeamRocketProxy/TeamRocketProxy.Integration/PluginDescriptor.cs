using System.Drawing;

namespace TeamRocketProxy.Integration
{
    public sealed class PluginDescriptor
    {
        public PluginDescriptor(string name, string description)
            : this(name, description, null)
        {
        }

        public PluginDescriptor(string name, string description, Image icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }

        public string Name { get; }
        public string Description { get; }
        public Image Icon { get; }
    }
}
