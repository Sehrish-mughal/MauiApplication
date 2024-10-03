using Microsoft.Maui.Controls;
using System.IO;
using MauiApp1.Data;

namespace MauiApp1
{
    public partial class App : Application
    {
        public static AppDatabase Database { get; private set; }
        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WeatherData.db");
            Database = new AppDatabase(dbPath);

            MainPage = new AppShell();
        }
    }
}
