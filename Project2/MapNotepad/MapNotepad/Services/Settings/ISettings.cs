﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.Services
{
    public interface ISettings
    {
        int UserId { get; set; }
        string UserPassword { get; set; }
        string UserEmail { get; set; }

    }
}
