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
        Task<AOResult<int>> AddPinAsync(PinModel pin);
        Task<AOResult<int>> DeletePinAsync(PinModel pin);
        Task<AOResult<PinModel>> GetPinAsync(int pinId);
        Task<AOResult<int>> UpdatePinAsync(PinModel pin);
        Task<AOResult<IEnumerable<PinModel>>> GetPinsAsync();
    }
}
