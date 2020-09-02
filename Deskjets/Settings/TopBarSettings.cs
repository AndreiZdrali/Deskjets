using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using Deskjets.Controls;
using Deskjets.Classes;

namespace Deskjets.Settings
{
    [Serializable]
    class TopBarSettings : ViewModelBase
    {
        private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { SetProperty(ref _enabled, value); }
        }

        [field:NonSerialized]
        private Brush _background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1AE0FFFF"));
        public Brush Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }

        //cam inutil ca oricum nu pun itemsource, ci le actualizaz din UnserializableSettings
        [field:NonSerialized]
        private List<BubbleButton> _bubbleButtons = new List<BubbleButton>();
        public List<BubbleButton> BubbleButtons
        {
            get { return _bubbleButtons; }
            set { SetProperty(ref _bubbleButtons, value); }
        }
    }
}
