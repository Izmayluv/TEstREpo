using Telegram.Bot;


await InitializeBotAsync();

static async Task InitializeBotAsync()
{
    var botClient = new TelegramBotClient("6883460944:AAGMEj6u0vDddIiM4TEePqQDD3uf-wVnUxE");

    var me = await botClient.GetMeAsync();
    Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
}