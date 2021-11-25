using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public static class Constants
    {
        public const string EMAIL_REGEX = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        public const string Password_REGEX = @"^(?=.{6,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$";
        public const string Latitude_REGEX = @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?)$"; 
        public const string Longitude_REGEX = @"^[+-]?((([1-9]?[0-9]|1[0-7][0-9])(\.[0-9]{1,6})?)|180(\.0{1,6})?)$";
    }
}
