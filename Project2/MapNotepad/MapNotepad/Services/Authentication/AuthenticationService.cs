using MapNotepad.Model;
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
        private IRepositoryService _repository;

        private ISettingsManager _settings;

        public AuthenticationService(IRepositoryService repository, ISettingsManager settings)
        {
            _repository = repository;
            _settings = settings;
        }

        #region --- public properties ---

        private UserModel _user;
        public UserModel User { get => _user; }

        #endregion



        #region --- public methods ---

        public async Task<string> AuthorizationAsync(string email, string password)
        {
            string success = "Wrong Email";

            _user = null;

            if (IsEmailMatched(email))
            {
                _user = GetUserByEmail(email);

                if (_user != null)
                {
                    success = "Wrong Password";

                    if (IsPasswordMatched(password))
                    {
                        if (_user.Password == password)
                        {
                            success = "successfulAuthentication";
                            _settings.UserId = _user.Id;
                            _settings.UserEmail = _user.Email;
                            _settings.UserPassword = _user.Password;
                        }

                    }
                }
            }

            return await Task.Run(() => success);

        }

        public bool IsEmailMatched(string email)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Length <= 129)
                {
                    if (Regex.IsMatch(email.Trim(), @"^[^@\s]{1,64}@[^@\s]+\.[^@\s]+"))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool IsPasswordMatched(string password)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(password))
            {
                if (password.Length >= 6)
                {
                    if (Regex.IsMatch(password.Trim(), @"[0-9]") && Regex.IsMatch(password.Trim(), @"[A-Z]"))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public void LogOut()
        {
            _settings.UserId = default;
            _settings.UserPassword = default;
            _settings.UserEmail = default;
        }

        #endregion

        #region --- private methods ---

        private UserModel GetUserByEmail(string email)
        {
            UserModel user = null;
            if (!string.IsNullOrWhiteSpace(email))
            {
                Task<List<UserModel>> listOfUsers = _repository.GetAllItemsAsync<UserModel>();
                if (listOfUsers != null)
                {
                    if (listOfUsers.Result != null)
                    {
                        user = listOfUsers.Result.Where(u => u.Email == email).FirstOrDefault();
                    }
                }
            }
            return user;
        }

        #endregion
    }
}
