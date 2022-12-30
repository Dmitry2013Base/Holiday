using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Interface
{
    public interface IApplication
    {
        string ApplicationVisibilityDesktop { get; set; }
        string ApplicationDesktopHeader { get; set; }
        string ApplicationAllApplication { get; set; }
        string ApplicationViewApplication { get; set; }
        int ApplicationId { get; set; }
        string ApplicationUserFullName { get; set; }
        string ApplicationUserEmail { get; set; }
        string ApplicationUserMessage { get; set; }
        DateTime ApplicationDate { get; set; }
    }
}
