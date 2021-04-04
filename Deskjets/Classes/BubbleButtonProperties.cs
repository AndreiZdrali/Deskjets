using System;
using System.Collections.Generic;
using System.Text;

namespace Deskjets.Classes
{
    [Serializable]
    class BubbleButtonProperties
    {
        public string Color { get; set; }
        public string HighlightColor { get; set; }
        public string ExecutablePath { get; set; }
    }
}
