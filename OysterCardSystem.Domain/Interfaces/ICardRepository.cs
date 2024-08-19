using System.Threading.Tasks;
using OysterCardSystem.Domain.Entities;

namespace OysterCardSystem.Domain.Interfaces
{
    public interface ICardRepository
    {
        Task<Card> GetCardByIdAsync(int cardId);   // Retrieve a card by its ID
        Task UpdateCardAsync(Card card);           // Update card details
    }
}
