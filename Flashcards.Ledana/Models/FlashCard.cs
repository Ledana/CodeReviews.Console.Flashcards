

namespace Flashcards.Ledana.Models
{
    internal class FlashCard
    {
        public int Id { get; set; }
        public int StackId { get; set; }
        public string Front { get; set; } = null!;
        public string Back { get; set; } = null!;
        public Stack Stack { get; set; } = null!;
    }
}
