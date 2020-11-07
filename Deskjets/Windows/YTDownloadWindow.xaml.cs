using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Deskjets.Classes;
using Deskjets.Settings;

namespace Deskjets.Windows
{
    /// <summary>
    /// Interaction logic for YTDownloadWindow.xaml
    /// </summary>
    public partial class YTDownloadWindow : Window
    {
        public YTDownloadWindow()
        {
            InitializeComponent();

            this.DataContext = Global.GeneralSettings;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.closeButton.MouseEnter += (s, e) => this.closeButton.Background = Brushes.Red;
            this.closeButton.MouseLeave += (s, e) => this.closeButton.Background = Brushes.IndianRed;
            this.closeButton.MouseUp += (s, e) => this.Close();

            this.minimizeButton.MouseEnter += (s, e) => this.minimizeButton.Background = Brushes.ForestGreen;
            this.minimizeButton.MouseLeave += (s, e) => this.minimizeButton.Background = Brushes.MediumSeaGreen;
            this.minimizeButton.MouseUp += (s, e) => this.WindowState = WindowState.Minimized;

            this.downloadButton.Click += downloadButton_Click;
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //pt ca daca apas pe un buton misca fereastra in loc sa activeze MouseUp
            if (e.LeftButton == MouseButtonState.Pressed && !this.minimizeButton.IsMouseOver && !this.closeButton.IsMouseOver)
                this.DragMove();
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            //download
        }
    }
}
