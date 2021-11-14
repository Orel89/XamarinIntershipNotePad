using MapNotepad.Helpers;
using MapNotepad.View;
using MapNotepad.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        public StartPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        #region ---public properties---

        private string image = "customLogo.png";
        public string ImageUrl
        {
            get => image;
            set => SetProperty(ref image, value);
        }
        #endregion 

        #region ---commands---

        private ICommand _createAccountButtonTapCommand;
        public ICommand CreateAccountButtonTapCommand => _createAccountButtonTapCommand ?? (_createAccountButtonTapCommand = SingleExecutionCommand.FromFunc(GoToRegistrationPage));

        private ICommand _goToLoginPageButtonTapCommand;
        public ICommand GoToLoginPageButtonTapCommand => _goToLoginPageButtonTapCommand ?? (_goToLoginPageButtonTapCommand = SingleExecutionCommand.FromFunc(GoToLoginPage));

        #endregion

        #region --- navigation ---

        private async Task GoToLoginPage()
        {
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }
        private async Task GoToRegistrationPage()
        {
            await _navigationService.NavigateAsync(nameof(RegistrationPage));
        }

        #endregion
    }
}
