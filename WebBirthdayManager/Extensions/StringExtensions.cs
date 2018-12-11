namespace WebBirthdayManager.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsEmpty(this string str) => str.Equals(string.Empty);
        public static bool IsNotEmpty(this string str) => !str.IsEmpty();
    }
}