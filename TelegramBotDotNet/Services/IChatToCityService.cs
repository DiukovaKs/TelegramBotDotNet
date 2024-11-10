using TelegramBotDotNet.Data;

namespace TelegramBotDotNet.Services;

public interface IChatToCityService
{
   public long GetCityId(long chatId);
   public void SetCityToChat(long chatId, long cityId);
}