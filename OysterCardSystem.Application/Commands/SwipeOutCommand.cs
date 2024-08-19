using System.Collections.Generic;
using System.Threading.Tasks;
using OysterCardSystem.Domain;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Interfaces;
using OysterCardSystem.Domain.Services;

namespace OysterCardSystem.Application.Commands
{
    public class SwipeOutCommand
    {
        private readonly ICardRepository _cardRepository;
        private readonly Dictionary<string, Station> _stations;
        private readonly FareCalculator _fareCalculator;

        public SwipeOutCommand(ICardRepository cardRepository, Dictionary<string, Station> stations, FareCalculator fareCalculator)
        {
            _cardRepository = cardRepository;
            _stations = stations;
            _fareCalculator = fareCalculator;
        }

        public async Task ExecuteAsync(int cardId, string exitStationName)
        {
            // Retrieve the card by its ID
            var card = await _cardRepository.GetCardByIdAsync(cardId);

            // If the card doesn't exist or there is no inward journey, exit the method
            if (card == null || card.InwardStation == null)
            {
                return;
            }

            // Retrieve the exit station from the station dictionary
            if (!_stations.TryGetValue(exitStationName, out var exitStation))
            {
                // If the exit station is not found, exit the method
                Console.WriteLine($"Exit station '{exitStationName}' not found.");
                return;
            }

            // Calculate the fare based on the journey type and zones
            var fare = _fareCalculator.CalculateFare(
                card.InwardJourneyType.Value,
                card.InwardStation.Zones,
                exitStation.Zones
            );

            // Determine the fare for the current journey
            var journeyFare = card.InwardJourneyType == JourneyType.Bus
                ? _fareCalculator.BusFare
                : fare;

            // Update the card's balance: add the fare difference
            card.Balance += (card.InwardJourneyType == JourneyType.Bus ? _fareCalculator.BusFare : 3m) - fare;

            // Reset the inward station and journey type on the card
            card.InwardStation = null;
            card.InwardJourneyType = null;

            // Save the updated card information back to the repository
            await _cardRepository.UpdateCardAsync(card);

            // Output the result for debugging or user feedback
            Console.WriteLine($"Swiped out at {exitStationName}. Fare charged: £{fare}. Updated Balance: £{card.Balance}");
        }

    }
}
