using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void OnNavigateToWeatherPageClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new WeatherPage());
        }
    }

}
