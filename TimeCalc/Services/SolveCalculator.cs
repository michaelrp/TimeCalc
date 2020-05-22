using TimeCalc.Models;

namespace TimeCalc.Services
{
    public class SolveCalculator : ISolveCalculator
    {
        public SolveCalculations GetCalculations(Solve[] solves, string pb)
        {
            return new SolveCalculations { IncludedSolves = new int[0] };
        }
    }
}