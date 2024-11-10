using Microsoft.EntityFrameworkCore;
using TelegramBotDotNet.Entities;

namespace TelegramBotDotNet.Services;

public class CityService : ICityService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public CityService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<IEnumerable<CityEntity>> GetAllCitiesAsync()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        return await dbContext.CityEntities.ToListAsync();
    }

    public async Task<CityEntity> GetCityByIdAsync(long id)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
        return await dbContext.CityEntities.FindAsync(id);
    }

    public async Task UpdateCityAsync(CityEntity user)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var existingUser = await dbContext.CityEntities.FindAsync(user.Id);
        
        if (existingUser != null)
        {
            existingUser.City = user.City;
            existingUser.Longitude = user.Longitude;
            existingUser.Latitude = user.Latitude;

            await dbContext.SaveChangesAsync();
        }
    }
}