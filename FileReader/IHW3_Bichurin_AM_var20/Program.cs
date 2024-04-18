// Нужно закинуть папку data_file в папку с исходным кодом :)

using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IHW3_Bichurin_AM_var20
{
    public class Program
    {
        private static ITelegramBotClient _botClient;

        private static ReceiverOptions _receiverOptions;
        static async Task Main(string[] args)
        {
            _botClient = new TelegramBotClient("7125461416:AAGPKPHNMgVWEOM-Rd6e0jpRe_mqRUs-VcQ");

            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery
                },
                ThrowPendingUpdates = true
            };

            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(GeneralTelegramMethods.UpdateHandler, GeneralTelegramMethods.ErrorHandler, _receiverOptions, cts.Token);

            var me = await _botClient.GetMeAsync(); 
            Console.WriteLine($"{me.FirstName} запущен!");

            await Task.Delay(-1);
        }


    }
}