using Acr.UserDialogs;
using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
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

        private ICommand _PinEditCommand;
        public ICommand PinEditCommand => _PinEditCommand ?? (_PinEditCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnPinEditAsync));

        private ICommand _PinDeleteCommand;
        public ICommand PinDeleteCommand => _PinDeleteCommand ?? (_PinDeleteCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnPinDeleteAsync));

        private ICommand _MapClickedCommand;
        public ICommand MapClickedCommand => _MapClickedCommand ?? (_MapClickedCommand = SingleExecutionCommand.FromFunc<Position>(OnMapClickedCommandAsync));

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

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
                    pin.EditCommand = PinEditCommand;
                }
            }
        }

        private async Task OnPinDeleteAsync(PinViewModel pin)
        {
            if (pin != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "Do you want to delete a pin?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);

                if (confirm)
                {

                    var result = await _pinService.DeletePinAsync(pin.ToPinModel());

                    if (result.IsSuccess)
                    {
                        Pins.Remove(pin);

                        await _navigationService.NavigateAsync(nameof(PinListPage));
                    }
                    else
                    {
                        confirmConfig.Message = result.Message;

                        await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                    }
                }
            }
            var pinId = (pin as PinViewModel).Id;

            
        }

        private Task OnPinClickedCommandAsync(Position arg)
        {
            var a = arg;

            return Task.CompletedTask;
        }

        private Task OnMapClickedCommandAsync(Position arg)
        {
            var a = arg;

            return Task.CompletedTask;
        }

        private async Task OnPinEditAsync(object pin)
        {
            var pinId = (pin as PinViewModel).Id;

            var parameter = new NavigationParameters();

            parameter.Add("id", pinId);

            await _navigationService.NavigateAsync(nameof(AddPinPage));

        }

        #endregion
    }
}
