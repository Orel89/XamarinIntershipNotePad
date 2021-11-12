using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Interfaces
{
   public interface IPin
    {
        string Label { get; set; }

        float Latitude { get; set; }

        float Longitude { get; set; }
    }
}
