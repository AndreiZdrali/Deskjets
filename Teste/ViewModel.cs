using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Media;

namespace Teste
{
    class ViewModel : ViewModelBase
    {
        private Brush _backColor = Brushes.AliceBlue;
        public Brush BackColor
        {
            get { return _backColor; }
            set { SetProperty(ref _backColor, value); }
        }
    }
}
