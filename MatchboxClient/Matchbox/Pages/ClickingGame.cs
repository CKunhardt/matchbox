using System;
using System.Threading.Tasks;

using Xamarin.Forms;

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
            clicksReq = 25;
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

            void refreshButt(Button refMainButt, int clickAmt)
            {
                // var newMainButt = new Button { };
                refMainButt.Text = clickAmt.ToString();

                int randomX = (new Random()).Next(0, 3);
                int randomY = (new Random()).Next(0, 3);

                if (randomX == 0)
                {
                    refMainButt.HorizontalOptions = LayoutOptions.StartAndExpand;
                }
                else if (randomX == 1)
                {
                    refMainButt.HorizontalOptions = LayoutOptions.CenterAndExpand;
                }
                else if (randomX == 2)
                {
                    refMainButt.HorizontalOptions = LayoutOptions.EndAndExpand;
                }
                else { refMainButt.HorizontalOptions = LayoutOptions.CenterAndExpand; }

                if (randomY == 0)
                {
                    refMainButt.VerticalOptions = LayoutOptions.StartAndExpand;
                }
                else if (randomY == 1)
                {
                    refMainButt.VerticalOptions = LayoutOptions.CenterAndExpand;
                }
                else if (randomY == 2)
                {
                    refMainButt.VerticalOptions = LayoutOptions.EndAndExpand;
                }
                else { refMainButt.VerticalOptions = LayoutOptions.CenterAndExpand; }
            }

            var mainButt = new Button
            {
                Text = "tap me",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            mainButt.Clicked += (sender, e) =>
            {
                timesClicked++;
                refreshButt(mainButt, clicksReq - timesClicked);
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
