namespace Flashcards.Ledana.DTOs;
    internal class StudySessionDTO
    {
        public string StackName { get; set; } = null!;
    public DateTime DateTime { get; set; }
        public string FrontCard { get; set; } = null!;
        public string BackCard { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string Score { get; set; } = null!;
    }
