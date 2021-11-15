using MapNotepad.Interfaces;
using MapNotepad.Model.Pin;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #region --- Public Properties ---

        public static readonly BindableProperty PinsSourceProperty = BindableProperty.Create(
            propertyName: nameof(PinsSource),
            returnType: typeof(IEnumerable<IPin>),
            declaringType: typeof(CustomMap));

        public IEnumerable<IPin> PinsSource
        {
            get => (IEnumerable<IPin>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(PinsSource) && PinsSource != null)
            {
                UpdatePins();
            }
        }

        #region -- Private helpers --

        private void UpdatePins()
        {
            Pins.Clear();

            foreach (var pin in PinsSource)
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