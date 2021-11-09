using MapNotepad.View;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
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

        private ICommand _toNextPageButtonTapCommand;
        public ICommand ToNextPageButtonTapCommand => _toNextPageButtonTapCommand ?? (_toNextPageButtonTapCommand = new Command(OnButtonTapNextPage));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = new Command(OnButtonTapGoToBackPage));

        private ICommand clearEntryNameButtonTapCommand;
        public ICommand ClearEntryNameButtonTapCommand => clearEntryNameButtonTapCommand ?? (clearEntryNameButtonTapCommand = new Command(OnClearEntryButtonTapCommand));

        private ICommand clearEntryEmailButtonTapCommand;
        public ICommand ClearEntryEmailButtonTapCommand => clearEntryEmailButtonTapCommand ?? (clearEntryEmailButtonTapCommand = new Command(OnClearEntryEmailButtonTapCommand));

        #endregion

        #region -- Public properties --

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

        private bool isVisibleNameEntryLeftButton;
        public bool IsVisibleNameEntryLeftButton
        {
            get => isVisibleNameEntryLeftButton;
            set => SetProperty(ref isVisibleNameEntryLeftButton, value);
        }

        private bool isVisibleEmailEntryLeftButton;
        public bool IsVisibleEmailEntryLeftButton
        {
            get => isVisibleEmailEntryLeftButton;
            set => SetProperty(ref isVisibleEmailEntryLeftButton, value);
        }

        #endregion

        #region --- overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(Name):
                    IsVisibleNameEntryLeftButton = !string.IsNullOrWhiteSpace(Name);
                    break;
                case nameof(Email):
                    IsVisibleEmailEntryLeftButton = !string.IsNullOrWhiteSpace(Email);
                    break;
            }
        }

        #endregion
        #region ---execution commands---

        private async void OnButtonTapGoToBackPage(object obj)
        {
            await _navigationService.GoBackAsync();
        }
        private async void OnButtonTapNextPage(object obj)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("Name", Name);
            navigationParameters.Add("Email", Email);

            await _navigationService.NavigateAsync(nameof(RegistrationPagePartTwo),navigationParameters);
        }
        
        private void OnClearEntryButtonTapCommand(object obj)
        {
            Name = null;
        }

        private void OnClearEntryEmailButtonTapCommand(object obj)
        {
            Email = null;
        }
        #endregion
    }
}
