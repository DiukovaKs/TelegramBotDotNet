namespace TelegramBotDotNet.DTOs.Request;

public record HourlyParameters(bool temperature2m, 
    bool relativeHumidity2m,
    bool precipitation,
    bool weatherCode,
    bool windSpeed10m,
    bool windDirection10m,
    bool uvIndex
    );