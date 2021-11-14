using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MapNotepad.Services.Services
{
    public class Settings : ISettings
    {
        public int UserId
        {
            get => Preferences.Get(nameof(UserId), 0);
            set => Preferences.Set(nameof(UserId), value);
        }
        public string UserPassword
        {
            get => Preferences.Get(nameof(UserPassword), string.Empty);
            set => Preferences.Set(nameof(UserPassword), value);
        }
        public string UserEmail
        {
            get => Preferences.Get(nameof(UserEmail), string.Empty);
            set => Preferences.Set(nameof(UserEmail), value);
        }
    }
}
