using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Auth;

using Matchbox.Abstractions;
using Matchbox.iOS.Services;
using Matchbox.Utilities;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace Matchbox.iOS.Services
{
    public class iOSLoginProvider : ILoginProvider
    {
        public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;

        public AccountStore AccountStore { get; private set; }

        public iOSLoginProvider()
        {
            AccountStore = AccountStore.Create();
        }

        public MobileServiceUser RetrieveTokenFromSecureStore()
        {
            var accounts = AccountStore.FindAccountsForService("matchbox");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    string token;

                    if (acct.Properties.TryGetValue("token", out token))
                    {
                        return new MobileServiceUser(acct.Username)
                        {
                            MobileServiceAuthenticationToken = token
                        };
                    }
                }
            }
            return null;
        }

        public void StoreTokenInSecureStore(MobileServiceUser user)
        {
            var account = new Account(user.UserId);
            account.Properties.Add("token", user.MobileServiceAuthenticationToken);
            AccountStore.Save(account, "matchbox");
        }

        public void RemoveTokenFromSecureStore()
        {
            var accounts = AccountStore.FindAccountsForService("matchbox");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    AccountStore.Delete(acct, "matchbox");
                }
            }
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client)
        {
            return await client.LoginAsync(RootView, MobileServiceAuthenticationProvider.Google, "matchbox");
        }

    }
}