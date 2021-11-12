using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
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
        private Position _pinPosition;

        public MapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        { 
        }

        #region -- Public properties --

        private ObservableCollection<PinViewModel> _Pins;
        public ObservableCollection<PinViewModel> Pins
        {
            get => _Pins;
            set => SetProperty(ref _Pins, value);
        }

        private ICommand _MapTapCommand;
        public ICommand MapTapCommand => _MapTapCommand ?? (_MapTapCommand = SingleExecutionCommand.FromFunc<Position>(OnMapTapCommandAsync));

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            //AOResult<ObservableCollection<PinModel>> getPinsResult = await someService.GetPinsAsync();

            //if (getPinsResult.IsSuccess)
            //{
            //    Pins = new ObservableCollection<PinViewModel>(getPinsResult.Result.Select(x => x.ToPinViewModel()));

            //    var tapCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnPinTapCommandAsync);

            //    foreach (var pin in Pins)
            //    {
            //        pin.TapCommand = tapCommand;
            //    }
            //}

            Pins = new ObservableCollection<PinViewModel>
            {
                new PinViewModel
                {
                    Latitude = 43,
                    Longitude = 43,
                    Label = "My first pin",
                },
                new PinViewModel
                {
                    Latitude = 29,
                    Longitude = 54,
                    Label = "My second pin",
                },
            };
        }

        #endregion

        #region -- Private helpers --

        private Task OnPinTapCommandAsync(PinViewModel pin)
        {
            return Task.CompletedTask;
        }

        private Task OnMapTapCommandAsync(Position position)
        {
            _pinPosition = position;

            return Task.CompletedTask;
        }

        #endregion
    }
}
