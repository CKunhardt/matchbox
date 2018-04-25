using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Xamarin.Forms;

using Matchbox.Abstractions;
using Matchbox.Models;

namespace Matchbox.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        Command findMatch;
        public Command FindMatch => findMatch ?? (findMatch = new Command(async () => await ExecuteFindMatch()));

        async Task ExecuteFindMatch()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new Matchbox.Pages.ClickingGame());
        }
        Command refreshCmd;
        public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                string userId = "temp";
                var table = App.CloudService.GetTable<User>();
                var list = await table.ReadItemAsync(userId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskList] Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
