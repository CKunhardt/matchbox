using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Matchbox.Abstractions;
using Matchbox.Utilities;

namespace Matchbox.ViewModels
{
    public class EntryPageViewModel : BaseViewModel
    {
        public EntryPageViewModel()
        {
            Title = "Task List";
        }

        Command loginWithGoogle;
        public Command LoginWithGoogle => loginWithGoogle ?? (loginWithGoogle = new Command(async () => await ExecuteLoginWithGoogle().ConfigureAwait(false)));

        async Task ExecuteLoginWithGoogle()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
                await cloudService.LoginAsync();
                Application.Current.MainPage = new NavigationPage(new Matchbox.Pages.UrhoPage());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Login] Error = {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoginWithUsername()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
                await cloudService.LoginAsync();
                Application.Current.MainPage = new NavigationPage(new Matchbox.Pages.MainPage());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Login] Error = {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}