using MapNotepad.Helpers;
using MapNotepad.Services.PinService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
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
    }

    #region --- Public Properties ---

    private ICommand _goBackCommand;
    public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = SingleExecutionCommand.FromFunc(OnGoBackCommandAsync));

    #endregion

    #region --- Private helpers ---

    private async Task OnGoBackCommandAsync()
    {
        await _navigationService.GoBackAsync();
    }

    #endregion
}
