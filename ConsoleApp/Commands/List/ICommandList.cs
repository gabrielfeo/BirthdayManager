using System.Collections.Generic;

namespace ConsoleApp.Commands.List
{
    public interface ICommandList : IReadOnlyList<ICommand>
    {
        int IndexOf(ICommand command);
    }
}