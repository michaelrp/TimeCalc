using System.Text;
using System.Web;
using TimeCalc.Models;
using TimeCalc.Services;

namespace TimeCalc.Utils
{
    public class SolveEncoder
    {
        private const string NA = " - ";

        public static string ToUrlEncode(
            PuzzleRound round,
            SolveCalculations calculations,
            PersonalRecord currentPr)
        {
            if (round == null)
            {
                return "";
            }

            var builder = new StringBuilder();

            builder.Append(round.Puzzle);
            if (!string.IsNullOrEmpty(round.Name))
            {
                builder.Append("%20");
                builder.Append(round.Name.Replace(" ", "%20"));
            }
            builder.Append("%0a");

            builder.Append("-----%0a");

            foreach (var solve in round.Solves)
            {
                builder.Append(solve.Number);
                builder.Append("%20-%20");
                builder.Append(solve.Result);
                builder.Append("%0a");
            }

            builder.Append("-----%0a");

            var hasCurrentAverage = calculations.CurrentAverage != NA;
            var hasNeededForNewPR = calculations.NeededForNewPr != NA;
            var hasBPA = calculations.BestPossibleAverage != NA;

            if (hasCurrentAverage)
            {
                builder.Append("Live%20");
                builder.Append(calculations.CurrentAverage);

                if (hasNeededForNewPR || hasBPA)
                {
                    builder.Append(",%20");
                }
            }

            if (hasNeededForNewPR)
            {
                builder.Append("For%20PR%20");
                builder.Append(calculations.NeededForNewPr);

                if (hasBPA)
                {
                    builder.Append(",%20");
                }
            }

            if (hasBPA)
            {
                builder.Append("BPA%20");
                builder.Append(calculations.BestPossibleAverage);
            }

            if (currentPr != null)
            {
                builder.Append("%0a-----%0a");
                builder.Append("PRs:%20Single%20");
                builder.Append(currentPr.Single);
                builder.Append("%20Avg%20");
                builder.Append(currentPr.Average);
            }

            return builder.ToString();
        }
    }
}
