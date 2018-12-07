using System;

namespace ConsoleApp.Resources
{
    public struct Messages
    {
        public const string AppIntro =
            "Welcome to the Birthday Manager!";

        public struct Instruction
        {
            public const string ChooseCommand =
                "Please type a command number to begin.";
        }

        public struct Error
        {
            public const string InvalidCommand =
                "That command is not available. Please try again.";

            public const string NoPeopleAdded =
                "There's no one here yet. Please add somebody's birthday to continue.";
        }
    }
}