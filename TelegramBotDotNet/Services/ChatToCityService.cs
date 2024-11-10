using Microsoft.EntityFrameworkCore;
using TelegramBotDotNet.Data;

namespace TelegramBotDotNet.Services;

public class ChatToCityService : IChatToCityService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public ChatToCityService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected async Task<ChatToCityEntity?> GetChatToCityByChatId(long chatId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
        return await dbContext.ChatToCityEntities.FirstOrDefaultAsync(ctc => ctc.ChatId == chatId);;
    }
    
    public long GetCityId(long chatId) {
        ChatToCityEntity? entity = GetChatToCityByChatId(chatId).Result;

        if (entity == null)
        {
            return 0;
        }
        
        return entity.CityId;
    }

    public void SetCityToChat(long chatId, long cityId) {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ChatToCityEntity? entity = GetChatToCityByChatId(chatId).Result;

        if (entity == null) {
            ChatToCityEntity newEntity = new ChatToCityEntity();
            newEntity.ChatId = chatId;
            newEntity.CityId = cityId;

            dbContext.ChatToCityEntities.AddAsync(newEntity);
            dbContext.SaveChangesAsync();
        } else if (!entity.CityId.Equals(cityId)) {
            entity.CityId = cityId;
            
            dbContext.ChatToCityEntities.Update(entity);
            dbContext.SaveChangesAsync(); 
        }
    }
}