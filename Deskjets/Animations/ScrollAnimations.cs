using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Deskjets.Animations
{
    static class ScrollAnimations
    {
        public static async void ScrollForward(object sender, int scrollAmount)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            for (int i = 0; i < scrollAmount; i += scrollAmount / 5)
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + i);
                await Task.Delay(10);
            }
        }
    }
}
