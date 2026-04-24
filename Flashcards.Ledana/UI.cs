using Flashcards.Ledana.DTOs;
using Flashcards.Ledana.Models;
using Flashcards.Ledana.Services;
using Spectre.Console;
using System.Collections;
using static Flashcards.Ledana.Enums;
using Stack = Flashcards.Ledana.Models.Stack;

namespace Flashcards.Ledana;
    internal class UI
    {
    internal static void ShowAllStacks(List<Stack> stacks)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        foreach (var item in stacks)
        {
            table.AddRow(item.Id.ToString(), item.Name);
        }
        AnsiConsole.Write(table);
    }

    internal static void ShowStackOfFlashCards(Stack stack, List<FlashCardDTO> flashCards)
    {
        var table = new Table()
            .Title(stack.Name);
        table.AddColumn("Id");
        table.AddColumn("Front");
        foreach(var item in flashCards)
        {
            table.AddRow(item.Id.ToString(), item.Front);
        }
        AnsiConsole.Write(table);
    }

    internal static void ShowStudySessions(List<StudySessionDTO> studySessionDTOs)
    {
        var table = new Table();
        table.AddColumn("StackName");
        table.AddColumn("DateTime");
        table.AddColumn("FrontCard");
        table.AddColumn("BackCard");
        table.AddColumn("Answer");
        table.AddColumn("Score"); 
        
        foreach (var item in studySessionDTOs)
        {
            table.AddRow(item.StackName, item.DateTime.ToString("d"),
                item.FrontCard, item.BackCard,
                item.Answer, item.Score);
        }
        AnsiConsole.Write(table);
    }

    public void MainMenu()
        {
        Console.Clear();
            bool isAppRunning = true;

            while (isAppRunning)
            {
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuOptions>()
                    .Title("What do you want to do?")
                    .AddChoices(
                    MainMenuOptions.ManageStacks,
                    MainMenuOptions.ManageFlashCards,
                    MainMenuOptions.Study,
                    MainMenuOptions.ViewStudySession,
                    MainMenuOptions.Quit
                    ));
                switch(option)
                {
                    case MainMenuOptions.ManageStacks:
                    StacksMenu();
                        break;
                    case MainMenuOptions.ManageFlashCards:
                    FlashCardsMenu();
                        break;
                    case MainMenuOptions.Study:
                    StudySessionService.OpenStudySession();
                        break;
                        case MainMenuOptions.ViewStudySession:
                    StudySessionService.ViewSessions();
                        break;
                    case MainMenuOptions.Quit:
                        Console.WriteLine("Goodbye");
                        isAppRunning = false;
                        break;
                }
            }
        }

    

    private void FlashCardsMenu()
    {
        bool isFlashCardsMenuRunning = true;

        while (isFlashCardsMenuRunning)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ManageFlashCardsOptions>()
                .Title("What do you want to do?")
                .AddChoices(
                ManageFlashCardsOptions.AddFlashCard,
                ManageFlashCardsOptions.DeleteFlashCard,
                ManageFlashCardsOptions.GoBack
                ));
            switch (option)
            {
                case ManageFlashCardsOptions.AddFlashCard:
                    FlashCardService.AddFlashCard();
                    break;
                case ManageFlashCardsOptions.DeleteFlashCard:
                    FlashCardService.DeleteFlashCard();
                    break;
                case ManageFlashCardsOptions.GoBack:
                    isFlashCardsMenuRunning = false;
                    break;
            }
        }
    }

    private void StacksMenu()
    {
        bool isStacksMenuRunning = true;

        while (isStacksMenuRunning)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ManageStacksOptions>()
                .Title("What do you want to do?")
                .AddChoices(
                ManageStacksOptions.AddStack,
                ManageStacksOptions.DeleteStack,
                ManageStacksOptions.ViewStack,
                ManageStacksOptions.ViewAllStacks,
                ManageStacksOptions.GoBack
                ));
            switch (option)
            {
                case ManageStacksOptions.AddStack:
                    StackService.AddStack();
                    break;
                case ManageStacksOptions.DeleteStack:
                    StackService.DeleteStack();
                    break;
                case ManageStacksOptions.ViewStack:
                    StackService.ViewStack();
                    break;
                case ManageStacksOptions.ViewAllStacks:
                    StackService.ViewAllStacks();
                    break;
                case ManageStacksOptions.GoBack:
                    isStacksMenuRunning = false;
                    break;
            }
        }
    }
}
