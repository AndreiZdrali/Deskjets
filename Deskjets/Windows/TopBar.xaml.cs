using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Deskjets.Controls;
using Deskjets.Animations;
using Deskjets.Classes;
using Microsoft.Win32;

namespace Deskjets.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TopBar : Window
    {
        //sa adaug si SetOnDesktop daca nu e suficient sa bag SetWindowPos cand schimba focus-ul
        #region SA FIE TOOLWINDOW
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private int GWL_EXSTYLE = -20;
        private int WS_EX_TOOLWINDOW = 0x00000080;
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;

        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        private void SetBottom(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
        }
        #endregion

        public static bool IsExtended { get; set; } = false;

        public TopBar()
        {
            InitializeComponent();

            this.DataContext = Global.GeneralSettings.TopBarSettings;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowLongPtr64(new WindowInteropHelper(this).Handle, GWL_EXSTYLE, (IntPtr)WS_EX_TOOLWINDOW);
            //this.SetBottom(this);

            this.Left = SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            this.Top = -this.Height + 5;

            this.MouseEnter += TopBarAnimations.TopBar_MouseEnter;
            this.MouseLeave += TopBarAnimations.TopBar_MouseLeave;

            //this.GotFocus += (s, e) => this.SetBottom(this);
            //this.LostFocus += (s, e) => this.SetBottom(this);

            this.settingsButton.MouseEnter += (s, e) =>
            {
                if (!TopBar.IsExtended) return;
                this.settingsButton.Background = Utils.StringToBrush("#33ADD8E6");
            };
            this.settingsButton.MouseLeave += (s, e) => this.settingsButton.Background = Brushes.Transparent;
            this.settingsButton.MouseLeftButtonUp += settingsButton_MouseLeftButtonUp;

            this.addButton.MouseEnter += (s, e) =>
            {
                if (!TopBar.IsExtended) return;
                this.addButton.Background = Utils.StringToBrush("#33ADD8E6");
            };
            this.addButton.MouseLeave += (s, e) => this.addButton.Background = Brushes.Transparent;
            this.addButton.MouseLeftButtonUp += addButton_MouseLeftButtonUp;

            foreach(BubbleButtonProperties buttonProperties in Global.UnserializableSettings.BubbleButtonPropertiesList)
            {
                this.bubbleStackPanel.Children.Add(new BubbleButton()
                {
                    Margin = new Thickness(5, 0, 5, 0),
                    Color = Utils.StringToBrush(buttonProperties.Color),
                    HighlightColor = Utils.StringToBrush(buttonProperties.HighlightColor),
                    ExecutablePath = buttonProperties.ExecutablePath
                });
            }

        }

        //de facut animatie smechera
        private void bubbleScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 50);
            }
            else
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 50);
            }
            e.Handled = true;
        }

        #region BUTOANELE DIN DREAPTA
        private void settingsButton_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            Utils.OpenWindow<SettingsWindow>(true);
        }
        
        private void addButton_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            //Utils.OpenWindow<TopBarAddWindow>(true);
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = @"D:\",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                //salveaza proprietatile unserializable si creeaza butonul
                BubbleButtonProperties buttonProperties = new BubbleButtonProperties()
                {
                    Color = Utils.GetAverageColor(System.Drawing.Icon.ExtractAssociatedIcon(openFileDialog.FileName).ToBitmap()),
                    HighlightColor = "#FFFFFF",
                    ExecutablePath = openFileDialog.FileName
                };

                this.bubbleStackPanel.Children.Add(new BubbleButton()
                {
                    Margin = new Thickness(5, 0, 5, 0),
                    Color = Utils.StringToBrush(buttonProperties.Color),
                    HighlightColor = Utils.StringToBrush(buttonProperties.HighlightColor),
                    ExecutablePath = buttonProperties.ExecutablePath
                });

                Global.UnserializableSettings.BubbleButtonPropertiesList.Add(buttonProperties);
                SaveLoad.SerializeUnserializableSettings();
            }
        }
        #endregion
    }
}
