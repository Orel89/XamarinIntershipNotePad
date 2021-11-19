using Acr.UserDialogs;
using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapNotepad.ViewModel
{
    public class PinListViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        public PinListViewModel(IPinService pinService)
        {
            _pinService = pinService;
        }

        #region -- Public Properties --

        private ICommand _addButtonCommand;
        public ICommand AddButtonTabCommand => _addButtonCommand ?? (_addButtonCommand = SingleExecutionCommand.FromFunc(OnAddButtonPinAsync));

        private ICommand _PinEditCommand;
        public ICommand PinEditCommand => _PinEditCommand ?? (_PinEditCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnPinEditAsync));

        private ICommand _PinDeleteCommand;
        public ICommand PinDeleteCommand => _PinDeleteCommand ?? (_PinDeleteCommand = SingleExecutionCommand.FromFunc<PinViewModel>(OnPinDeleteAsync));


        private ObservableCollection<PinViewModel> _observPinCollection;
        public ObservableCollection<PinViewModel> ObservPinCollection
        {
            get => _observPinCollection;
            set => SetProperty(ref _observPinCollection, value);
        }

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await InitAsync();
        }

        #endregion

        #region -- Private helpers --

        private async Task OnPinDeleteAsync(PinViewModel pin)
        {
            if (pin != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "Do you want to delete a pin?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.ConfirmAsync(confirmConfig);

                if (confirm)
                {

                    var result = await _pinService.DeletePinAsync(pin.ToPinModel());

                    if (result.IsSuccess)
                    {
                        ObservPinCollection.Remove(pin);

                        await NavigationService.NavigateAsync(nameof(PinListPage));
                    }
                    else
                    {
                        confirmConfig.Message = result.Message;

                        await UserDialogs.ConfirmAsync(confirmConfig);
                    }
                }
            }
        }

        private async Task OnPinEditAsync(PinViewModel pin)
        {
            var id = pin.Id;

            var parameter = new NavigationParameters();

            parameter.Add("pinId", id);

            await NavigationService.NavigateAsync(nameof(AddPinPage));

        }
        private async Task OnAddButtonPinAsync()
        {
            await NavigationService.NavigateAsync(nameof(AddPinPage));
        }

        private async Task InitAsync()
        {
            var userPins = await _pinService.GetPinsAsync();

            _observPinCollection = new ObservableCollection<PinViewModel>(userPins.Result.Select(x => x.ToPinViewModel()));

            //foreach (var pin in _observPinCollection)
            //{
            //    pin.DeleteCommand = PinDeleteCommand;

            //    pin.EditCommand = PinEditCommand;
            //}
        }

        #endregion
    }
}
