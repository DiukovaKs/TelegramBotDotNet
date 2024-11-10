namespace TelegramBotDotNet.DTOs;

public class CurrentForecastDto
{
    public string City { get; set; }
    public string[] Temperature { get; set; }
    public string[] Humidity { get; set; }
    public string[] WeatherCode { get; set; }
    public string[] WindSpeed { get; set; }
    public string[] WindDirection { get; set; }
    public string[] UvIndex { get; set; }
}