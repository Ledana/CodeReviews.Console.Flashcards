using Flashcards.Ledana.Controllers;
using Flashcards.Ledana.DTOs;
using Flashcards.Ledana.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Ledana.Services
{
    internal class StackService
    {
        internal static void AddStack()
        {
            var stack = new Stack
            {
                Name = AnsiConsole.Ask<string>("Enter name for stack: ")
            };
            StackController.AddStack(stack);
            while(AnsiConsole.Confirm("Do you want to add a card?"))
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

        internal static void DeleteStack()
        {
            var stack = GetStackOptionInput();
            if (stack is null)
            {
                Console.WriteLine("Stack doesn't exist");
                return;
            }
            StackController.DeleteStack(stack);
            Console.WriteLine("Stack deleted!");
        }

        internal static void ViewAllStacks()
        {
            List<Stack> stacks = StackController.GetAllStacks();
            UI.ShowAllStacks(stacks);
        }

        internal static void ViewStack()
        {
            var stack = GetStackOptionInput();
            if (stack is null)
            {
                Console.WriteLine("Stack doesn't exist");
                return;
            }

            List<FlashCardDTO> flashCards = FlashCardController.GetFlashCardDTOs(stack.Id);
            UI.ShowStackOfFlashCards(stack, flashCards);
        }

        internal static Stack? GetStackOptionInput()
        {
            List<Stack> stacks = StackController.GetAllStacks();
            if (stacks.Count == 0)
            {
                Console.WriteLine("List of stacks is empty");
                return null;
            }
            var stacksArray = stacks.Select(s => s.Name).ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose stack")
                .AddChoices(stacksArray));
            var stack = stacks.SingleOrDefault(s => s.Name == option);
            return stack;
        }
    }
}
