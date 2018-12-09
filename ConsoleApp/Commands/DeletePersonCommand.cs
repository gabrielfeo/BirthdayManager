using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConsoleApp.Commands.List;
using ConsoleApp.Extensions;
using ConsoleApp.Resources;
using Entities;

namespace ConsoleApp.Commands
{
    internal class DeletePersonCommand : Command
    {
        public override string Name { get; } = "Delete Birthday";
        public override string Description { get; } = "Remove a birthday entry from the application.";

        public override IEnumerable<ICommand> Dependencies { get; } = new List<ICommand>()
        {
            new ListPeopleCommand()
        };

        public override void Execute()
        {
            VerifyProperties();
            var indexOfPersonToDelete = AskForIndexOfPersonToDelete();
            var deleteSuccessful = DeletePersonOnIndex(indexOfPersonToDelete);

            Writer.SkipLine();
            if (deleteSuccessful) Writer.WriteLine(Messages.Success.DeletedPerson);
            else ErrorWriter.WriteLine(Messages.Error.DeleteFailed);
        }

        private int AskForIndexOfPersonToDelete()
        {
            Writer.Write(Messages.Instruction.ChoosePersonToDelete);
            var userInput = Reader.ReadLine();
            bool parseSuccessful = int.TryParse(userInput, out var index);
            return parseSuccessful ? index : int.MinValue;
        }

        private bool DeletePersonOnIndex(int displayIndex)
        {
            bool deleteSuccessful = false;
            try
            {
                var personToBeDeleted = Repository.GetAll()
                                                  .OrderByDescending(person => person.Birthday.GetNextDate())
                                                  .ElementAt(displayIndex - 1);
                Repository.Delete(personToBeDeleted.Id, out deleteSuccessful);
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                       || ex is ArgumentOutOfRangeException)
            {
            }
            return deleteSuccessful;
        }
    }
}