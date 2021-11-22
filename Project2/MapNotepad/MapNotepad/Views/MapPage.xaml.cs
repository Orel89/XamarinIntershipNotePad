using MapNotepad.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            MessagingCenter.Subscribe<MapPageViewModel, Position>(this, "MovePin", (s, a) =>
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(a, Distance.FromKilometers(1)));
            });
            //gfg
        }
    }
}