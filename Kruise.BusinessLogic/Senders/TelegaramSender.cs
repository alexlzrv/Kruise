using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Kruise.BusinessLogic.Senders;

public class TelegaramSender : ISender
{
    private readonly ITelegramBotClient _botClient;
    private readonly string _chatName;

    public TelegaramSender(ITelegramBotClient botClient, IOptions<TelegramConfiguration> telegramConfiguration)
    {
        _botClient = botClient;
        _chatName = telegramConfiguration.Value.ChatName;
    }

    public async Task Send(PostModel post)
    {
        var message = await _botClient.SendTextMessageAsync(
            chatId: _chatName,
            text: post.Title);
    }
}
