namespace OysterCardSystem.Domain.Entities
{
    // Represents an Oyster Card used in the OysterCardSystem.
    public class Card
    {
        // Unique identifier for the card.
        public int Id { get; set; }

        // Current balance on the card.
        public decimal Balance { get; set; }

        // The station where the user last entered the transport system.
        // This is relevant for tracking incomplete journeys.
        public Station InwardStation { get; set; }

        // The type of journey (e.g., Bus, Tube) that the user last started.
        // Nullable to indicate that there may be no active journey.
        public JourneyType? InwardJourneyType { get; set; }
    }
}
