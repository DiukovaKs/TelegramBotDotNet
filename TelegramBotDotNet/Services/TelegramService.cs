using Telegram.Bot;

namespace TelegramBotDotNet.Services;

public class TelegramService
{
    private readonly TelegramBotClient _botClient;
    private readonly long _chatId;

    public TelegramService(string botToken, long chatId)
    {
        _botClient = new TelegramBotClient(botToken);
        _chatId = chatId;
    }

    public async Task SendMessage(string message)
    {
        await _botClient.SendTextMessageAsync(_chatId, message);
    }
}