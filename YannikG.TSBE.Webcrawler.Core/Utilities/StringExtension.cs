namespace YannikG.TSBE.Webcrawler.Core.Utilities
{
    public static class StringExtension
    {
        public static string RemoveNewLine(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            str = str.Replace("\\n", "");
            str = str.Replace("\\r", "");

            return str;
        }
    }
}