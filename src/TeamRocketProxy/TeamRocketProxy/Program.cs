using System;
using System.Linq;
using System.Windows.Forms;
using TeamRocketProxy.PluginManagement;

namespace TeamRocketProxy
{
    static class Program
    {
        [STAThread]
        static int Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var reader = new PluginReader("Plugins");
            var plugins = reader.LoadPlugins();

            if (!plugins.Any())
            {
                MessageBox.Show(
                    "No plugins could be found. Please check that plugins are correctly installed in the Plugins directory.",
                    "Team Rocket Proxy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return -1;
            }

            var form = new PluginSelectionForm();
            form.SetPlugins(plugins);
            form.Show();

            Application.Run();
            return 0;
        }
    }
}
