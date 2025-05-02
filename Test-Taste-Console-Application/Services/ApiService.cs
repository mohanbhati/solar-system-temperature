using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Test_Taste_Console_Application.Domain.DataTransferObjects;
using Test_Taste_Console_Application.Domain.DataTransferObjects.JsonObjects;
using Test_Taste_Console_Application.Domain.Objects;

namespace Test_Taste_Console_Application.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.le-systeme-solaire.net/rest/")
            };
        }

        public async Task<List<Planet>> GetPlanetsWithMoonsAsync()
        {
            Console.WriteLine("🌍 Fetching planet data from API...");

            try
            {
                var response = await _httpClient.GetAsync("bodies/");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var bodyWrapper = JsonSerializer.Deserialize<BodyWrapper>(json);

                var planetDtos = bodyWrapper.Bodies
                    .FindAll(b => b.IsPlanet && b.Moons != null && b.Moons.Count > 0);

                var planets = new List<Planet>();

                foreach (var dto in planetDtos)
                {
                    // Fetch moon data one by one to get avgTemp
                    if (dto.Moons != null)
                    {
                        foreach (var moon in dto.Moons)
                        {
                            var moonDetail = await GetMoonDetailsAsync(moon.URLId);
                            moon.AvgTemp = moonDetail?.AvgTemp;
                        }
                    }

                    planets.Add(new Planet(dto));
                }

                return planets;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error fetching data: {ex.Message}");
                return new List<Planet>();
            }
        }

        private async Task<MoonDto?> GetMoonDetailsAsync(string moonId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"bodies/{moonId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MoonDto>(json);
            }
            catch
            {
                return null;
            }
        }

        // Wrapper class to read list of bodies from the API
        private class BodyWrapper
        {
            public List<PlanetDto> Bodies { get; set; }
        }
    }
}
