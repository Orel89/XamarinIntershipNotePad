using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Services.ProfileService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
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

        //private bool _CanSave;
        //public bool CanSave
        //{
        //    get => _CanSave;
        //    set
        //    {
        //        if (SetProperty(ref _CanSave, value))
        //        {
        //            RaisePropertyChanged(nameof(SavePinCommand));
        //        }
        //    }
        //}

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private float _longitude;
        public float Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private float _latitude;
        public float Latitude
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

        private Color _borderColorLabel;
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

        #endregion

        #region --- ovverides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            IsEnableSaveButton = !string.IsNullOrWhiteSpace(Label);

            switch(args.PropertyName)
            {
                case nameof(Label):
                    if (string.IsNullOrWhiteSpace(Label))
                    {
                        BorderColorLabel = Color.Red;
                    }
                    else
                    {
                        BorderColorLabel = Color.Gray;
                    }
                    break;

                case nameof(Latitude):
                    if (Latitude > -90 && Latitude > 90)
                    {
                        BorderColorLatitude = Color.Red;
                    }
                    else
                    {
                        BorderColorLatitude = Color.Green;
                    }
                    break;

                case nameof(Longitude):
                    if (Latitude > -180 && Latitude > 180)
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

        private Task OnSaveCommandAsync()
        {
            var result = _pinService.AddPin(new PinModel()
            {
                UserId = _userService.UserId,
                Label = Label,
                Description = Description,
                Latitude = Latitude,
                Longitude = Longitude,
                IsFavorite = false,
                CreationTime = DateTime.Now
            });

            if (result.Result.IsSuccess)
            {
                _navigationService.GoBackAsync();
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    OkText = "Ok",
                    Message = "Cannot save a pin"
                });
            }
            return Task.CompletedTask;
        }

        #endregion
    }
}
