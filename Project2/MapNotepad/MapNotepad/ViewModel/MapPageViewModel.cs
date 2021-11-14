using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModel
{
    class MapPageViewModel : BaseViewModel
    {
        private IPinService _pinService;

        public MapPageViewModel(INavigationService navigationService,
                                IPinService pinService)
            : base(navigationService)
        {
            _pinService = pinService;
            InitAsync();
        }

        #region --- commands ---

        #endregion
        #region -- Public properties --

        private ObservableCollection<PinViewModel> _Pins;
        public ObservableCollection<PinViewModel> Pins
        {
            get => _Pins;
            set => SetProperty(ref _Pins, value);
        }

        private List<PinModel> _pinCollection;
        public List<PinModel> PinCollection
        {
            get => _pinCollection;
            set => SetProperty(ref _pinCollection, value);
        }

        #endregion

        #region -- Overrides --

        #endregion

        #region -- Private helpers --

        private async void InitAsync()
        {
            PinCollection = await _pinService.GetPinsAsync();
        }

        #endregion
    }
}
