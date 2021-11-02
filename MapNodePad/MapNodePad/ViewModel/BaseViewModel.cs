using MapNodePad.View;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNodePad.ViewModel
{
    public class BaseViewModel : BindableBase 
    {
        private readonly INavigationService _navigationService;
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region --- Navigation ---

        public async void NavigateGoBack()
        {
            await _navigationService.GoBackAsync();
        }

        public async void GoToMainPage()
        {
            await _navigationService.NavigateAsync("/" + nameof(MainPage));
        }
        #endregion
    }
}
