namespace ConsoleApp.Commands.Exception
{
    public class CommandNotConfiguredException : System.Exception
    {
        public CommandNotConfiguredException(string message) : base(message)
        {
        }
    }
}