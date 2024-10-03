using MauiApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Data
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public AppDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<WeatherData>().Wait();
        }

        public Task<int> AddWeatherDataAsync(WeatherData weatherData)
        {
            return _database.InsertAsync(weatherData);
        }

        public Task<WeatherData> GetWeatherDataAsync(string city)
        {
            return _database.Table<WeatherData>().FirstAsync(x=> x.City == city);
        }


    }


}
