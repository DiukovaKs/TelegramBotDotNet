namespace TelegramBotDotNet.DTOs.Response.Emoji;

public class WeatherCodeEmoji
{
    public static readonly WeatherCodeEmoji Sunny = new("0", "☀\uFE0F", "Clear sky");

    public static readonly WeatherCodeEmoji PartlyCloudy = new("1,2,3", "\uD83C\uDF24",
        "Mainly clear, partly cloudy, and overcast");

    public static readonly WeatherCodeEmoji Fog = new("45,48", "\uD83C\uDF2B",
        "Fog and depositing rime fog");

    public static readonly WeatherCodeEmoji Drizzle = new("51,53,55", "\uD83C\uDF26",
        "Drizzle: Light, moderate, and dense intensity");

    public static readonly WeatherCodeEmoji FreezingDrizzle =
        new("56,57", "\uD83C\uDF26", "Freezing Drizzle: Light and dense intensity");

    public static readonly WeatherCodeEmoji Rain = new("61,63,65", "\uD83C\uDF27",
        "Rain: Slight, moderate and heavy intensity");

    public static readonly WeatherCodeEmoji FreezingRain =
        new("66,67", "\uD83C\uDF27", "Freezing Rain: Light and heavy intensity");

    public static readonly WeatherCodeEmoji Snow = new("71,73,75", "\uD83C\uDF28",
        "Snow fall: Slight, moderate, and heavy intensity");

    public static readonly WeatherCodeEmoji SnowGrains = new("77", "\uD83C\uDF28", "Snow grains");

    public static readonly WeatherCodeEmoji RainShowers = new("80,81,82", "\uD83C\uDF27",
        "Rain showers: Slight, moderate, and violent");

    public static readonly WeatherCodeEmoji
        SnowShowers = new("85, 86", "\uD83C\uDF28", "Snow showers slight and heavy");

    public static readonly WeatherCodeEmoji Thunderstorm = new("95", "⛈", "Thunderstorm: Slight or moderate");

    public static readonly WeatherCodeEmoji ThunderstormWithLight =
        new("96,99", "⛈", "Thunderstorm with slight and heavy hail");

    public string Code { get; }
    public string Emoji { get; }
    public string Description { get; }

    private WeatherCodeEmoji(string code, string emoji, string description)
    {
        Code = code;
        Emoji = emoji;
        Description = description;
    }

    public static List<WeatherCodeEmoji> Values() => new List<WeatherCodeEmoji> {
        Sunny, 
        PartlyCloudy, 
        Fog, 
        Drizzle, 
        FreezingDrizzle, 
        Rain, 
        FreezingRain,
        Snow, 
        SnowGrains, 
        RainShowers,
        SnowShowers, 
        Thunderstorm, 
        ThunderstormWithLight
    };
}