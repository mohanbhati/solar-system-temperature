namespace Test_Taste_Console_Application.Domain.Services.Interfaces
{
  
    /// <see href="https://api.le-systeme-solaire.net/"/> to a user via the console. 
   
    public interface IOutputService
    {
        void OutputAllPlanetsAndTheirMoonsToConsole();
        void OutputAllMoonsAndTheirMassToConsole();
        void OutputAllPlanetsAndTheirAverageMoonGravityToConsole();

        /// <summary>
        /// Outputs each planet that has at least one moon, along with the average temperature of its moons.
        /// </summary>
        void OutputAllPlanetsAndTheirAverageMoonTemperatureToConsole();
    }
}
