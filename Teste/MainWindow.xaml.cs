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

namespace Teste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();

        public MainWindow()
        {
            this.DataContext = viewModel;

            InitializeComponent();

            //https://stackoverflow.com/a/34361586 SA FOLOSESC SI USING, NU DOAR ASA
            var icon = System.Drawing.Icon.ExtractAssociatedIcon(@"D:\Descarcari\BakkesMod.exe");
            imagine.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, 
                new Int32Rect(0, 0, icon.Width, icon.Height), BitmapSizeOptions.FromEmptyOptions());

            System.Windows.Forms.NotifyIcon notify = new System.Windows.Forms.NotifyIcon();
            notify.Icon = icon;
            notify.Visible = true;
            notify.MouseClick += (s, e) => { if (e.Button == System.Windows.Forms.MouseButtons.Right) new MainWindow().Show(); };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.BackColor = Brushes.Red;
        }
    }
}
