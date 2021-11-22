using MapNotepad.Extensions;
using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Views;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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
        public ICommand AddButtonTapCommand => _addButtonCommand ?? (_addButtonCommand = SingleExecutionCommand.FromFunc(OnAddButtonPinAsync));


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

        private async Task OnAddButtonPinAsync()
        {
            await NavigationService.NavigateAsync(nameof(AddPinPage));
        }

        private async Task InitAsync()
        {
            var observUserPinCollection = new ObservableCollection<PinViewModel>();
            var userPins = await _pinService.GetPinsAsync();

            if (userPins.IsSuccess)
            {
                ObservPinCollection = new ObservableCollection<PinViewModel>(userPins.Result.Select(x => x.ToPinViewModel()));
            }
            foreach (var pin in ObservPinCollection)
            {
                pin.MoveToPinLocationCommand = SingleExecutionCommand.FromFunc<PinViewModel>(GoToPinLocation);
            }
        }

        #endregion

        #region -- Private helpers --
        private Task GoToPinLocation(PinViewModel pin)
        {
            LocatePin(new Position(pin.Latitude, pin.Longitude));

            return Task.CompletedTask;
        }

        private void LocatePin(Position position)
        {
            MessagingCenter.Send(this, "MovePin", position);
        }

        #endregion

    }
}
