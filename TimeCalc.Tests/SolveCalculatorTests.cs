using System;
using System.Linq;
using Xunit;
using TimeCalc.Models;
using TimeCalc.Services;

namespace TimeCalc.Tests
{
    public class SolveCalculatorTests
    {
        // [Theory]
        // [InlineData("9.342", 934)]
        // [InlineData(".42", 42)]
        // [InlineData("43.1", 4310)]
        // [InlineData("1:02.010", 6201)]
        // [InlineData("1:0.1", 6010)]
        // public void ConvertStringToSeconds(string input, int result)
        // {
        //     var calc = new SolveCalculator();

        //     Assert.Equal(result, calc.ConvertToCentiseconds(input));
        // }

        [Theory]
        [InlineData("9.342", 9.34f)]
        [InlineData(".42", 0.42f)]
        [InlineData("43.10", 43.1f)]
        [InlineData("43.1", 43.1f)]
        [InlineData("43.100", 43.1f)]
        [InlineData("43.109", 43.1f)]
        [InlineData("1:02.010", 62.01f)]
        [InlineData("1:0.1", 60.1f)]
        public void ConvertResultToSeconds(string input, float result)
        {
            var calc = new SolveCalculator();

            Assert.Equal(result, calc.ConvertResultToSeconds(input));
        }

        [Theory]
        [InlineData(9.342f, "9.34")]
        [InlineData(8.0f, "8.00")]
        [InlineData(.42f, "0.42")]
        [InlineData(43.1f, "43.10")]
        [InlineData(43.109, "43.10")]
        [InlineData(62.01f, "1:02.01")]
        [InlineData(62.019f, "1:02.01")]
        [InlineData(62.0109f, "1:02.01")]
        [InlineData(60.1f, "1:00.10")]
        [InlineData(60.0001f, "1:00.00")]
        public void ConvertSecondsToResult(float input, string result)
        {
            var calc = new SolveCalculator();

            Assert.Equal(result, calc.ConvertSecondsToResult(input));
        }

/*
8.36		7.71	8.77	7.93	9.89	8.39
43.54		42.63	54.83	38.05	39.73	48.26
56.62		1:13.91	1:09.15	52.52	48.19	44.33
1.90		0.68	1.98	1.99	1.93	1.80
1:06.32		1:06.54	1:01.79	1:35.42	1:03.27	1:09.14
*/
        [Theory]
        [InlineData("7.71", "8.77", "7.93", "9.89", "8.39", 2, 3, 5)]
        [InlineData("42.63", "54.83", "38.05", "39.73", "48.26", 1, 4, 5)]
        [InlineData("1:13.91", "1:09.15", "52.52", "48.19", "44.33", 2, 3, 4)]
        [InlineData("0.68", "1.98", "1.99", "1.93", "1.80", 2, 4, 5)]
        [InlineData("1:06.54", "1:01.79", "1:35.42", "1:03.27", "1:09.14", 1, 4, 5)]
        public void GetIncludedSolves(string s1, string s2, string s3, string s4, string s5, params int[] included)
        {
            Solve[] solves = {
                new Solve { Number = 1, Result = s1 },
                new Solve { Number = 2, Result = s2 },
                new Solve { Number = 3, Result = s3 },
                new Solve { Number = 4, Result = s4 },
                new Solve { Number = 5, Result = s5 }
            };

            var calc = new SolveCalculator();
            var result = calc.GetIncludedSolves(solves);
            var orderedResult = result.OrderBy(r => r.Item1).Select(r => r.Item1).ToArray();

            Assert.Equal(included, orderedResult);
        }

        [Theory]
        [InlineData("8.36", 8.77f, 7.93f, 8.39f)]
        [InlineData("43.54", 42.63f, 39.73f, 48.26f)]
        [InlineData("1:06.32", 66.54f, 63.27f, 69.14f)]
        [InlineData(" - ", 7.86f, 8.01f)]
        public void GetCurrentAverage(string result, params float[] times)
        {
            var calc = new SolveCalculator();

            Assert.Equal(result, calc.GetCurrentAverage(times));
        }
    }    
}