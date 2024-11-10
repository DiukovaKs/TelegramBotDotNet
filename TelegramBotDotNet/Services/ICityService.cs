using TelegramBotDotNet.Entities;

namespace TelegramBotDotNet.Services;

public interface ICityService
{
    Task<IEnumerable<CityEntity>> GetAllCitiesAsync();
    Task<CityEntity> GetCityByIdAsync(long id);
    Task UpdateCityAsync(CityEntity user);
}