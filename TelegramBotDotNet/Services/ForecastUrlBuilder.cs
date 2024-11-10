using System.Text;
using TelegramBotDotNet.DTOs.Request;

namespace TelegramBotDotNet.Services;

public class ForecastUrlBuilder
{
    private const string Url = "https://api.open-meteo.com/v1/forecast?";
    
    public ForecastUrlBuilder()
    {
    }
    
    public string CreateForecastUrl(ForecastServiceUrlParametersDto parametersDto) {
        string result;

        StringBuilder builder = new StringBuilder();
        builder.Append(Url)
                .Append("latitude=")
                .Append(parametersDto.latitude)
                .Append("&longitude=")
                .Append(parametersDto.longitude)
                .Append("&hourly=");

        if (parametersDto.hourlyParameters.temperature2m) {
            builder.Append("temperature_2m,");
        }
        if (parametersDto.hourlyParameters.relativeHumidity2m) {
            builder.Append("relative_humidity_2m,");
        }
        if (parametersDto.hourlyParameters.precipitation) {
            builder.Append("precipitation,");
        }
        if (parametersDto.hourlyParameters.weatherCode) {
            builder.Append("weather_code,");
        }
        if (parametersDto.hourlyParameters.windSpeed10m) {
            builder.Append("wind_speed_10m,");
        }
        if (parametersDto.hourlyParameters.windDirection10m) {
            builder.Append("wind_direction_10m,");
        }
        if (parametersDto.hourlyParameters.uvIndex) {
            builder.Append("uv_index");
        }

        builder.Append("&wind_speed_unit=")
                .Append(parametersDto.windSpeedUnit)
                .Append("&timezone=")
                .Append(parametersDto.timezone)
                .Append("&forecast_days=")
                .Append(parametersDto.forecastDays);

        result = builder.ToString();

        return result;
    }

    public string GetDefaultURL() {
        return CreateForecastUrl(GetDefaultParameters());
    }

    public ForecastServiceUrlParametersDto GetDefaultParameters() {
        HourlyParameters hourlyParameters = new HourlyParameters(true,
            true,
            true,
            true,
            true,
            true, 
            true);

        ForecastServiceUrlParametersDto parametersDto = new ForecastServiceUrlParametersDto();
        parametersDto.latitude = "174.7756";
        parametersDto.longitude = "-41.2866";
        parametersDto.hourlyParameters = hourlyParameters;
        parametersDto.windSpeedUnit = "ms";
        parametersDto.timezone = "Pacific/Auckland";
        parametersDto.forecastDays = 1;

        return parametersDto;
    }

}