using System;
using System.Collections.Generic;
using System.Text;
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
            PinClicked += CustomMap_PinClicked;
            MapClicked += CustomMap_Clicked;
        }

        #region --- private methods ---
        private void CustomMap_PinClicked(object sender, PinClickedEventArgs e)
        {
            MapClickedCommand?.Execute(null);
        }

        private void CustomMap_Clicked(object sender, MapClickedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        public static readonly BindableProperty MapClickedCommandProperty =
     BindableProperty.Create(nameof(MapClickedCommand), typeof(ICommand), typeof(CustomMap), null, BindingMode.TwoWay);
        public ICommand MapClickedCommand
        {
            get { return (ICommand)GetValue(MapClickedCommandProperty); }
            set { SetValue(MapClickedCommandProperty, value); }
        }

    }
}
