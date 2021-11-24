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

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string _pageTitle = "Add pin";
        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        private int _pinIdToEdit;
        public int PinIdToEdit
        {
            get => _pinIdToEdit;
            set => SetProperty(ref _pinIdToEdit, value);
        }

        private string _longitude;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private string _latitude;
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

        private bool _IsEnableSaveButton;
        public bool IsEnableSaveButton
        {
            get => _IsEnableSaveButton;
            set => SetProperty(ref _IsEnableSaveButton, value);
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

        private Color _borderColorLongitude;
        public Color BorderColorLongitude
        {
            get => _borderColorLongitude;
            set => SetProperty(ref _borderColorLongitude, value);
        }

        private Color _borderColorLatitude;
        public Color BorderColorLatitude
        {
            get => _borderColorLatitude;
            set => SetProperty(ref _borderColorLatitude, value);
        }

        private Color _borderColorLabel = Color.Gray;
        public Color BorderColorLabel
        {
            get => _borderColorLabel;
            set => SetProperty(ref _borderColorLabel, value);
        }

        private ICommand clearEntryPasswordButtonTapCommand;
        public ICommand ClearEntryPasswordButtonTapCommand => clearEntryPasswordButtonTapCommand ?? (clearEntryPasswordButtonTapCommand = new Command(OnClearEntryDescription));

        private ICommand _clearEntryLabelButtonCommand;
        public ICommand ClearEntryLabelButtonCommand => _clearEntryLabelButtonCommand ?? (_clearEntryLabelButtonCommand = new Command(OnClearLabelAsync));

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

        public async override void InitializeAsync(INavigationParameters parameters)
        {
            parameters.TryGetValue("pinId", out _pinIdToEdit);

            parameters.TryGetValue("pageTitle", out _pageTitle);

            if (_pageTitle == "Edit pin" && _pinIdToEdit > 0)
            {
                await InitAsync();
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            IsEnableSaveButton = !string.IsNullOrWhiteSpace(Label) && double.Parse(Latitude) >= -90 && double.Parse(Latitude) <= 90 && double.Parse(Longitude) >= -180 && double.Parse(Longitude) <= 180;
        }

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            
            var userPins = await _pinService.GetPinsAsync();

            if (userPins.IsSuccess)
            {
                var pin = userPins.Result.TakeWhile(x => x.Id == _pinIdToEdit).FirstOrDefault();

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

        private void OnClearEntryDescription()
        {
            Description = null;
        }

        private async Task OnGoBackCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private void OnClearLabelAsync()
        {
            Label = null;
        }

        private async Task OnSaveCommandAsync()
        {
            var result = await _pinService.AddPinAsync(new PinModel()
            {
                UserId = _userService.UserId,
                Label = Label,
                Description = Description,
                Latitude = double.Parse(Latitude),
                Longitude = double.Parse(Longitude),
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

        #endregion
    }
}
