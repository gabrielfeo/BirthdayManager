using System.IO;

namespace ConsoleBirthdayManager.Extensions
{
    internal static class TextWriterExtensions
    {
        public static void SkipLine(this TextWriter writer) => writer.WriteLine();
    }
}