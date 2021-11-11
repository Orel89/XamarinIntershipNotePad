using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Services.Authentication;
using MapNotepad.View;
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

        private IRegistration _registration;
        private string _name;
        private string _email;
        public RegistrationPagePartTwoViewModel(INavigationService navigationService, IRegistration registration) : base (navigationService)
        {
            _registration = registration;
        }

        #region ---commands---

        private ICommand _createAccountButtonTapCommand;
        public ICommand CreateAccountButtonTapCommand => _createAccountButtonTapCommand ?? (_createAccountButtonTapCommand = SingleExecutionCommand.FromFunc(ExecuteCreateAccountButtonTapCommand));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapGoToBackPage));

        private ICommand _hideEntryPasswordButtonTapCommand;
        public ICommand HideEntryPasswordButtonTapCommand => _hideEntryPasswordButtonTapCommand ?? (_hideEntryPasswordButtonTapCommand = new Command(HidePasswordEntryButtonTapCommand));

        #endregion

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

        #endregion

        #region --- execution commands ---

        private async Task ExecuteCreateAccountButtonTapCommand()
        {
            ConfirmConfig confirnConfig = new ConfirmConfig()
            {
                OkText = "Ok",
                CancelText = string.Empty
            };
                
            confirnConfig.Message = await _registration.RegistrationAsync(_email, _password, _confirmpassword, _name);

            await UserDialogs.Instance.ConfirmAsync(confirnConfig);

            await _navigationService.NavigateAsync(nameof(LoginPage));

        }

        private void HidePasswordEntryButtonTapCommand()
        {
            IsPassword = !IsPassword;
        }

        private async Task OnButtonTapGoToBackPage()
        {
            await _navigationService.GoBackAsync();
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
            base.Initialize(parameters);
            _name = parameters.GetValue<string>("Name");
            _email = parameters.GetValue<string>("Email");

        }

        #endregion
    }
}
