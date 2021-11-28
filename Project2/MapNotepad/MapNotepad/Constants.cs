using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public static class Constants 
    {
        public static string AppName = "MapNotepad";
        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string iOSClientId = "297764194337-85dctf2trold24gks7cbak72evt0arnv.apps.googleusercontent.com";
        public static string AndroidClientId = "297764194337-8jvsn8oanj4gomgslv8tiuo6arg79qqp.apps.googleusercontent.com"; 
        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://oauth2.googleapis.com/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "com.googleusercontent.apps.297764194337-85dctf2trold24gks7cbak72evt0arnv:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.297764194337-8jvsn8oanj4gomgslv8tiuo6arg79qqp:/oauth2redirect";

        public const string EMAIL_REGEX = @"^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$"; 
        public const string Password_REGEX = @"^(?=.{6,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$";
        public const string Latitude_REGEX = @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?)$"; 
        public const string Longitude_REGEX = @"^[+-]?((([1-9]?[0-9]|1[0-7][0-9])(\.[0-9]{1,6})?)|180(\.0{1,6})?)$";
    }
}
