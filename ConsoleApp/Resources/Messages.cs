using System;

namespace ConsoleApp.Resources
{
    public struct Messages
    {
        public const string APP_INTRO = "Welcome to the Birthday Manager!";
        public const string INSTRUCTION_CHOOSE_COMMAND = "Please type a command number to begin.";
        public const string ERROR_INVALID_COMMAND = "That command is not available. Please try again.";
        public const string ERROR_NO_PEOPLE_ADDED = "There's no one here yet. Please add somebody's birthday to continue.";
    }
}