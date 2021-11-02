using MapNodePad.View;
using MapNodePad.ViewModel;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNodePad
{
    public partial class App : PrismApplication
    {
        public App() { }


        #region ---Overrides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services


            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Login_Register, Login_RegisterViewModel>();
            containerRegistry.RegisterForNavigation<Create_Account, Create_AccountViewModel>();
            containerRegistry.RegisterForNavigation<Create_AccountPart2, Create_AccountPart2ViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<Pins, PinsAddViewModel>();
            containerRegistry.RegisterForNavigation<PinsAdd, PinsAddViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();

        }
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync($"/{nameof(Login_Register)}");
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
