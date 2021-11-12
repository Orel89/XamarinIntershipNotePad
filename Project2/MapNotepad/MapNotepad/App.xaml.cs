using MapNotepad.Services.Authentication;
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
        public App()
        {

        }
        #region ---Overrides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettings>(Container.Resolve<Settings>());
            containerRegistry.RegisterInstance<IRegistration>(Container.Resolve<Registration>());

            // Navigation
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MainProfilePage, MainProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPage, RegistrationPageViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPagePartTwo, RegistrationPagePartTwoViewModel>();

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //MainPage = new MainProfilePage();

            await NavigationService.NavigateAsync($"/{nameof(MainProfilePage)}");
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
