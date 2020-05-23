using TimeCalc.Models;

namespace TimeCalc.Services
{
    public class SolveCalculator : ISolveCalculator
    {
        public SolveCalculations GetCalculations(Solve[] solves, string pb)
        {
            return new SolveCalculations { IncludedSolves = new int[] { 1, 2 } };
        }

        public int ConvertToCentiseconds(string input)
        {
            var decParts = input.Split('.');

            // get milliseconds
            var msec = 0;
            if(decParts.Length == 2)
            {
                var msecPart = decParts[1];
                while(msecPart.Length < 2)
                {
                    msecPart += "0";
                }
                if(msecPart.Length > 0)
                {
                    msecPart = msecPart.Substring(0, 2);
                }
                msec = int.Parse(msecPart);
            }

            // calculate seconds from minutes
            var sec = 0;
            var secParts = decParts[0].Split(':');
            if(secParts.Length == 1 && secParts[0].Length > 0)
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