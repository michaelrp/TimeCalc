using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCalc.Models;

namespace TimeCalc.Services
{
    public interface ILocalStorage
    {
        Task ClearAllDataAsync();
        Task<List<PersonalBest>> GetPersonalBestsAsync();
        Task<List<PuzzleRound>> GetPuzzleRoundsAsync();
        Task<WcaInfo> GetWcaInfoAsync();
        Task SavePersonalBestsAsnyc(List<PersonalBest> personalBests);
        Task SavePuzzleRounds(List<PuzzleRound> puzzleRounds);
        Task SaveWcaInfoAsync(WcaInfo wcaInfo);
    }

}