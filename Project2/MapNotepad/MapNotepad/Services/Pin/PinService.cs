using MapNotepad.Helpers;
using MapNotepad.Model;
using MapNotepad.Model.Pin;
using MapNotepad.Services.Repository;
using MapNotepad.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.PinService
{
    public class PinService : IPinService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly ISettingsManager _settings;
        public PinService(IRepositoryService repositoryService, ISettingsManager settings)
        {
            _repositoryService = repositoryService;
            _settings = settings;
        }

        public async Task<AOResult<int>> AddPinAsync(PinModel pin)
        {
            var result = new AOResult<int>();

            try
            {
                await _repositoryService.InsertAsync(pin);

                result.SetSuccess();
            }
            catch (Exception ex)
            {

                result.SetError("0", "Exeption from PinService AddPin", ex);
            }

            return result;
        }

        public async Task<AOResult<int>> DeletePinAsync(PinModel pin)
        {
            var result = new AOResult<int>();

            try
            {
                var id = await _repositoryService.DeleteAsync(pin);

                result.SetSuccess(id);
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exeption PinService DeletePin", ex);
                
            }
            return result;
        }

        public async Task<AOResult<IEnumerable<PinModel>>> GetPinsAsync()
        {
            var result = new AOResult<IEnumerable<PinModel>>();

            try
            {
                var pins = await _repositoryService.GetAllItemsAsync<PinModel>();

                if (pins != null)
                {
                    result.SetSuccess(pins.Where(p => p.UserId == _settings.UserId));
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetPinsAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }
    }
}
