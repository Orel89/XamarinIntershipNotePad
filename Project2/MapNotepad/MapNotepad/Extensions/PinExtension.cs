using MapNotepad.Model.Pin;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Extensions
{
    public static class PinExtension
    {
        public static PinModel ToPinModel(this PinViewModel pin) => new PinModel
        {
            Id = pin.Id,
            UserId = pin.UserId,
            Label = pin.Label,
            Latitude = pin.Latitude,
            Longitude = pin.Longitude,
            IsFavorite = pin.IsFavorite,
            Description = pin.Description,
            CreationTime = pin.CreationTime,
        };

        public static PinViewModel ToPinViewModel(this PinModel pin) => new PinViewModel
        {
            Id = pin.Id,
            UserId = pin.UserId,
            Label = pin.Label,
            Latitude = pin.Latitude,
            Longitude = pin.Longitude,
            IsFavorite = pin.IsFavorite,
            Description = pin.Description,
            CreationTime = pin.CreationTime,
        };
    }
}
