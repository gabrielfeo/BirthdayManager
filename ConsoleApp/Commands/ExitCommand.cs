using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ConsoleApp.Resources;

namespace ConsoleApp.Commands
{
    internal class ExitCommand : Command
    {
        public override string Name { get; } = "Exit";
        public override string Description { get; } = "Exit the Birthday Manager.";

        public override IEnumerable<ICommand> Dependencies { get; } = ImmutableList<ICommand>.Empty;

        public override void Execute()
        {
            Writer.WriteLine(Messages.Declaration.Exiting);
            Environment.Exit(0);
        }
    }
}