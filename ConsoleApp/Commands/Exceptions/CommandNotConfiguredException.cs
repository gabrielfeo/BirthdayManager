namespace ConsoleApp.Commands.Exceptions
{
    public class CommandNotConfiguredException : System.Exception
    {
        public CommandNotConfiguredException(string message) : base(message)
        {
        }
    }
}