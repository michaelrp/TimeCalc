using System;
using System.Collections.Generic;
using System.Linq;
using TimeCalc.Models;

namespace TimeCalc.Services
{
    public class SolveCalculator : ISolveCalculator
    {
        public SolveCalculations GetCalculations(Solve[] solves, string pb)
        {
            var includedSolvesWithConvertedTime = GetIncludedSolves(solves);
            var includedSolves = includedSolvesWithConvertedTime.Select(s => s.Item1).ToArray();

            var currentAverage = "";
            var neededForNewPb = "";

            return new SolveCalculations { IncludedSolves = includedSolves, CurrentAverage = currentAverage, NeededForNewPB = neededForNewPb };
        }

        public string GetCurrentAverage(float[] times)
        {
            if(times.Count() < 3)
            {
                return " - ";
            }
            
            return ConvertSecondsToResult(times.Average());
        }
        /*

        getAverage(): string {
            let average = 0.0;

            const times = this.getIncludedTimes();

            if (times.length > 0) {
                average = this.round(times.reduce((a, c) => a + c, 0.0) / times.length);
            }

            const result = this.convertSecondsToResult(average);

            // console.log('getAverage', average, result);

            return result;
        }

        */
        public IEnumerable<Tuple<int, float>> GetIncludedSolves(Solve[] solves)
        {
            var converted = solves.Where(s => !string.IsNullOrEmpty(s.Result))
                                  .Select(s => new Tuple<int, float>(s.Number, ConvertResultToSeconds(s.Result)))
                                  .OrderBy(s => s.Item2);

            return converted.Count() < 4 ? converted : converted.Skip(1).Take(3);
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
            float converted;
            if (timeParts.Length == 1)
            {
                // seconds
                converted = float.Parse(timeParts[0]);
            }
            else
            {
                // minutes, seconds
                converted = float.Parse(timeParts[0]) * 60 + float.Parse(timeParts[1]);
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