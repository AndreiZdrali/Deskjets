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
        public bool IsOpen { get; set; }
        public List<BubbleButton> bubbleButtons { get; set; }
        private Brush _background = Brushes.AliceBlue;
        public Brush Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }
    }
}
