using MapNotepad.Helpers;
using MapNotepad.Helpers.Validation;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.ProfileService;
using MapNotepad.Services.Registration;
using MapNotepad.View;
using Prism.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModel
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public RegistrationPageViewModel(IUserService userService) 
        {
            _userService = userService;
        }

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

        private ICommand _toNextPageButtonTapCommand;
        public ICommand ToNextPageButtonTapCommand => _toNextPageButtonTapCommand ?? (_toNextPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapNextPageAsync, () => false));

        private ICommand _goToBackPageButtonTapCommand;
        public ICommand GoToBackPageButtonTapCommand => _goToBackPageButtonTapCommand ?? (_goToBackPageButtonTapCommand = SingleExecutionCommand.FromFunc(OnButtonTapGoToBackPage));

        private ICommand clearEntryNameButtonTapCommand;
        public ICommand ClearEntryNameButtonTapCommand => clearEntryNameButtonTapCommand ?? (clearEntryNameButtonTapCommand = new Command(OnClearEntryButtonTapCommand));

        private ICommand clearEntryEmailButtonTapCommand;
        public ICommand ClearEntryEmailButtonTapCommand => clearEntryEmailButtonTapCommand ?? (clearEntryEmailButtonTapCommand = new Command(OnClearEntryEmailButtonTapCommand));

        #endregion

        #region -- Overrides --

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

        #region -- Private helpers --

        private async Task OnButtonTapGoToBackPage()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task OnButtonTapNextPageAsync()
        {
            var message = "Email is mismatch";

            if (Validator.IsEmailMatched(Email))
            {
                var IsEmailExists = await _userService.CheckEmailExists(Email);

                if (!IsEmailExists.IsSuccess)
                {
                    var navigationParameters = new NavigationParameters
                    {
                        { nameof(Name), Name },
                        { nameof(Email), Email }
                    };

                    await NavigationService.NavigateAsync(nameof(RegistrationPagePartTwo), navigationParameters);
                }
                else
                {
                    message = "Email is already in database";
                    await UserDialogs.AlertAsync(message);
                }
            }
            else
            {
                message = "Email is not valid";
                await UserDialogs.AlertAsync(message);
            }
          
        }
        
        private void OnClearEntryButtonTapCommand()
        {
            Name = null;
        }

        private void OnClearEntryEmailButtonTapCommand()
        {
            Email = null;
        }

        #endregion
    }
}
