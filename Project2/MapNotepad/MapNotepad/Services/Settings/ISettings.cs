using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.Services
{
    public interface ISettings
    {
        string Login { get;set; }
        string Password { get; set; }
    }
}
