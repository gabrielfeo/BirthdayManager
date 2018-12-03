using System;
using Entities;
using Repository;
using ConsoleApp.Adapter;

namespace ConsoleApp
{
    public class ConsoleEntryPoint
    {
        public static void Main(string[] args)
        {
            new ConsoleBirthdayManager().Start();
        }
    }
}