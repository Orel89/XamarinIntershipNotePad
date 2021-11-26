using Acr.UserDialogs;
using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.PinService;
using MapNotepad.Services.SearchService;
using MapNotepad.View;
using MapNotepad.Views;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModel
{
    class MapPageViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        private readonly ISearchService _searchService;
        private readonly IAuthenticationService _authenticationService;

        public MapPageViewModel(IPinService pinService,
                                ISearchService searchService,
                                IAuthenticationService authenticationService)
        {
            _pinService = pinService;
            _searchService = searchService;
            _authenticationService = authenticationService;
        }

        #region -- Public properties --
        public bool DisplayFoundPinList => !String.IsNullOrWhiteSpace(SearchEntry) && SeachPinList != null;

        private string _searchEntry;
        public string SearchEntry
        {
            get => _searchEntry;
            set
            {
                SetProperty(ref _searchEntry, value);
                RaisePropertyChanged(nameof(DisplayFoundPinList));
            }
        }

        private ObservableCollection<PinViewModel> _seachPinList;
        public ObservableCollection<PinViewModel> SeachPinList
        {
            get => _seachPinList;
            set => SetProperty(ref _seachPinList, value);
        }

        private ObservableCollection<PinViewModel> _Pins;
        public ObservableCollection<PinViewModel> Pins
        {
            get => _Pins;
            set => SetProperty(ref _Pins, value);
        }

        private ICommand _PinClickedCommand;
        public ICommand PinClickedCommand => _PinClickedCommand ?? (_PinClickedCommand = SingleExecutionCommand.FromFunc<Position>(OnPinClickedCommandAsync));


        private ICommand _MapClickedCommand;
        public ICommand MapClickedCommand => _MapClickedCommand ?? (_MapClickedCommand = SingleExecutionCommand.FromFunc<Position>(OnMapClickedCommandAsync));

        private ICommand _logOutCommand;
        public ICommand LogOutCommand => _logOutCommand ?? (_logOutCommand = SingleExecutionCommand.FromFunc(OnLogOutCommandAsync));

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await InitPins();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SearchEntry) && !String.IsNullOrWhiteSpace(SearchEntry))
            {
                var pinsModelList = Pins.Select(x => x.ToPinModel());

                var foundPinlist = _searchService.Search(SearchEntry, pinsModelList);

                if (foundPinlist.Count > 0)
                {
                    SeachPinList = new ObservableCollection<PinViewModel>(foundPinlist.AsEnumerable().Select(x => x.ToPinViewModel()));
                    foreach (var pin in SeachPinList)
                    {
                        pin.TapCommand = PinClickedCommand;
                        pin.MoveToPinLocationCommand = SingleExecutionCommand.FromFunc<PinViewModel>(GoToPinLocation);
                    }
                }
                else
                {
                    SeachPinList = null;
                }
            }
            else if (args.PropertyName == nameof(SearchEntry) && String.IsNullOrWhiteSpace(SearchEntry))
            {
                SeachPinList = null;
            }
        }


        #endregion

        #region -- Private helpers --

        private async Task OnLogOutCommandAsync()
        {
            var confirmConfig = new ConfirmConfig()
            {
                Message = "Do you want to logout?",
                OkText = "Ok",
                CancelText = "Cancel"
            };

            var confirm = await UserDialogs.ConfirmAsync(confirmConfig);

            if (confirm)
            {
                _authenticationService.LogOut();

                await NavigationService.NavigateAsync($"/{nameof(StartPage)}");
            }
        }

        private Task GoToPinLocation(PinViewModel pin)
        {
            LocatePin(new Position(pin.Latitude, pin.Longitude));

            return Task.CompletedTask;
        }

        private async Task InitPins()
        {
            var getPinsResult = await _pinService.GetPinsAsync();

            if (getPinsResult.IsSuccess)
            {
                Pins = new ObservableCollection<PinViewModel>(getPinsResult.Result.Select(x => x.ToPinViewModel()).Where( x =>x.IsFavorite == true)); //(getPinsResult.Result.Select(x => x.ToPinViewModel()).Where(x => x.IsFavorite == true));

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

        private Task OnMapClickedCommandAsync(Position arg)
        {
            var a = arg;

            return Task.CompletedTask;
        }

        private void LocatePin(Position position)
        {
            MessagingCenter.Send(this, "MoveFromFoundPinInSearchBarToMainPage", position);
        }

        #endregion
    }
}