namespace TimeCalc.Utils
{
    public class SolveTimeFormatter : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!this.Equals(formatProvider))
                return null;

            string numericString = arg.ToString().Trim().Replace(":", "").Replace(".", "").TrimStart('0');

            if (numericString.Length == 0)
                return numericString;
            else if (numericString.Length == 1)
                return "0.0" + numericString;
            else if (numericString.Length == 2)
                return "0." + numericString;
            else if (numericString.Length == 3)
                return numericString.Substring(0, 1) + "." + numericString.Substring(1, 2);
            else if (numericString.Length == 4)
                return numericString.Substring(0, 2) + "." + numericString.Substring(2, 2);
            else if (numericString.Length == 5)
                return numericString.Substring(0, 1) + ":" + numericString.Substring(1, 2) + "." + numericString.Substring(3, 2);
            else if (numericString.Length == 6)
                return numericString.Substring(0, 2) + ":" + numericString.Substring(2, 2) + "." + numericString.Substring(4, 2);
            else
                return numericString;
        }
    }
}