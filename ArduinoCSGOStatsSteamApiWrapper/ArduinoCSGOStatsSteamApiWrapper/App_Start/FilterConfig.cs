using System.Web.Mvc;

namespace ArduinoCSGOStatsSteamApiWrapper
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}