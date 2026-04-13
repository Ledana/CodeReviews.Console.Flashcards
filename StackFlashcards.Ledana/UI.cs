using System.Xml.Linq;

namespace StackFlashcards
{
    internal class UI
    {
        readonly StackFlashcardServices stackFlashcardServices = new();
        private readonly TableVisualisation tableVisualisation = new();
        public void WelcomeUser()
        {
            //keep running this method till the user inputs 0
            while (true) ////////////
            {
                ShowMenu();

                string input = GetUserInput("Please type a number between 0 and 7");

                switch (input)
                {
                    case "0":
                        return;
                    case "1":
                        OpenStudySession();
                        break;
                    case "2":
                        CreateStack();
                        break;
                    case "3":
                        ViewStack();
                        break;
                    case "4":
                        ViewAllFlashcards();
                        break;
                    case "5":
                        ViewStudySessions();
                        break;
                    case "6":
                        DeleteStack();
                        break;
                    case "7":
                        DeleteFlashcard();
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please type a number between 0 and 7");
                        break;
                }
            }
        }

        private void DeleteFlashcard()
        {
            ViewAllStacks();
            string stackName = GetExistingStackName("PLease put the name of stack you want to delete flashcard in");
            if (stackName == "0") return;

            List<FlashcardDto>? stack = stackFlashcardServices.GetStack(stackName);
            if (stack is null)
            {
                Console.WriteLine("Stack not found");
                return;
            }

            tableVisualisation.ShowFlashCards(stackName, stack);

            int id = GetValidatedInt("Please put the id of the flashcard you want to delete or type 0 to return\n");

            //check if the id exists in the db, if not keep asking for an id or 0 to exit

            while (!stackFlashcardServices.ValidateIdInput(stackName, id))
            {
                id = GetValidatedInt("Please put the id of the flashcard you want to delete or type 0 to return");
                if (id == 0) return;

            }

            //if the id is found in db and user didn't type 0 to exit then delete the card and notify user in console
            stackFlashcardServices.DeleteFlashcard(stackName, id);

            Console.WriteLine($"\nFlashcard with Id {id} was deleted");
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("0. Close the App");
            Console.WriteLine("1. Study a stack of flashcards");
            Console.WriteLine("2. Create a stack of flashcard");
            Console.WriteLine("3. View a stack");
            Console.WriteLine("4. View all flashcards");
            Console.WriteLine("5. View Study Sessions");
            Console.WriteLine("6. Delete a stack");
            Console.WriteLine("7. Delete a Flashcard");
        }

        private void DeleteStack()
        {
            ViewAllStacks();
            string name = GetExistingStackName("Please put the name of the stack you want to delete or type '0' to return to main menu");
            if (name == "0") return;

            stackFlashcardServices.DeleteStack(name);
            Console.WriteLine($"\nStack {name} was deleted");
        }

        private void ViewAllFlashcards()
        {
            //getting all the cards from the db and showing them with spectre.console
            List<FlashcardDto> allFlashcards = stackFlashcardServices.GetAllFlashcards();
            tableVisualisation.ShowFlashCards(allFlashcards);
        }

        private void ViewStudySessions()
        {
            List<StudySessionDetails> studySessionsDetails = stackFlashcardServices.GetStudySessionsDetails();
            tableVisualisation.ShowStudySessions(studySessionsDetails);
        }
        private void ViewAllStacks()
        {
            List<Stack> allStacks = stackFlashcardServices.ViewStackList();
            tableVisualisation.ShowStacks(allStacks);
        }
        private void ViewStack()
        {
            ViewAllStacks();
            string name = GetExistingStackName("Please put the name of the stack you want or type '0' to return to main menu");
            if (name == "0") return;

            List<FlashcardDto>? stack = stackFlashcardServices.GetStack(name);
            if (stack is null)
            {
                Console.WriteLine("Stack not found");
                return;
            }

            tableVisualisation.ShowFlashCards(name, stack);
        }

        private void CreateStack()
        {
            string name = GetNewStackName("Please enter the name of the stack or type '0' to exit");
            if (name == "0") return;

            stackFlashcardServices.CreateStack(name);

            bool addFlashcard = AskUserToAddFlashcard();

            while (addFlashcard)
            {
                string front = GetFrontOfStack();
                string back = GetBackOfStack();
                stackFlashcardServices.CreateFlashCard(name, front, back);
                addFlashcard = AskUserToAddFlashcard();
            }
        }

        private bool AskUserToAddFlashcard()
        {
            Console.WriteLine("Do you want to add a flashcard to the stack?");
            string input = GetUserInput("Type 'y' to add or '0' to continue");
            if (input == "y") return true;

            return false;
        }

        private string GetBackOfStack()
        {
            return GetUserInput("Please put the back part of the stack");
        }

        private string GetFrontOfStack()
        {
            return GetUserInput("Please put the front part of the stack");
        }

        private void OpenStudySession()
        {
            ViewAllStacks();
            string name = GetExistingStackName("Please enter the name of the stack you want to study or type '0' to return to main menu");
            if (name == "0") return;

            DateTime date = DateTime.Today.Date;
            string? score = StudyStack(name);

            if (score is null)
            {
                return;
            }

            //if the stack was empty not add the study session
            if (score == "0") return;

            Console.WriteLine($"\nYour score is {score}");

            stackFlashcardServices.OpenStudySession(name, date, score);
        }

        private string? StudyStack(string name)
        {
            int score = 0;
            int? maxScore = stackFlashcardServices.GetFlashcardNumber(name);

            if (maxScore is null) return null;
            //counting question answered so the user doesn't answer the same one more than once
            int question = 0;

            //if the stack doesn't have cards return 0
            if (maxScore == 0)
            {
                Console.WriteLine("The stack is empty");
                return "0";
            }

            var flashcards = stackFlashcardServices.GetStack(name);
            if (flashcards is null)
            {
                Console.WriteLine("Stack not found");
                return null;
            }

            tableVisualisation.ShowFlashCards(name, flashcards);

            while (question < maxScore)
            {
                int id = GetValidatedInt("Please put the id of the flashcard you want to study or '0' to exit");

                if (id == 0) break;

                while (!stackFlashcardServices.ValidateIdInput(name, id))
                {
                    Console.WriteLine($"The card with Id {id} doesn't exist\n");
                    id = GetValidatedInt("Please put the id of the flashcard you want to study or '0' to exit");
                }
                if (id == 0) break;

                string? front = stackFlashcardServices.GetFront(name, id);
                Console.WriteLine();
                Console.WriteLine(front);
                string? answer = GetUserInput("Please type the answer of the flashcard\n");

                if (stackFlashcardServices.ValidateAnswer(name, id, answer))
                {
                    score++;
                    question++;
                    RightAnswer(name, id);
                }
                else
                {
                    question++;
                    WrongAnswer(name, id);
                }
            }
            return score + "/" + maxScore;
        }
        private void RightAnswer(string name, int id)
        {
            Console.WriteLine("\nCorrect answer");
            Console.WriteLine(stackFlashcardServices.GetRightAnswer(name, id));
        }
        private void WrongAnswer(string name, int id)
        {
            Console.WriteLine("\nWrong answer");
            Console.WriteLine("The correct answer was: ");
            Console.WriteLine(stackFlashcardServices.GetRightAnswer(name, id));
        }

        private string GetUserInput(string message)
        {
            Console.WriteLine(message);
            string? input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }

            return input;
        }

        //helper method to validate input as int and as a name in the db or not
        private int GetValidatedInt(string message)
        {
            string input = GetUserInput(message);
            if (input == "0") return 0;

            int num;
            while (!int.TryParse(input, out num))
            {
                input = GetUserInput(message);
                if (input == "0") return 0;
            }
            return num;
        }
        private string GetExistingStackName(string message)
        {
            string name = GetUserInput(message);
            if (name == "0") return "0";

            while (!stackFlashcardServices.ValidateStackName(name))
            {
                name = GetUserInput(message);
                if (name == "0") return "0";
            }
            return name;
        }
        private string GetNewStackName(string message)
        {
            string name = GetUserInput(message);
            if (name.Trim() == "0") return "0";

            while (stackFlashcardServices.ValidateStackName(name))
            {
                name = GetUserInput(message);
                if (name.Trim() == "0") return "0";
            }
            return name;
        }
    }
}