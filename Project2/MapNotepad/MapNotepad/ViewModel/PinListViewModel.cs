using Acr.UserDialogs;
using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.PinService;
using MapNotepad.Views;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModel
{
    public class PinListViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        private readonly IAuthenticationService _authenticationService;
        public PinListViewModel(IPinService pinService,
                                IAuthenticationService authenticationService)
        {
            _pinService = pinService;
            _authenticationService = authenticationService;
        }

        #region -- Public Properties --

        private bool _isFavorite;

        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }


        private ICommand _addButtonCommand;
        public ICommand AddButtonTapCommand => _addButtonCommand ?? (_addButtonCommand = SingleExecutionCommand.FromFunc(OnAddButtonPinAsync));

        private ICommand _logOutCommand;
        public ICommand LogOutCommand => _logOutCommand ?? (_logOutCommand = SingleExecutionCommand.FromFunc(OnLogOutCommand));

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnEditCommandAsync));

        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnDeleteCommandAsync));

        private ObservableCollection<PinViewModel> _observPinCollection;
        public ObservableCollection<PinViewModel> ObservPinCollection
        {
            get => _observPinCollection;
            set => SetProperty(ref _observPinCollection, value);
        }

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await InitAsync();
        }

        #endregion

        #region -- Private helpers --
        private async Task OnDeleteCommandAsync(PinViewModel pin)
        {
            if (pin != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "Do you want to delete a pin?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.ConfirmAsync(confirmConfig);

                if (confirm)
                {

                    var result = await _pinService.DeletePinAsync(pin.ToPinModel());

                    if (result.IsSuccess)
                    {
                        ObservPinCollection.Remove(pin);
                    }
                    else
                    {
                        confirmConfig.Message = result.Message;

                        await UserDialogs.ConfirmAsync(confirmConfig);
                    }
                }
            }
        }
        private Task OnLogOutCommand()
        {
            _authenticationService.LogOut();
            return Task.CompletedTask;
        }

        private async Task OnEditCommandAsync(PinViewModel pin)
        {
            var navigationParameters = new NavigationParameters
            {
                {"pinId", pin.Id},
                {"pageTitle", "Edit pin"}
            };

            await NavigationService.NavigateAsync(nameof(AddPinPage), navigationParameters);
        }

        private async Task OnAddButtonPinAsync()
        {
            await NavigationService.NavigateAsync(nameof(AddPinPage));
        }

        private async Task InitAsync()
        {
            var observUserPinCollection = new ObservableCollection<PinViewModel>();
            var userPins = await _pinService.GetPinsAsync();

            if (userPins.IsSuccess)
            {
                ObservPinCollection = new ObservableCollection<PinViewModel>(userPins.Result.Select(x => x.ToPinViewModel()));
            }
            foreach (var pin in ObservPinCollection)
            {
                pin.MoveToPinLocationCommand = SingleExecutionCommand.FromFunc<PinViewModel>(GoToPinLocation);
                pin.DeleteCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnDeleteCommandAsync);
                pin.EditCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnEditCommandAsync);
                pin.IsFavoriteSwitchCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnSwitchStatusCommandAsync);
            }
        }

        private async Task OnSwitchStatusCommandAsync(PinViewModel pin)
        {
            pin.IsFavorite = !pin.IsFavorite;

            var response = await _pinService.UpdatePinAsync(pin.ToPinModel());

            if (response.IsSuccess)
            {
                var idx = ObservPinCollection.IndexOf(pin);

                ObservPinCollection[idx] = pin;
            }
            else
            {
                pin.IsFavorite = !pin.IsFavorite;
            }
        }

        #endregion

        #region -- Private helpers --
        private async Task GoToPinLocation(PinViewModel pin)
        {
            LocatePin(new Position(pin.Latitude, pin.Longitude));
        }

        private async Task LocatePin(Position position)
        {
            MessagingCenter.Send(this, "MoveFromFoundPinInPinPageToMainPage", position);
            await NavigationService.SelectTabAsync(nameof(MapPage));
        }

        #endregion

    }
}
