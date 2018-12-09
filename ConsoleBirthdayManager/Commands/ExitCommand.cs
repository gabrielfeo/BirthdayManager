using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleBirthdayManager.Resources;

namespace ConsoleBirthdayManager.Commands
{
    internal class ExitCommand : Command
    {
        public override string Name { get; } = "Exit";
        public override string Description { get; } = "Exit the Birthday Manager.";

        public override IEnumerable<ICommand> Dependencies { get; } = Enumerable.Empty<ICommand>();

        public override void Execute()
        {
            Writer.WriteLine(Messages.Declaration.Exiting);
            Environment.Exit(0);
        }
    }
}