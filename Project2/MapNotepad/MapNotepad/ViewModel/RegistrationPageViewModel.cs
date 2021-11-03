using MapNotepad.View;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        public RegistrationPageViewModel(INavigationService navigationService) : base (navigationService)
        {

        }
        #region ---commands---
        public ICommand NextButtonTapCommand => new Command(OnButtonTapNext);

       
       
        #endregion
        #region ---public properties---

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        #endregion

        #region ---private helpers---

        private async void OnButtonTapNext(object obj)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("Name", Name);
            navigationParameters.Add("Email", Email);

            await _navigationService.NavigateAsync(nameof(RegistrationPagePartTwo),navigationParameters);
        }

        #endregion
    }
}
