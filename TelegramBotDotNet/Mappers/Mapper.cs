using TelegramBotDotNet.DTOs;
using TelegramBotDotNet.DTOs.Response.Emoji;

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
        newDto.WeatherCode = GetWeatherCodeEmoji(weatherCode);
        newDto.WindSpeed = windSpeed;
        newDto.WindDirection = GetWindDirection(windDirection);
        newDto.UvIndex = uvIndex;

        return newDto;
    }

    private string[] GetFourValues<T>(T[] array) {
        if (array == null) {
            return new string[4];
        }

        return new[]{array[7].ToString(), array[11].ToString(), array[15].ToString(), array[19].ToString()};
    }
    
        private string[] GetWindDirection(string[] windDirection) {
        if (windDirection == null) {
            return GetEmptyResultArray();
        }

        string[] result = new string[windDirection.Length];

        for (int i = 0; i < windDirection.Length; i++) {
            int degree = int.Parse(windDirection[i]);

            if (degree < 90 || degree > 270) {
                if (degree < 22 || degree > 338) {
                    result[i] = WindDirectionEmoji.North.Emoji + " " + WindDirectionEmoji.North.Abbreviation;
                } else if ((degree == 22 || degree > 22) && degree < 90) {
                    result[i] = WindDirectionEmoji.Northeast.Emoji + " " + WindDirectionEmoji.Northeast.Abbreviation;
                } else if ((degree == 338 || degree < 338 ) && degree > 270) {
                    result[i] = WindDirectionEmoji.Northwest.Emoji + " " + WindDirectionEmoji.Northwest.Abbreviation;
                }
            } else if (degree > 90 && degree < 270) {
                if (degree > 158 && degree < 202) {
                    result[i] = WindDirectionEmoji.South.Emoji + " " + WindDirectionEmoji.South.Abbreviation;
                } else if (degree == 158 || degree < 158) {
                    result[i] = WindDirectionEmoji.Southeast.Emoji + " " + WindDirectionEmoji.Southeast.Abbreviation;
                } else if (degree == 202 || degree > 202) {
                    result[i] = WindDirectionEmoji.Southwest.Emoji + " " + WindDirectionEmoji.Southwest.Abbreviation;
                }
            } else if (degree == 90) {
                result[i] = WindDirectionEmoji.East.Emoji + " " + WindDirectionEmoji.East.Abbreviation;
            } else {
                result[i] = WindDirectionEmoji.West.Emoji + " " + WindDirectionEmoji.West.Abbreviation;
            }
        }

        return result;
    }

    private string[] GetWeatherCodeEmoji(string[] weatherCode) {
        if (weatherCode == null) {
            return GetEmptyResultArray();
        }

        string[] result = new string[weatherCode.Length];

        for (int i = 0; i < weatherCode.Length; i++) {
            string code = weatherCode[i];

            List<WeatherCodeEmoji> codes = WeatherCodeEmoji.Values()
                .Where(e => e.Code.Contains(code))
                .ToList();

            result[i] = codes.FirstOrDefault()?.Emoji ?? string.Empty;
        }

        return result;
    }

    private string[] GetEmptyResultArray() {
        return new[]{"N/A", "N/A", "N/A", "N/A"};
    }
}