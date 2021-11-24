using Acr.UserDialogs;
using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Services.SearchService;
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

        public MapPageViewModel(IPinService pinService,
                                ISearchService searchService)
        {
            _pinService = pinService;
            _searchService = searchService;
        }

        #region -- Public properties --

        private PinModel _currentPin;

        public PinModel CurrentPin
        {
            get => _currentPin;
            set
            {
                SetProperty(ref _currentPin, value);
                //RaisePropertyChanged(nameof(DisplayCurrentPinDescript));
            }
        }

        public bool DisplayCurrentPinDescript => CurrentPin != null;

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

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SearchEntry) && !String.IsNullOrWhiteSpace(SearchEntry))
            {
                //CurrentPin = null;
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
                Pins = new ObservableCollection<PinViewModel>(getPinsResult.Result.Select(x => x.ToPinViewModel()));

                foreach (var pin in Pins)
                {
                    pin.TapCommand = PinClickedCommand;
                    //pin.EditCommand = PinEditCommand;
                    //pin.DeleteCommand = PinDeleteCommand;
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

                var confirm = await UserDialogs.ConfirmAsync(confirmConfig);

                if (confirm)
                {

                    var result = await _pinService.DeletePinAsync(pin.ToPinModel());

                    if (result.IsSuccess)
                    {
                        Pins.Remove(pin);

                        await NavigationService.NavigateAsync(nameof(PinListPage));
                    }
                    else
                    {
                        confirmConfig.Message = result.Message;

                        await UserDialogs.ConfirmAsync(confirmConfig);
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
            var pinDescription = (pin as PinViewModel).Description;
            var pinLongitude = (pin as PinViewModel).Longitude;
            //var pinLongitude = (pin as PinViewModel).Longitude;

            var parameter = new NavigationParameters();

            parameter.Add("id", pinId);
            parameter.Add("", pinId);
            parameter.Add("id", pinId);

            await NavigationService.NavigateAsync(nameof(AddPinPage));

        }

        private void LocatePin(Position position)
        {
            MessagingCenter.Send(this, "MoveFromFoundPinInSearchBarToMainPage", position);
        }

        #endregion
    }
}