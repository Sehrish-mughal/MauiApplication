﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Main
    {
        public float Temp { get; set; }
        public float Humidity { get; set; }
    }
    public class WeatherResponse
    {
        public Main main { get; set; }
        public string Name { get; set; }
    }
}
