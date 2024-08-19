﻿using System.Threading.Tasks;
using OysterCardSystem.Domain.Interfaces;

namespace OysterCardSystem.Application.Commands
{
    public class LoadMoneyCommand
    {
        private readonly ICardRepository _cardRepository;

        public LoadMoneyCommand(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task ExecuteAsync(int cardId, decimal amount)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);
            card.Balance += amount;
            await _cardRepository.UpdateCardAsync(card);
        }
    }
}
