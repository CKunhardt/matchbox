using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;

namespace Matchbox.Abstractions
{
    public interface ILoginProvider
    {
        MobileServiceUser RetrieveTokenFromSecureStore();

        void StoreTokenInSecureStore(MobileServiceUser user);

        void RemoveTokenFromSecureStore();

        Task<MobileServiceUser> LoginAsync(MobileServiceClient client);
    }
}
