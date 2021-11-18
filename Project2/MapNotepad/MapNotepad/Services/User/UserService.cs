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

        private readonly ISettingsManager _settingsService;

        public UserService(ISettingsManager settingsService,
                           IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
            _settingsService = settingsService;
        }

        #region -- Public Properties --

        public int UserId { get => _settingsService.UserId; }

        #endregion


        #region -- UserService implementation  --

        public async Task<AOResult<int>> AddUserAsync(UserModel user)
        {
            var result = new AOResult<int>();

            try
            {
                await _repositoryService.InsertAsync(user);
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exeption from UserService AddUser", ex);
            }

            return result;
        }

        public async Task<AOResult<UserModel>> GetUserAsync(string email, string password)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var users = await _repositoryService.GetAllItemsAsync<UserModel>();

                if(users != null)
                {
                    result.SetSuccess(users.FirstOrDefault(u => u.Email == email && u.Password == password));
                }
                else 
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {

                result.SetError($"{nameof(GetUserAsync)}: exception", "Error from UserService GetUserAsync", ex);
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
