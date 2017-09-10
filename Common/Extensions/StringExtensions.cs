namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string PadCenter(this string str, int length, char leftPaddingChar = ' ', char rightPaddingChar = ' ')
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft, leftPaddingChar).PadRight(length, rightPaddingChar);
        }
    }
}
