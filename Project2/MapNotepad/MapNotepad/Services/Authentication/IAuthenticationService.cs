using MapNotepad.Helpers;
using MapNotepad.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Authentication
{
    public interface IAuthenticationService
    {
        UserModel User { get; }

        void RegisterWithGoogleAccount(string username, string email);
        Task<AOResult<bool>> AuthorizationAsync(string email, string password);
        void LogOut();
    }
}
