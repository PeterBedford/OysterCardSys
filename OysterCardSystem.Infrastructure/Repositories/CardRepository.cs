using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Interfaces;

namespace OysterCardSystem.Infrastructure.Repositories
{
    // Repository class for managing 'Card' entities in memory.
    // Implements the ICardRepository interface.
    public class CardRepository : ICardRepository
    {
        // Static list to store cards. This acts as an in-memory database.
        private static readonly List<Card> Cards = new List<Card>();

        // Retrieves a card by its ID asynchronously.
        // Returns the card if found, otherwise returns null.
        public Task<Card> GetCardByIdAsync(int cardId)
        {
            // Find the card in the list by matching the ID.
            var card = Cards.FirstOrDefault(c => c.Id == cardId);

            // Return the result as a completed task.
            return Task.FromResult(card);
        }

        // Updates or adds a card in the in-memory list asynchronously.
        // If the card already exists, it will be updated; otherwise, it will be added.
        public Task UpdateCardAsync(Card card)
        {
            // Find the index of the card in the list by its ID.
            var index = Cards.FindIndex(c => c.Id == card.Id);

            if (index >= 0)
            {
                // If the card exists, update it.
                Cards[index] = card;
            }
            else
            {
                // If the card doesn't exist, add it to the list.
                Cards.Add(card);
            }

            // Return a completed task since the operation is synchronous.
            return Task.CompletedTask;
        }
    }
}
