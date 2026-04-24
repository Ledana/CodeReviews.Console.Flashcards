using Flashcards.Ledana.DTOs;
using Flashcards.Ledana.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Ledana.Controllers
{
    internal class FlashCardController
    {
        internal static void AddFlashCard(FlashCard card)
        {
            using var db = new FlashcardsContext();
            db.Flashcards.Add(card);
            db.SaveChanges();
        }

        internal static void DeleteCard(FlashCard card)
        {
            using var db = new FlashcardsContext();
            db.Flashcards.Remove(card);
            db.SaveChanges();
        }

        internal static List<FlashCardDTO> GetFlashCardDTOs(int id)
        {
            try
            {
                using var db = new FlashcardsContext();
                var cards = db.Flashcards
                    .AsEnumerable()
                    .Where(f => f.StackId == id)
                    .Select((f, index) => new FlashCardDTO
                    {
                        Id = index + 1,
                        Front = f.Front
                    })
                    .ToList();
                return cards;
            }
            catch (Exception e)
            {
                Console.WriteLine("Query didn't work. " + e.Message);
                return [];
            }
        }

        internal static int GetFlashCardNumberPerStack(int id)
        {
            using var db = new FlashcardsContext();
            return db.Flashcards
                .Where(f => f.StackId == id)
                .Count();
        }

        internal static List<FlashCard> GetFlashCards(int id)
        {
            using var db = new FlashcardsContext();
            return db.Flashcards
                .Where(f => f.StackId == id)
                .ToList();
        }
    }
}
