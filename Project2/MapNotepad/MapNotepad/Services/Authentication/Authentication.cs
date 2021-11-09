using MapNotepad.Model;
using MapNotepad.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Authentication
{
    public class Authentication : IAuthentication
    {
        private IRepository _repository;

        public Authentication(IRepository repository)
        {
            _repository = repository;
        }

        #region --- public properties ---

        private UserModel _profile;
        public UserModel Profile { get => _profile; }

        private bool _state;
        public bool State { get => _state; }

        #endregion

        #region --- public methods ---

        public bool Authorization(string mail, string password)
        {
            _profile = null;

            UserModel user = GetUserByEmail(mail);
            if (user!= null)
            {
                if (user.Password == password)
                {
                    _profile = user;
                }
            }
            return _state;
        }

        public bool IsEmailMatched(string email)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailAvailable(string email)
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(email))
            {
                UserModel user = GetUserByEmail(email);
                if (user == null)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool IsPasswordMatched(string passsword)
        {
            throw new NotImplementedException();
        }

        public bool Registration(string email, string password)
        {
            bool result = false;

            UserModel user = GetUserByEmail(email);
            if(user == null)
            {
                var newUser = new UserModel()
                {
                    Email = email,
                    Password = password,
                    CreationTime = DateTime.Now
                };
                
            }

            Task<int> userId = _repository.InsertAsync(user);
            if (userId != null)
            {
                result = true;
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
                Task<List<UserModel>> result = _repository.GetAllItemsAsync<UserModel>();

                if (result != null)
                {
                    if (result.Result != null)
                    {
                        user = result.Result.Where(r => r.Email == email).FirstOrDefault();
                    }
                }
            }
            return user;
        }

        #endregion

    }
}
