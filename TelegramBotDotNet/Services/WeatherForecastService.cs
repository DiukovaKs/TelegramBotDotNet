using TelegramBotDotNet.DTOs;
using TelegramBotDotNet.Mappers;

namespace TelegramBotDotNet.Services;

public class WeatherForecastService
{
    private readonly ILogger<BotListener> _logger;
    private readonly HttpService _httpService;
    private readonly Mapper _mapper;
    
    public WeatherForecastService(ILogger<BotListener> logger, HttpService httpService, Mapper mapper)
    {
        _logger = logger;
        _httpService = httpService;
        _mapper = mapper;
    }
    
    public string GetWindForecastMessage(long chatId) {
        string forecastMessage;
        DateTime today = DateTime.Now;
        string formattedDate = today.ToString("d MMMM", new System.Globalization.CultureInfo("en-UK"));

        try {
            CurrentForecastDto dto = GetCurrentWeather(chatId);

            string header = string.Format("""
                                          Wind forecast for Wellington {0} {1}
                                          """, formattedDate, Environment.NewLine);
            string lines = string.Format("""
                                           8 AM      {0}      {1}  m/s
                                          12 PM      {2}      {3}  m/s
                                           4 PM      {4}      {5}  m/s
                                           8 PM      {6}      {7}  m/s
                                         """, dto.WindDirection[0], dto.WindSpeed[0],
                dto.WindDirection[1], dto.WindSpeed[1],
                dto.WindDirection[2], dto.WindSpeed[2],
                dto.WindDirection[3], dto.WindSpeed[3]);

            forecastMessage = header + lines;
        } catch (HttpRequestException ex) {
            forecastMessage = ex.ToString();
        }

        _logger.LogDebug(forecastMessage);

        return forecastMessage;
    }
    
    private CurrentForecastDto GetCurrentWeather(long chatId) {
        WeatherDto dto;

        string url = "https://api.open-meteo.com/v1/forecast?latitude=-41.2283&longitude=174.8702&hourly=temperature_2m,relative_humidity_2m,precipitation,weather_code,wind_speed_10m,wind_direction_10m,uv_index&wind_speed_unit=ms&timezone=Pacific%2FAuckland&forecast_days=1";

        dto = _httpService.FetchDataFromUrl(url).Result;

        return _mapper.ConvertToCurrentForecastDto(dto);
    }
}