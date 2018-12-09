using System;

namespace ConsoleBirthdayManager.Resources
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
            public const string ChoosePersonToUpdateName =
                "Please type the number of the person you\'d like to update: ";
            public const string TypeSearchQuery =
                "Search by name or birthday (month/day): ";
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
            public const string EditingPerson =
                "Editing {0}";
            public const string Exiting =
                "Bye!";
        }


        public struct Success
        {
            public const string AddedPerson =
                "Birthday added!";
            public const string DeletedPerson =
                "Successfully deleted {0}'s birthday.";
            public const string UpdatedPersonName =
                "Successfully updated {0}'s name to {1}.";
            public const string SearchResults =
                "Results:";
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
            public const string UpdateNameFailed =
                "Couldn\'t edit the given person. Please try again.";
            public const string SearchGotNoResults =
                "No one matching your description.";
            public const string GenericError =
                "Something went wrong. Please try again.";
        }
    }
}