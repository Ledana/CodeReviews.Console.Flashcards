using Flashcards.Ledana.DTOs;
using Flashcards.Ledana.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Ledana.Controllers
{
    internal class StudySessionController
    {
        internal static void AddStudySession(StudySession studySession)
        {
            try
            {
                using var db = new FlashcardsContext();
                db.StudySessions.Add(studySession);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Query didn't work to add study session " + e.Message);
            }
        }

        internal static List<StudySessionDTO> GetSessions()
        {
            try
            {
                using var db = new FlashcardsContext();
                var sessions = db.StudySessions
                    .Select(s => new StudySessionDTO
                    {
                        StackName = s.FlashCard.Stack.Name,
                        FrontCard = s.FlashCard.Front,
                        BackCard = s.FlashCard.Back,
                        Answer = s.Answer,
                        Score = s.Score
                    })
                    .ToList();
                return sessions;
            }
            catch (Exception e)
            {
                Console.WriteLine("Query didn't work. " + e.Message);
                return [];
            }
        }

        internal static bool ValidateAnswer(int id, string answer)
        {
            using var db = new FlashcardsContext();
            var result = db.Flashcards
                .Any(f => f.Id == id && f.Back == answer);
            return result;
        }
    }
}
