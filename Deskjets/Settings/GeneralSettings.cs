using Deskjets.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deskjets.Settings
{
    class GeneralSettings : ViewModelBase
    {
        public TopBarSettings TopBarSettings { get; set; } = new TopBarSettings();

        public bool OpenOnStartup { get; set; } = true;
    }
}
