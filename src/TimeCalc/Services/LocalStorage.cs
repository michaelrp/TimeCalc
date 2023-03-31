using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using TimeCalc.Models;

namespace TimeCalc.Services
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IJSRuntime jsRuntime;

        public LocalStorage(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task ClearAllDataAsync()
        {
            await SavePersonalRecordsAsnyc(null);
            await SavePuzzleRounds(null);
            await SaveWcaInfoAsync(null);
        }

        public async Task<List<PersonalRecord>> GetPersonalRecordsAsync()
            => await GetValueAsync<List<PersonalRecord>>("pbs");

        public async Task<PuzzleRound> GetPuzzleRoundAsync(string id)
            => (await GetPuzzleRoundsAsync()).FirstOrDefault(p => p.Id == id);

        public async Task<List<PuzzleRound>> GetPuzzleRoundsAsync()
            => await GetValueAsync<List<PuzzleRound>>("prs");

        public async Task<WcaInfo> GetWcaInfoAsync()
            => await GetValueAsync<WcaInfo>("wca");

        public async Task SavePersonalRecordsAsnyc(List<PersonalRecord> personalBests)
            => await SetValueAsync<List<PersonalRecord>>("pbs", personalBests);

        public async Task SavePuzzleRounds(List<PuzzleRound> puzzleRounds)
            => await SetValueAsync<List<PuzzleRound>>("prs", puzzleRounds);

        public async Task SaveWcaInfoAsync(WcaInfo wcaInfo)
            => await SetValueAsync<WcaInfo>("wca", wcaInfo);

        public async Task UpdateSolve(string roundId, int solveNumber, string solveResult)
        {
            if(string.IsNullOrEmpty(roundId))
            {
                throw new ArgumentNullException(nameof(roundId), "Must have a value.");
            }

            var rounds = await GetPuzzleRoundsAsync();
            if(rounds == null)
            {
                throw new NullReferenceException($"No rounds exist.");
            }

            var round = rounds.First(r => r.Id == roundId);
            if(round == null)
            {
                throw new NullReferenceException($"Puzzle round {roundId} not found in local store.");
            }

            if(solveNumber < 1 || solveNumber > round.Solves.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(solveNumber), $"Should be between 1 and {round.Solves.Count()}");
            }

            var solveIndex = solveNumber - 1;
            var solve = round.Solves[solveIndex];
            solve.Result = solveResult;
            round.Solves[solveIndex] = solve;

            await SavePuzzleRounds(rounds);
        }

        private async Task<T> GetValueAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Must not be null or empty");
            }

            var storageValue = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (storageValue != null)
            {
                return JsonSerializer.Deserialize<T>(storageValue);
            }
            else
            {
                return default(T);
            }
        }

        private async Task SetValueAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Must not be null or empty");
            }

            if (value == null)
            {
                await jsRuntime.InvokeAsync<object>("localStorage.removeItem", key);
            }
            else
            {
                var storageValue = JsonSerializer.Serialize(value);
                await jsRuntime.InvokeAsync<object>("localStorage.setItem", key, storageValue);
            }
        }
    }
}