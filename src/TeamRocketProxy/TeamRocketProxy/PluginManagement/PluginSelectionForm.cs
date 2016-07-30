using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TeamRocketProxy.Integration;
using TeamRocketProxy.Interception;

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
            pluginListView.LargeImageList = new ImageList { ImageSize = new Size(64, 64) };

            var imageIndex = 0;

            foreach(var plugin in plugins)
            {
                var item = new ListViewItem();
                var descriptor = plugin.GetDescriptor();

                item.Name = descriptor.Name;
                item.Text = descriptor.Name;

                if (descriptor.Icon != null)
                {
                    pluginListView.LargeImageList.Images.Add(descriptor.Icon);
                    item.ImageIndex = imageIndex;
                    imageIndex++;
                }

                item.Tag = plugin;

                pluginListView.Items.Add(item);
            }

        }

        void OnStartButtonClicked(object sender, EventArgs e)
        {
            var plugin = pluginListView.SelectedItems.Cast<ListViewItem>().FirstOrDefault()?.Tag as IRocketPlugin;
            if (plugin == null)
            {
                MessageBox.Show(this, "Please select a plugin.", "No Plugin Selected.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var interceptionForm = new InterceptionSessionForm();
                interceptionForm.SetPlugin(plugin);
                interceptionForm.Show();
                Close();
            }
        }
    }
}
