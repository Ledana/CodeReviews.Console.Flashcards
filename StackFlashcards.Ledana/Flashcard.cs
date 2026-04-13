namespace StackFlashcards
{
    internal class Flashcard
    {
        public int Id { get; set; }
        public int StackId { get; set; }
        public string Front { get; set; } = null!;
        public string Back { get; set; } = null!;
    }

    internal class FlashcardDto
    {
        public int Id { get; set; }
        public string Front { get; set; } = null!;
        public string Back { get; set; } = null!;
        public int RowNum { get; set; }
    }
}