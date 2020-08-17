using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Threading;
using System.Threading.Tasks;
using Deskjets.Windows;

namespace Deskjets.Animations
{
    static class TopBarAnimations
    {
        private static Duration animationDuration = new Duration(new TimeSpan(0, 0, 0, 0, 200));

        //sa folosesc async
        public static async void TopBar_MouseEnter(object sender, MouseEventArgs e)
        {
            Window window = (Window)sender;
            DoubleAnimation animation = new DoubleAnimation(0, animationDuration);
            window.BeginAnimation(Window.TopProperty, animation);
            await Task.Delay(Convert.ToInt32(animationDuration.TimeSpan.TotalMilliseconds));
            TopBar.IsExtended = true;
        }

        public static void TopBar_MouseLeave(object sender, MouseEventArgs e)
        {
            Window window = (Window)sender;
            DoubleAnimation animation = new DoubleAnimation(-window.Height + 5, animationDuration);
            window.BeginAnimation(Window.TopProperty, animation);
            TopBar.IsExtended = false;
        }
    }
}
