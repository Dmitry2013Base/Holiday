using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Contact = SkillProfiCompanyTelegramBot.Model.Contact;

namespace SkillProfiCompanyTelegramBot.Command
{
    public class CommandContact : ICommand
    {
        private readonly TelegramBotClient _bot;
        private readonly string _server;

        public CommandContact(TelegramBotClient bot, string server)
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
                    string resalt = httpClient.GetStringAsync($"{_server}/api/contact/items").Result;
                    List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(resalt);

                    for (int i = 0; i < contacts.Count; i++)
                    {
                        string value = contacts[i].Value;

                        await _bot.SendTextMessageAsync(update.Message.Chat.Id, $"<b><u>{value}</u></b>", ParseMode.Html);
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
