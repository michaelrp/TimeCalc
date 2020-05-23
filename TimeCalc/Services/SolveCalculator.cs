using TimeCalc.Models;

namespace TimeCalc.Services
{
    public class SolveCalculator : ISolveCalculator
    {
        public SolveCalculations GetCalculations(Solve[] solves, string pb)
        {
            return new SolveCalculations { IncludedSolves = new int[] { 1, 2 } };
        }

        /*
    private convertResultToSeconds(input: string): number {
        let result = 0.0;

        // truncate decimal
        let truncated: string;
        const decimalParts = input.split('.');
        if (decimalParts.length > 1) {
            truncated = decimalParts[0] + '.' + decimalParts[1].substring(0, 2);
        } else {
            truncated = decimalParts[0];
        }

        // convert minutes to seconds and parse to number
        let converted = 0.0;
        const timeParts = truncated.split(':');
        if (timeParts.length === 1) {
            // seconds
            converted = parseFloat(timeParts[0]);
        } else if (timeParts.length === 2) {
            // minutes, seconds
            converted = parseFloat(timeParts[0]) * 60 + parseFloat(timeParts[1]);
        }

        if (!Number.isNaN(converted)) {
            result = converted;
        }

        // console.log('convertResultToSeconds', input, result);

        return result;
    }

        */
    }
}