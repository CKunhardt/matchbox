using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Matchbox.Abstractions;

namespace Matchbox.Droid.Services
{
    public class DroidLoginProvider : ILoginProvider
    {
        Context context;

        public void Init(Context context)
        {
            this.context = context;
        }

        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync(context, MobileServiceAuthenticationProvider.Google, "matchbox");
        }
    }
}