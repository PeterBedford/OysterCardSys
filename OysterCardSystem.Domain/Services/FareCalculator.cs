using System.Collections.Generic;
using System.Linq;

namespace OysterCardSystem.Domain.Services
{
    // This class is responsible for calculating fares for different types of journeys (Bus or Tube).
    public class FareCalculator
    {
        // Dictionary to store fare rates for specific zone combinations on the Tube.
        // The key is a string representing a combination of zones, and the value is the fare for that combination.
        private readonly Dictionary<string, decimal> _tubeFareRates = new Dictionary<string, decimal>
        {
            { "1", 2m },     // Fare for traveling within Zone 1
            { "2", 2m },     // Fare for traveling within Zone 2
            { "1,2", 3m },   // Fare for traveling between Zone 1 and Zone 2
            { "2,3", 2m }    // Fare for traveling between Zone 2 and Zone 3
        };

        // The fixed fare for a bus journey.
        public decimal BusFare { get; } = 2.50m;

        // Method to calculate the fare based on the journey type and zones traveled.
        // - For bus journeys, it returns a fixed bus fare.
        // - For tube journeys, it calculates the fare based on the zones traveled.
        public decimal CalculateFare(JourneyType journeyType, List<int> entryZones, List<int> exitZones)
        {
            // If the journey is by bus, return the fixed bus fare.
            if (journeyType == JourneyType.Bus)
            {
                return BusFare;
            }

            // Combine entry and exit zones into a single list of all zones involved in the journey.
            var allZones = entryZones
                .Union(exitZones)      // Combine both lists, removing duplicates.
                .Distinct()            // Ensure that each zone is only included once.
                .OrderBy(z => z)       // Sort the zones in ascending order.
                .ToList();

            // Create a key by joining the sorted zones with commas (e.g., "1,2").
            var zonesKey = string.Join(",", allZones);

            // Look up the fare in the dictionary using the zones key.
            // If the key exists, return the corresponding fare; otherwise, return 0 (indicating an unknown route).
            return _tubeFareRates.TryGetValue(zonesKey, out var fare) ? fare : 0m;
        }
    }
}
