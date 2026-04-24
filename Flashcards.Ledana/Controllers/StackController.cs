using Flashcards.Ledana.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Ledana.Controllers
{
    internal static class StackController
    {
        internal static void AddStack(Stack stack)
        {
            using var db = new FlashcardsContext();
            db.Stacks.Add(stack);
            db.SaveChanges();
        }

        internal static void DeleteStack(Stack stack)
        {
            using var db = new FlashcardsContext();
            db.Stacks.Remove(stack);
            db.SaveChanges();
        }

        internal static List<Stack> GetAllStacks()
        {
            using var db = new FlashcardsContext();
            return db.Stacks
                .Include(s => s.Flashcards)
                .ToList();
        }
    }
}
