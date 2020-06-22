using System;
using System.Linq;
using Xunit;
using TimeCalc.Models;
using TimeCalc.Services;

namespace TimeCalc.Tests
{
    public class SolveCalculatorTests
    {
        [Theory]
        [InlineData("54.12", "56.88", "1:00.85", "1:04.97", "1:04.22", "1:21.07", "1:00.65", " - ", 2, 3, 5)]
        public void GetCalculations(string s1, string s2, string s3, string s4, string s5, string pb, 
            string avg, string needed, params int[] results)
        {
            Solve[] solves = {
                new Solve { Number = 1, Result = s1 },
                new Solve { Number = 2, Result = s2 },
                new Solve { Number = 3, Result = s3 },
                new Solve { Number = 4, Result = s4 },
                new Solve { Number = 5, Result = s5 }
            };

            var calc = new SolveCalculator();
            var calculations = calc.GetCalculations(solves, pb);

            Assert.Equal(avg, calculations.CurrentAverage);
            Assert.Equal(needed, calculations.NeededForNewPB);

            var orderedResults = calc.GetIncludedSolves(solves)
                        .OrderBy(s => s.Item1)
                        .Select(s => s.Item1).ToArray();

            Assert.Equal(results, orderedResults);
        }

        [Fact]
        public void GetSolvesCompleted()
        {
            var calc = new SolveCalculator();
            var solves = Enumerable.Range(1, 5).Select(n => new Solve { Number = n, Result = "" }).ToArray();

            var count = solves.Count();
            Assert.Equal(count, calc.GetRemainingSolvesCount(solves));
            for(int i = 0; i < count; i++)
            {
                var remaining = count - 1 - i;
                solves[i].Result = $"9.0";

                Assert.Equal(remaining, calc.GetRemainingSolvesCount(solves));
            }


            solves = Enumerable.Range(1, 3).Select(n => new Solve { Number = n, Result = "" }).ToArray();
            count = solves.Count();
            Assert.Equal(count, calc.GetRemainingSolvesCount(solves));
            for(int i = 0; i < count; i++)
            {
                var remaining = count - 1 - i;
                solves[i].Result = $"9.0";

                Assert.Equal(remaining, calc.GetRemainingSolvesCount(solves));
            }
        }

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

        [Theory]
        [InlineData("7.71", "8.77", "7.93", "9.89", "8.39", 2, 3, 5)]
        [InlineData("42.63", "54.83", "38.05", "39.73", "48.26", 1, 4, 5)]
        [InlineData("1:13.91", "1:09.15", "52.52", "48.19", "44.33", 2, 3, 4)]
        [InlineData("0.68", "1.98", "1.99", "1.93", "1.80", 2, 4, 5)]
        [InlineData("1:06.54", "1:01.79", "1:35.42", "1:03.27", "1:09.14", 1, 4, 5)]
        public void GetIncludedSolves(string s1, string s2, string s3, string s4, string s5, params int[] results)
        {
            Solve[] solves = {
                new Solve { Number = 1, Result = s1 },
                new Solve { Number = 2, Result = s2 },
                new Solve { Number = 3, Result = s3 },
                new Solve { Number = 4, Result = s4 },
                new Solve { Number = 5, Result = s5 }
            };

            var calc = new SolveCalculator();
            var orderedResults = calc.GetIncludedSolves(solves)
                                    .OrderBy(s => s.Item1)
                                    .Select(s => s.Item1).ToArray();

            Assert.Equal(results, orderedResults);
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

        [Theory]
        [InlineData("24.91", "28.93", 29.36f, 32.5f, 35.85f)]
        [InlineData("7.50", "8.36", 7.3f, 10.26f, 10.33f)]
        public void GetNeededForNewPb(string result, string pb, params float[] times)
        {
            var calc = new SolveCalculator();

            Assert.Equal(result, calc.GetNeededForNewPb(times, pb));
        }
    }    
}
