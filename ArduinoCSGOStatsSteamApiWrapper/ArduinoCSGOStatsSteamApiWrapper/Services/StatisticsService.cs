using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArduinoCSGOStatsSteamApiWrapper.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ArduinoCSGOStatsSteamApiWrapper.Services
{
    internal interface IStatisticsService
    {
        Task<IEnumerable<StatisticNameValuePair>> GetStatistics();
    }

    internal sealed class StatisticsService : IStatisticsService
    {
        private readonly Settings _settings;

        public StatisticsService()
        {
            _settings = Settings.Instance;
        }

        public async Task<IEnumerable<StatisticNameValuePair>> GetStatistics()
        {
            var valveStatistics = await GetValveStatistics();

            var desiredStatistics = GetOnlyDesiredStatistics(valveStatistics.PlayerStats.Stats);

            return desiredStatistics.Select(statistic => new StatisticNameValuePair
            {
                Name = GetNameStringFormatted(statistic.Name),
                Value = statistic.Value.ToString()
            });
        }

        private string GetNameStringFormatted(string name)
        {
            var nameTrimmed = name.Replace(@"total_kills_", @"").Trim();

            var nameCleaned = Regex.Replace(nameTrimmed, _settings.InacceptableCharacterRemoveRegex, @"");

            var nameCut = nameCleaned.Substring(0, Math.Min(nameCleaned.Length, _settings.MaxWordWidth));

            return nameCut.ToUpper();
        }

        private static IEnumerable<Statistic> GetOnlyDesiredStatistics(IEnumerable<Statistic> statistics)
        {
            return statistics
                .Where(statistic => statistic.Name.StartsWith(@"total_kills_", StringComparison.OrdinalIgnoreCase) && statistic.Name.Count(character => character == '_') < 3);
        }

        private async Task<ValveStatistics> GetValveStatistics()
        {
            var url =
                $"ISteamUserStats/GetUserStatsForGame/v0002/?appid={_settings.AppId}&key={_settings.SteamKey}&steamid={_settings.PlayerId}";

            var restClient = new RestClient(@"http://api.steampowered.com");
            var restRequest = new RestRequest(url);

            var result = await restClient.ExecuteGetTaskAsync(restRequest);

            var valveStatistics = JsonConvert.DeserializeObject<ValveStatistics>(result.Content);

            return valveStatistics;
        }
    }
}