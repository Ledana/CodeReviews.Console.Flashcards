namespace StackFlashcards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in our Application");
            UI ui = new();
            ui.WelcomeUser();

            Console.WriteLine("Good bye");
        } 
    }
}
