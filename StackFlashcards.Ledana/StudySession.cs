namespace StackFlashcards
{
    internal class StudySession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Score { get; set; }
        public int StackId { get; set; }
    }
    //creating another class for studysessions so when showing the list show other details from stack and cards with the right query
    internal class StudySessionDetails
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Score { get; set; }
        public int StackId { get; set; }
        public string Front { get; set; } = null!;
        public string Back { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}