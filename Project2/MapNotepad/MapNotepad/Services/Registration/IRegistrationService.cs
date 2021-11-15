using MapNotepad.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Registration
{
    public interface IRegistrationService
    {
        UserModel User { get;}
        bool IsPasswordMatched(string passsword);
        Task<string> RegistrationAsync(string email, string password, string confirmPassword, string name);
        bool IsEmailMatched(string email);
        bool IsEmailAvailable(string email);
    }
}
