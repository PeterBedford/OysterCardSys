using System.Collections.Generic;
using System.Threading.Tasks;
using OysterCardSystem.Domain;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Interfaces;
using OysterCardSystem.Domain.Services;

namespace OysterCardSystem.Application.Commands
{
    // Command class responsible for handling the swipe-in process at the start of a journey.
    public class SwipeInCommand
    {
        // Dependency on the card repository to access and update card data.
        private readonly ICardRepository _cardRepository;

        // Dictionary mapping station names to Station objects.
        // Allows lookup of a Station by its name.
        private readonly Dictionary<string, Station> _stations;

        // Service to calculate fares based on journey types and zones.
        private readonly FareCalculator _fareCalculator;

        // Constructor to initialize dependencies.
        // - Parameters:
        //   - cardRepository: Provides methods for retrieving and updating card data.
        //   - stations: A dictionary for looking up Station objects by their names.
        //   - fareCalculator: A service for calculating fares based on journey type.
        public SwipeInCommand(ICardRepository cardRepository, Dictionary<string, Station> stations, FareCalculator fareCalculator)
        {
            _cardRepository = cardRepository;
            _stations = stations;
            _fareCalculator = fareCalculator;
        }

        // Handles the swipe-in action for a card at a specified station and journey type.
        // - Parameters:
        //   - cardId: The unique identifier of the card being used.
        //   - stationName: The name of the station where the card is swiped in.
        //   - journeyType: The type of journey (e.g., Bus or Tube).
        // - Returns:
        //   - A Task representing the asynchronous operation.
        public async Task SwipeInAsync(int cardId, string stationName, JourneyType journeyType)
        {
            // Retrieve the card details from the repository using the card's ID.
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            
            // If the card does not exist, exit the method.
            if (card == null) return;

            // Lookup the station details using the provided station name.
            if (!_stations.TryGetValue(stationName, out var station))
            {
                // If the station is not found, handle the error (optional: throw an exception or log an error).
                return;
            }

            // Update the card's inward station and journey type.
            card.InwardStation = station;
            card.InwardJourneyType = journeyType;

            // Deduct the fare from the card's balance based on the journey type.
            // For Tube journeys, use a default fare of 3m (modify if needed).
            // For Bus journeys, use the fare from the fare calculator.
            decimal fare = journeyType == JourneyType.Bus ? _fareCalculator.BusFare : 3m;
            card.Balance -= fare;

            // Save the updated card information back to the repository.
            await _cardRepository.UpdateCardAsync(card);
        }
    }
}
