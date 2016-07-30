﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SetPlugin(IRocketPlugin plugin)
        {
            DestroyContext();

            this.plugin = plugin;
            context = plugin.GetInterceptionContext(new InterceptionServiceProvider());
            context.OnNewMessageIntercepted += OnNewMessageIntercepted;
        }

        void OnExitMenuItemClicked(object sender, EventArgs e)
            => Close();
        
        void OnNewMessageIntercepted(object sender, MessageInterceptionEventArgs e)
            => this.BeginInvoke(RepopulateListBox);

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

            if (text == searchTextBoxPlaceholderText && filterUserTextTextBox.ForeColor == searchTextBoxPlaceholderColor)
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
                Text = Enum.GetName(typeof(MessageDirection), message.Direction)
            });

            item.SubItems.Add(new ListViewItem.ListViewSubItem()
            {
                Name = "Message",
                Text = message.MessageType
            });

            return item;
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
    }
}
