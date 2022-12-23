namespace YannikG.TSBE.Webcrawler.Core.Utilities
{
    /// <summary>
    /// Collection of custom extensions for <seealso cref="string"/>
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// replaces common
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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