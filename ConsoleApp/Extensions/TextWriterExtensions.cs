using System.IO;

namespace ConsoleApp.Extensions
{
    internal static class TextWriterExtensions
    {
        public static void SkipLine(this TextWriter writer) => writer.WriteLine();
    }
}