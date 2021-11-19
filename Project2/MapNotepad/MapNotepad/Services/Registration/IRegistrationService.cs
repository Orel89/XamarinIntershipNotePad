using MapNotepad.Helpers;
using MapNotepad.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Registration
{
    public interface IRegistrationService
    {
        Task<AOResult<bool>> RegistrationAsync(UserModel user);
    }
}
