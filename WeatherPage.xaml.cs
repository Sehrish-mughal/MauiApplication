namespace MauiApp1;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.ViewModel;
using Microsoft.Maui.Controls;

public partial class WeatherPage : ContentPage
{

    readonly string _apiKey = "a6500206369f3dbfa456e6a456c9c9a3";
    readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/weather";

    public WeatherPage()
    {
        InitializeComponent();
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

            WeatherResponse weather = await GetWeatherDataAsync(city);


            if (weather != null)
            {
                CityLabel.Text = $"City: {weather.Name}";
                TempLabel.Text = $"Temp: {weather.main.Temp}°C";
                HumidityLabel.Text = $"Humidity: {weather.main.Humidity}%";
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

public class Main
{
    public float Temp { get; set; }
    public float Humidity { get; set; }
}