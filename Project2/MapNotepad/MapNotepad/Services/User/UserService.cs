using MapNotepad.Helpers;
using MapNotepad.Model;
using MapNotepad.Services.Repository;
using MapNotepad.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapNotepad.Services.ProfileService
{
    public class UserService : IUserService
    {
        private readonly IRepositoryService _repositoryService;

        private readonly ISettingsManager _settingsManager;

        public UserService(ISettingsManager settingsManager,
                           IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
            _settingsManager = settingsManager;
        }

        #region -- Public Properties --

        public int UserId 
        { 
            get => _settingsManager.UserId;
        }

        #endregion


        #region -- UserService implementation  --

        public async Task<AOResult<bool>> AddUserAsync(UserModel user)
        {
            var result = new AOResult<bool>();

            try
            {
                int rowNumber = await _repositoryService.InsertAsync(user);

                if (rowNumber > 0)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(AddUserAsync)}: exception", "Exeption from UserService AddUserAsync", ex);
            }

            return result;
        }

        public async Task<AOResult<int>> CheckUserExists(string email, string password)
        {
            var result = new AOResult<int>();

            try
            {
                var users = await _repositoryService.GetAllItemsAsync<UserModel>();

                if(users != null)
                {
                    var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

                    if (user != null)
                    {
                        result.SetSuccess(user.Id);
                    }
                    else
                    {
                        result.SetFailure();
                    }
                }
              
            }
            catch (Exception ex)
            {

                result.SetError($"{nameof(CheckUserExists)}: exception", "Error from UserService CheckUserExists", ex);
            }
            return result;
        }

        public async Task<AOResult> CheckEmailExists(string email)
        {
            var result = new AOResult();

            try
            {
                var users = await _repositoryService.GetAllItemsAsync<UserModel>();

                if (users.Any(x => x.Email == email))
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(CheckEmailExists)}: exception", "Error from UserService IsEmailAvailable", ex);
            }

            return result;
        }

        #endregion


    }
}
