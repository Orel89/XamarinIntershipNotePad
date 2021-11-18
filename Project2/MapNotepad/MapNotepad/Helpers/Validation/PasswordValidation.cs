using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MapNotepad.Helpers.Validation
{
    public static class PasswordValidation
    {
        public static bool IsPasswordMatched(string password)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(password))
            {
                if (password.Length >= 6)
                {
                    if (Regex.IsMatch(password.Trim(), @"[0-9]") && Regex.IsMatch(password.Trim(), @"[A-Z]"))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
