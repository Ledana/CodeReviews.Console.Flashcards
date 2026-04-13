using Spectre.Console;
namespace StackFlashcards
{
    internal class TableVisualisation
    {
        internal void ShowStacks(List<Stack> allStacks)
        {
            var table = new Table()
                .RoundedBorder()
                .BorderColor(Color.Red);
            table.AddColumn("Name");

            foreach (var item in allStacks)
            {
                table.AddRow(item.Name);
            }
            AnsiConsole.Write(table);
        }

        //when showing a stack show the flashcards with stacks name as title
        internal void ShowFlashCards(string name, List<FlashcardDto> stack)
        {
            var table = new Table()
                .Title(name)
                .RoundedBorder()
                .BorderColor(Color.Red);
            table.AddColumn("Id");
            table.AddColumn("Front");

            foreach (var item in stack)
            {
                table.AddRow(item.RowNum.ToString(), item.Front);
            }
            AnsiConsole.Write(table);
        }
        //when showing all stacks list them without a title
        internal void ShowFlashCards(List<FlashcardDto> flashcards)
        {
            var table = new Table()
                .RoundedBorder()
                .BorderColor(Color.Red);
            table.AddColumn("Id");
            table.AddColumn("Front");
            table.AddColumn("Back");

            foreach (var item in flashcards)
            {
                table.AddRow(item.RowNum.ToString(), item.Front, item.Back);
            }
            AnsiConsole.Write(table);
        }
        internal void ShowStudySessions(List<StudySessionDetails> studySessions)
        {
            var table = new Table()
                .RoundedBorder()
                .BorderColor(Color.Red);
            table.ShowRowSeparators = true;

            table.AddColumn("Name");
            table.AddColumn("Front");
            table.AddColumn("Back");
            table.AddColumn("Date");
            table.AddColumn("Score");

            foreach (var item in studySessions)
            {
                //if score is null show 0 in table
                table.AddRow(item.Name, item.Front, item.Back, item.Date.ToString("d"), (item.Score == null ? "0" : item.Score));
            }
            AnsiConsole.Write(table);
        }
    }
}