using Flashcards.Ledana.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Ledana;

internal class FlashcardsContext : DbContext
{
    public DbSet<FlashCard> Flashcards { get; set; }
    public DbSet<Stack> Stacks { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FlashCardDb;");
    }
}
