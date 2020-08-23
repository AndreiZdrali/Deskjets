using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using Deskjets.Classes;
using Deskjets.Windows;

namespace Deskjets
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //nu stiu dc, dar daca nu tin asta aici imi dispare din system tray
            Global.notifyIcon.DoubleClick += (s, e) => { Utils.OpenWindow<SettingsWindow>(true); };

            TopBar topBar = new TopBar();
            topBar.Show();

            TopBarAddWindow topBarAddWindow = new TopBarAddWindow();
            topBarAddWindow.Show();

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Global.notifyIcon.Dispose();
        }
    }
}
