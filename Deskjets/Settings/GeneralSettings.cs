using Deskjets.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Deskjets.Settings
{
    [Serializable]
    class GeneralSettings : ViewModelBase
    {
        public TopBarSettings TopBarSettings { get; set; } = new TopBarSettings();
        public YTDownloadSettings YTDownloadSettings { get; set; } = new YTDownloadSettings();

        public bool OpenOnStartup { get; set; } = File.Exists(
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Deskjets.lnk"));
    }
}
