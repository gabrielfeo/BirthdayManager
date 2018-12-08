using System;
using ConsoleApp.Commands.Exception;

namespace ConsoleApp.Commands
{
    internal static class CommandVerifier
    {
        public static void Verify(ICommand command)
        {
            if (command.Repository is null)
                throw new CommandNotConfiguredException("Repository not configured in Command");
            if (command.Writer is null)
                throw new CommandNotConfiguredException("Writer not configured in Command");
            if (command.Reader is null)
                throw new CommandNotConfiguredException("Reader not configured in Command");
        }
    }
}