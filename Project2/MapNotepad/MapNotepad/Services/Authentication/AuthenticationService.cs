using MapNotepad.Helpers;
using MapNotepad.Model;
using MapNotepad.Services.ProfileService;
using MapNotepad.Services.Repository;
using MapNotepad.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MapNotepad.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private ISettingsManager _settingsManager;
        private IUserService _userService;

        public AuthenticationService(IUserService userService,
                                     ISettingsManager settings)
        {
            _settingsManager = settings;
            _userService = userService;
        }

        #region -- Public properties --

        private UserModel _user;
        public UserModel User { get => _user; }

        #endregion

        #region -- AuthenticationService implementation --

        public async Task<AOResult<bool>> AuthorizationAsync(string email, string password)
        {
            var result = new AOResult<bool>();

            try
            {
                var user = await _userService.CheckUserExists(email, password);

                if (user.IsSuccess)
                {
                    _settingsManager.UserId = user.Result;
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {

                result.SetError($"{nameof(AuthorizationAsync)}: exception", "Error from AuthenticationService AuthenticationAsync", ex);
            }
            return result;
        }

        public void LogOut()
        {
            _settingsManager.UserId = default;
        }

        #endregion
    }
}
