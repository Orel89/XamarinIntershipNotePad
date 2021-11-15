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

        public Task AddPin(string label, string description, float longitude, float latitude, bool isfavorite)
        {
            PinModel pinModel = new PinModel()
            {
                Label = label,
                Description = description,
                UserId = _settings.UserId,
                IsFavorite = isfavorite,
                Latitude = latitude,
                Longitude = longitude
            };
            return _repository.InsertAsync(pinModel);
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
