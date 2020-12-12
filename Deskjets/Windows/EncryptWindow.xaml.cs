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
using System.Threading.Tasks;
using System.IO;
using Deskjets.Classes;

namespace Deskjets.Windows
{
    /// <summary>
    /// Interaction logic for EncryptWindow.xaml
    /// </summary>
    public partial class EncryptWindow : Window
    {
        private bool isEncrypting = false;

        public EncryptWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.closeButton.MouseEnter += (s, e) => this.closeButton.Background = Brushes.Red;
            this.closeButton.MouseLeave += (s, e) => this.closeButton.Background = Brushes.IndianRed;
            this.closeButton.MouseUp += (s, e) => this.Close();

            this.minimizeButton.MouseEnter += (s, e) => this.minimizeButton.Background = Brushes.ForestGreen;
            this.minimizeButton.MouseLeave += (s, e) => this.minimizeButton.Background = Brushes.MediumSeaGreen;
            this.minimizeButton.MouseUp += (s, e) => this.WindowState = WindowState.Minimized;

            this.chooseFileButton.Click += chooseFileButton_Click;
            this.encryptButton.Click += encryptButton_Click;
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //pt ca daca apas pe un buton misca fereastra in loc sa activeze MouseUp
            if (e.LeftButton == MouseButtonState.Pressed && !this.minimizeButton.IsMouseOver && !this.closeButton.IsMouseOver)
                this.DragMove();
        }

        private void chooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                InitialDirectory = @"D:\",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePathBox.Text = openFileDialog.FileName;
            }
        }

        private async void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = filePathBox.Text;
            string key = keyBox.Text;
            bool deleteOriginal = deleteOriginalCheck.IsChecked.HasValue ? deleteOriginalCheck.IsChecked.Value : false;

            #region VERIFICARI
            if (isEncrypting) return;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Invalid file!", "Error");
                return;
            }

            if (String.IsNullOrWhiteSpace(key))
            {
                MessageBox.Show("Invalid key!", "Error");
                return;
            }
            #endregion

            string outputPath = System.IO.Path.ChangeExtension(filePath, ".crypt");
            //in caz ca mai e un fisier cu numele ala
            if (File.Exists(outputPath))
            {
                if (MessageBox.Show("There is already a file with that name! Do you want to continue and overwrite the file?",
                    "Warning", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
            }

            isEncrypting = true;
            encryptButton.IsEnabled = false;

            //cheia sunt primele 8 litere din hash-ul parolei, ca sa aiba 128 biti
            string actualKey = Utils.CreateMD5(key).Substring(0, 8);
            //daca dadea eroare tot continua functia si stergea fisierul original
            bool successful = await Task.Run(() => Utils.EncryptFile(filePath, outputPath, actualKey));
            if (!successful)
            {
                isEncrypting = false;
                encryptButton.IsEnabled = true;
                return;
            }

            if (deleteOriginal)
            {
                File.Delete(filePath);
            }

            isEncrypting = false;
            encryptButton.IsEnabled = true;
            MessageBox.Show("Encryption finished!", "Info");
        }
    }
}
