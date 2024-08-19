using OysterCardSystem.Domain.Interfaces;

namespace OysterCardSystem.Application.Queries
{
    // Query class responsible for retrieving the balance of a specific card.
    public class GetCardBalanceQuery
    {
        // Dependency on the card repository to access card data.
        private readonly ICardRepository _cardRepository;

        // Constructor to initialize the card repository dependency.
        // - Parameters:
        //   - cardRepository: Provides methods for retrieving card information.
        public GetCardBalanceQuery(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        // Asynchronously retrieves the balance of a card based on its ID.
        // - Parameters:
        //   - cardId: The unique identifier of the card whose balance is to be retrieved.
        // - Returns:
        //   - The balance of the card as a decimal value.
        //   - If the card is not found, returns 0 as a default value.
        public async Task<decimal> GetCardBalanceAsync(int cardId)
        {
            // Retrieve the card details from the repository using the card's ID.
            var card = await _cardRepository.GetCardByIdAsync(cardId);

            // Return the card's balance if the card is found; otherwise, return 0.
            return card?.Balance ?? 0m;
        }
    }
}
