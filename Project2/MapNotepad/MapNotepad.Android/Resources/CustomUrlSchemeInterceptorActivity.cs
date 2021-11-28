using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapNotepad.Droid.Resources
{
	[Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
	[IntentFilter(
				 new[] { Intent.ActionView },
				 Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
				 DataSchemes = new[] { "com.googleusercontent.apps.297764194337-8jvsn8oanj4gomgslv8tiuo6arg79qqp" },
				 DataPath = "/oauth2redirect")]
	public class CustomUrlSchemeInterceptorActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Convert Android.Net.Url to Uri
			var uri = new Uri(Intent.Data.ToString());

			// Load redirectUrl page
			Helpers.AuthHelpers.AuthenticationState.Authenticator.OnPageLoading(uri);

			Finish();
		}
	}
}