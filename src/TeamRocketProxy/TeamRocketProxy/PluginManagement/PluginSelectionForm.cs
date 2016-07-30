using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy
{
    public partial class PluginSelectionForm : Form
    {
        public PluginSelectionForm()
        {
            InitializeComponent();
        }
        
        public void SetPlugins(IEnumerable<IRocketPlugin> plugins)
        {
            pluginListView.Items.Clear();

            foreach(var plugin in plugins)
            {
                var item = new ListViewItem();
                var descriptor = plugin.GetDescriptor();

                item.Name = descriptor.Name;
                item.Text = descriptor.Name;

                if (descriptor.Icon != null)
                {
                    item.ImageList.Images.Add(descriptor.Icon);
                    item.ImageIndex = 0;
                }

                item.Tag = plugin;

                pluginListView.Items.Add(item);
            }

        }

        void OnStartButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
