using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MapNotepad.Services.Services
{
    public class Settings : ISettings
    {
        public string Login 
        {
            get => Preferences.Get(nameof(Login), string.Empty);
            set => Preferences.Set(nameof(Login), value);
        }
        public string Password
        {
            get => Preferences.Get(nameof(Password), string.Empty);
            set => Preferences.Set(nameof(Password), value);
        }
    }
}
