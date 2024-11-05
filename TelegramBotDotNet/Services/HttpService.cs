using System.Text.Json;
using TelegramBotDotNet.DTOs;

namespace TelegramBotDotNet.Services;

public class HttpService
{
    private readonly HttpClient _client;

    public HttpService(HttpClient client)
    {
        _client = new HttpClient();
    }

    public async Task<WeatherDto> FetchDataFromUrl(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var responseString = await response.Content.ReadAsStringAsync();
            
            WeatherDto weatherData = JsonSerializer.Deserialize<WeatherDto>(responseString);
            return weatherData;
        }
        catch (JsonException jsonEx)  //JSON exceptions
        {
            Console.WriteLine($"Exceptions caused during JSON deserialization: {jsonEx.Message}");
            return null;
        }
        catch (Exception ex)  // Other exceptions
        {
            Console.WriteLine($"An error ocuured: {ex.Message}");
            return null;
        }
    }
}