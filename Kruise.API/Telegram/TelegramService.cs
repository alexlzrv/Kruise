using Kruise.Domain;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Kruise.API.Telegram;
public class TelegramService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramService> _logger;
    private readonly string _chatName;

    public TelegramService(ITelegramBotClient botClient, ILogger<TelegramService> logger, IOptions<TelegramConfiguration> telegramConfiguration)
    {
        _botClient = botClient;
        _logger = logger;
        _chatName = telegramConfiguration.Value.ChatName;
    }

    public async Task SendPost(PostModel post)
    {
        var message = await _botClient.SendTextMessageAsync(
            chatId: _chatName,
            text: post.Title);
    }

    public async Task SendExceptionPost(string exception)
    {
        var message = await _botClient.SendTextMessageAsync(
            chatId: _chatName,
            text: exception);
    }
}
