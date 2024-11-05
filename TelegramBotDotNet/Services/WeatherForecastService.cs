using TelegramBotDotNet.DTOs;
using TelegramBotDotNet.Mappers;

namespace TelegramBotDotNet.Services;

public class WeatherForecastService
{
    private readonly ILogger<BotListener> _logger;
    private readonly HttpService _httpService;
    private readonly Mapper _mapper;
    private readonly ForecastUrlBuilder _forecastUrlBuilder;
    
    public WeatherForecastService(ILogger<BotListener> logger, 
        HttpService httpService, 
        Mapper mapper,
        ForecastUrlBuilder forecastUrlBuilder)
    {
        _logger = logger;
        _httpService = httpService;
        _mapper = mapper;
        _forecastUrlBuilder = forecastUrlBuilder;
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

        string url = _forecastUrlBuilder.getDefaultURL();
        
        dto = _httpService.FetchDataFromUrl(url).Result;

        return _mapper.ConvertToCurrentForecastDto(dto);
    }
}