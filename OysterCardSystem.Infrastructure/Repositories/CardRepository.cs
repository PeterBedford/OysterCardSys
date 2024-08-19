using System.Collections.Generic;
using System.Threading.Tasks;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Interfaces;

namespace OysterCardSystem.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private static readonly Dictionary<int, Card> Cards = new Dictionary<int, Card>();

        public Task<Card> GetCardByIdAsync(int cardId)
        {
            Cards.TryGetValue(cardId, out var card);
            return Task.FromResult(card);
        }

        public Task UpdateCardAsync(Card card)
        {
            Cards[card.Id] = card;
            return Task.CompletedTask;
        }
    }
}
