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
        }

        #region -- Public properties --

        private ObservableCollection<PinViewModel> _Pins;
        public ObservableCollection<PinViewModel> Pins
        {
            get => _Pins;
            set => SetProperty(ref _Pins, value);
        }

        private ICommand _PinClickedCommand;
        public ICommand PinClickedCommand => _PinClickedCommand ?? (_PinClickedCommand = SingleExecutionCommand.FromFunc<Position>(OnPinClickedCommandAsync));


        #endregion

        #region -- Overrides --

        public override async void InitializeAsync(INavigationParameters parameters)
        {
            base.InitializeAsync(parameters);

            await InitPins();
        }

        #endregion

        #region -- Private helpers --

        private async Task InitPins()
        {
            var getPinsResult = await _pinService.GetPinsAsync();

            if (getPinsResult.IsSuccess)
            {
                Pins = new ObservableCollection<PinViewModel>(getPinsResult.Result.Select(x => x.ToPinViewModel()));

                foreach (var pin in Pins)
                {
                    pin.TapCommand = PinClickedCommand;
                }
            }
        }

        private Task OnPinClickedCommandAsync(Position arg)
        {
            var a = arg;

            return Task.CompletedTask;
        }

        #endregion
    }
}
