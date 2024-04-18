using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using WifiParks;

namespace IHW3_Bichurin_AM_var20
{
    public class GeneralTelegramMethods
    {
        public static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {                                             
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            var user = message.From;

                            Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                            var chat = message.Chat;

                            switch (message.Type)
                            {
                                case MessageType.Text:
                                    {
                                        if (message.Text != null)
                                        {
                                            if (message.Text == "/start")
                                            {
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Доброго времени суток! Данный бот создан для работы с csv и json файлами. Для открытия меню напишите \"/menu\".");

                                                return;
                                            }

                                            if (message.Text == "/menu")
                                            {
                                                var replyKeyboard = new ReplyKeyboardMarkup(
                                                    new List<KeyboardButton[]>()
                                                    {
                                                        new KeyboardButton[]
                                                        {
                                                            new KeyboardButton("Загрузить CSV файл на обработку"),
                                                            new KeyboardButton("Загрузить JSON файл на обработку"),
                                                        },
                                                        new KeyboardButton[]
                                                        {
                                                            new KeyboardButton("Произвести выборку по id"),
                                                            new KeyboardButton("Отсортировать по id")
                                                        }
                                                    })
                                                {
                                                    ResizeKeyboard = true,
                                                };

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Меню открыто.",
                                                    replyMarkup: replyKeyboard);

                                                return;
                                            }

                                            if (message.Text == "Загрузить CSV файл на обработку")
                                            {
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Хорошо, скидывайте файл.",
                                                    replyToMessageId: message.MessageId);

                                                return;
                                            }

                                            if (message.Text == "Загрузить JSON файл на обработку")
                                            {
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Хорошо, скидывайте файл.",
                                                    replyToMessageId: message.MessageId);

                                                return;
                                            }

                                            if (message.Text == "Произвести выборку по id")
                                            {
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Введите число от 1 - 1100",
                                                    replyToMessageId: message.MessageId);

                                                return;
                                            }

                                            if (int.TryParse(message.Text, out int n) && n >= 1 && n <= 1100)
                                            {
                                                string[] titles;
                                                List<Park> parks = new List<Park>();
                                                (parks, titles) = CsvProcessing.ReadFile(@"../../../../data_file/wifi-parks.csv");

                                                parks = DataProcessing.Selection(parks, n);

                                                JsonProcessing.WriteToFile(parks, "wifi-parks.json");
                                                CsvProcessing.WriteToFile(parks, titles, "wifi-parks.csv");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Обработанный файл:");

                                                await using Stream csvStream = System.IO.File.OpenRead(@"../../../../data_file/wifi-parks.csv");
                                                Message sendableMessage1 = await botClient.SendDocumentAsync(chat.Id,
                                                    InputFile.FromStream(csvStream, @"../../../../data_file/wifi-parks.csv"));
                                                csvStream.Close();

                                                await using Stream jsonStream = System.IO.File.OpenRead(@"../../../../data_file/wifi-parks.json");
                                                Message sendableMessage2 = await botClient.SendDocumentAsync(chat.Id,
                                                    InputFile.FromStream(jsonStream, @"../../../../data_file/wifi-parks.json"));
                                                jsonStream.Close();

                                                return;
                                            }

                                            if (message.Text == "Отсортировать по id")
                                            {
                                                List<Park> parks = new List<Park>();
                                                parks = JsonProcessing.ReadFile($@"../../../../data_file/wifi-parks.json");
                                                string[] titles = {"ID", "global_id", "Name", "AdmArea", "District", "ParkName",
                                                "WiFiName", "CoverageArea", "FunctionFlag", "AccessFlag", "Password",
                                                "Longitude_WGS84", "Latitude_WGS84", "geodata_center", "geoarea"};

                                                parks = DataProcessing.BubbleSort(parks);

                                                JsonProcessing.WriteToFile(parks, $"wifi-parks.json");
                                                CsvProcessing.WriteToFile(parks, titles, "wifi-parks.csv");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Обработанный файл:");

                                                await using Stream csvStream = System.IO.File.OpenRead(@"../../../../data_file/wifi-parks.csv");
                                                Message sendableMessage1 = await botClient.SendDocumentAsync(chat.Id,
                                                    InputFile.FromStream(csvStream, @"../../../../data_file/wifi-parks.csv"));
                                                csvStream.Close();

                                                await using Stream jsonStream = System.IO.File.OpenRead(@"../../../../data_file/wifi-parks.json");
                                                Message sendableMessage2 = await botClient.SendDocumentAsync(chat.Id,
                                                    InputFile.FromStream(jsonStream, @"../../../../data_file/wifi-parks.json"));
                                                jsonStream.Close();

                                                return;
                                            }

                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "Неизвестная команда!",
                                                replyToMessageId: message.MessageId);
                                        }

                                        return;
                                    }

                                case MessageType.Document:
                                    {
                                        if (message.Document != null && message.Document.FileName.Contains(".csv"))
                                        {
                                            var fileId = update.Message.Document.FileId;
                                            var fileInfo = await botClient.GetFileAsync(fileId);
                                            var filePath = fileInfo.FilePath;

                                            string destinationFilePath = $@"../../../../data_file/{message.Document.FileName}";

                                            await using Stream fileStream = System.IO.File.Create(destinationFilePath);
                                            await botClient.DownloadFileAsync(filePath, fileStream);
                                            fileStream.Close();

                                            await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Файл загружен.");
                                        }

                                        if (message.Document != null && message.Document.FileName.Contains(".json"))
                                        {
                                            var fileId = update.Message.Document.FileId;
                                            var fileInfo = await botClient.GetFileAsync(fileId);
                                            var filePath = fileInfo.FilePath;

                                            string destinationFilePath = $@"../../../../data_file/{message.Document.FileName}";

                                            await using Stream fileStream = System.IO.File.Create(destinationFilePath);
                                            await botClient.DownloadFileAsync(filePath, fileStream);
                                            fileStream.Close();

                                            await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Файл загружен.");
                                        }

                                        return;
                                    }
                                default:
                                    {
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Неизвестная команда!",
                                            replyToMessageId: message.MessageId);

                                        return;
                                    }
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cansellationToken)
        {
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
