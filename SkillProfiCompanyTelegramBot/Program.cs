using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using SkillProfiCompanyTelegramBot.Command;

namespace SkillProfiCompanyTelegramBot
{
    class Program
    {
        private static TelegramBotClient bot;
        private static readonly string server = "https://localhost:5001";

        static void Main(string[] args)
        {
            StartBot();
            Console.ReadKey();
        }
        private static void StartBot()
        {
            if (System.IO.File.Exists("../../TelegramBotToken.txt"))
            {
                using (StreamReader stream = new StreamReader("../../TelegramBotToken.txt"))
                {
                    string token = stream.ReadLine();
                    bot = new TelegramBotClient(token);
                }

                ReceiverOptions receiverOptions = new ReceiverOptions()
                {
                    AllowedUpdates = new UpdateType[]
                    {
                        UpdateType.Message,
                        UpdateType.EditedMessage,
                    }
                };

                bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);
                Console.WriteLine("StartBot");
            }
            else
            {
                Console.WriteLine("Файл \"TelegramBotToken.txt\" не обнаружен");
            }
        }

        private static async Task UpdateHandler(ITelegramBotClient arg1, Update update, CancellationToken arg3)
        {
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {
                    if (update.Message.Text == "/start")
                    {
                        ICommand command = new CommandStart(bot);
                        await command.ExecuteCommand(update);
                    }
                    else if (update.Message.Text == "/help")
                    {
                        ICommand command = new CommandHelp(bot);
                        await command.ExecuteCommand(update);
                    }
                    else if (update.Message.Text == "/question")
                    {
                        ICommand command = new CommandQuestion(bot, server);
                        await command.ExecuteCommand(update);
                    }
                    else if (update.Message.Text == "/services")
                    {
                        ICommand command = new CommandService(bot, server);
                        await command.ExecuteCommand(update);
                    }
                    else if (update.Message.Text == "/projects")
                    {
                        ICommand command = new CommandProject(bot, server);
                        await command.ExecuteCommand(update);
                    }
                    else if (update.Message.Text == "/blogs")
                    {
                        ICommand command = new CommandBlog(bot, server);
                        await command.ExecuteCommand(update);
                    }
                    else if (update.Message.Text == "/contacts")
                    {
                        ICommand command = new CommandContact(bot, server);
                        await command.ExecuteCommand(update);
                    }
                    else
                    {
                        if (CommandQuestion.GetLock())
                        {
                            ICommand command = new CommandQuestion(bot, server);
                            await command.ExecuteCommand(update);
                        }
                    }

                    MessageLog(update);
                }
            }
        }

        private static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static void MessageLog(Update update)
        {
            Console.WriteLine($"[{update.Message.Chat.Id}] {update.Message.Chat.FirstName} ({update.Message.Date}) - {update.Message.Text}");
        }
    }
}