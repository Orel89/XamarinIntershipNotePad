﻿using System;
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
    }
}
