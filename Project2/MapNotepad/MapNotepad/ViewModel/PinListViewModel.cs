using MapNotepad.Model.Pin;
using MapNotepad.Services.PinService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapNotepad.ViewModel
{
    public class PinListViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        public PinListViewModel(INavigationService navigationService,
                                IPinService pinService)
            : base(navigationService)
        {
            _pinService = pinService;
            InitAsync();
        }

        #region --- Command ----

        private ICommand _onAddButtonTap;
        //public ICommand OnAddButtonTap => _onAddButtonTap ?? (_onAddButtonTap = new Command(GoToAddPage);

        #endregion

        #region --- Public Properties ---

        private ObservableCollection<PinModel> _observPinCollection;
        public ObservableCollection<PinModel> ObservPinCollection
        {
            get => _observPinCollection;
            set => SetProperty(ref _observPinCollection, value);
        }

        #endregion

        #region --- Navigation ---

        private async Task GoToAddPage()
        {
            //await _navigationService.NavigateAsync(nameof(AddPinPage));
        }

        #endregion

        #region --- Private Helpers ---

        private async void InitAsync()
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
