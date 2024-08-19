using System.Threading.Tasks;
using OysterCardSystem.Domain.Interfaces;

namespace OysterCardSystem.Application.Commands
{
    // Command class responsible for loading money onto a card's balance.
    public class LoadBalanceCommand
    {
        // Dependency on the card repository to interact with card data.
        private readonly ICardRepository _cardRepository;

        // Constructor to inject the card repository dependency.
        // - Parameters:
        //   - cardRepository: An instance of ICardRepository for accessing and updating card data.
        public LoadBalanceCommand(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        // Asynchronously adds a specified amount of money to the card's balance.
        // - Parameters:
        //   - cardId: The unique identifier of the card to which money will be added.
        //   - amount: The amount of money to be added to the card's balance.
        // - Returns:
        //   - A Task representing the asynchronous operation.
        public async Task LoadBalanceAsync(int cardId, decimal amount)
        {
            // Retrieve the card details using the card's ID.
            var card = await _cardRepository.GetCardByIdAsync(cardId);

            // Add the specified amount to the card's current balance.
            card.Balance += amount;

            // Update the card's details in the repository to reflect the new balance.
            await _cardRepository.UpdateCardAsync(card);
        }
    }
}
