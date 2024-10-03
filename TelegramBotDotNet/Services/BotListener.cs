using System.Runtime.InteropServices.JavaScript;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace TelegramBotDotNet.Services;

public class BotListener
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly ILogger<BotListener> _logger;
    private readonly WeatherForecastService _weatherForecastService;
    
    public BotListener(TelegramBotClient telegramBotClient, ILogger<BotListener> logger, WeatherForecastService weatherForecastService)
    {
        _telegramBotClient = telegramBotClient;
        _logger = logger;
        _weatherForecastService = weatherForecastService;
        Start();
    }

    private void Start() {
        _logger.LogInformation("BotListener is executing!!!!!!!!");
        _telegramBotClient.StartReceiving(Update, Error);
    }

    public void Execute() {
        _logger.LogInformation("MyService is executing!!!!!!!!");
    }

    private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var message = update.Message;

        if (message != null && message.Text != null)
        {
            switch (message.Text.ToLower())
            {
                case "/start":
                    StartCommandReceived(message.Chat.Id, "you send a start request", botClient);
                    break;
                case "/wind":
                    string windMessage = _weatherForecastService.GetWindForecastMessage(message.Chat.Id);
                    await botClient.SendTextMessageAsync(message.Chat.Id, windMessage);
                    break;
                case "/weather":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "you send a weather request");
                    break;
                default:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "I dont know this command!");
                    break;
            }
        }
    }
    
    private static void StartCommandReceived(long chatId, string name, ITelegramBotClient telegramBotClient) {
        string answer = "Hi, " + name + ", nice to meet you! \n" +
                        "To get weather condition for current day use command   /weather \n" +
                        "To get wind information for current day use command   /wind";

        telegramBotClient.SendTextMessageAsync(answer,  Convert.ToString(chatId));;
    }
    
    private static async Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
    {
        throw new NotImplementedException();
    }

}