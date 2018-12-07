using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Commands
{
    class AddPersonCommand : ICommand
    {

        private const string ADD_PERSON_DESCRIPTION = "Add Birthday";
        public string Description
        {
            get => ADD_PERSON_DESCRIPTION;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
