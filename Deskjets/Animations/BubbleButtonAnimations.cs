using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Input;
using Deskjets.Controls;

namespace Deskjets.Animations
{
    static class BubbleButtonAnimations
    {
        public static void BubbleButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BubbleButton button = (BubbleButton)sender;
            Border border = (Border)button.bubbleBorder;
            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames
            {
                Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100))
            };
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(23), KeyTime.FromPercent(.2)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(21), KeyTime.FromPercent(.4)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(19), KeyTime.FromPercent(.6)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(17), KeyTime.FromPercent(.8)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(15), KeyTime.FromPercent(1)));
            border.BeginAnimation(Border.CornerRadiusProperty, animation);
        }

        public static void BubbleButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BubbleButton button = (BubbleButton)sender;
            Border border = (Border)button.bubbleBorder;
            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames
            {
                Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100))
            };
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(17), KeyTime.FromPercent(.2)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(19), KeyTime.FromPercent(.4)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(21), KeyTime.FromPercent(.6)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(23), KeyTime.FromPercent(.8)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(25), KeyTime.FromPercent(1)));
            border.BeginAnimation(Border.CornerRadiusProperty, animation);
        }

        public static void BubbleButton_ChangeCornerRadius(object sender, double targetRadius, Duration duration)
        {
            BubbleButton button = (BubbleButton)sender;
            Border border = (Border)button.bubbleBorder;

            double initialRadius = border.CornerRadius.TopLeft;
            double difference = initialRadius - targetRadius;
            double step = difference / 5;

            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames
            {
                Duration = duration
            };
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(initialRadius - step * 1), KeyTime.FromPercent(.2)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(initialRadius - step * 2), KeyTime.FromPercent(.4)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(initialRadius - step * 3), KeyTime.FromPercent(.6)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(initialRadius - step * 4), KeyTime.FromPercent(.8)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(initialRadius - step * 5), KeyTime.FromPercent(1)));
            border.BeginAnimation(Border.CornerRadiusProperty, animation);
        }

        public static void BubbleButton_ChangeCornerRadiusWithSteps(object sender, List<double> steps, Duration duration)
        {
            BubbleButton button = (BubbleButton)sender;
            Border border = (Border)button.bubbleBorder;

            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames
            {
                Duration = duration
            };
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(steps[0]), KeyTime.FromPercent(.2)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(steps[1]), KeyTime.FromPercent(.4)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(steps[2]), KeyTime.FromPercent(.6)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(steps[3]), KeyTime.FromPercent(.8)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(new CornerRadius(steps[4]), KeyTime.FromPercent(1)));
            border.BeginAnimation(Border.CornerRadiusProperty, animation);
        }


    }
}
