using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SkillProfiCompanyTelegramBot
{
    public interface ICommand
    {
        Task ExecuteCommand(Update update);
    }
}
