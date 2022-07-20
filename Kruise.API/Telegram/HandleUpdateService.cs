using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Kruise.API.Telegram;
public class HandleUpdateService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<HandleUpdateService> _logger;
    private readonly string _chatName;

    public HandleUpdateService(ITelegramBotClient botClient, ILogger<HandleUpdateService> logger, IOptions<TelegramConfiguration> telegramConfiguration)
    {
        _botClient = botClient;
        _logger = logger;
        _chatName = telegramConfiguration.Value.ChatName;
    }

    public async Task SendPost()
    {
        var message = await _botClient.SendTextMessageAsync(
            chatId: _chatName,
            text: "Trying *all the parameters* of `sendMessage` method");
    }

    public async Task SendPostException()
    {
        var random = new Random();
        var value = random.Next(0, 2);
        if (value == 0)
        {
            var message = await _botClient.SendTextMessageAsync(
                chatId: _chatName,
                text: "Trying *all the parameters* of `sendMessage` method");
        }
        else
        {
            throw new Exception("exception");
        }
    }
}
