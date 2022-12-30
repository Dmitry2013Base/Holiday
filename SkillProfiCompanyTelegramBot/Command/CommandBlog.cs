using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using SkillProfiCompanyTelegramBot.Model;

namespace SkillProfiCompanyTelegramBot.Command
{
    public class CommandBlog : ICommand
    {
        private readonly TelegramBotClient _bot;
        private readonly string _server;

        public CommandBlog(TelegramBotClient bot, string server)
        {
            _bot = bot;
            _server = server;
        }

        public async Task ExecuteCommand(Update update)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string resalt = httpClient.GetStringAsync($"{_server}/api/blog/items").Result;
                    List<Blog> blogs = JsonConvert.DeserializeObject<List<Blog>>(resalt);

                    for (int i = 0; i < blogs.Count; i++)
                    {
                        byte[] image = Convert.FromBase64String(blogs[i].Image.Split(',')[1]);

                        using (MemoryStream stream = new MemoryStream(image))
                        {
                            InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                            string header = blogs[i].Header;

                            await _bot.SendPhotoAsync(update.Message.Chat.Id, inputOnlineFile, $"<b><u>{header}</u></b>", ParseMode.Html);
                        }
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
