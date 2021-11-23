using MapNotepad.Helpers;
using MapNotepad.Helpers.Validation;
using MapNotepad.Model;
using MapNotepad.Services.Registration;
using MapNotepad.View;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class RegistrationPagePartTwoViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;
        private string _name;
        private string _email;

        public RegistrationPagePartTwoViewModel(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        #region -- Public properties --

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

        private string _imageButtonPassword = "ic_eye_off.png";
        public string ImageButtonPassword
        {
            get => _imageButtonPassword;
            set => SetProperty(ref _imageButtonPassword, value);
        }

        private string _imageButtonConfirmPassword = "ic_eye_off.png";
        public string ImageButtonConfirmPassword
        {
            get => _imageButtonConfirmPassword;
            set => SetProperty(ref _imageButtonConfirmPassword, value);
        }


        private bool _isVisiblePasswordEntryLeftButton;
        public bool IsVisiblePasswordEntryLeftButton
        {
            get => _isVisiblePasswordEntryLeftButton;
            set => SetProperty(ref _isVisiblePasswordEntryLeftButton, value);
        }

        private bool _isVisibleConfirmPasswordEntryLeftButton;
        public bool IsVisibleConfirmPasswordEntryLeftButton
        {
            get => _isVisibleConfirmPasswordEntryLeftButton;
            set => SetProperty(ref _isVisibleConfirmPasswordEntryLeftButton, value);
        }

        private bool _isPassword = true;
        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }

        private bool _isConfirmPassword = true;
        public bool IsConfirmPassword
        {
            get => _isConfirmPassword;
            set => SetProperty(ref _isConfirmPassword, value); //IsMessageVisible
        }

        private bool _IsMessageVisible;
        public bool IsMessageVisible
        {
            get => _IsMessageVisible;
            set => SetProperty(ref _IsMessageVisible, value);
        }


        private ICommand _createAccountButtonTapCommand;
        public ICommand CreateAccountButtonTapCommand => _createAccountButtonTapCommand ?? (_createAccountButtonTapCommand = SingleExecutionCommand.FromFunc(ExecuteCreateAccountButtonTapCommand));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapGoToBackPage));

        private ICommand _hideEntryPasswordButtonTapCommand;
        public ICommand HideEntryPasswordButtonTapCommand => _hideEntryPasswordButtonTapCommand ?? (_hideEntryPasswordButtonTapCommand = new Command(OnHidePasswordEntryCommand));

        private ICommand _hideEntryConfirmPasswordButtonTapCommand;
        public ICommand HideEntryConfirmPasswordButtonTapCommand => _hideEntryConfirmPasswordButtonTapCommand ?? (_hideEntryConfirmPasswordButtonTapCommand = new Command(OnHideConfirmPasswordEntryCommand));
        #endregion

        #region -- Private helpers --

        private async Task ExecuteCreateAccountButtonTapCommand()
        {
            var message = "Password mismatch";

            if (Password == ConfirmPassword)
            {
                message = "The Password must contain 6 symbols, one digit and a capital letter";

                if (Validator.IsPasswordMatched(Password))
                {
                    var result = await _registrationService.RegistrationAsync(new UserModel()
                    {
                        UserName = _name,
                        Email = _email,
                        Password = Password,
                        CreationTime = DateTime.Now
                    });

                    if (result.IsSuccess)
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

        private void OnHidePasswordEntryCommand()
        {
            IsPassword = !IsPassword;
        }

        private void OnHideConfirmPasswordEntryCommand()
        {
            IsConfirmPassword = !IsConfirmPassword;
        }

        private async Task OnButtonTapGoToBackPage()
        {
            await NavigationService.GoBackAsync();
        }

        #endregion

        #region -- Ovverides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Password):
                    IsVisiblePasswordEntryLeftButton = !string.IsNullOrWhiteSpace(nameof(Password));
                    break;
                case nameof(ConfirmPassword):
                    IsVisibleConfirmPasswordEntryLeftButton = !string.IsNullOrWhiteSpace(nameof(ConfirmPassword));
                    IsMessageVisible = Password != ConfirmPassword;
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
