using MapNotepad.Resources.Strings;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace MapNotepad.Services.LocalizationService
{
    public class LocalizationService : ILocalizationService
    {
        private static string RESOURCES_PATH { get; } = "MapNotepad.Resources.Strings.Resource";

        public ResourceManager ResourceManager => new ResourceManager(
            RESOURCES_PATH, typeof(LocalizationService).GetTypeInfo().Assembly);
        public string this[string key] => ResourceManager.GetString(key, Resource.Culture);
    }
}
