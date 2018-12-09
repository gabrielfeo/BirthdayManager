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
            LoopIndefinitely(action: PresentCommandsAndAskForAction);
        }

        private static void LoopIndefinitely(Action action)
        {
            while (true) action();
        }

        private static void PresentCommandsAndAskForAction()
        {
            Console.Clear();
            BirthdayManager.PresentAvailableCommands();
            var command = BirthdayManager.AskForCommand(maxTries: 4);
            Pause(1000);
            Console.Clear();
            BirthdayManager.Execute(command);
            Console.ReadKey();
        }
        
        private static void Pause(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }
    }
}