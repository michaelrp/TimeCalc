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
            await SavePersonalBestsAsnyc(null);
            await SavePuzzleRounds(null);
            await SaveWcaInfoAsync(null);
        }

        public async Task<List<PersonalBest>> GetPersonalBestsAsync()
            => await GetValueAsync<List<PersonalBest>>("pbs");

        public async Task<PuzzleRound> GetPuzzleRoundAsync(string id)
            => (await GetPuzzleRoundsAsync()).FirstOrDefault(p => p.Id == id);

        public async Task<List<PuzzleRound>> GetPuzzleRoundsAsync()
            => await GetValueAsync<List<PuzzleRound>>("prs");

        public async Task<WcaInfo> GetWcaInfoAsync()
            => await GetValueAsync<WcaInfo>("wca");

        public async Task SavePersonalBestsAsnyc(List<PersonalBest> personalBests)
            => await SetValueAsync<List<PersonalBest>>("pbs", personalBests);

        public async Task SavePuzzleRounds(List<PuzzleRound> puzzleRounds)
            => await SetValueAsync<List<PuzzleRound>>("prs", puzzleRounds);

        public async Task SaveWcaInfoAsync(WcaInfo wcaInfo)
            => await SetValueAsync<WcaInfo>("wca", wcaInfo);

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