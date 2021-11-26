﻿using MapNotepad.Helpers;
using MapNotepad.Services.ProfileService;
using MapNotepad.View;
using MapNotepad.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        private IUserService _userService;
        public StartPageViewModel(IUserService userService)
        {
            _userService = userService;
        }

        #region -- Public properties --

        private ICommand _createAccountButtonTapCommand;
        public ICommand CreateAccountButtonTapCommand => _createAccountButtonTapCommand ?? (_createAccountButtonTapCommand = SingleExecutionCommand.FromFunc(GoToRegistrationPage));

        private ICommand _goToLoginPageButtonTapCommand;
        public ICommand GoToLoginPageButtonTapCommand => _goToLoginPageButtonTapCommand ?? (_goToLoginPageButtonTapCommand = SingleExecutionCommand.FromFunc(GoToLoginPage));

        #endregion

        #region -- Private helpers --

        private async Task GoToLoginPage()
        {
            await NavigationService.NavigateAsync(nameof(LoginPage));
        }
        private async Task GoToRegistrationPage()
        {
            await NavigationService.NavigateAsync(nameof(RegistrationPage));
        }

        #endregion

    }
}
