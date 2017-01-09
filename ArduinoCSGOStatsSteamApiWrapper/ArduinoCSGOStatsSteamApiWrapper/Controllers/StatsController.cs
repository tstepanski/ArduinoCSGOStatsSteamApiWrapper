using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ArduinoCSGOStatsSteamApiWrapper.Services;

namespace ArduinoCSGOStatsSteamApiWrapper.Controllers
{
    public sealed class StatsController : ApiController
    {
        private readonly IStatisticsService _statisticsService;

        public StatsController()
        {
            _statisticsService = new StatisticsService();
        }

        public async Task<string> Get()
        {
            var statistics = await _statisticsService.GetStatistics();

            var text = statistics
                .Aggregate(new StringBuilder(), (previous, next) => previous.Append($"{next.Name},{next.Value},"))
                .ToString();

            return text.Substring(0, text.Length - 1);
        }
    }
}