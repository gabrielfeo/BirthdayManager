using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ConsoleBirthdayManager.Extensions;
using ConsoleBirthdayManager.Resources;
using Entities;

namespace ConsoleBirthdayManager.Commands
{
    internal class UpdatePersonNameCommand : Command
    {
        public override string Name { get; } = "Update Person name";
        public override string Description { get; } = "Edit a person's name in the Birthday Manager.";

        public override IEnumerable<ICommand> Dependencies { get; } = new List<ICommand>()
        {
            new ListPeopleCommand()
        };

        private string _originalName;

        public override void Execute()
        {
            VerifyProperties();
            var indexOfPersonToUpdate = AskForIndexOfPersonToUpdate();
            var person = FindPersonOnIndex(indexOfPersonToUpdate);
            if (person != null)
            {
                Writer.WriteLine(Messages.Declaration.EditingPerson, _originalName);
                person.Name = AskNewName();
                var updateSuccessful = Update(person);

                Writer.SkipLine();
                if (updateSuccessful) WriteUpdateSuccessfulMessage(person);
                else WriteUpdateFailedMessage();
            }
        }

        private int AskForIndexOfPersonToUpdate()
        {
            Writer.Write(Messages.Instruction.ChoosePersonToUpdateName);
            var userInput = Reader.ReadLine();
            bool parseSuccessful = int.TryParse(userInput, out var displayIndex);
            return parseSuccessful ? displayIndex : int.MinValue;
        }

        private Person FindPersonOnIndex(int displayIndex)
        {
            Person person = null;
            try
            {
                person = Repository.GetAll().ElementAt(displayIndex - 1);
                _originalName = person.Name;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                       || ex is ArgumentOutOfRangeException)
            {
            }
            return person;
        }

        private string AskNewName()
        {
            Writer.Write("New name: ");
            return Reader.ReadLine();
        }

        private bool Update(Person renamedPerson)
        {
            Repository.Update(renamedPerson, out bool successful);
            return successful;
        }
        
        private void WriteUpdateSuccessfulMessage(Person updatedPerson)
        {
            Writer.WriteLine(Messages.Success.UpdatedPersonName, _originalName, updatedPerson.Name);
            Repository.Get(updatedPerson);
        }

        private void WriteUpdateFailedMessage()
        {
            ErrorWriter.WriteLine(Messages.Error.UpdateNameFailed);
        }
    }
}