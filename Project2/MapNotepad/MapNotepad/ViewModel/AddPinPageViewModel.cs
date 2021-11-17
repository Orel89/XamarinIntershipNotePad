using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Services.ProfileService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
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
        public AddPinPageViewModel(INavigationService navigationService,
                                   IUserService userService,
                                   IPinService pinService)
               : base(navigationService)
        {
            _pinService = pinService;
            _userService = userService;
        }

        #region --- Public Properties ---

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private double _latitude;
        public double Latitude
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

        #region --- ovverides ---

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

            IsEnableSaveButton = !string.IsNullOrWhiteSpace(Label) && Latitude >= -90 && Latitude <=90 && Longitude >= -180 && Longitude <= 180;

            switch(args.PropertyName)
            {
                case nameof(Latitude):
                    if (Latitude < -90 && Latitude > 90)
                    {
                        BorderColorLatitude = Color.Red;
                    }
                    else
                    {
                        BorderColorLatitude = Color.Green;
                    }
                    break;

                case nameof(Longitude):
                    if (Latitude < -180 && Latitude > 180)
                    {
                        BorderColorLongitude = Color.Red;
                    }
                    else
                    {
                        BorderColorLongitude = Color.Green;
                    }
                    break;
            }
        }

        #endregion

        #region --- Private helpers ---

        private Task OnMapClickedCommandAsync(Position position)
        {
            Longitude = position.Longitude;

            Latitude = position.Latitude;

            var pins = new ObservableCollection<PinViewModel>();
            pins.Add(new PinViewModel()
            {
               Label = Label ?? "No label",
               Latitude = Latitude,
               Longitude = Longitude
            });

            Pins = new ObservableCollection<PinViewModel>(pins);

            return Task.CompletedTask;
        }

        private void OnClearEntryDescription()
        {
            Description = null;
        }

        private async Task OnGoBackCommandAsync()
        {
            await _navigationService.GoBackAsync();
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
                Latitude = Latitude,
                Longitude = Longitude,
                IsFavorite = false,
                CreationTime = DateTime.Now
            });

            if (result.IsSuccess)
            {
                await _navigationService.GoBackAsync();
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    OkText = "Ok",
                    Message = "Cannot save a pin"
                });
            }
        }

        #endregion
    }
}
