using System;
using System.Collections.Generic;
using System.Linq;
using Test_Taste_Console_Application.Domain.Objects;

namespace Test_Taste_Console_Application.Services
{
    public class ConsoleOutputService
    {
        public void PrintPlanetsWithMoonsAndTemperatures(List<Planet> planets)
        {
            Console.WriteLine("🌗 Displaying planets with moons and their average moon temperature...\n");

            foreach (var planet in planets)
            {
                if (!planet.HasMoons())
                    continue;

                var moonsWithTemp = planet.Moons.Where(static m => (bool)m.AvgTemp).ToList();

                if (moonsWithTemp.Count == 0)
                {
                    Console.WriteLine($"🔸 {planet.Id} has moons but no temperature data available.");
                    continue;
                }

                var avgTemp = planet.Moons.Where(static m => (bool)m.AvgTemp).ToList();
                Console.WriteLine($"🔹 Planet: {planet.Id} | Average Moon Temperature: {avgTemp:F2} K");

                foreach (var moon in moonsWithTemp)
                {
                    Console.WriteLine($"    🌙 Moon: {moon.Id}, Temp: {moon.AvgTemp} K");
                }

                Console.WriteLine(); // blank line between planets
            }
        }
    }
}
