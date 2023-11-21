using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("6883460944:AAGMEj6u0vDddIiM4TEePqQDD3uf-wVnUxE");

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            using CancellationTokenSource cts = new();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            Console.ReadLine();
        }

        private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            Console.WriteLine($"{DateTime.Now} Произошла ошибка API: {exception.Message}");
            return Task.CompletedTask;
        }

        private static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            if (update.Type is not UpdateType.Message) return Task.CompletedTask;

            var message = update.Message;
            var chatId = message!.Chat.Id;
            var messageId = message!.MessageId;

            try
            {//457733185 - chat with me

                //
                //Greetings
                if (message.Text == "/start") botClient.SendTextMessageAsync(chatId: chatId, parseMode: ParseMode.Html, text: Config.getGreeting());
                else
                {
                    //
                    //Night message
                    if (DateTime.Now.Hour > 11 && DateTime.Now.Hour > 21) botClient.SendTextMessageAsync(chatId: chatId, parseMode: ParseMode.Html, text: Config.getNightMessage());

                    //
                    //Reply at customer message by reply, in other ways - customers messages
                    if (message.Text is not null && message.ReplyToMessage is not null && message.ReplyToMessage.ForwardFrom is not null)
                    {
                        //Msg from admin
                        botClient.CopyMessageAsync(messageId: messageId, chatId: message.ReplyToMessage.ForwardFrom.Id, fromChatId: chatId);
                    }
                    else
                    {
                        //Msg from clietns
                        botClient.ForwardMessageAsync(457733185, fromChatId: chatId, messageId);
                        //
                        //Night message notice
                        if (DateTime.Now.Hour > 11 && DateTime.Now.Hour > 21) botClient.SendTextMessageAsync(chatId: 457733185, text: Config.getNightMessageMark());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} Произошла ошибка: {e.Message}");
            }

            Console.WriteLine($"{DateTime.Now} Получено новое сообщение от {message.Chat.LastName} {message.Chat.FirstName} | {message.Chat.Username} - {message.Text} {message.Chat.Id}");

            return Task.CompletedTask;
        }
    }
}