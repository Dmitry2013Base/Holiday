using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.IO;
using Telegram.Bot.Types.InputFiles;
using SkillProfiCompanyTelegramBot.Model;

namespace SkillProfiCompanyTelegramBot.Command
{
    public class CommandProject : ICommand
    {
        private readonly TelegramBotClient _bot;
        private readonly string _server;

        public CommandProject(TelegramBotClient bot, string server)
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
                    string resalt = httpClient.GetStringAsync($"{_server}/api/project/items").Result;
                    List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(resalt);

                    for (int i = 0; i < projects.Count; i++)
                    {
                        byte[] image = Convert.FromBase64String(projects[i].Image.Split(',')[1]);

                        using (MemoryStream stream = new MemoryStream(image))
                        {
                            InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);
                            await _bot.SendPhotoAsync(update.Message.Chat.Id, inputOnlineFile, projects[i].Header);
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