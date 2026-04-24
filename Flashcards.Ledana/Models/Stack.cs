using Microsoft.EntityFrameworkCore;

namespace Flashcards.Ledana.Models;

    [Index(nameof(Name), IsUnique = true)]
    internal class Stack
    {
        public string Name { get; set; } = null!;
    public int Id { get; set; }
    public List<FlashCard> Flashcards { get; set; } = [];
    }
