using System;
using System.Collections.Generic;
using TimeCalc.Models;

namespace TimeCalc.Services
{
    public interface ISolveCalculator
    {
        SolveCalculations GetCalculations(Solve[] solves, string pb);
        IEnumerable<Tuple<int, float>> GetConvertedSolves(Solve[] solves);
        string GetAverage(float[] times);
        float ConvertResultToSeconds(string input);
        string ConvertSecondsToResult(float input);
    }
}