using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SkillProfiCompanyTelegramBot.Command
{
    public class CommandStart : ICommand
    {
        private readonly TelegramBotClient _bot;

        public CommandStart(TelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task ExecuteCommand(Update update)
        {
            await _bot.SendTextMessageAsync(update.Message.Chat.Id, "Добро пожаловать! Для помощи нажмите /help");
        }
    }
}
