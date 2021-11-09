using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.Authentication
{
    public interface IAuthentication
    {
        bool IsPasswordMatched(string passsword);
        bool Registration(string login, string password);
        bool Authorization(string login, string password);
        bool IsEmailAvailable(string email);
        bool IsEmailMatched(string email);
    }
}
