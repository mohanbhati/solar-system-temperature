using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; 
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }

        public float AverageMoonGravity
        {
            get => 0.0f; 
        }

        public Planet(PlanetDto planetDto)
        {
            if (planetDto == null)
                throw new ArgumentNullException(nameof(planetDto));

            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();

            if (planetDto.Moons != null)
            {
                foreach (var moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
        }

        public bool HasMoons()
        {
            return Moons != null && Moons.Count > 0;
        }


        public double? AverageMoonTemperature
        {
            get
            {
                if (Moons == null || Moons.Count == 0)
                    return null;

                var temperatures = Moons
                    .Where(m => m.Temperature.HasValue)
                    .Select(m => m.Temperature.Value)
                    .ToList();

                if (!temperatures.Any())
                    return null;

                return temperatures.Average();
            }
        }
    }
}
