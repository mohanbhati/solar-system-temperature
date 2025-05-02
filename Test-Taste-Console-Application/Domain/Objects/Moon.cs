using System;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    /// <summary>
    /// Domain representation of a moon, with mass and temperature details.
    /// </summary>
    public class Moon
    {
        /// <summary>
        /// Unique identifier for the moon.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Mass value component (coefficient) of the moon, in scientific notation.
        /// </summary>
        public float MassValue { get; set; }

        /// <summary>
        /// Mass exponent component of the moon, in scientific notation.
        /// </summary>
        public float MassExponent { get; set; }

        /// <summary>
        /// Average surface temperature of the moon, in Kelvin (nullable if data not provided).
        /// </summary>
        public double? Temperature { get; set; }
        public object AvgTemp { get; internal set; }

        /// <summary>
        /// Constructs a Moon domain object from its DTO.
        /// </summary>
        public Moon(MoonDto moonDto)
        {
            if (moonDto == null)
                throw new ArgumentNullException(nameof(moonDto));

            Id = moonDto.Id;
            MassValue = moonDto.MassValue;
            MassExponent = moonDto.MassExponent;

            // Map API's average temperature into our domain model
            Temperature = moonDto.AvgTemp;
        }
    }
}
