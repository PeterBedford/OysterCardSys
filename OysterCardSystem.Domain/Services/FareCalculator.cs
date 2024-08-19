using System.Collections.Generic;
using System.Linq;

namespace OysterCardSystem.Domain.Services
{
    public class FareCalculator
    {
        private readonly Dictionary<string, decimal> _tubeFareRates = new Dictionary<string, decimal>
        {
            { "1", 2m },
            { "2", 2m },
            { "1,2", 3m },
            { "2,3", 2m }
        };

        public decimal BusFare { get; } = 2.50m;

        public decimal CalculateFare(JourneyType journeyType, List<int> entryZones, List<int> exitZones)
        {
            if (journeyType == JourneyType.Bus)
            {
                return BusFare;
            }

            var allZones = entryZones
                .Union(exitZones)
                .Distinct()
                .OrderBy(z => z)
                .ToList();
            var zonesKey = string.Join(",", allZones);

            return _tubeFareRates.TryGetValue(zonesKey, out var fare) ? fare : 0m;
        }
    }
}
