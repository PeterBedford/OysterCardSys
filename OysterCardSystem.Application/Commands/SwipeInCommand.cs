using System.Collections.Generic;
using System.Threading.Tasks;
using OysterCardSystem.Domain;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Interfaces;
using OysterCardSystem.Domain.Services;

namespace OysterCardSystem.Application.Commands
{
    public class SwipeInCommand
    {
        private readonly ICardRepository _cardRepository;
        private readonly Dictionary<string, Station> _stations;
        private readonly FareCalculator _fareCalculator;

        public SwipeInCommand(ICardRepository cardRepository, Dictionary<string, Station> stations, FareCalculator fareCalculator)
        {
            _cardRepository = cardRepository;
            _stations = stations;
            _fareCalculator = fareCalculator;
        }

        public async Task ExecuteAsync(int cardId, string stationName, JourneyType journeyType)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            if (card == null) return;

            var station = _stations[stationName];
            card.InwardStation = station;
            card.InwardJourneyType = journeyType;
            card.Balance -= journeyType == JourneyType.Bus ? _fareCalculator.BusFare : 3m; // Adjust this if needed
            await _cardRepository.UpdateCardAsync(card);
        }
    }
}
