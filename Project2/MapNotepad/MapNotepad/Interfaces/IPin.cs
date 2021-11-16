using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Interfaces
{
   public interface IPin
    {
        string Label { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }
    }
}
