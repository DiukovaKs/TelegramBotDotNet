using TelegramBotDotNet.DTOs;

namespace TelegramBotDotNet.Mappers;

public class Mapper
{
    public Mapper()
    {
    }

    public CurrentForecastDto ConvertToCurrentForecastDto(WeatherDto weatherDto) {
        CurrentForecastDto newDto = new CurrentForecastDto();
        
        string[] temperature = GetFourValues(weatherDto.hourly.temperature_2m);
        string[] humidity = GetFourValues(weatherDto.hourly.relative_humidity_2m);
        string[] precipitation = GetFourValues(weatherDto.hourly.precipitation);
        string[] weatherCode = GetFourValues(weatherDto.hourly.weather_code);
        string[] windSpeed = GetFourValues(weatherDto.hourly.wind_speed_10m);
        string[] windDirection = GetFourValues(weatherDto.hourly.wind_direction_10m);
        string[] uvIndex = GetFourValues(weatherDto.hourly.uv_index);

        newDto.Temperature = temperature;
        newDto.Humidity = humidity;
        newDto.WeatherCode = weatherCode;
        newDto.WindSpeed = windSpeed;
        newDto.WindDirection = windDirection;
        newDto.UvIndex = uvIndex;

        return newDto;
    }

    private string[] GetFourValues<T>(T[] array) {
        if (array == null) {
            return new string[4];
        }

        return new string[]{array[7].ToString(), array[11].ToString(), array[15].ToString(), array[19].ToString()};
    }
}