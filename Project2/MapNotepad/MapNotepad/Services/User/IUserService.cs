using MapNotepad.Helpers;
using MapNotepad.Model;
using MapNotepad.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.ProfileService
{
    public interface IUserService
    {
        int UserId { get;}
        Task<AOResult> CheckEmailExists(string email);
        Task<AOResult<int>> CheckUserExists(string email, string password);
        Task<AOResult<bool>> AddUserAsync(UserModel user);
    }
}
