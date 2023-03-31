using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCalc.Models;

namespace TimeCalc.Services
{
    public interface ILocalStorage
    {
        Task ClearAllDataAsync();
        Task<List<PersonalRecord>> GetPersonalRecordsAsync();
        Task<PuzzleRound> GetPuzzleRoundAsync(string id);
        Task<List<PuzzleRound>> GetPuzzleRoundsAsync();
        Task<WcaInfo> GetWcaInfoAsync();
        Task SavePersonalRecordsAsnyc(List<PersonalRecord> personalBests);
        Task SavePuzzleRounds(List<PuzzleRound> puzzleRounds);
        Task SaveWcaInfoAsync(WcaInfo wcaInfo);
        Task UpdateSolve(string roundId, int solveNumber, string solveResult);
    }

}