using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Ledana
{
    internal class Enums
    {
        internal enum MainMenuOptions
        {
            ManageStacks,
            ManageFlashCards,
            Study,
            ViewStudySession,
            Quit
        }
        internal enum ManageStacksOptions
        {
            AddStack,
            DeleteStack,
            ViewStack,
            ViewAllStacks,
            GoBack
        }
        internal enum ManageFlashCardsOptions
        {
            AddFlashCard,
            DeleteFlashCard,
            GoBack
        }
    }
}
