using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Test_Taste_Console_Application.Domain.Objects;

namespace Test_Taste_Console_Application.Services
{
    public class FileOutputService
    {
        private const string OutputPath = "planet_moons_temperature.txt";

        public void WritePlanetsWithMoonsAndTemperatures(List<Planet> planets)
        {
            Console.WriteLine("💾 Writing output to file...");

            try
            {
                using (var writer = new StreamWriter(OutputPath, false))
                {
                    foreach (var planet in planets)
                    {
                        if (!planet.HasMoons())
                            continue;

                        var moonsWithTemp = planet.Moons.Where(static m => (bool)m.AvgTemp).ToList();

                        if (moonsWithTemp.Count>0)
                        {
                            writer.WriteLine($"Planet: {planet.Id} has moons but no temperature data available.\n");
                            continue;
                        }

                        var  avgTemp = planet.Moons.Where(static m => (bool)m.AvgTemp).ToList();
                        writer.WriteLine($"Planet: {planet.Id} | Average Moon Temperature: {avgTemp:F2} K");

                        foreach (var moon in moonsWithTemp)
                        {
                            writer.WriteLine($"    Moon: {moon.Id}, Temp: {moon.AvgTemp} K");
                        }

                        writer.WriteLine(); // blank line
                    }
                }

                Console.WriteLine($"✅ File written: {Path.GetFullPath(OutputPath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to write file: {ex.Message}");
            }
        }
    }
}
