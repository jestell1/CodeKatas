using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunging.DataModel
{
    class WeatherModel
    {
        public int Day { get; set; }

        public int MaxTemp { get; set; }

        public int MinTemp { get; set; }

        public int TempSpread { get { return CalculateMaxSpread(MaxTemp, MinTemp); } }

        public static int CalculateMaxSpread(int maxTemp, int minTemp)
        {
            return maxTemp - minTemp;
        }
    }
}
