using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MapNotepad.Helpers.Validation
{
    public static class EmailValidation
    {
        public static bool IsEmailMatched(string email)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Length <= 129)
                {
                    if (Regex.IsMatch(email.Trim(), @"^[^@\s]{1,64}@[^@\s]+\.[^@\s]+"))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
