using TelegramBotDotNet.DTOs.Request;

namespace TelegramBotDotNet.DTOs.Request;

public record ForecastServiceUrlParametersDto(
     string longitude,
     string latitude,
     HourlyParameters hourlyParameters,
     string windSpeedUnit,
     string timezone,
     int forecastDays 
);

