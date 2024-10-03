namespace TelegramBotDotNet.DTOs;

public class HourlyUnitsDto
{
    string time { get; set; }
    string temperature_2m { get; set; }
    string relative_humidity_2m { get; set; }
    string rain { get; set; }
}