using System.Threading.Tasks;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Interfaces;

namespace OysterCardSystem.Application.Queries
{
    public class GetCardBalanceQuery
    {
        private readonly ICardRepository _cardRepository;

        public GetCardBalanceQuery(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<decimal> ExecuteAsync(int cardId)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            return card?.Balance ?? 0m;
        }
    }
}
