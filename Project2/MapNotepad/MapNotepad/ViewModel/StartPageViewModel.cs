using MapNotepad.View;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        public StartPageViewModel(INavigationService navigationService): base (navigationService)
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
        public ICommand CreateAccountButtonTapCommand => new Command(OnButtonTapCreateAccount);
        #endregion

        #region ovverides---
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        #endregion


        #region ---private helpers---
        private async void OnButtonTapCreateAccount(object obj)
        {
            await _navigationService.NavigateAsync(nameof(RegistrationPage));
        }

        #endregion
    }
}
