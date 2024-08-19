using System.Collections.Generic;
using System.Threading.Tasks;
using OysterCardSystem.Domain;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Interfaces;
using OysterCardSystem.Domain.Services;

namespace OysterCardSystem.Application.Commands
{
    // Command class responsible for handling the swipe-out process at the end of a journey.
    public class SwipeOutCommand
    {
        // Dependency on the card repository to access and update card data.
        private readonly ICardRepository _cardRepository;

        // Dictionary for looking up Station objects by their names.
        private readonly Dictionary<string, Station> _stations;

        // Service for calculating fares based on journey types and zones.
        private readonly FareCalculator _fareCalculator;

        // Constructor to initialize dependencies.
        // - Parameters:
        //   - cardRepository: Provides methods for retrieving and updating card data.
        //   - stations: A dictionary for looking up Station objects by their names.
        //   - fareCalculator: A service for calculating fares based on journey type and zones.
        public SwipeOutCommand(ICardRepository cardRepository, Dictionary<string, Station> stations, FareCalculator fareCalculator)
        {
            _cardRepository = cardRepository;
            _stations = stations;
            _fareCalculator = fareCalculator;
        }

        // Handles the swipe-out action for a card at a specified exit station.
        // - Parameters:
        //   - cardId: The unique identifier of the card being used.
        //   - exitStationName: The name of the station where the card is swiped out.
        // - Returns:
        //   - A Task representing the asynchronous operation.
        public async Task SwipeOutAsync(int cardId, string exitStationName)
        {
            // Retrieve the card details using the card's ID.
            var card = await _cardRepository.GetCardByIdAsync(cardId);

            // If the card is not found or there is no record of an inward journey, exit the method.
            if (card == null || card.InwardStation == null)
            {
                // Optionally, log an error or notify the user (e.g., through an exception or UI message).
                return;
            }

            // Retrieve the exit station details using the provided station name.
            if (!_stations.TryGetValue(exitStationName, out var exitStation))
            {
                // If the exit station is not found, log the error or notify the user.
                Console.WriteLine($"Exit station '{exitStationName}' not found.");
                return;
            }

            // Calculate the fare for the journey based on the journey type and the zones of both the inward and exit stations.
            var fare = _fareCalculator.CalculateFare(
                card.InwardJourneyType.Value,  // Journey type (e.g., Bus or Tube)
                card.InwardStation.Zones,      // Zones from the inward station
                exitStation.Zones              // Zones from the exit station
            );

            // Determine the fare for the current journey based on the journey type.
            var journeyFare = card.InwardJourneyType == JourneyType.Bus
                ? _fareCalculator.BusFare    // Fixed fare for Bus journeys
                : fare;                      // Calculated fare for Tube journeys

            // Update the card's balance by adding the fare difference.
            // Note: The fare calculation may include additional logic as needed.
            card.Balance += journeyFare - fare;

            // Reset the inward station and journey type on the card to indicate the end of the journey.
            card.InwardStation = null;
            card.InwardJourneyType = null;

            // Save the updated card information back to the repository.
            await _cardRepository.UpdateCardAsync(card);

            // Output the result for debugging or user feedback.
            Console.WriteLine($"Swiped out at {exitStationName}. Fare charged: £{fare}. Updated Balance: £{card.Balance}");
        }
    }
}
