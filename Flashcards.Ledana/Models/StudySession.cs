namespace Flashcards.Ledana.Models;
    internal class StudySession
    {
        public int Id { get; set; }
        public int StackId { get; set; }
    public int FlashCardId { get; set; }
        public string Score { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public FlashCard? FlashCard { get; set; }
        public string Answer { get; set; } = null!;
    }
