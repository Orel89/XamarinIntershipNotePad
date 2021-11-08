using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class RegistrationPagePartTwoViewModel : BaseViewModel
    {
        public RegistrationPagePartTwoViewModel(INavigationService navigationService) : base (navigationService)
        {
            
        }

        #region ---commands---
        public ICommand NextButtonTapCommand => new Command(CreateAccountButtonTapCommand);

        List<string> list = new List<string>();

        #endregion

        #region ---public properties---

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private string confirmpassword;
        public string ConfirmPassword
        {
            get => password;
            set => SetProperty(ref confirmpassword, value);
        }
        #endregion

        #region ---private helpers---

        private  void CreateAccountButtonTapCommand(object obj)
        {
            List<string> list = new List<string>();
        }

        #endregion

        #region ---ovverides---

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            list.Add(parameters.GetValue<string>("Name"));
            list.Add(parameters.GetValue<string>("Email"));

        }
        #endregion
    }
}
