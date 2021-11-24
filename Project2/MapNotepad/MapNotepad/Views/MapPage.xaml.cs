using MapNotepad.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<MapPageViewModel, Position>(this, "MoveFromFoundPinInSearchBarToMainPage", (s, a) =>
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(a, Distance.FromKilometers(1)));
            });

            MessagingCenter.Subscribe<PinListViewModel, Position>(this, "MoveFromFoundPinInPinPageToMainPage", (s, a) =>
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(a, Distance.FromKilometers(1)));
            });
        }
    }
}
