using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OysterCardSystem.Application.Commands;
using OysterCardSystem.Application.Queries;
using OysterCardSystem.Domain;
using OysterCardSystem.Domain.Entities;
using OysterCardSystem.Domain.Services;
using OysterCardSystem.Infrastructure.Repositories;

namespace OysterCardSystem.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cardRepository = new CardRepository();
            var stations = new Dictionary<string, Station>
            {
                { "Holborn", new Station { Name = "Holborn", Zones = new List<int> { 1 } } },
                { "Earl's Court", new Station { Name = "Earl's Court", Zones = new List<int> { 1, 2 } } },
                { "Wimbledon", new Station { Name = "Wimbledon", Zones = new List<int> { 3 } } },
                { "Hammersmith", new Station { Name = "Hammersmith", Zones = new List<int> { 2 } } },
                { "Chelsea", new Station { Name = "Chelsea", Zones = new List<int> { 2 } } } // Add Chelsea for bus journey
            };

            var fareCalculator = new FareCalculator();
            var swipeInCommand = new SwipeInCommand(cardRepository, stations, fareCalculator);
            var swipeOutCommand = new SwipeOutCommand(cardRepository, stations, fareCalculator);
            var getCardBalanceQuery = new GetCardBalanceQuery(cardRepository);

            // Load the card with £30
            var card = new Card { Id = 1, Balance = 30m };
            await cardRepository.UpdateCardAsync(card);
            Console.WriteLine("Card loaded with £30.");
            await Task.Delay(2000); // Delay to simulate time passing

            // Tube journey: Holborn to Earl’s Court
            Console.WriteLine("Swiping in at Holborn (Tube)...");
            await swipeInCommand.ExecuteAsync(1, "Holborn", JourneyType.Tube);
            Console.WriteLine("Swiped in at Holborn (Tube).");

            await Task.Delay(2000); // Delay to simulate time passing

            Console.WriteLine("Swiping out at Earl's Court (Tube)...");
            await swipeOutCommand.ExecuteAsync(1, "Earl's Court");
            var balanceAfterTube = await getCardBalanceQuery.ExecuteAsync(1);
            Console.WriteLine($"Swiped out at Earl's Court (Tube). Updated Balance: £{balanceAfterTube}");

            await Task.Delay(2000); // Delay to simulate time passing

            // Bus journey: Earl’s Court to Chelsea
            Console.WriteLine("Swiping in for bus journey at Earl's Court...");
            await swipeInCommand.ExecuteAsync(1, "Earl's Court", JourneyType.Bus);
            Console.WriteLine("Swiped in for bus journey at Earl's Court.");

            await Task.Delay(2000); // Delay to simulate time passing

            Console.WriteLine("Swiping out at Chelsea (Bus)...");
            await swipeOutCommand.ExecuteAsync(1, "Chelsea");
            var balanceAfterBus = await getCardBalanceQuery.ExecuteAsync(1);
            Console.WriteLine($"Swiped out at Chelsea (Bus). Updated Balance: £{balanceAfterBus}");

            await Task.Delay(2000); // Delay to simulate time passing

            // Tube journey: Earl’s Court to Hammersmith
            Console.WriteLine("Swiping in at Earl's Court (Tube)...");
            await swipeInCommand.ExecuteAsync(1, "Earl's Court", JourneyType.Tube);
            Console.WriteLine("Swiped in at Earl's Court (Tube).");

            await Task.Delay(2000); // Delay to simulate time passing

            Console.WriteLine("Swiping out at Hammersmith (Tube)...");
            await swipeOutCommand.ExecuteAsync(1, "Hammersmith");
            var finalBalance = await getCardBalanceQuery.ExecuteAsync(1);
            Console.WriteLine($"Swiped out at Hammersmith (Tube). Final Balance: £{finalBalance}");

            await Task.Delay(2000); // Delay to simulate time passing
        }
    }
}
