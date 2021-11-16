using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.ProfileService;
using MapNotepad.Views;
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
    public class LoginPageViewModel : BaseViewModel
    {
        IAuthenticationService _authentication;
        IUserService _profileService;
        public LoginPageViewModel(INavigationService navigationService,
                                  IUserService profileService,
                                  IAuthenticationService authentication)
               : base(navigationService)
        {
            _profileService = profileService;
            _authentication = authentication;
        }

        #region ---commands---

        private ICommand _loginButtonTapCommand;
        public ICommand LoginButtonTapCommand => _loginButtonTapCommand ?? (_loginButtonTapCommand = SingleExecutionCommand.FromFunc(OnLoginButtonAsync, () => false));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapGoToBackPage));

        private ICommand clearEntryPasswordButtonTapCommand;
        public ICommand ClearEntryPasswordButtonTapCommand => clearEntryPasswordButtonTapCommand ?? (clearEntryPasswordButtonTapCommand = new Command(OnClearEntryPassword));

        private ICommand clearEntryEmailButtonTapCommand;
        public ICommand ClearEntryEmailButtonTapCommand => clearEntryEmailButtonTapCommand ?? (clearEntryEmailButtonTapCommand = new Command(OnClearEntryEmail));

        #endregion

        #region -- Public properties --

        private Task OnClearLabelAsync()
        {
            throw new NotImplementedException();
        }

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

        #endregion

        #region --- overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Password):
                    IsVisibleEntryPasswordLeftButton = !string.IsNullOrWhiteSpace(Password);
                    break;
                case nameof(Email):
                    IsVisibleEntryEmailLeftButton = !string.IsNullOrWhiteSpace(Email);
                    break;
            }
        }

        #endregion

        #region ---execution commands---

        private async Task OnButtonTapGoToBackPage()
        {
            await _navigationService.GoBackAsync();
        }
        private async Task OnLoginButtonAsync()
        {

            ConfirmConfig confirnConfig = new ConfirmConfig()
            {
                OkText = "Ok",
                CancelText = string.Empty
            };

            confirnConfig.Message = await _authentication.AuthorizationAsync(Email, Password);

            if (confirnConfig.Message == "successfulAuthentication")
            {
                await _navigationService.NavigateAsync(nameof(MainProfilePage));
            }

            else { await UserDialogs.Instance.ConfirmAsync(confirnConfig); }

        }

        private void OnClearEntryPassword()
        {
            Password = null;
        }

        private void OnClearEntryEmail()
        {
            Email = null;
        }

        #endregion
    }
}
