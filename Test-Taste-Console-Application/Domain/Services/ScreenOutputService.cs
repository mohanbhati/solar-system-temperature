using System;
using System.Linq;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    public class ScreenOutputService : IOutputService
    {
        private readonly IPlanetService _planetService;
        private readonly IMoonService _moonService;

        public ScreenOutputService(IPlanetService planetService, IMoonService moonService)
        {
            _planetService = planetService;
            _moonService = moonService;
        }

        public void OutputAllPlanetsAndTheirMoonsToConsole()
        {
            //The service gets all the planets from the API.
            var planets = _planetService.GetAllPlanets().ToArray();

            //If the planets aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            //The column sizes and labels for the planets are configured here. 
            var columnSizesForPlanets = new[] { 20, 20, 30, 20 };
            var columnLabelsForPlanets = new[]
            {
                OutputString.PlanetNumber,
                OutputString.PlanetId,
                OutputString.PlanetSemiMajorAxis,
                OutputString.TotalMoons
            };

            //The column sizes and labels for the moons are configured here.
            var columnSizesForMoons = new[] { 20, 70 + 2 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber,
                OutputString.MoonId
            };

            for (int i = 0; i < planets.Length; i++)
            {
                int planetIndex = i + 1;

                ConsoleWriter.CreateLine(columnSizesForPlanets);
                ConsoleWriter.CreateText(
                    columnLabelsForPlanets,
                    columnSizesForPlanets
                );
                ConsoleWriter.CreateText(
                    new[]
                    {
                        planetIndex.ToString(),
                        CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id),
                        planets[i].SemiMajorAxis.ToString(),
                        planets[i].Moons.Count.ToString()
                    },
                    columnSizesForPlanets
                );
                ConsoleWriter.CreateLine(columnSizesForPlanets);

                ConsoleWriter.CreateText(
                    columnLabelsForMoons,
                    columnSizesForMoons
                );

                for (int j = 0; j < planets[i].Moons.Count; j++)
                {
                    int moonIndex = j + 1;
                    var moon = planets[i].Moons.ElementAt(j);

                    ConsoleWriter.CreateText(
                        new[]
                        {
                            moonIndex.ToString(),
                            CultureInfoUtility.TextInfo.ToTitleCase(moon.Id)
                        },
                        columnSizesForMoons
                    );
                }

                ConsoleWriter.CreateLine(columnSizesForMoons);
                ConsoleWriter.CreateEmptyLines(2);
            }
        }

        public void OutputAllMoonsAndTheirMassToConsole()
        {
            var moons = _moonService.GetAllMoons().ToArray();

            if (!moons.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            var columnSizes = new[] { 20, 20, 30, 20 };
            var columnLabels = new[]
            {
                OutputString.MoonNumber,
                OutputString.MoonId,
                OutputString.MoonMassExponent,
                OutputString.MoonMassValue
            };

            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            for (int i = 0; i < moons.Length; i++)
            {
                int index = i + 1;
                var moon = moons[i];

                ConsoleWriter.CreateText(
                    new[]
                    {
                        index.ToString(),
                        CultureInfoUtility.TextInfo.ToTitleCase(moon.Id),
                        moon.MassExponent.ToString(),
                        moon.MassValue.ToString()
                    },
                    columnSizes
                );
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);
        }

        public void OutputAllPlanetsAndTheirAverageMoonGravityToConsole()
        {
            var planets = _planetService.GetAllPlanets().ToArray();

            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            var columnSizes = new[] { 20, 30 };
            var columnLabels = new[]
            {
                OutputString.PlanetId,
                OutputString.PlanetMoonAverageGravity
            };

            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach (var planet in planets)
            {
                if (planet.HasMoons())
                {
                    ConsoleWriter.CreateText(
                        new[]
                        {
                            planet.Id,
                            planet.AverageMoonGravity.ToString()
                        },
                        columnSizes
                    );
                }
                else
                {
                    ConsoleWriter.CreateText(
                        new[] { planet.Id, "-" },
                        columnSizes
                    );
                }
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);
        }

        /// <inheritdoc />
        public void OutputAllPlanetsAndTheirAverageMoonTemperatureToConsole()
        {
            // Inform user of operation start
            Console.WriteLine("Loading planets and moon temperatures...");

            var planets = _planetService.GetAllPlanets().ToArray();

            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            var columnSizes = new[] { 20, 30 };
            var columnLabels = new[]
            {
                OutputString.PlanetId,
                OutputString.PlanetMoonAverageTemperature
            };

            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach (var planet in planets)
            {
                if (planet.HasMoons() && planet.AverageMoonTemperature.HasValue)
                {
                    ConsoleWriter.CreateText(
                        new[]
                        {
                            planet.Id,
                            planet.AverageMoonTemperature.Value.ToString("F2")
                        },
                        columnSizes
                    );
                }
                else if (planet.HasMoons())
                {
                    // Moons exist but no temperature data
                    ConsoleWriter.CreateText(
                        new[] { planet.Id, "N/A" },
                        columnSizes
                    );
                }
                else
                {
                    // No moons
                    ConsoleWriter.CreateText(
                        new[] { planet.Id, "-" },
                        columnSizes
                    );
                }
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);

            // Inform user of completion
            Console.WriteLine("Done.");
        }
    }
}
