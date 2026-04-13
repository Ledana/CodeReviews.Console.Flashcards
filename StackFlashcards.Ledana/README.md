# StackFlashcards📖 Overview
This is a console-based flashcards application designed to help users create, manage, and study flashcard stacks.
The project was built with SQL Server for database management and Dapper for data access, with a focus on clean, reusable code and minimal repetition.

⚙️ Features
• 	Stack Management
• 	Create new stacks and assign them a name.
• 	Delete stacks (all cards within the stack are deleted automatically).
• 	Card Management
• 	Add cards to a stack with front and back text.
• 	Delete individual cards.
• 	View all cards with sequential IDs (no gaps).
• 	Study Mode
• 	Choose a stack to study.
- See only the front of each card.
- Indicate whether you know the back → score updates accordingly.
- Sessions Tracking
- View study sessions with:
- Stack name
- Cards studied
- Score per stack
- Date of study
- DTOs
- First-time use of Data Transfer Objects (DTOs) to structure data between layers.

🗄️ Database
- Database and tables were created in SQL Server Management Studio (SSMS).
- Code for table creation and population is stored in dbFile.
- Relationships:
- Stacks → Cards (cascade delete: deleting a stack removes its cards).
- Sessions → Stacks/Cards (tracks study history).

💻 Code Design
- Dapper used for lightweight ORM functionality.
- Main Program kept as plain as possible:
- Delegates logic to service classes and helpers.
- Focuses on user interaction and menu flow.
- Clean Code Principles
- Reduced repetition with reusable helpers.
- Clear separation of concerns (DB operations, DTOs, UI flow).

🚀 How to Use
- Create a stack → give it a name.
- Add cards → define front and back text.
- View cards → see IDs, front, and back.
- Study a stack → test yourself on the front, check if you know the back, and track your score.
- Delete stacks/cards → manage your collection.
- View study sessions → review past performance and progress.

📌 Notes
- This is my first time using DTOs — they helped me structure data flow between layers.
- The app emphasizes auditability and clean control flow.
- IDs are processed with Row_Number function in console to avoid gaps when items are deleted.

