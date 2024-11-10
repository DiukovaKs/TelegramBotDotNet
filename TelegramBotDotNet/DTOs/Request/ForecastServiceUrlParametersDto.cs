using TelegramBotDotNet.DTOs.Request;

namespace TelegramBotDotNet.DTOs.Request;

public class ForecastServiceUrlParametersDto()
{
    public string longitude { get; set; }
    public string latitude { get; set; }
    public HourlyParameters hourlyParameters { get; set; }
    public string windSpeedUnit { get; set; }
    public string timezone { get; set; }
    public int forecastDays { get; set; }
};

