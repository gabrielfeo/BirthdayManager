using System.Collections.Generic;

namespace ConsoleBirthdayManager.Commands.List
{
    public interface ICommandList : IReadOnlyList<ICommand>
    {
        int IndexOf(ICommand command);
    }
}