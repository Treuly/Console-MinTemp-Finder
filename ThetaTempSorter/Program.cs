using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThetaTempSorter

{

    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            CompareTemperatures();
        }

        //read from text file within folder
        public List<string> ReadWeatherText()
        { 
            string weatherFile = "weather.txt";

            List<string> lines = new List<string>();
            
            try
            {
                StreamReader sr = new StreamReader(weatherFile);

                for(var i = 0; i < 8; i++)
                {
                    sr.ReadLine();
                }

                for (var i = 0; i < 30; i++)
                {
                    string line = sr.ReadLine();
                    lines.Add(line);
                }


                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return lines;
           

        }

        public List<TemperatureInfo> CreateTempDays()
        {
            List<string> lines = ReadWeatherText();
            List<TemperatureInfo> weatherDays = new List<TemperatureInfo>();

            foreach (String line in lines)
            {
                //get specific characters from each line from file, convert from string to int
                int day = Convert.ToInt32(line.Substring(2, 2));
                int min = Convert.ToInt32(line.Substring(12, 2));
                int max = Convert.ToInt32(line.Substring(6, 2));

                TemperatureInfo dayTemp = new TemperatureInfo(day, min, max);
                
                weatherDays.Add(dayTemp);

            }

            return weatherDays;
        }

        public void CompareTemperatures()
        {
            List <TemperatureInfo> weatherDays = CreateTempDays();
            int lowest = weatherDays.Min(day => day.Difference);
            
            foreach (TemperatureInfo t in weatherDays)
            {
                if (t.Difference == lowest)
                {
                    Console.WriteLine("Day with the lowest difference in temperature for this month: "
                        + t.Day);
                    Console.WriteLine("Minimum Difference: " + t.Difference);
                    Console.WriteLine("Max Temperature: " + t.MaxTemp + " // Min Temperature: " + t.MinTemp);
                }
            }

        }


        
    }

    //object that holds each day, minimum temperature, and maximum temperature
    class TemperatureInfo
    {
      

        public TemperatureInfo(int day, int minTemp, int maxTemp)
        {
            Day = day;
            MinTemp = minTemp;
            MaxTemp = maxTemp;
            Difference = maxTemp - minTemp;
        }

        public int Difference { get; }


        public int Day { get; set; }
       

        public int MinTemp { get; set; }
       

        public int MaxTemp { get; set; }
        

    }
}