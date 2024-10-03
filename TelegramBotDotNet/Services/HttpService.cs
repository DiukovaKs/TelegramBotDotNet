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
        catch (JsonException jsonEx)  // Исключения, связанные с JSON
        {
            Console.WriteLine($"Ошибка при десериализации JSON: {jsonEx.Message}");
            return null;
        }
        catch (Exception ex)  // Ловим другие общие ошибки
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
            return null;
        }
    }
}