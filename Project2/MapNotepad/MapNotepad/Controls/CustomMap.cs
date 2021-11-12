using MapNotepad.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Controls
{
    public class CustomMap : Map
    {
        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
            MyLocationEnabled = true;
            UiSettings.ZoomControlsEnabled = false;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                UpdatePins();
            }
        }

        #region -- Private helpers --

        private void UpdatePins()
        {
            Pins.Clear();

            foreach (var pin in ItemsSource as IEnumerable<IPin>)
            {
                Pins.Add(new Pin
                {
                    Label = pin.Label,
                    Position = new Position(pin.Latitude, pin.Longitude),
                });

            }
        
        }

        #endregion
    }
}