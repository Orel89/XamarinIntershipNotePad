using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Prism;
using Prism.Ioc;
using MapNotepad.Services.PermissionsManager;
using MapNotepad.Droid.Services.PermissionsManager;
using ContextMenu.Droid;

namespace MapNotepad.Droid
{
    [Activity(Label = "MapNotepad", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static PermissionsService _permissionsService = new PermissionsService();
        #region -- Ovverides --
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitPackages(savedInstanceState);
          
            LoadApplication(new App(new AndroidInitializer()));


            //// TROUBLE: Crash on first launch  after confirmation of permissions: Java.Lang.SecurityException: 'my location requires permission ACCESS_FINE_LOCATION or ACCESS_COARSE_LOCATION'
            //if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
            //{
            //    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine("Permission Granted!!!");
            //}
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion


        #region -- Privet helpers --

        private void InitPackages(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);
        }
        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance<IPermissionsService>(_permissionsService);
            }
        }

        #endregion
    }
}