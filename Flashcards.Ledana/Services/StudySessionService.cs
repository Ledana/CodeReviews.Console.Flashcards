using Flashcards.Ledana.Controllers;
using Flashcards.Ledana.DTOs;
using Flashcards.Ledana.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Ledana.Services
{
    internal class StudySessionService
    {
        internal static void OpenStudySession()
        {
            string stringScore = "";
            string response = "";
            int score = 0;
            
            var stack = StackService.GetStackOptionInput();
            if (stack is null)
            {
                Console.WriteLine("Stack not found");
                return;
            }

            int maxScore = FlashCardController.GetFlashCardNumberPerStack(stack.Id);
            bool studying = true;
            DateTime dateTime = DateTime.Today;
            while (studying)
            {
                var card = FlashCardService.GetCardOptionInput(stack.Id);
                if (card is null)
                {
                    Console.WriteLine("Card not found");
                    return;
                }
                var answer = AnsiConsole.Ask<string>("Your answer: ");

                bool answeredRight = StudySessionController.ValidateAnswer(card.Id, answer);
                if (answeredRight) score++;

                response = answeredRight ? "Correct" : "Wrong";
                Console.WriteLine("Your answer was " + response);
                studying = AnsiConsole.Confirm("Do you want to keep studying?");
                stringScore = $"{score}/{maxScore}";
                AddStudySession(stack.Id, card.Id, stringScore, dateTime, response);
            }
            
            
        }
        internal static void AddStudySession(int stackId, int cardId, string score, DateTime dateTime, string response)
        {
            StudySession studySession = new()
            {
                StackId = stackId,
                FlashCardId = cardId,
                Score = score,
                DateTime = dateTime,
                Answer = response
            };
            StudySessionController.AddStudySession(studySession);
        }

        internal static void ViewSessions()
        {
            List<StudySessionDTO> studySessionDTOs = StudySessionController.GetSessions();
            UI.ShowStudySessions(studySessionDTOs);
        }
    }
}
