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
        private readonly IRepository _repository;
        private readonly ISettings _settings;
        public PinService(IRepository repository, ISettings settings)
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

        public async Task<List<PinModel>> GetPinsAsync()
        {
            List<PinModel> pinCollection = await _repository.GetAllItemsAsync<PinModel>();

            var userPinsList = pinCollection.Where(p => p.UserId == _settings.UserId).ToList<PinModel>();

            return userPinsList;
        }

    }
}
