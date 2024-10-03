namespace TelegramBotDotNet.DTOs;

public class HourlyDto
{
    public string[] time { get; set; }
    public float[] temperature_2m { get; set; }
    public int[] relative_humidity_2m { get; set; }
    public double[] precipitation { get; set; }
    public int[] weather_code { get; set; }
    public float[] wind_speed_10m { get; set; }
    public int[] wind_direction_10m { get; set; }
    public float[] uv_index { get; set; }
}