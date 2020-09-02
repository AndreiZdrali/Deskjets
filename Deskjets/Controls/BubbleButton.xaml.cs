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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Deskjets.Animations;
using Deskjets.Windows;

namespace Deskjets.Controls
{
    /// <summary>
    /// Interaction logic for BubbleButton.xaml
    /// </summary>
    /// sa fac o clasa care tine informatiile astea pt ca nu pot sa serializez direct controlul
    public partial class BubbleButton : UserControl
    {
        private Duration animationDuration = new Duration(new TimeSpan(0, 0, 0, 0, 100));
        private List<double> extendSteps = new List<double>();
        private List<double> shrinkSteps = new List<double>();

        public Brush Color { get; set; } = Brushes.Aquamarine;
        public Brush HighlightColor { get; set; } = Brushes.Aqua;
        public string ExecutablePath { get; set; }

        public BubbleButton()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseEnter += BubbleButton_MouseEnter;
            this.MouseLeave += BubbleButton_MouseLeave;

            #region calculeaza pasii pt animatii
            double extendCornerRadius = bubbleBorder.Width / 2 * (3.0 / 5);

            double extendDifference = bubbleBorder.CornerRadius.TopLeft - extendCornerRadius;
            for (int i = 1; i <= 5; i++)
                extendSteps.Add(bubbleBorder.CornerRadius.TopLeft - extendDifference / 5 * i);

            double shrinkDifference = extendCornerRadius - bubbleBorder.Width / 2;
            for (int i = 1; i <= 5; i++)
                shrinkSteps.Add(extendCornerRadius - shrinkDifference / 5 * i);
            #endregion
        }

        private void BubbleButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!TopBar.IsExtended) return;

            bubbleBorder.Background = this.HighlightColor;
            BubbleButtonAnimations.BubbleButton_ChangeCornerRadiusWithSteps(sender, extendSteps, animationDuration);
        }

        private void BubbleButton_MouseLeave(object sender, MouseEventArgs e)
        {
            //s-ar putea sa cauzeze bug-uri
            if (!TopBar.IsExtended) return;

            bubbleBorder.Background = this.Color;
            BubbleButtonAnimations.BubbleButton_ChangeCornerRadiusWithSteps(sender, shrinkSteps, animationDuration);
        }
    }
}
