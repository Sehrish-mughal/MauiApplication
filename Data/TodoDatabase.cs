using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace YourAppNamespace.Services
{
    public class TodoDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TodoDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<WeatherData>().Wait(); // Ensure table is created
        }

        // CRUD operations
        public Task<List<WeatherData>> GetWeatherDatasAsync()
        {
            return _database.Table<WeatherData>().ToListAsync();
        }

        public Task<WeatherData> GetWeatherDataAsync(int id)
        {
            return _database.Table<WeatherData>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveWeatherDataAsync(WeatherData item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> DeleteWeatherDataAsync(WeatherData item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
