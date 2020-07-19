using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCalc.Models;

namespace TimeCalc.Services
{
    public interface ILocalStorage
    {
        Task ClearAllDataAsync();
        Task<List<PersonalBest>> GetPersonalBestsAsync();
        Task<PuzzleRound> GetPuzzleRoundAsync(string id);
        Task<List<PuzzleRound>> GetPuzzleRoundsAsync();
        Task<WcaInfo> GetWcaInfoAsync();
        Task SavePersonalBestsAsnyc(List<PersonalBest> personalBests);
        Task SavePuzzleRounds(List<PuzzleRound> puzzleRounds);
        Task SaveWcaInfoAsync(WcaInfo wcaInfo);
        Task UpdateSolve(string roundId, int solveNumber, string solveResult);
    }

}