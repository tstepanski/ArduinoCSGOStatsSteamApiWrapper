using System.Web.Configuration;

namespace ArduinoCSGOStatsSteamApiWrapper
{
    public sealed class Settings
    {
        private Settings()
        {
            AppId = WebConfigurationManager.AppSettings["AppId"];
            SteamKey = WebConfigurationManager.AppSettings["SteamKey"];
            PlayerId = WebConfigurationManager.AppSettings["PlayerId"];
            MaxWordWidth = int.Parse(WebConfigurationManager.AppSettings["MaxWordWidth"]);
            InacceptableCharacterRemoveRegex = WebConfigurationManager.AppSettings["InacceptableCharacterRemoveRegex"];
        }

        public static Settings Instance { get; } = new Settings();

        public string AppId { get; }
        public string SteamKey { get; }
        public string PlayerId { get; }
        public int MaxWordWidth { get; }
        public string InacceptableCharacterRemoveRegex { get; }
    }
}