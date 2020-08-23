using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Deskjets.Controls;
using Deskjets.Animations;
using Deskjets.Settings;
using Deskjets.Classes;

namespace Deskjets.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TopBar : Window
    {
        public static bool IsExtended { get; set; } = false;

        public TopBar()
        {
            InitializeComponent();

            this.DataContext = Global.generalSettings.topBarSettings;

            #region TEST
            for (int i = 0; i < 30; i++)
            {
                bubbleStackPanel.Children.Add(new BubbleButton() { Margin = new Thickness(5, 0, 5, 0)});
            }
            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            this.Top = -this.Height + 5;

            this.MouseEnter += TopBarAnimations.TopBar_MouseEnter;
            this.MouseLeave += TopBarAnimations.TopBar_MouseLeave;

            this.settingsButton.MouseEnter += (s, e) =>
            {
                if (!TopBar.IsExtended) return;
                this.settingsButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#33ADD8E6");
            };
            this.settingsButton.MouseLeave += (s, e) => this.settingsButton.Background = Brushes.Transparent;
            this.settingsButton.MouseUp += settingsButton_MouseUp;

            this.addButton.MouseEnter += (s, e) =>
            {
                if (!TopBar.IsExtended) return;
                this.addButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#33ADD8E6");
            };
            this.addButton.MouseLeave += (s, e) => this.addButton.Background = Brushes.Transparent;
            this.addButton.MouseUp += addButton_MouseUp;
        }

        //de facut animatie smechera
        private void bubbleScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 50);
                //ScrollAnimations.ScrollForward(sender, 50);
            }
            else
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 50);
            }
            e.Handled = true;
        }

        #region BUTOANELE DIN DREAPTA
        private void settingsButton_MouseUp(object sender, MouseEventArgs e)
        {
            Utils.OpenWindow<SettingsWindow>(true);
        }
        
        private void addButton_MouseUp(object sender, MouseEventArgs e)
        {
            Utils.OpenWindow<TopBarAddWindow>(true);
        }
        #endregion
    }
}
