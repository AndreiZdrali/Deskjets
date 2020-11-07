using System;
using System.Collections.Generic;
using System.Text;
using Deskjets.Classes;

namespace Deskjets.Settings
{
    [Serializable]
    class YTDownloadSettings : ViewModelBase
    {
        private string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }
    }
}
