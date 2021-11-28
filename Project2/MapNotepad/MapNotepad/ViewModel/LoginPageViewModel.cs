using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Helpers.AuthHelpers;
using MapNotepad.Helpers.Validation;
using MapNotepad.Model;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.ProfileService;
using MapNotepad.Views;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
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

        public ICommand GoogleAuthCommand { get; set; }

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
            GoogleAuthCommand = new Command(OnGoogleAuthCommand);
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

        void OnGoogleAuthCommand()
        {
            string clientId = null;
            string redirectUri = null;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.iOSClientId;
                    redirectUri = Constants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }
            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new OAuthLoginPresenter();
            presenter.Login(authenticator);
        }
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                }
                if (user != null)
                {
                    _authenticationService.RegisterWithGoogleAccount(user.Name, user.Email);

                    await NavigationService.NavigateAsync(nameof(MainProfilePage));
                }
            }
        }
        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            Debug.WriteLine("Authentication error: " + e.Message);
        }


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
