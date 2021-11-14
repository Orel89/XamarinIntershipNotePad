using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        public int UserId { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
    }
}
