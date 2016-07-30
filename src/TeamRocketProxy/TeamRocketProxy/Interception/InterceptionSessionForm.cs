using System;
using System.Drawing;
using System.Windows.Forms;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy.Interception
{
    public partial class InterceptionSessionForm : Form
    {
        public InterceptionSessionForm()
        {
            InitializeComponent();
            SaveFilterUserTextTextBoxPlaceholderValues();
        }

        IRocketPlugin plugin;
        IInterceptionContext context;
        volatile bool dirty;

        public void SetPlugin(IRocketPlugin plugin)
        {

            this.plugin = plugin;

            var supportsPersistence = plugin.HasCapabilities(PluginCapabilities.SupportsSessionPersistence);
            openToolStripMenuItem.Enabled = supportsPersistence;
            saveToolStripMenuItem.Enabled = supportsPersistence;

            LoadNewContext();
            context.Initialize(); // TODO: Clean up for live capture flags
        }

        void LoadNewContext()
        {
            DestroyContext();
            context = plugin.GetInterceptionContext(new InterceptionServiceProvider());
            context.OnNewMessageIntercepted += OnNewMessageIntercepted;
        }

        void OnExitMenuItemClicked(object sender, EventArgs e)
            => Close();

        void OnNewMessageIntercepted(object sender, MessageInterceptionEventArgs e)
        {
            dirty = true;
            this.BeginInvoke(() =>
            {
                if (this.dirty)
                {
                    RepopulateListBox();
                }
            });
        }

        #region Filter Options

        string searchTextBoxPlaceholderText;
        Color searchTextBoxPlaceholderColor;
        readonly Color searchTextBoxUserTextColor = Color.Black;

        void OnFilterRadioButtonCheckedChanged(object sender, EventArgs e)
            => RepopulateListBox();

        void OnFilterUserTextChanged(object sender, EventArgs e)
            => RepopulateListBox();

        void OnFilterUserTextTextBoxEnter(object sender, EventArgs e)
        {
            if (filterUserTextTextBox.Text == searchTextBoxPlaceholderText)
            {
                filterUserTextTextBox.Text = string.Empty;
                filterUserTextTextBox.ForeColor = searchTextBoxUserTextColor;
            }
            else
            {
                filterUserTextTextBox.SelectAll();
            }
        }

        void OnFilterUserTextTextBoxLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filterUserTextTextBox.Text))
            {
                filterUserTextTextBox.Text = searchTextBoxPlaceholderText;
                filterUserTextTextBox.ForeColor = searchTextBoxPlaceholderColor;
            }
        }

        void SaveFilterUserTextTextBoxPlaceholderValues()
        {
            searchTextBoxPlaceholderText = filterUserTextTextBox.Text;
            searchTextBoxPlaceholderColor = filterUserTextTextBox.ForeColor;
        }

        string GetFilterUserText()
        {
            var text = filterUserTextTextBox.Text;

            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            if (text == searchTextBoxPlaceholderText)
            {
                return null;
            }

            return text;
        }

        MessageDirection GetFilterMessageDirection()
        {
            var direction = MessageDirection.None;

            if (filterDirectionAnyRadioButton.Checked || filterDirectionInRadioButton.Checked)
            {
                direction |= MessageDirection.Inbound;
            }

            if (filterDirectionAnyRadioButton.Checked || filterDirectionOutRadioButton.Checked)
            {
                direction |= MessageDirection.Outbound;
            }

            return direction;
        }

        #endregion

        void RepopulateListBox()
        {
            dirty = false;

            var filterOptions = new FilterOptions(GetFilterMessageDirection(), GetFilterUserText());
            context.SetFilterOptions(filterOptions);

            messagesListView.Items.Clear();

            var messages = context.GetMessages();
            foreach (var message in messages)
            {
                var item = MakeListViewItem(message);
                messagesListView.Items.Add(item);
            }
        }

        #region Messages List View Selection

        ListViewItem selectedListViewItem;

        void OnMessagesListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (messagesListView.SelectedItems.Count != 1)
            {
                return;
            }

            var selectedItem = messagesListView.SelectedItems[0];
            if (selectedItem != selectedListViewItem)
            {
                selectedListViewItem = selectedItem;
                RepopulateTreeView(selectedItem);
            }
        }

        void RepopulateTreeView(ListViewItem listViewItem)
        {
            var message = (IInterceptedMessage)listViewItem.Tag;
            var node = new TreeNode();
            var explorer = new TreeViewMessageExplorer(node);

            plugin.Describe(explorer, message);

            messageExplorerTreeView.Nodes.Clear();

            foreach (TreeNode childNode in node.Nodes)
            {
                messageExplorerTreeView.Nodes.Add(childNode);
                childNode.Expand();
            }

            RecursiveExpandNodes(node, threshold: 15);
        }

        static void RecursiveExpandNodes(TreeNode node, int threshold)
        {
            if (node.Nodes.Count > 0 && node.Nodes.Count <= threshold)
            {
                node.Expand();
            }

            foreach (TreeNode child in node.Nodes)
            {
                RecursiveExpandNodes(child, threshold);
            }
        }

        #endregion

        static ListViewItem MakeListViewItem(IInterceptedMessage message)
        {
            var item = new ListViewItem(message.MessageType);
            item.Tag = message;

            item.SubItems.Add(new ListViewItem.ListViewSubItem()
            {
                Name = "#",
                Text = message.MessageID
            });

            item.SubItems.Add(new ListViewItem.ListViewSubItem()
            {
                Name = "Direction",
                Text = GetDirectionText(message.Direction)
            });

            item.SubItems.Add(new ListViewItem.ListViewSubItem()
            {
                Name = "Message",
                Text = message.MessageType
            });

            return item;
        }

        static string GetDirectionText(MessageDirection direction)
        {
            switch (direction)
            {
                case MessageDirection.None:
                    return "---";

                case MessageDirection.Inbound:
                    return "In";

                case MessageDirection.Outbound:
                    return "Out";

                default:
                    return "???";
            }
        }

        void DestroyContext()
        {
            if (context != null)
            {
                context.OnNewMessageIntercepted -= OnNewMessageIntercepted;
                context.Dispose();
            }
        }

        void OnFormClosing(object sender, FormClosingEventArgs e)
            => DestroyContext();

        void OnFormClosed(object sender, FormClosedEventArgs e)
            => Application.Exit();

        void OnOpenMenuItemClicked(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Multiselect = false;

            var result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadNewContext();
                context.Load(openDialog.FileName);
            }

        }

        void OnSaveMenuItemClicked(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            var result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                context.Save(saveDialog.FileName);
            }
        }
    }
}
