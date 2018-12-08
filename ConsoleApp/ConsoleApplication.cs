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
            BirthdayManager.PresentPeople();
            BirthdayManager.PresentAllCommands();
            BirthdayManager.TryReadingCommand(maxTries: 4);
        }
    }
}