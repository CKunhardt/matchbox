using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using Microsoft.WindowsAzure.MobileServices;

using Matchbox.Abstractions;

namespace Matchbox.iOS.Services
{
    public class iOSLoginProvider : ILoginProvider
    {
        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync(RootView, MobileServiceAuthenticationProvider.Google, "matchbox");
        }

        public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;
    }
}