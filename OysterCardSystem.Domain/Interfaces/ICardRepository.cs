using System.Threading.Tasks;
using OysterCardSystem.Domain.Entities;

namespace OysterCardSystem.Domain.Interfaces
{
    // Interface defining the operations for managing Card entities in the repository.
    public interface ICardRepository
    {
        // Asynchronously retrieves a Card by its unique ID.
        // - Parameters: 
        //   - cardId: The unique identifier for the card.
        // - Returns:
        //   - A Task representing the asynchronous operation, with the Card object as the result.
        Task<Card> GetCardByIdAsync(int cardId);

        // Asynchronously updates the details of a given Card.
        // - Parameters:
        //   - card: The Card object containing updated information.
        // - Returns:
        //   - A Task representing the asynchronous operation.
        Task UpdateCardAsync(Card card);
    }
}
