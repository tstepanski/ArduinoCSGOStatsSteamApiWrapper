namespace ArduinoCSGOStatsSteamApiWrapper.Models
{
    internal sealed class Achievement
    {
        public string Name { get; set; }
        public byte Achieved { get; set; }
        public bool IsAchieved => Achieved == 1;
    }
}