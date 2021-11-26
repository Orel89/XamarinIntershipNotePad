using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MapNotepad.Helpers.Validation
{
    public static class Validator
    {
        public static bool IsEmailMatched(string email)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Length <= 129)
                {
                    if (Regex.IsMatch(email.Trim(), Constants.EMAIL_REGEX))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public static bool IsPasswordMatched(string password)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(password))
            {
                if (password.Length >= 6)
                {
                    if (Regex.IsMatch(password.Trim(), Constants.Password_REGEX))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
