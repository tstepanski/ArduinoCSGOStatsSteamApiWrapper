namespace ArduinoCSGOStatsSteamApiWrapper.Models
{
    internal sealed class PlayerStatistics
    {
        public string SteamId { get; set; }
        public string GameName { get; set; }
        public Statistic[] Stats { get; set; }
        public Achievement[] Achievements { get; set; }
    }
}