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


        public async Task<AOResult<int>> AddUserAsync(UserModel user)
        {
            var result = new AOResult<int>();

            try
            {
                var a = await _repositoryService.InsertAsync(user);

                result.SetSuccess(a);
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exeption from UserService AddUser", ex);
            }

            return result;
        }

     

        #region -- UserService implementation  --



        //public async Task<AOResult<UserModel>> GetCurrentUserAsync()
        //{
        //    var result = new AOResult<UserModel>();

        //    try
        //    {
        //        var users = await _repositoryService.GetAllItemsAsync<UserModel>();

        //        if (users != null)
        //        {
        //            result.SetSuccess(users.FirstOrDefault(u => u.Id == _settingsService.UserId));
        //        }
        //        else
        //        {
        //            result.SetFailure("Current User wasn't found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.SetError($"{nameof(GetCurrentUserAsync)}: exception", "Error from UserService GetUsersAsync", ex);
        //    }

        //    return result;
        //}

        public async Task<AOResult<bool>> IsEmailAvailable(string email)
        {
            var result = new AOResult<bool>();

            try
            {
                var users = await _repositoryService.GetAllItemsAsync<UserModel>();

                if (users.Any(x => x.Email == email))
                {
                    result.SetSuccess(false);
                }
                else
                {
                    result.SetSuccess(true);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(IsEmailAvailable)}: exception", "Error from UserService IsEmailAvailable", ex);
            }

            return result;
        }

        #endregion


    }
}
