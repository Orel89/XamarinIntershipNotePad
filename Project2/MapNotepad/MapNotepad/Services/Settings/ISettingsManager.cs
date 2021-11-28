using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.Services
{
    public interface ISettingsManager
    {
        int UserId { get; set; }
        string UserName { get; set; }
    }
}
