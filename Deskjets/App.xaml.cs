using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using Deskjets.Classes;
using Deskjets.Settings;
using Deskjets.Windows;
using System.Diagnostics.CodeAnalysis;

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

            #region SETARI
            //mai intai verifica daca exista fisier de setari, daca nu creeaza
            if (File.Exists(Global.SettingsFile) &&  new FileInfo(Global.SettingsFile).Length != 0)
                Global.GeneralSettings = SaveLoad.DeserializeObject<GeneralSettings>(Global.SettingsFile);
            else
            {
                if (!Directory.Exists(Global.SettingsFolder))
                    Directory.CreateDirectory(Global.SettingsFolder);

                if (!File.Exists(Global.SettingsFile))
                    File.Create(Global.SettingsFile).Close();
            }

            //mai intai verifica daca exista fisier de setari, daca nu creeaza
            if (File.Exists(Global.UnserializableSettingsFile) && new FileInfo(Global.UnserializableSettingsFile).Length != 0)
                Global.UnserializableSettings = SaveLoad.DeserializeObject<UnserializableSettings>(Global.UnserializableSettingsFile);
            else
            {
                if (!Directory.Exists(Global.SettingsFolder))
                    Directory.CreateDirectory(Global.SettingsFolder);

                if (!File.Exists(Global.UnserializableSettingsFile))
                    File.Create(Global.UnserializableSettingsFile).Close();
            }

            Global.GeneralSettings.OpenOnStartup = File.Exists(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Deskjets.lnk"));
            Global.GeneralSettings.TopBarSettings.Background = Utils.StringToBrush(Global.UnserializableSettings.TopBarBackground);
            #endregion

            #region DESCHIDE FERESTRELE ACTIVATE
            if (Global.GeneralSettings.TopBarSettings.Enabled)
                new TopBar().Show();

            Utils.OpenWindow<YTDownloadWindow>(true);
            #endregion
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveLoad.SerializeGeneralSettings();
            SaveLoad.SerializeUnserializableSettings();
            Global.notifyIcon.Dispose();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }
    }
}
