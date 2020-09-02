using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using Deskjets.Windows;
using Deskjets.Settings;
using System.IO;

namespace Deskjets.Classes
{
    static class Global
    {
        public static string SettingsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            @"My Apps\Deskjets");
        public static string SettingsFile = Path.Combine(SettingsFolder, "Serializable.dat");
        public static string UnserializableSettingsFile = Path.Combine(SettingsFolder, "Unserializable.dat");

        public static GeneralSettings GeneralSettings = new GeneralSettings();
        public static UnserializableSettings UnserializableSettings = new UnserializableSettings();

        public static System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon() { Visible = true };
        
        static Global()
        {
            #region SETARI NOTIFYICON
            using (var btm = Utils.BitmapImageToBitmap((BitmapImage)Application.Current.FindResource("AppIcon")))
                notifyIcon.Icon = Icon.FromHandle(btm.GetHicon());

            //Global.notifyIcon.DoubleClick += (s, e) => Utils.OpenWindow<SettingsWindow>(true);

            notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Settings", null, (s, e) => Utils.OpenWindow<SettingsWindow>(true));
            notifyIcon.ContextMenuStrip.Items.Add("-");
            notifyIcon.ContextMenuStrip.Items.Add("Add Button TB", null, (s, e) => Utils.OpenWindow<TopBarAddWindow>(true));
            notifyIcon.ContextMenuStrip.Items.Add("-");
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, (s, e) => Application.Current.Shutdown());
            #endregion
        }
    }
}
