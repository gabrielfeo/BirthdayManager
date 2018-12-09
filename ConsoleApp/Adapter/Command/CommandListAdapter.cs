using System.IO;
using System.Reflection.Metadata;
using ConsoleApp.Commands.List;
using ConsoleApp.Extensions;
using static ConsoleApp.Resources.Messages;

namespace ConsoleApp.Adapter.Command
{
    internal class CommandListAdapter : IConsoleAdapter<ICommandList>
    {
        public TextWriter Writer { get; }

        public CommandListAdapter(TextWriter writer)
        {
            Writer = writer;
        }

        public void Write(ICommandList commands)
        {
            foreach (var command in commands)
            {
                var commandKey = commands.IndexOf(command);
                Writer.WriteLine($"{commandKey}. {command.Name}");
            }
        }
    }
}