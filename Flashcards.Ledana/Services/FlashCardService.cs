using Flashcards.Ledana.Controllers;
using Flashcards.Ledana.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Ledana.Services
{
    internal class FlashCardService
    {
        internal static void AddFlashCard()
        {
            var stack = StackService.GetStackOptionInput();
            if (stack is null)
            {
                Console.WriteLine("Stack was not found");
                return;
            }
            while (AnsiConsole.Confirm("Do you want to add a card?"))
            {
                var card = new FlashCard
                {
                    StackId = stack.Id,
                    Front = AnsiConsole.Ask<string>("Put the front part: "),
                    Back = AnsiConsole.Ask<string>("Put the back part: ")
                };
                FlashCardController.AddFlashCard(card);
            }
        }

        internal static void DeleteFlashCard()
        {
            var stack = StackService.GetStackOptionInput();
            if (stack is null)
            {
                Console.WriteLine("Stack not found");
                return;
            }

            FlashCard card = GetCardOptionInput(stack.Id);
            if (card is null)
            {
                Console.WriteLine("Card nout found");
                return;
            }
            FlashCardController.DeleteCard(card);
            Console.WriteLine("Card deleted!");
        }

        internal static FlashCard? GetCardOptionInput(int stackId)
        {
            List<FlashCard> FlashCards = FlashCardController.GetFlashCards(stackId);
            if (FlashCards.Count == 0)
            {
                Console.WriteLine("List of FlashCard is empty");
                return null;
            }
            var stacksArray = FlashCards.Select(c => c.Front).ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose card")
                .AddChoices(stacksArray));
            var card = FlashCards.SingleOrDefault(s => s.Front == option);
            return card;
        }

    }
}
