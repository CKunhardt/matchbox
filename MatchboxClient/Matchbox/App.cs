using System;

using Xamarin.Forms;

using Matchbox.Abstractions;
using Matchbox.Services;
using Matchbox.Pages;

namespace Matchbox
{
	public class App : Application
	{
        public static ICloudService CloudService { get; set; }
        
		public App ()
		{
            CloudService = new AzureCloudService();

			// The root page of your application
			MainPage = new EntryPage();
		}

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

