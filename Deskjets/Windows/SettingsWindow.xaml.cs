using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Deskjets.Classes;
using Deskjets.Settings;
using AngleSharp.Common;
using System.Diagnostics;

namespace Deskjets.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            this.DataContext = Global.GeneralSettings;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.closeButton.MouseEnter += (s, e) => this.closeButton.Background = Brushes.Red;
            this.closeButton.MouseLeave += (s, e) => this.closeButton.Background = Brushes.IndianRed;
            this.closeButton.MouseUp += (s, e) =>  this.Close();

            this.minimizeButton.MouseEnter += (s, e) => this.minimizeButton.Background = Brushes.ForestGreen;
            this.minimizeButton.MouseLeave += (s, e) => this.minimizeButton.Background = Brushes.MediumSeaGreen;
            this.minimizeButton.MouseUp += (s, e) => this.WindowState = WindowState.Minimized;

            this.openOnStartupToggle.Click += openOnStartupToggle_Click;

            this.ytPathBox.TextChanged += ytPathBox_TextChanged;

            this.topBarEnabledToggle.Checked += topBarEnabledToggle_Checked;
            this.topBarEnabledToggle.Unchecked += topBarEnabledToggle_Unchecked;

            this.bgColorBox.TextChanged += bgColorBox_TextChanged;

            this.resetButton.Click += resetButton_Click;
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //pt ca daca apas pe un buton misca fereastra in loc sa activeze MouseUp
            if (e.LeftButton == MouseButtonState.Pressed && !this.minimizeButton.IsMouseOver && !this.closeButton.IsMouseOver)
                this.DragMove();
        }

        private void openOnStartupToggle_Click(object sender, RoutedEventArgs e)
        {
            string shortcutPath = Path.Combine(Global.StartupFolder, "Deskjets.lnk");
            if (File.Exists(shortcutPath) && !Global.GeneralSettings.OpenOnStartup)
            {
                File.Delete(shortcutPath);
            }
            else if (Global.GeneralSettings.OpenOnStartup)
            {
                Utils.CreateShortcut(Global.ExecutablePath, shortcutPath, AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        private void ytPathBox_TextChanged(object sender, RoutedEventArgs e) //POT SA FOLOSESC SI LOSTFOCUS
        {
            if (Directory.Exists(ytPathBox.Text))
                SaveLoad.SerializeGeneralSettings();
        }

        private void topBarEnabledToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (!Utils.IsWindowOpen<TopBar>())
                Utils.OpenWindow<TopBar>(true);
            SaveLoad.SerializeGeneralSettings();
        }

        private void topBarEnabledToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Utils.IsWindowOpen<TopBar>())
                Application.Current.Windows.OfType<TopBar>().ToArray()[0].Close();
            SaveLoad.SerializeGeneralSettings();
        }

        private void bgColorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //daca nu da eroare atunci este culoare hex valida
            try
            {
                Utils.StringToBrush(bgColorBox.Text);
                Global.UnserializableSettings.TopBarBackground = bgColorBox.Text;
                SaveLoad.SerializeUnserializableSettings();
            }
            catch
            {

            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e) //SA SCHIMB IN DIALOG BOX CUSTOM
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure? This will delete are reset the settings files of the " +
                "application. May solve issues if using files from older versions.", "Delete settings files", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                File.Delete(Global.SettingsFile);
                File.Delete(Global.UnserializableSettingsFile);
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
