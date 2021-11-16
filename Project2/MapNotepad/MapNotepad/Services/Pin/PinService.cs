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
        private readonly IRepositoryService _repository;
        private readonly ISettingsManager _settings;
        public PinService(IRepositoryService repository, ISettingsManager settings)
        {
            _repository = repository;
            _settings = settings;
        }

        public async Task<AOResult<int>> AddPin(PinModel pin)
        {
            AOResult<int> result = new AOResult<int>();

            try
            {
                if (pin == null)
                {
                    result.SetFailure();
                }
                else
                {
                    var response = await _repository.InsertAsync(pin);
                    if (response == 0)
                    {
                        result.SetFailure();
                    }
                    else
                    {
                        result.SetSuccess(result.Result);
                    }
                }
            }
            catch (Exception ex)
            {

                result.SetError("0", "Exeption from PinService AddPin", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<PinModel>>> GetPinsAsync()
        {
            var result = new AOResult<IEnumerable<PinModel>>();

            try
            {
                var pins = await _repository.GetAllItemsAsync<PinModel>();

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
