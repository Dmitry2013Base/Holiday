using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Newtonsoft.Json;
using System.Net.Http;
using Telegram.Bot.Types.Enums;
using SkillProfiCompanyTelegramBot.Model;

namespace SkillProfiCompanyTelegramBot.Command
{
    public class CommandService : ICommand
    {
        private readonly TelegramBotClient _bot;
        private readonly string _server;

        public CommandService(TelegramBotClient bot, string server)
        {
            _bot = bot;
            _server = server;
        }

        public async Task ExecuteCommand(Update update)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string resalt = httpClient.GetStringAsync($"{_server}/api/service/items").Result;
                    List<ServiceClient> services = JsonConvert.DeserializeObject<List<ServiceClient>>(resalt);

                    for (int i = 0; i < services.Count; i++)
                    {
                        string header = services[i].Header;
                        string descriprion = services[i].Description;

                        await _bot.SendTextMessageAsync(update.Message.Chat.Id, $"<b><u>{header}</u></b>\n{descriprion}", ParseMode.Html);
                    }
                }
            }
            catch
            {
                await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Произошла ошибка при выполении запроса. Попробуйте позже");
            }
        }
    }
}
