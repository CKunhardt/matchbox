using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using Urho;
using Urho.Forms;

using Matchbox.Components;

namespace Matchbox.Pages
{
    public class ClickingGame : ContentPage
    {
        StackLayout stack;
        int timesClicked;
        int clicksReq;
        long userScore; // the score that will be passed to the server to be compared to the opponent's score
        long opponentScore;
        Boolean firstClick;
        System.Diagnostics.Stopwatch timer;

        public ClickingGame()
        {
            userScore = 0;
            opponentScore = 10000;
            timesClicked = 0;
            clicksReq = 50;
            firstClick = false;
            timer = new System.Diagnostics.Stopwatch();

            stack = new StackLayout();
            stack.VerticalOptions = LayoutOptions.FillAndExpand;

            var winLoss = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(EntryCell)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var returnButt = new Button
            {
                Text = "Return to Main Menu",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            returnButt.Clicked += async (sender, e) =>
            {
                await Navigation.PopModalAsync();
            };

            var mainButt = new Button
            {
                Text = "tap me",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            mainButt.Clicked += async (sender, e) =>
            {
                timesClicked++;
                mainButt.Text = (clicksReq - timesClicked).ToString();
                if (timesClicked==clicksReq)
                {
                    timer.Stop();
                    userScore = timer.ElapsedMilliseconds;
                    stack.Children.Remove(mainButt);
                    if (userScore <= opponentScore) { winLoss.Text = "YOU WON IN " + userScore.ToString() + " MILLISECONDS"; }
                    else { winLoss.Text = "YOUR OPPONENT BEAT YOU BY " + (userScore - opponentScore).ToString() + " MILLISECONDS"; }
                    stack.Children.Add(winLoss);
                    stack.Children.Add(returnButt);
                }
                if (firstClick == false)
                {
                    timer.Start();
                    firstClick = true;
                }
            };

            stack.Children.Add(mainButt);

            Title = "Clicking Game";
            Content = stack;
        }
    }
}
