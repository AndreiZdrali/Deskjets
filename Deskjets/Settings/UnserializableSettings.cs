using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Deskjets.Classes;
using Deskjets.Settings;

namespace Deskjets.Settings
{
    //setarile care nu merg serializate le bag aici si cand deschid aplicatia le bag
    [Serializable]
    class UnserializableSettings
    {
        public List<BubbleButtonProperties> BubbleButtonPropertiesList = new List<BubbleButtonProperties>();
        public string TopBarBackground = "#1AE0FFFF";
    }
}
