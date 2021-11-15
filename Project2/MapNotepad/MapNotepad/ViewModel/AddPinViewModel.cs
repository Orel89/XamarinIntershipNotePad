using MapNotepad.Helpers;
using MapNotepad.Services.PinService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModel
{
    public class AddPinViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        public AddPinViewModel(INavigationService navigationService,
                               IPinService pinService)
               : base(navigationService)
        {
            _pinService = pinService;
        }


        #region --- Public Properties ---

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private bool _isVisibleEntryLabelLeftButton;
        public bool IsVisibleEntryLabelLeftButton
        {
            get => _isVisibleEntryLabelLeftButton;
            set => SetProperty(ref _isVisibleEntryLabelLeftButton, value);
        }

        private bool _isVisibleEntryDescriptionLeftButton;
        public bool IsVisibleEntryDescriptionLeftButton
        {
            get => _isVisibleEntryDescriptionLeftButton;
            set => SetProperty(ref _isVisibleEntryDescriptionLeftButton, value);
        }

        private ICommand _loginButtonTapCommand;
        public ICommand LoginButtonTapCommand => _loginButtonTapCommand ?? (_loginButtonTapCommand = SingleExecutionCommand.FromFunc(OnLoginButtonAsync, () => false));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapGoToBackPage));

        private ICommand clearEntryPasswordButtonTapCommand;
        public ICommand ClearEntryPasswordButtonTapCommand => clearEntryPasswordButtonTapCommand ?? (clearEntryPasswordButtonTapCommand = new Command(OnClearEntryPassword));

        private ICommand _clearEntryLabelButtonCommand;
        public ICommand ClearEntryLabelButtonCommand => _clearEntryLabelButtonCommand ?? (_clearEntryLabelButtonCommand = SingleExecutionCommand.FromFunc(OnClearLabelAsync));

      

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = SingleExecutionCommand.FromFunc(OnGoBackCommandAsync));

        #endregion

        #region --- Private helpers ---

        private async Task OnGoBackCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task OnClearLabelAsync()
        {
            Label = null;
        }

        #endregion
    }
}
