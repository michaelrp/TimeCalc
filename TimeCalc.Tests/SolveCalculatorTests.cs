using System;
using Xunit;
using TimeCalc.Models;
using TimeCalc.Services;

namespace TimeCalc.Tests
{
    public class SolveCalculatorTests
    {
        [Theory]
        [InlineData("9.342", 934)]
        [InlineData(".42", 42)]
        [InlineData("43.1", 4310)]
        [InlineData("1:02.010", 6201)]
        [InlineData("1:0.1", 6010)]
        public void ConvertStringToSeconds(string input, int result)
        {
            var calc = new SolveCalculator();

            Assert.Equal(result, calc.ConvertToCentiseconds(input));
        }
    }    
}