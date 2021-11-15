using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.PinService
{
    public interface IPinService
    {
        Task AddPin(string label, string description, float longitude, float latitude, bool isfavorite);
        Task<AOResult<IEnumerable<PinModel>>> GetPinsAsync();
    }
}
