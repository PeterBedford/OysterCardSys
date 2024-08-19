using System.Collections.Generic;

namespace OysterCardSystem.Domain.Entities
{
    public class Station
    {
        public string Name { get; set; }          // Name of the station
        public List<int> Zones { get; set; }      // Zones served by this station
    }
}
