using MapNotepad.Helpers;
using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using MapNotepad.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
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
        public ICommand AddButtonTapCommand => _addButtonCommand ?? (_addButtonCommand = SingleExecutionCommand.FromFunc(OnAddButtonPinAsync));


        private ObservableCollection<PinModel> _observPinCollection;
        public ObservableCollection<PinModel> ObservPinCollection
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
            var observUserPinCollection = new ObservableCollection<PinModel>();
            var userPins = await _pinService.GetPinsAsync();

            foreach (var p in userPins.Result)
            {
                observUserPinCollection.Add(p);
            }

            ObservPinCollection = observUserPinCollection;

        }

        #endregion
    }
}
