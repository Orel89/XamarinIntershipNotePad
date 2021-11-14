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
            MapClicked += CustomMapClicked;
            PinClicked += CustomMapPinClicked;
        }

        #region --- execute command ---

        private void CustomMapClicked(object sender, MapClickedEventArgs e)
        {
            MapClickedCommand?.Execute(null);
        }

        private void CustomMapPinClicked(object sender, PinClickedEventArgs e)
        {
            var model = PinSource?.FirstOrDefault(item => item.Label == e.Pin.Label);
            if (model != null)
                PinClickedCommand?.Execute(model);
        }

        #endregion


        #region --- Public Properties ---

        public static readonly BindableProperty MapClickedCommandProperty =
         BindableProperty.Create(nameof(MapClickedCommand), typeof(ICommand), typeof(CustomMap), null, BindingMode.TwoWay);
        public ICommand MapClickedCommand
        {
            get { return (ICommand)GetValue(MapClickedCommandProperty); }
            set { SetValue(MapClickedCommandProperty, value); }
        }

        public static readonly BindableProperty PinSourceProperty =
            BindableProperty.Create(nameof(PinSource), typeof(List<PinModel>), typeof(CustomMap), null);

        public List<PinModel> PinSource
        {
            get { return (List<PinModel>)GetValue(PinSourceProperty); }
            set { SetValue(PinSourceProperty, value); }
        }

        public static readonly BindableProperty PinClickedCommandProperty =
        BindableProperty.Create(nameof(PinClickedCommand), typeof(ICommand), typeof(CustomMap), null, BindingMode.TwoWay);

        public ICommand PinClickedCommand
        {
            get { return (ICommand)GetValue(PinClickedCommandProperty); }
            set { SetValue(PinClickedCommandProperty, value); }
        }

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(PinSource) && PinSource != null)
            {
                UpdatePins();
            }
        }

        #region -- Private helpers --

        private void UpdatePins()
        {
            Pins.Clear();

            foreach (var pin in PinSource)
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