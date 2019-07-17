namespace PictureCopier.Form.Helpers
{
    public static class StringExtensions
    {
        public static string FormatString(this string stringToFormat,params string[] values)
        {
            return string.Format(stringToFormat, values);
        }


        public static string ReplaceIllegalCharacters(this string value)
        {
            var illegalChars = new[] { "/", @"\", ":", "*", "?", "\"", "<", ">", "|" };

            foreach (var oldValue in illegalChars)
            {
                value=value.Replace(oldValue, "");
            }

            return value;
        }
    }
}