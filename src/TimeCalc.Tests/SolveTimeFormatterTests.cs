using Xunit;
using TimeCalc.Utils;

namespace TimeCalc.Tests
{
    public class SolveTimeFormatterTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("1", "0.01")]
        [InlineData("12", "0.12")]
        [InlineData("123", "1.23")]
        [InlineData("1234", "12.34")]
        [InlineData("12345", "1:23.45")]
        [InlineData("123456", "12:34.56")]
        [InlineData("1234567", "1234567")]
        public void Format(string input, string output)
        {
            Assert.Equal(output, string.Format(new SolveTimeFormatter(), "{0}", input));
        }
    }
}