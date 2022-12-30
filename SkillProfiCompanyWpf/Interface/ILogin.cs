using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Interface
{
    public interface ILogin
    {
        string LoginAbortRegistrationVisibility { get; set; }
        string LoginAuthorizationContent { get; set; }
        string LoginAuthorizationVisibility { get; set; }
        string LoginUserName { get; set; }
        string LoginUserLogin { get; set; }
        string LoginLoginVisibility { get; set; }
        string LoginEmailVisibility { get; set; }
        string LoginUserEmail { get; set; }
        string LoginErrorContent { get; set; }
        string LoginEnterButtonVisibility { get; set; }
        string LoginRegistrationButtonVisibility { get; set; }
        string LoginRegistrationVisibility { get; set; }
        string LoginControlButtonVisibility { get; set; }
        string LoginExitVisibility { get; set; }
        string LoginServer { get; set; }
        string LoginControlError { get; set; }
        string GlobalErrorVisibility { get; set; }
        string GlobalError { get; set; }
        
    }
}
