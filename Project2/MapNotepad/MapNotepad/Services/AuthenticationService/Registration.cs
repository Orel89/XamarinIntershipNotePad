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
    public class Registration : IRegistration
    {
        private IRepository _repository;

        private ISettings _settings;

        public Registration(IRepository repository, ISettings settings)
        {
            _repository = repository;

            _settings = settings;
        }

        #region --- public properties ---

        private UserModel _user;

        public UserModel User { get => _user; }

        #endregion

        #region --- public methods ---

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

        public async Task<string> RegistrationAsync(string email, string password, string confirmMessage, string name)
        {
            string messageForUser = "Password mismatch";
            if (password == confirmMessage)
            {
                messageForUser = "Password must have minimum 6 symbols and at least one digit with a capital letter";
                if (IsPasswordMatched(password))
                {
                    messageForUser = "Successful user registration";
                    _user = new UserModel()
                    {
                        UserName = name,
                        Password = password,
                        Email = email,
                        CreationTime = DateTime.Now
                    };
                }
                    
                var userId = _repository.InsertAsync(_user);

            }
            return await Task.Run(() => messageForUser);
          
        }

        public bool IsEmailAvailable(string email)
        {
            bool result = false;

            if (IsEmailMatched(email))
            {
                var response = _repository.GetAllItemsAsync<UserModel>().Result.Where(u => u.Email == email).FirstOrDefault();

                if (response == null)
                {
                    result = true;
                }
            }
        
            return result;
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
