using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using Urho;
using Urho.Forms;

namespace Matchbox.Pages
{
    public class MatchBoxesPage : ContentPage
    {
        UrhoSurface urhoSurface;
        MatchBoxes urhoApp;
        Label turnInfo;
        int currentPlayer;

        public MatchBoxesPage()
        {
            urhoSurface = new UrhoSurface();
            urhoSurface.VerticalOptions = LayoutOptions.FillAndExpand;

            currentPlayer = 1;

            turnInfo = new Label { Text = "Player " + currentPlayer + "\'s Turn" };

            Title = "MatchBoxes";
            Content = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 40),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    urhoSurface,
                    turnInfo
                }
            };
        }

        void ChangeTurns()
        {
            currentPlayer = currentPlayer == 1 ? 2 : 1;
            turnInfo.Text = "Player " + currentPlayer + "\'s Turn";
        }

        protected override void OnDisappearing()
        {
            UrhoSurface.OnDestroy();
            base.OnDisappearing();
        }

        protected override async void OnAppearing()
        {
            await StartUrhoApp();
        }

        async Task StartUrhoApp()
        {
            urhoApp = await urhoSurface.Show<MatchBoxes>(new ApplicationOptions(assetsFolder: null) { Orientation = ApplicationOptions.OrientationType.Portrait });
        }
    }
}
