using System;

namespace ConsoleApp.Resources
{
    public struct Messages
    {
        public struct Instruction
        {
            public const string ChooseCommand =
                "Please decide what to do: ";
            public const string PressKeyToContinue =
                "Press any key to continue.";
            public const string ChoosePersonToDelete =
                "Please type the number of the birthday you\'d like to delete: ";
        }


        public struct Declaration
        {
            public const string Welcoming =
                "Welcome to the Birthday Manager, {0}!";
            public const string Header =
                "Birthday Manager";
            public const string ListingPeople =
                "These are all the upcoming birthdays, starting from the closest one:";
            public const string SelectedCommand =
                "You selected \"{0}\": {1}";
            public const string Exiting =
                "Bye!";
        }


        public struct Success
        {
            public const string AddedPerson =
                "Birthday added!";
            public const string DeletedPerson =
                "Birthday deleted successfully.";
        }


        public struct Error
        {
            public const string InvalidCommand =
                "That command is not available. Please try again.";
            public const string NoCommandToExecute =
                "There\'s nothing to do then. Should we try again?";
            public const string NoPeopleAdded =
                "There\'s no one here yet. Please add somebody\'s birthday to get started.";
            public const string AddFailed =
                "Couldn't add this birthday. Please try again.";
            public const string DeleteFailed =
                "Couldn\'t delete the given birthday. Please try again.";
            public const string GenericError =
                "Something went wrong. Please try again.";
        }
    }
}