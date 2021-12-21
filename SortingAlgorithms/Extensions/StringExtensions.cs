namespace SortingAlgorithms.Extensions
{
    internal static class StringExtensions
    {
        internal static int CharAt(this string s, int i)
        {
            return s.Length <= i ? -1 : s[i];
        }
    }
}