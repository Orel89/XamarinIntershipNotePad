using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        #region ---public properties---

        private string image = "logo4.png";
        public string ImageUrl
        {
            get => image;
            set => SetProperty(ref image, value);
        }
        #endregion
    }
}
