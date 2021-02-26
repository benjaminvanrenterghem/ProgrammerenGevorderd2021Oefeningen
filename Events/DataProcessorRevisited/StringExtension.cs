// HOGENT

namespace ProcessorExample
{
    public static class StringExtension
    {
        public static string SurroundWith(this string s, string extra)
        {
            return extra + s + extra;
        }
    }
}
