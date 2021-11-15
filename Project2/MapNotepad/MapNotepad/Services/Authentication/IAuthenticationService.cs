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
        Task<string> AuthorizationAsync(string email, string password);
        bool IsPasswordMatched(string passsword);
        bool IsEmailMatched(string email);
        void LogOut();
    }
}
