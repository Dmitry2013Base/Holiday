using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace SkillProfiCompanyTelegramBot.Command
{
    public class CommandHelp : ICommand
    {
        private readonly TelegramBotClient _bot;

        public CommandHelp(TelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task ExecuteCommand(Update update)
        {
            string helpMessage = "Список команд:\n" +
                "/question - Задать вопрос или оставить заяявку\n" +
                "/services - Посмотреть услуги, которые мы предоствляем\n" +
                "/projects - Посмотреть проекты\n" +
                "/blogs - Посмотреть блог\n" +
                "/contacts - Посмотреть контактные данные";

            await _bot.SendTextMessageAsync(update.Message.Chat.Id, helpMessage);
        }
    }
}
