namespace TelegramBotDotNet.DTOs;

public class WeatherDto
{
    public float longitude { get; set; }
    public float latitude { get; set; }
    public float elevation { get; set; }
    public float generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public HourlyDto hourly { get; set; }
    public HourlyUnitsDto hourly_units { get; set; }
   
}