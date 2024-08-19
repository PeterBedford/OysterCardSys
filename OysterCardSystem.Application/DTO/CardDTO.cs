namespace OysterCardSystem.Application.DTOs
{
    // Data Transfer Object (DTO) representing a card.
    // Used to transfer card data between different layers of the application.
    public class CardDTO
    {
        // Unique identifier for the card.
        // - Represents the card's ID.
        public int Id { get; set; }

        // Current balance of the card.
        // - Represents the amount of money available on the card.
        public decimal Balance { get; set; }
    }
}
