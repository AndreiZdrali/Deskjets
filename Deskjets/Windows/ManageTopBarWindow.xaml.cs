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
using Deskjets.Windows;
using Microsoft.Win32;

namespace Deskjets.Windows
{
    /// <summary>
    /// Interaction logic for TopBarAddWindow.xaml
    /// </summary>
    public partial class ManageTopBarWindow : Window
    {
        public ManageTopBarWindow()
        {
            InitializeComponent();

            //cred ca e cam inutila asta
            this.DataContext = Global.UnserializableSettings;
            this.pathsListBox.ItemsSource = Global.UnserializableSettings.BubbleButtonPropertiesList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.closeButton.MouseEnter += (s, e) => this.closeButton.Background = Brushes.Red;
            this.closeButton.MouseLeave += (s, e) => this.closeButton.Background = Brushes.IndianRed;
            this.closeButton.MouseUp += (s, e) => this.Close();

            this.minimizeButton.MouseEnter += (s, e) => this.minimizeButton.Background = Brushes.ForestGreen;
            this.minimizeButton.MouseLeave += (s, e) => this.minimizeButton.Background = Brushes.MediumSeaGreen;
            this.minimizeButton.MouseUp += (s, e) => this.WindowState = WindowState.Minimized;


            this.addButton.Click += addButton_Click;
            this.moveUpButton.Click += moveUpButton_Click;
            this.moveDownButton.Click += moveDownButton_Click;
            this.removeButton.Click += removeButton_Click;
            #region TEST
            #endregion
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //pt ca daca apas pe un buton misca fereastra in loc sa activeze MouseUp
            if (e.LeftButton == MouseButtonState.Pressed && !this.minimizeButton.IsMouseOver && !this.closeButton.IsMouseOver)
                this.DragMove();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
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

                Global.UnserializableSettings.BubbleButtonPropertiesList.Add(buttonProperties);
                SaveLoad.SerializeUnserializableSettings();

                TopBar.UpdateTopBar();
            }
        }

        private void moveUpButton_Click(object sender, RoutedEventArgs e)
        {
            BubbleButtonProperties selectedItem = (BubbleButtonProperties)this.pathsListBox.SelectedItem;

            if (selectedItem == null) return;

            int index = this.pathsListBox.SelectedIndex;
            if (this.pathsListBox.SelectedIndex > 0)
            {
                Global.UnserializableSettings.BubbleButtonPropertiesList.Remove(selectedItem);
                Global.UnserializableSettings.BubbleButtonPropertiesList.Insert(index - 1, selectedItem);
                this.pathsListBox.SelectedItem = selectedItem;
            }

            TopBar.UpdateTopBar();
        }

        private void moveDownButton_Click(object sender, RoutedEventArgs e)
        {
            BubbleButtonProperties selectedItem = (BubbleButtonProperties)this.pathsListBox.SelectedItem;

            if (selectedItem == null) return;

            int index = this.pathsListBox.SelectedIndex;
            if (this.pathsListBox.SelectedIndex < Global.UnserializableSettings.BubbleButtonPropertiesList.Count - 1)
            {
                Global.UnserializableSettings.BubbleButtonPropertiesList.Remove(selectedItem);
                Global.UnserializableSettings.BubbleButtonPropertiesList.Insert(index + 1, selectedItem);
                this.pathsListBox.SelectedItem = selectedItem;
            }

            TopBar.UpdateTopBar();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            BubbleButtonProperties selectedItem = (BubbleButtonProperties)this.pathsListBox.SelectedItem;

            if (selectedItem == null) return;

            Global.UnserializableSettings.BubbleButtonPropertiesList.Remove(selectedItem);

            TopBar.UpdateTopBar();
        }
    }
}
