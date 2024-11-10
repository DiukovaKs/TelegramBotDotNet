using TelegramBotDotNet.DTOs;
using TelegramBotDotNet.DTOs.Request;
using TelegramBotDotNet.Entities;
using TelegramBotDotNet.Mappers;

namespace TelegramBotDotNet.Services;

public class WeatherForecastService
{
    public const long DefaultCityId = 1L;
    public const string DefaultCityName = "Wellington";
    
    private readonly ILogger<BotListener> _logger;
    private readonly HttpService _httpService;
    private readonly Mapper _mapper;
    private readonly ForecastUrlBuilder _forecastUrlBuilder;
    private readonly ICityService _cityService;
    private readonly IChatToCityService _chatToCityService;
    
    public WeatherForecastService(ILogger<BotListener> logger, 
        HttpService httpService, 
        Mapper mapper,
        ForecastUrlBuilder forecastUrlBuilder,
        ICityService cityService,
        IChatToCityService chatToCityService)
    {
        _logger = logger;
        _httpService = httpService;
        _mapper = mapper;
        _forecastUrlBuilder = forecastUrlBuilder;
        _cityService = cityService;
        _chatToCityService = chatToCityService;
    }
    
    public string GetWindForecastMessage(long chatId) {
        string forecastMessage;
        DateTime today = DateTime.Now;
        string formattedDate = today.ToString("d MMMM", new System.Globalization.CultureInfo("en-UK"));

        try {
            CurrentForecastDto dto = GetCurrentWeather(chatId);

            string header = string.Format("""
                                          Wind forecast for {0} {1} {2}
                                          """, dto.City, formattedDate, Environment.NewLine);
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

    public string GetWeatherForecastMessage(long chatId)
    {
        string forecastMessage;
        DateTime today = DateTime.Now;
        string formattedDate = today.ToString("d MMMM", new System.Globalization.CultureInfo("en-UK"));
    
        try
        {
            CurrentForecastDto dto = GetCurrentWeather(chatId);
            
            string header = string.Format("""
                                          Weather forecast for {0} {1} {2}
                                          """, dto.City, formattedDate, Environment.NewLine);

            string lines = string.Format($"""
                                           Time     Temp.°C      Hum. %       UV
                                            8 AM      {dto.Temperature[0]}  {dto.WeatherCode[0]}        {dto.Humidity[0]}          {dto.UvIndex[0]}
                                           12 PM      {dto.Temperature[1]}  {dto.WeatherCode[1]}        {dto.Humidity[1]}          {dto.UvIndex[1]}
                                            4 PM      {dto.Temperature[2]}  {dto.WeatherCode[2]}        {dto.Humidity[2]}          {dto.UvIndex[2]}
                                            8 PM      {dto.Temperature[3]}  {dto.WeatherCode[3]}        {dto.Humidity[3]}          {dto.UvIndex[3]}
                                          """);

            forecastMessage = header + lines;
        }
        catch (HttpRequestException ex)
        {
            forecastMessage = ex.ToString();
        }
        
        _logger.LogDebug(forecastMessage);

        return forecastMessage;
    }

    private CurrentForecastDto GetCurrentWeather(long chatId) {
        WeatherDto? dto;
        
        if (chatId == null) {
            dto = _httpService.FetchDataFromUrl(_forecastUrlBuilder.GetDefaultURL()).Result;

            if (dto != null)
            {
                dto.city = DefaultCityName;
            }
           
        } else {
            var cityId = _chatToCityService.GetCityId(chatId);

            if (cityId == null || cityId == 0) {
                throw new NullReferenceException("Choose city previously", new NullReferenceException("ChatToCityTable is empty"));
            }

            CityEntity city = _cityService.GetCityByIdAsync(cityId).Result;

            ForecastServiceUrlParametersDto parameters = _forecastUrlBuilder.GetDefaultParameters();
            parameters.latitude = city.Latitude.Replace(',', '.');
            parameters.longitude = city.Longitude.Replace(',', '.');

            string url = _forecastUrlBuilder.CreateForecastUrl(parameters);

            dto = _httpService.FetchDataFromUrl(url).Result;

            if (dto != null) {
                dto.city = city.City;
            }
        }
      
        return _mapper.ConvertToCurrentForecastDto(dto);
    }
}