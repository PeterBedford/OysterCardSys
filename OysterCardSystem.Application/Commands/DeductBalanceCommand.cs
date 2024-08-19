using System.Threading.Tasks;
using OysterCardSystem.Domain.Interfaces;

namespace OysterCardSystem.Application.Commands
{
    // Command class responsible for deducting money from a card's balance.
    public class DeductBalanceCommand
    {
        // Dependency on the card repository to interact with card data.
        private readonly ICardRepository _cardRepository;

        // Constructor to inject the card repository dependency.
        public DeductBalanceCommand(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        // Executes the command to deduct a specified amount of money from the card's balance.
        // - Parameters:
        //   - cardId: The unique identifier of the card from which money will be deducted.
        //   - amount: The amount of money to be deducted from the card's balance.
        public async Task DeductBalanceAsync(int cardId, decimal amount)
        {
            // Retrieve the card details using the card's ID.
            var card = await _cardRepository.GetCardByIdAsync(cardId);

            // Deduct the specified amount from the card's balance.
            card.Balance -= amount;

            // Update the card's details in the repository to reflect the new balance.
            await _cardRepository.UpdateCardAsync(card);
        }
    }
}