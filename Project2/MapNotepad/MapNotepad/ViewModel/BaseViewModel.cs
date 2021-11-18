using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.ViewModel
{
    public class BaseViewModel : BindableBase, INavigatedAware, IInitialize
    {
        public BaseViewModel()
        {
            NavigationService = App.Resolve<INavigationService>();
            UserDialogs = App.Resolve<IUserDialogs>();
        }
        protected INavigationService NavigationService { get; }

        protected IUserDialogs UserDialogs { get; }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }



        #region ---navigation---

        #endregion
        public virtual void InitializeAsync(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
