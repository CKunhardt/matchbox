using System;

using Xamarin.Forms;

using Urho;
using Urho.Forms;

using Matchbox.Components;

namespace Matchbox.Pages
{
    public class UrhoPage : ContentPage
    {
        Slider selectedBarSlider, rotationSlider;
        UrhoSurface urhoSurface;
        MatchboxGame urhoApp;

        public UrhoPage()
        {
            var restartBtn = new Button { Text = "Restart" };
            restartBtn.Clicked += (sender, e) => StartUrhoApp();

            urhoSurface = new UrhoSurface();
            urhoSurface.VerticalOptions = LayoutOptions.FillAndExpand;

            rotationSlider = new Slider(0, 500, 250);
            rotationSlider.ValueChanged += (s, e) => urhoApp?.Rotate((float)(e.NewValue - e.OldValue));

            selectedBarSlider = new Slider(0, 5, 2.5);
            selectedBarSlider.ValueChanged += OnValuesSliderValueChanged;

            Title = " UrhoSharp + Xamarin.Forms";
            Content = new StackLayout
            {
                Padding = new Thickness(12, 12, 12, 40),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    urhoSurface,
                    restartBtn,
                    new Label { Text = "ROTATION: "},
                    rotationSlider,
                    new Label { Text = "SELECTED VALUE:" },
                    selectedBarSlider,
                }
            };
        }

        protected override void OnDisappearing()
        {
            UrhoSurface.OnDestroy();
            base.OnDisappearing();
        }

        void OnValuesSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (urhoApp?.SelectedBar != null)
                urhoApp.SelectedBar.Value = (float)e.NewValue;
        }

        private void OnBarSelection(MBBar bar)
        {
            //reset value
            selectedBarSlider.ValueChanged -= OnValuesSliderValueChanged;
            selectedBarSlider.Value = bar.Value;
            selectedBarSlider.ValueChanged += OnValuesSliderValueChanged;
        }

        protected override async void OnAppearing()
        {
            StartUrhoApp();
        }

        async void StartUrhoApp()
        {
            urhoApp = await urhoSurface.Show<MatchboxGame>(new ApplicationOptions(assetsFolder: null) { Orientation = ApplicationOptions.OrientationType.LandscapeAndPortrait });
        }
    }
}
