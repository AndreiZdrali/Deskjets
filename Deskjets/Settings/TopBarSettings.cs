using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using Deskjets.Controls;
using Deskjets.Classes;

namespace Deskjets.Settings
{
    class TopBarSettings : ViewModelBase
    {
        private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { SetProperty(ref _enabled, value); }
        }

        private Brush _background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#60ADD8E6"));
        public Brush Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }

        private List<BubbleButton> _bubbleButtons;
        public List<BubbleButton> BubbleButtons
        {
            get { return _bubbleButtons; }
            set { SetProperty(ref _bubbleButtons, value); }
        }
    }
}
