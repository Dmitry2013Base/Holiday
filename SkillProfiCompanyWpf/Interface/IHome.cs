using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Interface
{
    public interface IHome
    {
        string ApplicationUserName { get; set; }
        string ApplicationEmailText { get; set; }
        bool ApplicationEmailRead { get; set; }
        string ApplicationMessageText { get; set; }
        bool ApplicationMessageRead { get; set; }
        string ButtonAddApplicationTooltip { get; set; }
        string ApplicationError { get; set; }
    }
}
