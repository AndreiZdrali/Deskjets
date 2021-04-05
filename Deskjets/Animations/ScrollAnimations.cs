using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Deskjets.Animations
{
    static class ScrollAnimations
    {
        //async ca sa pot sa folosesc await ca sa evit mai multe scroll-uri in acelasi timp
        public static async Task ScrollPixels(object sender, int scrollAmount)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            int steps = 50;
            double currentOffset = scrollViewer.HorizontalOffset;
            for (int i = 1; i <= steps; i++)
            {
                double stepValue = EaseInOutCubic(i, 0, scrollAmount, steps);
                scrollViewer.ScrollToHorizontalOffset(currentOffset + stepValue);
                await Task.Delay(10);
            }
        }

        private static double EaseInOutCubic(double time, double startValue, double change, double duration)
        {
            time /= duration / 2;
            if (time < 1) return change / 2 * time * time * time + startValue;
            time -= 2;
            return change / 2 * (time * time * time + 2) + startValue;
        }
    }
}
