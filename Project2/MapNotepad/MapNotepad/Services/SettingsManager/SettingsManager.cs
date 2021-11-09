using MapNotepad.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        private ISettings _settings;
        public SettingsManager(ISettings settings)
        {
            _settings = settings;
        }
        public string Login 
        {
            get => _settings.Login;
            set => _settings.Login = value;
        }
    }
}
