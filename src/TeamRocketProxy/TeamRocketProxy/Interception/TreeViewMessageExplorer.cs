using System;
using System.Globalization;
using System.Windows.Forms;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy.Interception
{
    sealed class TreeViewMessageExplorer : IMessageExplorer
    {
        public TreeViewMessageExplorer(TreeNode node)
        {
            this.node = node;
        }

        readonly TreeNode node;

        void IMessageExplorer.AddKeyValuePair(string key, Type valueType, object value)
        {
            var node = new TreeNode(FormattableString.Invariant($"{key}: {FormatObject(valueType, value)}"));
            this.node.Nodes.Add(node);
        }

        IMessageExplorer IMessageExplorer.AddChildObject(string key)
        {
            var node = new TreeNode(key);
            this.node.Nodes.Add(node);
            return new TreeViewMessageExplorer(node);
        }

        void IMessageExplorer.AddKeyValuePair(int index, Type valueType, object value)
        {
            var node = new TreeNode(FormattableString.Invariant($"[ {index} ]: {FormatObject(valueType, value)}"));
            this.node.Nodes.Add(node);
        }

        IMessageExplorer IMessageExplorer.AddChildObject(int index, string key)
        {
            var node = new TreeNode(FormattableString.Invariant($"[ {index} ]: {key}"));
            this.node.Nodes.Add(node);
            return new TreeViewMessageExplorer(node);
        }

        void IMessageExplorer.AddError(string errorText)
        {
            var node = new TreeNode(errorText);
            this.node.Nodes.Add(node);
        }

        static string FormatObject(Type valueType, object value)
        {
            string text;

            if (value == null)
            {
                text = "<null>";
            }
            else if (valueType == typeof(string))
            {
                text = FormattableString.Invariant($@"""{value}""");
            }
            else if (typeof(IFormattable).IsAssignableFrom(valueType))
            {
                text = ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture);
            }
            else
            {
                text = value.ToString();
            }

            return text;
        }
    }
}
