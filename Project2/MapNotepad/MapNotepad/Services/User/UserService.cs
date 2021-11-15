using MapNotepad.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.ProfileService
{
    public class UserService : IUserService
    {
        private readonly ISettingsManager _settingsManager;

        public UserService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public int UserId { get => _settingsManager.UserId; }

    }
}
