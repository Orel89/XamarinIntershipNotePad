using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Helpers.Validation;
using MapNotepad.Model;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.ProfileService;
using MapNotepad.Views;
using Prism.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        IAuthenticationService _authenticationService;
        IUserService _userService;
        public LoginPageViewModel(IUserService profileService,
                                  IAuthenticationService authenticationService)
        {
            _userService = profileService;
            _authenticationService = authenticationService;
        }

        #region -- Public property --

        private ICommand _loginButtonTapCommand;
        public ICommand LoginButtonTapCommand => _loginButtonTapCommand ?? (_loginButtonTapCommand = SingleExecutionCommand.FromFunc(OnLoginButtonAsync, () => false));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapGoToBackPage));

        private ICommand clearEntryPasswordButtonTapCommand;
        public ICommand ClearEntryPasswordButtonTapCommand => clearEntryPasswordButtonTapCommand ?? (clearEntryPasswordButtonTapCommand = SingleExecutionCommand.FromFunc(OnClearEntryPassword));

        private ICommand clearEntryEmailButtonTapCommand;
        public ICommand ClearEntryEmailButtonTapCommand => clearEntryEmailButtonTapCommand ?? (clearEntryEmailButtonTapCommand = SingleExecutionCommand.FromFunc(OnClearEntryEmail));

        private ICommand _hideEntryPasswordCommand;
        public ICommand HideEntryPasswordCommand => _hideEntryPasswordCommand ?? (_hideEntryPasswordCommand = SingleExecutionCommand.FromFunc(OnHidePasswordEntryCommand));

     
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        private bool isVisibleEntryPasswordLeftButton;
        public bool IsVisibleEntryPasswordLeftButton
        {
            get => isVisibleEntryPasswordLeftButton;
            set => SetProperty(ref isVisibleEntryPasswordLeftButton, value);
        }

        private bool isVisibleEntryEmailLeftButton;
        public bool IsVisibleEntryEmailLeftButton
        {
            get => isVisibleEntryEmailLeftButton;
            set => SetProperty(ref isVisibleEntryEmailLeftButton, value);
        }

        private bool _IsMessageEmailVisible;
        public bool IsMessageEmailVisible
        {
            get => _IsMessageEmailVisible;
            set => SetProperty(ref _IsMessageEmailVisible, value);
        }

        private bool _isPassword = true;
        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }

        #endregion

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            var res = parameters.TryGetValue("_email", out string userEmail);
            Email = userEmail;
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Password):
                    IsVisibleEntryPasswordLeftButton = !string.IsNullOrWhiteSpace(Password);
                    break;
                case nameof(Email):
                    IsVisibleEntryEmailLeftButton = !string.IsNullOrWhiteSpace(Email);
                    await CheckEmailExists();
                    break;
            }
        }

        private async Task CheckEmailExists()
        {
            var response = await _userService.CheckEmailExists(Email);

            if (response.IsSuccess)
            {
                IsMessageEmailVisible = false;
            }
            else
            {
                IsMessageEmailVisible = true;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnButtonTapGoToBackPage()
        {
            await NavigationService.GoBackAsync();
        }
        private async Task OnLoginButtonAsync()
        {
            var message = "Wrong email";

            if (Validator.IsEmailMatched(Email))
            {
                message = "Email not found";

                var IsEmailExists = await _userService.CheckEmailExists(Email);

                if (IsEmailExists.IsSuccess)
                {
                    message = "The password is incorrect";

                    var user = await _authenticationService.AuthorizationAsync(Email, Password);

                    if (user.IsSuccess)
                    {
                        await NavigationService.NavigateAsync(nameof(MainProfilePage));
                    }
                    else
                    {
                        await UserDialogs.AlertAsync(message);
                    }
                }
                else
                {
                    await UserDialogs.AlertAsync(message);
                }
            }

            else
            {
                await UserDialogs.AlertAsync(message);
            }
        }

        private Task OnHidePasswordEntryCommand()
        {
            IsPassword = !IsPassword;

            return Task.CompletedTask;
        }
        private Task OnClearEntryPassword()
        {
            Password = null;

            return Task.CompletedTask;
        }

        private Task OnClearEntryEmail()
        {
            Email = null;

            return Task.CompletedTask;
        }

        #endregion
    }
}
