using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Matchbox.Droid.Services;
using Matchbox.Abstractions;

namespace Matchbox.Droid
{
	[Activity (Label = "Matchbox.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : FormsApplicationActivity
	{
        // Define a authenticated user.
        private MobileServiceUser user;

        protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate (bundle);

			// Initialize Azure Mobile Apps
			CurrentPlatform.Init();

            // Initialize Xamarin Forms
            Forms.Init (this, bundle);

            var loginProvider = (DroidLoginProvider)DependencyService.Get<ILoginProvider>();
            loginProvider.Init(this);

            // Load the main application
            LoadApplication (new App ());
		}

    }
}

