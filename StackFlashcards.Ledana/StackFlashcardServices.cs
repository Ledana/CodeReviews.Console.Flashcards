using Dapper;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace StackFlashcards
{
    internal class StackFlashcardServices
    {
        readonly string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        //creating a new flashcard in a new stack
        internal void CreateFlashCard(string name, string front, string back)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);

            var createFlashcardQuery = @"INSERT INTO Flashcard (StackId, Front, Back)
                                         VALUES (@StackId, @Front, @Back)";

            connection.Execute(createFlashcardQuery, new { StackId = stack.Id, Front = front, Back = back });
        }

        //creating a new stack with name
        internal void CreateStack(string name)
        {
            using SqlConnection connection = new(connectionString);

            var createStackQuery = "INSERT INTO Stack ([Name]) VALUES (@Name)";

            connection.Execute(createStackQuery, new { Name = name });
        }

        //deleting a stack with ame
        internal void DeleteStack(string name)
        {
            using SqlConnection connection = new(connectionString);

            var deleteStackQuery = "DELETE FROM Stack WHERE [Name] = @Name";

            connection.Execute(deleteStackQuery, new { Name = name });
        }

        //getting all the flashcards in the db with their true id
        internal List<FlashcardDto> GetAllFlashcards()
        {
            using SqlConnection connection = new(connectionString);

            var getAllFlashcardsQuery = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Id, Front, Back FROM Flashcard";

            return connection.Query<FlashcardDto>(getAllFlashcardsQuery).ToList();
        }

        //getting the number of flashcards in a stack to calculate score 
        internal int? GetFlashcardNumber(string name)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);
            if (stack is null) return null;

            var getFlashcardCountQuery = "SELECT COUNT(*) FROM Flashcard WHERE StackId = @StackId";

            return connection.ExecuteScalar<int>(getFlashcardCountQuery, new { StackId = stack.Id });
        }

        //getting the front part of a card to show to user when in study session
        internal string? GetFront(string name, int id)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);
            if (stack is null) return null;

            var flashcards = GetFlashcardsByStackId(stack.Id);
            var card = flashcards.FirstOrDefault(f => f.RowNum == id);

            return card?.Front;
        }
        //getting the back side of the card to show it to user
        internal string? GetRightAnswer(string name, int id)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);
            if (stack is null) return null;

            var flashcards = GetFlashcardsByStackId(stack.Id);
            var card = flashcards.FirstOrDefault(f => f.RowNum == id);

            return card?.Back;
        }

        //helper method to get a stack by id
        internal List<FlashcardDto> GetFlashcardsByStackId(int Id)
        {
            using SqlConnection connection = new(connectionString);
            var getFlashcardsQuery = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Id, Front, Back FROM Flashcard WHERE StackId = @StackId";

            return connection.Query<FlashcardDto>(getFlashcardsQuery, new { StackId = Id }).ToList();
        }

        //getting all study sessions with a query with joins so other details show also
        internal List<StudySessionDetails> GetStudySessionsDetails()
        {
            using SqlConnection connection = new(connectionString);

            var getStudySessionDetailsQuery = "SELECT s.[Name], f.Front, f.Back, ss.[Date], ss.Score\r\nFROM StudySessions ss\r\nJOIN Stack s ON s.Id = ss.StackId\r\nJOIN Flashcard f ON f.StackId = s.Id";

            return connection.Query<StudySessionDetails>(getStudySessionDetailsQuery).ToList();
        }

        //inserting a new row in studysessions with the stackid, date and score
        internal void OpenStudySession(string name, DateTime date, string score)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);
            if (stack is null)
            {
                Console.WriteLine("Stack not found");
                return;
            }

            var insertQuery = "INSERT INTO StudySessions (Date, StackId, Score) VALUES (@Date, @StackId, @Score)";

            connection.Execute(insertQuery, new StudySession() { Date = date, StackId = stack.Id, Score = score });
        }

        //comparig the back side of the card with the given answer by user with the name of stack, rownum shown as id.
        internal bool ValidateAnswer(string name, int id, string answer)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);

            var getFlashcardsQuery = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Front, Back FROM Flashcard WHERE StackId = @StackId";
            var flashcards = connection.Query<FlashcardDto>(getFlashcardsQuery, new { StackId = stack.Id }).ToList();
            var card = flashcards.FirstOrDefault(f => f.RowNum == id);

            return answer == card?.Back;
        }
        //checking if id form user matches any card with rownum as id when deleting or vstuding 
        internal bool ValidateIdInput(string name, int num)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);

            var getFlashcardsQuery = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Front, Back FROM Flashcard WHERE StackId = @StackId";

            var flashcards = connection.Query<FlashcardDto>(getFlashcardsQuery, new { StackId = stack.Id }).ToList();

            return flashcards.Any(c => c.RowNum == num);
        }
        //checking if stack name is in the db
        internal bool ValidateStackName(string name)
        {
            using SqlConnection connection = new(connectionString);

            var checkNamesQuery = "SELECT COUNT(*) FROM Stack WHERE [Name] = @Name";

            var nameExists = connection.ExecuteScalar<int>(checkNamesQuery, new { Name = name });

            return nameExists > 0;
        }
        //getting the list of all stacks in db
        internal List<Stack> ViewStackList()
        {
            using SqlConnection connection = new(connectionString);

            var getStackListQuery = "SELECT * FROM Stack ORDER BY Id";

            return connection.Query<Stack>(getStackListQuery).ToList();
        }
        //getting the list of cards in a stack given its name
        internal List<FlashcardDto>? GetStack(string name)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);
            if (stack is null) return null;

            var getFlashcardQuery = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Front FROM Flashcard WHERE StackId = @StackId";

            return connection.Query<FlashcardDto>(getFlashcardQuery, new { StackId = stack.Id }).ToList();
        }
        //helper method to get a stack by its name
        internal Stack? GetStackByName(string name)
        {
            using SqlConnection connection = new(connectionString);

            var getStackQuery = "SELECT * FROM Stack WHERE [Name] = @Name";

            return connection.QuerySingleOrDefault<Stack>(getStackQuery, new { Name = name });
        }

        //deleting a row in flashcard table given the id
        internal void DeleteFlashcard(string name, int id)
        {
            using SqlConnection connection = new(connectionString);

            Stack? stack = GetStackByName(name);

            var getFlashcardsQuery = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Id, Front, Back FROM Flashcard WHERE StackId = @StackId";
            List<FlashcardDto> cards = connection.Query<FlashcardDto>(getFlashcardsQuery, new { StackId = stack.Id }).ToList();

            var cardToDelete = cards.FirstOrDefault(f => f.RowNum == id);

            var deleteFlashcardQuery = "DELETE FROM Flashcard WHERE Id = @Id";

            connection.Execute(deleteFlashcardQuery, new { Id = cardToDelete.Id });
        }

    }
}