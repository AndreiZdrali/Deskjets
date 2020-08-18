using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            TopBar topBar = new TopBar();
            topBar.Show();

            TopBarAddWindow topBarAddWindow = new TopBarAddWindow();
            topBarAddWindow.Show();

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}
