using Acr.UserDialogs;
using MapNotepad.Services.Authentication;
using MapNotepad.Services.PinService;
using MapNotepad.Services.ProfileService;
using MapNotepad.Services.Registration;
using MapNotepad.Services.Repository;
using MapNotepad.Services.Services;
using MapNotepad.View;
using MapNotepad.ViewModel;
using MapNotepad.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad
{
    public partial class App : PrismApplication
    {
        public static T Resolve<T>() => Current.Container.Resolve<T>();

        public App(){}
        #region ---Overrides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(UserDialogs.Instance);

            //Services
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());

            // Navigation
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPage, RegistrationPageViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPagePartTwo, RegistrationPagePartTwoViewModel>();
            containerRegistry.RegisterForNavigation<PinListPage, PinListViewModel>();
            containerRegistry.RegisterForNavigation<MainProfilePage, MainProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPinPage, AddPinPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var authenticationService = Container.Resolve<IUserService>();

            if (authenticationService.UserId != 0)
            {
                await NavigationService.NavigateAsync($"/{nameof(MainProfilePage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(StartPage)}");
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion
    }
}
