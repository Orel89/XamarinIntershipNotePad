using MapNotepad.Helpers;
using MapNotepad.Model;
using MapNotepad.Services.ProfileService;
using MapNotepad.Services.Repository;
using MapNotepad.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MapNotepad.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserService _userService;

        public RegistrationService(IUserService userService)
        {
            _userService = userService;
        }

        #region -- RegistrationService implementation --

        public async Task<AOResult<bool>> RegistrationAsync(UserModel user)
        {
            var result = new AOResult<bool>();

            try
            {
                var userIdResponse = await _userService.AddUserAsync(user);

                if (userIdResponse.IsSuccess)
                {
                    result.SetSuccess();
                }

                else
                {
                    result.SetFailure("row number is <= 0");
                }
             
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exeption from RegistrationService Registration", ex);
            }
            
            return result;
        }


        #endregion
    }
}
