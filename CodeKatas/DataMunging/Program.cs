using DataMunging.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataMunging
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteWeatherToScreen(GetWeatherWithHighestSpread(ReadFile()));
            Console.ReadLine();
        }

        static List<WeatherModel> ReadFile()
        {
            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            string dataFileLocation = Path.Combine(executableLocation, "weather.dat");

            var weatherModels = new List<WeatherModel>();

            using (StreamReader file = new StreamReader(dataFileLocation))
            {
                string line;
                int counter = 0;
                while ((line = file.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && counter != 0 && counter != 32)
                    {
                        weatherModels.Add(new WeatherModel
                        {
                            Day = ConvertBadStringToInt(line.Substring(0, 4)),
                            MaxTemp = ConvertBadStringToInt(line.Substring(4, 6)),
                            MinTemp = ConvertBadStringToInt(line.Substring(10, 6))
                        });
                    }

                    counter++;
                }
            }

            return weatherModels;
        }

        static WeatherModel GetWeatherWithHighestSpread(List<WeatherModel> weatherModels)
        {
            return weatherModels.OrderByDescending(x => x.TempSpread).FirstOrDefault();
        }

        static void WriteWeatherToScreen(WeatherModel weatherModel)
        {
            Console.WriteLine($"Largest Spread Day {weatherModel.Day.ToString()}, Spread {weatherModel.TempSpread.ToString()}");
        }

        static int ConvertBadStringToInt(string tempString)
        {
            int temp;
            if (!int.TryParse(tempString.Trim(), out temp))
            {
                if (!int.TryParse(tempString.Replace("*", "").Trim(), out temp))
                    throw new ArgumentException("I Don't even");
            }

            return temp;
        }
    }
}
