namespace OysterCardSystem.Domain.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public Station InwardStation { get; set; }
        public JourneyType? InwardJourneyType { get; set; }
    }
}
