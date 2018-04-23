using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Matchbox.Pages
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var title = new Label
            {
                Text = "Welcome to Matchbox",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(EntryCell)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var findMatch = new Button
            {
                Text = "Find Match",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            findMatch.Clicked += async (sender, e) =>
            {
                await Navigation.PushModalAsync(new Matchbox.Pages.ClickingGame());
            }; 

            Content = new StackLayout
            {
                Children = {
                    title,
                    findMatch
                }
            };
        }
    }
}