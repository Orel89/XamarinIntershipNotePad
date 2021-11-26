using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public static class Constants 
    {
        public const string EMAIL_REGEX = @"^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$"; 
        public const string Password_REGEX = @"^(?=.{6,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$";
        public const string Latitude_REGEX = @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?)$"; 
        public const string Longitude_REGEX = @"^[+-]?((([1-9]?[0-9]|1[0-7][0-9])(\.[0-9]{1,6})?)|180(\.0{1,6})?)$";
    }
}
