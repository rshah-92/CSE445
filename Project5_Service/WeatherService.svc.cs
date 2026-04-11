using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace Project5_Service
{
    public class WeatherService : IWeatherService
    {
        private readonly Dictionary<string, (double lat, double lon)> cityMap =
            new Dictionary<string, (double, double)>(StringComparer.OrdinalIgnoreCase)
        {
            { "Phoenix", (33.4484, -112.0740) },
            { "Tempe", (33.4255, -111.9400) },
            { "Chandler", (33.3062, -111.8413) },
            { "Mesa", (33.4152, -111.8315) },
            { "Tucson", (32.2226, -110.9747) },
            { "Scottsdale", (33.4942, -111.9261) },
            { "Glendale", (33.5387, -112.1860) },
            { "Gilbert", (33.3528, -111.7890) },
            { "Peoria", (33.5806, -112.2374) }
        };

        public string GetWeather(string city, string date)
        {
            if (!cityMap.ContainsKey(city))
                return "City not supported.";

            if (!DateTime.TryParse(date, out DateTime parsed))
                return "Invalid date format. Use yyyy-MM-dd.";

            string dateStr = parsed.ToString("yyyy-MM-dd");

            var (lat, lon) = cityMap[city];

            string url =
                $"https://api.open-meteo.com/v1/forecast" +
                $"?latitude={lat}" +
                $"&longitude={lon}" +
                $"&daily=temperature_2m_max,temperature_2m_min,weathercode" +
                $"&timezone=auto" +
                $"&start_date={dateStr}" +
                $"&end_date={dateStr}";

            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dynamic data = js.Deserialize<dynamic>(json);

                    double tMax = Convert.ToDouble(data["daily"]["temperature_2m_max"][0]);
                    double tMin = Convert.ToDouble(data["daily"]["temperature_2m_min"][0]);
                    int code = Convert.ToInt32(data["daily"]["weathercode"][0]);

                    string desc = WeatherCodeToText(code);

                    return $"{city} on {dateStr}: {desc}, High {tMax}°C, Low {tMin}°C";
                }
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        private string WeatherCodeToText(int code)
        {
            switch (code)
            {
                case 0: return "Clear sky";
                case 1: return "Mainly clear";
                case 2: return "Partly cloudy";
                case 3: return "Overcast";
                case 45: return "Fog";
                case 48: return "Rime fog";
                case 51: return "Light drizzle";
                case 61: return "Slight rain";
                case 63: return "Moderate rain";
                case 65: return "Heavy rain";
                case 71: return "Slight snow";
                default: return "Unknown";
            }
        }
    }
    }

