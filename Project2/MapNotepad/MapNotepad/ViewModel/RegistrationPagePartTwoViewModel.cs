using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Helpers.Validation;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.ProfileService;
using MapNotepad.Services.Registration;
using MapNotepad.View;
using Prism.Common;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class RegistrationPagePartTwoViewModel : BaseViewModel
    {

        private readonly IRegistrationService _registrationService;

        private readonly IUserService _userService;

        private string _name;

        private string _email;

        public RegistrationPagePartTwoViewModel(IUserService userService,
                                                IRegistrationService registrationService)
        {
            _userService = userService;

            _registrationService = registrationService;
        }

        #region ---public properties---

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmpassword;
        public string ConfirmPassword
        {
            get => _confirmpassword;
            set => SetProperty(ref _confirmpassword, value);
        }

        private string _imageButton = "ic_eye_off.png";
        public string ImageButton
        {
            get => _imageButton;
            set => SetProperty(ref _imageButton, value);
        }

        private bool _isVisiblePasswordEntryLeftButton;
        public bool IsVisiblePasswordEntryLeftButton
        {
            get => _isVisiblePasswordEntryLeftButton;
            set => SetProperty(ref _isVisiblePasswordEntryLeftButton, value);
        }

        private bool _isPassword = true;
        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }

        private ICommand _createAccountButtonTapCommand;
        public ICommand CreateAccountButtonTapCommand => _createAccountButtonTapCommand ?? (_createAccountButtonTapCommand = SingleExecutionCommand.FromFunc(ExecuteCreateAccountButtonTapCommand));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapGoToBackPage));

        private ICommand _hideEntryPasswordButtonTapCommand;
        public ICommand HideEntryPasswordButtonTapCommand => _hideEntryPasswordButtonTapCommand ?? (_hideEntryPasswordButtonTapCommand = new Command(HidePasswordEntryButtonTapCommand));
        #endregion

        #region -- Private helpers --

        private async Task ExecuteCreateAccountButtonTapCommand()
        {
            var message = "Password mismatch";

            if (Password == ConfirmPassword)
            {
                message = "The Password must contain 6 symbols, one digit and a capital letter";

                if (PasswordValidation.IsPasswordMatched(Password))
                {
                    var result = await _userService.AddUserAsync(new Model.UserModel()
                    {
                        UserName = _name,
                        Email = _email,
                        Password = Password,
                        CreationTime = DateTime.Now
                    });

                    if (result.IsSuccess && result.Result > 0)
                    {
                        message = "Successful registration";
                        await UserDialogs.AlertAsync(message);
                        await NavigationService.NavigateAsync(nameof(StartPage));
                    }
               
                }
            }
            else
            {
                await UserDialogs.AlertAsync(message);
            }

        }

        private void HidePasswordEntryButtonTapCommand()
        {
            IsPassword = !IsPassword;
            if (IsPassword)
            {
                ImageButton = "ic_eye_off.png";
            }
            else
            {
                ImageButton ="ic_eye.png";
            }
        }

        private async Task OnButtonTapGoToBackPage()
        {
            await NavigationService.GoBackAsync();
        }

        #endregion

        #region ---ovverides---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Password):
                    IsVisiblePasswordEntryLeftButton = !string.IsNullOrWhiteSpace(Password);
                    break;
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            
            parameters.TryGetValue("Email", out _email);
            parameters.TryGetValue("Name", out _name);

        }

        #endregion
    }
}
