namespace MauiApp1;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.ViewModel;
using Microsoft.Maui.Controls;
using YourAppNamespace.Services;

public partial class WeatherPage : ContentPage
{

    readonly string _apiKey = "a6500206369f3dbfa456e6a456c9c9a3";
    readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/weather";
    public static TodoDatabase _database { get; private set; }


    public WeatherPage()
    {
        InitializeComponent();
        BindingContext = new WeatherViewModel();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WeatherData1.db");
        _database = new TodoDatabase(dbPath);
    }

    public async Task<WeatherResponse> GetWeatherDataAsync(string city)
    {
        string requestUrl = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric";
        

        try
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return weatherResponse;
            }
            else
            {

                return null;
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }


    private async void OnGetWeatherClicked(object sender, EventArgs e)
    {

        string city = CityEntry.Text;

        if (!string.IsNullOrEmpty(city))
        {
            var dbAllWeather = await _database.GetWeatherDatasAsync();
            var dbweather = dbAllWeather.FirstOrDefault(x => x.City.Equals(city, StringComparison.CurrentCultureIgnoreCase));


            if (dbweather != null)
            {
                CityLabel.Text = "city:" + dbweather.City;
                TempLabel.Text = "temp:" + dbweather.Temp.ToString();
                HumidityLabel.Text = "humidity:" + dbweather.Humidity.ToString();
                return;
            }


            WeatherResponse weather = await GetWeatherDataAsync(city);


            if (weather != null)
            {
                CityLabel.Text = $"City: {weather.Name}";
                TempLabel.Text = $"Temp: {weather.main.Temp}°C";
                HumidityLabel.Text = $"Humidity: {weather.main.Humidity}%";

                var weatherData = new WeatherData
                {
                    Id = dbAllWeather.Count + 1,
                    City = weather.Name,
                    Temp = weather.main.Temp,
                    Humidity = weather.main.Humidity
                };

                // Save weather data to SQLite
                await App.Database.AddWeatherDataAsync(weatherData);
                await _database.SaveWeatherDataAsync(weatherData);

            }
            else
            {
                CityLabel.Text = "City: Not found";
                TempLabel.Text = "Temp: --";
                HumidityLabel.Text = "Humidity: --";
            }
        }
        else
        {

            CityLabel.Text = "City: Please enter a city";
            TempLabel.Text = "Temp: --";
            HumidityLabel.Text = "Humidity: --";
        }
    }


}

