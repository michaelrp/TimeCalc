using System;
using System.Collections.Generic;
using System.Linq;
using TimeCalc.Models;

namespace TimeCalc.Services
{
    public class SolveCalculator : ISolveCalculator
    {
        private const string NA = " - ";

        public SolveCalculations GetCalculations(Solve[] solves, string pb)
        {
            var includedSolves = GetIncludedSolves(solves);
            var includedSolvesNumbers = includedSolves.Select(s => s.Item1).ToArray();

            var times = includedSolves.Select(s => s.Item2).ToArray();
            var currentAverage = GetCurrentAverage(times);
            var neededForNewPb = GetRemainingSolvesCount(solves) == 0 ? NA : GetNeededForNewPb(times, pb);

            return new SolveCalculations { IncludedSolves = includedSolvesNumbers, CurrentAverage = currentAverage, NeededForNewPB = neededForNewPb };
        }

        public IEnumerable<Tuple<int, float>> GetIncludedSolves(Solve[] solves)
        {
            var converted = solves.Where(s => !string.IsNullOrEmpty(s.Result))
                                  .Select(s => new Tuple<int, float>(s.Number, ConvertResultToSeconds(s.Result)))
                                  .OrderBy(s => s.Item2);

            return converted.Count() < 4 ? converted : converted.Skip(1).Take(3);
        }

        public string GetCurrentAverage(float[] times)
        {
            if(times.Count() < 3)
            {
                return NA;
            }

            // No round for float, so convert to a double and back?
            var avg = Math.Round((double)(times.Average()), 2);

            return ConvertSecondsToResult((float)avg);
        }

        public string GetNeededForNewPb(float[] times, string currentPb)
        {
            if(times.Count() < 2)
            {
                return NA;
            }

            var pb = ConvertResultToSeconds(currentPb);
            var roundedPb = Math.Round((double)pb, 2);

            // iterate through average calculation and round to find
            // needed difference in a single solve, will be 0.01 - 0.03
            // also, floating point math is :( when decimal places matter
            var neededDiff = 0.01d;
            var multipliedPb = Math.Round(3.0d * pb, 2);
            while(Math.Round(Math.Round(multipliedPb - neededDiff, 2) / 3.0, 2) == roundedPb)
            {
                neededDiff = Math.Round(neededDiff + 0.01, 2);
            }

            var needed = (float)Math.Round(multipliedPb - times[0] - times[1] - neededDiff, 2);

            return ConvertSecondsToResult(needed);
        }

        public int GetRemainingSolvesCount(Solve[] solves)
        {
            return solves.Count(s => string.IsNullOrEmpty(s.Result));
        }

        public float ConvertResultToSeconds(string input)
        {
            var decimalParts = input.Split(".");

            // truncate decimal
            var truncated = decimalParts[0];
            if (decimalParts.Length > 1)
            {
                truncated += ".";
                if (decimalParts[1].Length > 2)
                {
                    truncated += decimalParts[1].Substring(0, 2);
                }
                else
                {
                    truncated += decimalParts[1];
                }
            }

            var timeParts = truncated.Split(":");

            // convert minutes to seconds and parse to number
            float converted = 0.0f;
            if (timeParts.Length == 1)
            {
                // seconds
                if(timeParts[0].Length > 0)
                {
                    converted = float.Parse(timeParts[0]);
                }
            }
            else
            {
                // minutes
                if(timeParts[0].Length > 0)
                {
                    converted = float.Parse(timeParts[0]) * 60;
                }
                // seconds
                if(timeParts[1].Length > 0)
                {
                    converted += float.Parse(timeParts[1]);
                }
            }

            return converted;
        }

        public string ConvertSecondsToResult(float input)
        {
            var seconds = (int)Math.Truncate(input);
            var mins = seconds / 60;
            var secs = seconds % 60;
            var mils = "0";

            var parts = input.ToString().Split(".");
            if (parts.Length > 1)
            {
                mils = parts[1].Length > 2 ? parts[1].Substring(0, 2) : parts[1];
            }
            mils = mils.PadRight(2, '0');

            var result = "0.00";

            if (input < 60)
            {
                result = $"{secs}.{mils}";
            }
            else
            {
                result = $"{mins}:{secs.ToString("D2")}.{mils}";
            }

            return result;
        }

        [Obsolete]
        public int ConvertToCentiseconds(string input)
        {
            var decParts = input.Split('.');

            // get milliseconds
            var msec = 0;
            if (decParts.Length == 2)
            {
                var msecPart = decParts[1];
                while (msecPart.Length < 2)
                {
                    msecPart += "0";
                }
                if (msecPart.Length > 0)
                {
                    msecPart = msecPart.Substring(0, 2);
                }
                msec = int.Parse(msecPart);
            }

            // calculate seconds from minutes
            var sec = 0;
            var secParts = decParts[0].Split(':');
            if (secParts.Length == 1 && secParts[0].Length > 0)
            {
                // seconds
                sec = int.Parse(secParts[0]);
            }
            else if (secParts.Length == 2)
            {
                // minutes, seconds
                sec = int.Parse(secParts[0]) * 60 + int.Parse(secParts[1]);
            }

            var result = (sec * 100) + msec;
            return result;
        }
    }
}