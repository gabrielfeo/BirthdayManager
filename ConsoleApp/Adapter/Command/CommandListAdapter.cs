using System.IO;
using System.Reflection.Metadata;
using ConsoleApp.Commands.List;
using ConsoleApp.Extensions;
using static ConsoleApp.Resources.Messages;

namespace ConsoleApp.Adapter.Command
{
    internal class CommandListAdapter : IConsoleAdapter<ICommandList>
    {
        private readonly TextWriter _writer;
        public TextWriter Writer => _writer;

        public CommandListAdapter(TextWriter writer)
        {
            _writer = writer;
        }

        public void Write(ICommandList commands)
        {
            foreach (var command in commands)
            {
                var commandKey = commands.IndexOf(command);
                Writer.WriteLine($"{commandKey}. {command.Description}");
            }
            Writer.SkipLine();
        }
    }
}