using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.LocalizationService
{
    public interface ILocalizationService
    {
        string this[string key] { get; }
    }
}
