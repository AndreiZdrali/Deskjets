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
        public static bool IsScrolling { get; set; } = false;

        public TopBar()
        {
            InitializeComponent();

            this.DataContext = Global.GeneralSettings.TopBarSettings;
        }

        //ca sa pot sa dau update din alte ferestre fara referinta la fereastra 
        public static void UpdateTopBar()
        {
            if (Utils.IsWindowOpen<TopBar>())
            {
                StackPanel bubbleStackPanel = Application.Current.Windows.OfType<TopBar>().ToList()[0].bubbleStackPanel;
                bubbleStackPanel.Children.Clear();
                foreach (BubbleButtonProperties buttonProperties in Global.UnserializableSettings.BubbleButtonPropertiesList)
                {
                    bubbleStackPanel.Children.Add(new BubbleButton()
                    {
                        Margin = new Thickness(5, 0, 5, 0),
                        Color = Utils.StringToBrush(buttonProperties.Color),
                        HighlightColor = Utils.StringToBrush(buttonProperties.HighlightColor),
                        ExecutablePath = buttonProperties.ExecutablePath
                    });
                }
                bubbleStackPanel.Width = (Global.UnserializableSettings.BubbleButtonPropertiesList.Count / 9 + 1) * 540;
            }
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

            this.bubbleScrollViewer.PreviewMouseWheel += bubbleScrollViewer_PreviewMouseWheel;

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
            bubbleStackPanel.Width = (Global.UnserializableSettings.BubbleButtonPropertiesList.Count / 9 + 1) * 540;
        }

        private async void bubbleScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (IsScrolling)
            {
                e.Handled = true;
                return;
            }
            if (e.Delta < 0)
            {
                IsScrolling = true;
                await ScrollAnimations.ScrollPixels(sender, 540);
                IsScrolling = false;
            }
            else
            {
                IsScrolling = true;
                await ScrollAnimations.ScrollPixels(sender, -540);
                IsScrolling = false;
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
            Utils.OpenWindow<ManageTopBarWindow>(true);
        }
        #endregion
    }
}
