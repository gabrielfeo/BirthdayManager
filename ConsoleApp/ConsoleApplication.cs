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
            BirthdayManager.PresentPeople();
            LoopIndefinitely(action: PresentCommandsAndAskForAction);
        }

        private static void LoopIndefinitely(Action action)
        {
            while (true) action();
        }

        private static void PresentCommandsAndAskForAction()
        {
            BirthdayManager.PresentAvailableCommands();
            var command = BirthdayManager.AskForCommand(maxTries: 4);
            BirthdayManager.Execute(command);
        }
    }
}