using TimeCalc.Models;

namespace TimeCalc.Services
{
    public interface ISolveCalculator
    {
        SolveCalculations GetCalculations(Solve[] solves, string pb);        
    }
}