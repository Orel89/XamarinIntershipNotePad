using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Services.ProfileService;
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
    public class AddPinPageViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        private IUserService _userService;
        public AddPinPageViewModel(IUserService userService,
                                   IPinService pinService)
        {
            _pinService = pinService;
            _userService = userService;
        }

        #region -- Public Properties --

        private int _pinIdToEdit;

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string _longitude = String.Empty;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private string _latitude = String.Empty;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private bool _isVisibleEntryLabelLeftButton;
        public bool IsVisibleEntryLabelLeftButton
        {
            get => _isVisibleEntryLabelLeftButton;
            set => SetProperty(ref _isVisibleEntryLabelLeftButton, value);
        }

        private bool _isVisibleEntryDescriptionLeftButton;
        public bool IsVisibleEntryDescriptionLeftButton
        {
            get => _isVisibleEntryDescriptionLeftButton;
            set => SetProperty(ref _isVisibleEntryDescriptionLeftButton, value);
        }

        private Color _entryBorderLabelColor = Color.Red;
        public Color EntryBorderLabelColor
        {
            get => _entryBorderLabelColor;
            set => SetProperty(ref _entryBorderLabelColor, value);
        }

        private Color _entryBorderLongitudeColor = Color.FromHex("#D7DDE8");
        public Color EntryBorderLongitudeColor
        {
            get => _entryBorderLongitudeColor;
            set => SetProperty(ref _entryBorderLongitudeColor, value);
        }

        private Color _entryBorderLatitudeColor = Color.FromHex("#D7DDE8");
        public Color EntryBorderLatitudeColor
        {
            get => _entryBorderLatitudeColor;
            set => SetProperty(ref _entryBorderLatitudeColor, value);
        }

        private bool _IsVisibleLongitudeEntryLeftButton;
        public bool IsVisibleLongitudeEntryLeftButton
        {
            get => _IsVisibleLongitudeEntryLeftButton;
            set => SetProperty(ref _IsVisibleLongitudeEntryLeftButton, value);
        }

        private bool _IsVisibleLatitudeEntryLeftButton;
        public bool IsVisibleLatitudeEntryLeftButton
        {
            get => _IsVisibleLatitudeEntryLeftButton;
            set => SetProperty(ref _IsVisibleLatitudeEntryLeftButton, value);
        }

        private bool _IsVisibleLabelEntryLeftButton;
        public bool IsVisibleLabelEntryLeftButton
        {
            get => _IsVisibleLabelEntryLeftButton;
            set => SetProperty(ref _IsVisibleLabelEntryLeftButton, value);
        }

        private bool _IsVisibleDescriptionEntryLeftButton;
        public bool IsVisibleDescriptionEntryLeftButton
        {
            get => _IsVisibleDescriptionEntryLeftButton;
            set => SetProperty(ref _IsVisibleDescriptionEntryLeftButton, value);
        }

        private ICommand _clearEntryLatitudeCommand;
        public ICommand ClearEntryLatitudeCommand => _clearEntryLatitudeCommand ?? (_clearEntryLatitudeCommand = SingleExecutionCommand.FromFunc(OnClearLatitudeCommandAsync));

        private ICommand _clearEntryLongitudeCommand;
        public ICommand ClearEntryLongitudeCommand => _clearEntryLongitudeCommand ?? (_clearEntryLongitudeCommand = SingleExecutionCommand.FromFunc(OnClearyLongitudeCommandAsync));

        private ICommand _clearEntryLabelCommand;
        public ICommand ClearEntryLabelCommand => _clearEntryLabelCommand ?? (_clearEntryLabelCommand = SingleExecutionCommand.FromFunc(OnClearLabelCommandAsync));

        private ICommand _clearEntryDescriptionCommand;
        public ICommand ClearEntryDescriptionCommand => _clearEntryDescriptionCommand ?? (_clearEntryDescriptionCommand = SingleExecutionCommand.FromFunc(OnClearDescriptionCommandAsync));

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = SingleExecutionCommand.FromFunc(OnGoBackCommandAsync));

        private ICommand _saveButtonCommand;
        public ICommand SaveButtonCommand => _saveButtonCommand ?? (_saveButtonCommand = SingleExecutionCommand.FromFunc(OnSaveCommandAsync));

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ?? (_mapClickedCommand = SingleExecutionCommand.FromFunc<Position>(OnMapClickedCommandAsync));

        private ObservableCollection<PinViewModel> _Pins;
        public ObservableCollection<PinViewModel> Pins
        {
            get => _Pins;
            set => SetProperty(ref _Pins, value);
        }

        #endregion

        #region -- Ovverides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Latitude):
                    IsVisibleLatitudeEntryLeftButton = !string.IsNullOrWhiteSpace(Latitude);
                    if (IsVisibleLatitudeEntryLeftButton)
                    {
                        if (double.TryParse(Latitude, out double _latitudeDoubleCheck))
                        {
                            if (_latitudeDoubleCheck >= -90 && _latitudeDoubleCheck <= 90)
                            {
                                EntryBorderLatitudeColor = Color.FromHex("#D7DDE8");
                            }
                            else
                            {
                                EntryBorderLatitudeColor = Color.Red;
                            }
                        }
                        else
                        {
                            EntryBorderLatitudeColor = Color.Red;
                        }
                    }
                    else
                    {
                        EntryBorderLatitudeColor = Color.Red;
                    }
                    break;
                case nameof(Longitude):
                    IsVisibleLongitudeEntryLeftButton = !string.IsNullOrWhiteSpace(Longitude);
                    if (IsVisibleLongitudeEntryLeftButton)
                    {
                        if (double.TryParse(Longitude, out double _longitudeDoubleCheck))
                        {
                            if (_longitudeDoubleCheck >= -180 && _longitudeDoubleCheck <= 180)
                            {
                                EntryBorderLongitudeColor = Color.FromHex("#D7DDE8");
                            }
                            else
                            {
                                EntryBorderLongitudeColor = Color.Red;
                            }
                        }
                        else
                        {
                            EntryBorderLongitudeColor = Color.Red;
                        }
                    }
                    else
                    {
                        EntryBorderLongitudeColor = Color.Red;
                    }
                    break;
                case nameof(Label):
                    IsVisibleLabelEntryLeftButton = !string.IsNullOrWhiteSpace(Label);
                    if (IsVisibleLabelEntryLeftButton)
                    {
                        EntryBorderLabelColor = Color.FromHex("#D7DDE8");
                    }
                    else
                    {
                        EntryBorderLabelColor = Color.Red;
                    }
                    break;
                case nameof(Description):
                    IsVisibleDescriptionEntryLeftButton = !string.IsNullOrWhiteSpace(Description);
                    break;
            }
        }

        public override void InitializeAsync(INavigationParameters parameters)
        {
            base.InitializeAsync(parameters);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            parameters.TryGetValue("pinId", out _pinIdToEdit);

            if ( _pinIdToEdit > 0) { await InitAsync(); }
        }

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            var userPins = await _pinService.GetPinsAsync();

            if (userPins.IsSuccess)
            {
                var pin = userPins.Result.Where(x => x.Id == _pinIdToEdit).FirstOrDefault();

                if (pin != null)
                {
                    Label = pin.Label;
                    Description = pin.Description;
                    Longitude = pin.Longitude.ToString();
                    Latitude = pin.Latitude.ToString();
                }
            }
        }

        private Task OnMapClickedCommandAsync(Position position)
        {
            var pins = new ObservableCollection<PinViewModel>();
            pins.Add(new PinViewModel()
            {
                Label = Label ?? "No label",
                Latitude = position.Latitude,
                Longitude = position.Longitude
            });

            Pins = new ObservableCollection<PinViewModel>(pins);

            Longitude = position.Longitude.ToString();

            Latitude = position.Latitude.ToString();

            return Task.CompletedTask;
        }

        private Task OnClearLatitudeCommandAsync()
        {
            Latitude = null;

            return Task.CompletedTask;
        }
        private Task OnClearLabelCommandAsync()
        {
            Label = null;

            return Task.CompletedTask;
        }
        private Task OnClearDescriptionCommandAsync()
        {
            Description = null;

            return Task.CompletedTask;
        }

        private Task OnClearyLongitudeCommandAsync()
        {
            Longitude = null;

            return Task.CompletedTask;
        }

        private async Task OnGoBackCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task OnSaveCommandAsync()
        {
            if (!string.IsNullOrWhiteSpace(Label) && double.TryParse(Latitude, out double _latitudeDoubleCheck) && double.TryParse(Longitude, out double _longitudeeDoubleCheck))
            {
                if (_latitudeDoubleCheck >= -90 && _latitudeDoubleCheck <= 90 && _longitudeeDoubleCheck >= -180 && _longitudeeDoubleCheck <= 180)
                {
                    if (_pinIdToEdit == 0)
                    {
                        var result = await _pinService.AddPinAsync(new PinModel()
                        {
                            UserId = _userService.UserId,
                            Label = Label,
                            Description = Description,
                            Latitude = double.Parse(Latitude),
                            Longitude = double.Parse(Longitude),
                            IsFavorite = true,
                            CreationTime = DateTime.Now
                        });

                        if (result.IsSuccess)
                        {
                            await NavigationService.GoBackAsync();
                        }
                        else
                        {
                            UserDialogs.Alert(new AlertConfig()
                            {
                                OkText = "Ok",
                                Message = "Cannot save a pin"
                            });
                        }
                    }
                    else
                    {
                        var result = await _pinService.UpdatePinAsync(new PinModel()
                        {
                            Id = _pinIdToEdit,
                            UserId = _userService.UserId,
                            Label = Label,
                            Description = Description,
                            Latitude = double.Parse(Latitude),
                            Longitude = double.Parse(Longitude)
                        });

                        if (result.IsSuccess)
                        {
                            await NavigationService.GoBackAsync();
                        }
                        else
                        {
                            UserDialogs.Alert(new AlertConfig()
                            {
                                OkText = "Ok",
                                Message = "Cannot save a pin"
                            });
                        }
                    }
                }
            }
        }

        #endregion
    }
}
