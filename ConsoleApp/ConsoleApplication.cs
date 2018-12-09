using System;
using Entities;
using Repository;
using ConsoleApp.Adapter;
using ConsoleApp.Commands;
using ConsoleApp.Resources;

namespace ConsoleApp
{
    public class ConsoleApplication
    {
        // ReSharper disable once InconsistentNaming
        private static ConsoleBirthdayManager BirthdayManager;

        public static void Main(string[] args)
        {
            BirthdayManager = new ConsoleBirthdayManager(Console.Out, Console.In);
            BirthdayManager.PresentIntro();
            Pause(750);
            LoopIndefinitely(PresentCommands,
                             AskForAction,
                             WaitForKeyToContinue);
        }

        private static void PresentCommands()
        {
            Console.Clear();
            BirthdayManager.PresentAvailableCommands();
        }

        private static void AskForAction()
        {
            var command = BirthdayManager.AskForCommand(maxTries: 4);
            Pause(1250);
            ClearConsole();
            BirthdayManager.Execute(command);
        }

        public static void WaitForKeyToContinue()
        {
            BirthdayManager.DisplayMessage(Messages.Instruction.PressKeyToContinue);
            WaitForInteraction();
        }

        private static void LoopIndefinitely(params Action[] actions)
        {
            while (true)
                foreach (var action in actions)
                    action.Invoke();
        }

        private static void Pause(int milliseconds) => System.Threading.Thread.Sleep(milliseconds);
        private static void ClearConsole() => Console.Clear();
        private static void WaitForInteraction() => Console.ReadKey();
    }
}