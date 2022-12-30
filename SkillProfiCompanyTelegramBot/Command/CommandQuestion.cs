using System;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Net.Http.Json;

namespace SkillProfiCompanyTelegramBot.Command
{
    public class CommandQuestion : ICommand
    {
        private readonly TelegramBotClient _bot;
        private readonly string _server;
        private static sbyte _stage;
        private static bool _lock = false;
        private static string _email;

        public CommandQuestion(TelegramBotClient bot, string server)
        {
            _bot = bot;
            _server = server;
        }

        public static bool GetLock()
        {
            return _lock;
        }

        public async Task ExecuteCommand(Update update)
        {
            if (_stage == 0)
            {
                _lock = true;
                await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Введите свой email");
            }
            else if (_stage == 1 && update.Message.Text.Contains("@"))
            {
                _email = update.Message.Text;
                await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Введите свой вопрос");
            }
            else if (_stage == 2 && !update.Message.Text.Contains("@"))
            {
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        JsonContent application = JsonContent.Create(new { UserFullName = $"_{update.Message.Chat.FirstName}", UserEmail = _email, UserMessage = update.Message.Text });
                        httpClient.BaseAddress = new Uri($"{_server}/api/application/items");
                        var result = await httpClient.PostAsync($"{_server}/api/application/items", application);

                        var t = result.StatusCode;
                        if (result.StatusCode is System.Net.HttpStatusCode.OK)
                        {
                            await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Заявка создана");
                        }
                        else
                        {
                            await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Ошибка: заявка не создана");
                        }

                        _stage = -1;
                    }
                }
                catch
                {
                    await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Произошла ошибка при выполении запроса. Попробуйте позже");
                }

                _lock = false;
            }
            _stage++;
        }
    }
}
