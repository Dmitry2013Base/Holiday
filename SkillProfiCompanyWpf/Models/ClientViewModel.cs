using SkillProfiCompanyWpf.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyWpf.Models
{
    public class ClientViewModel : IService, IProject, IHome, ILogin, IBlog, IApplication, IContact
    {
        public int ServiceId { get; set; }
        public string ServiceHeader { get; set; }
        public string ServiceVisibilityButtonCancel { get; set; }
        public string ServiceVisibilityContextMenu { get; set; }
        public string ServiceError { get; set; }

        public string ProjectTitle { get; set; }
        public int ProjectId { get; set; }
        public string ProjectHeader { get; set; }
        public string ProjectDescription { get; set; }
        public bool ProjectIsReadOnly { get; set; }
        public string ProjectVisibilityContextMenu { get; set; }
        public string ProjectError { get; set; }
        public BitmapImage ProjectImage { get; set; }
        public string ProjectFileName { get; set; }
        public string ProjectVisibility { get; set; }

        public string ApplicationUserName { get; set; }
        public string ApplicationDesktopHeader { get; set; }
        public string ApplicationEmailText { get; set; }
        public bool ApplicationEmailRead { get; set; }
        public string ApplicationMessageText { get; set; }
        public bool ApplicationMessageRead { get; set; }
        public string ButtonAddApplicationTooltip { get; set; }
        public string ApplicationError { get; set; }
        public string ServiceDescription { get; set; }

        public string LoginAbortRegistrationVisibility { get; set; }
        public string LoginAuthorizationContent { get; set; }
        public string LoginAuthorizationVisibility { get; set; }
        public string LoginUserName { get; set; }
        public string LoginUserLogin { get; set; }
        public string LoginLoginVisibility { get; set; }
        public string LoginEmailVisibility { get; set; }
        public string LoginUserEmail { get; set; }
        public string LoginErrorContent { get; set; }
        public string LoginEnterButtonVisibility { get; set; }
        public string LoginRegistrationButtonVisibility { get; set; }
        public string LoginRegistrationVisibility { get; set; }
        public string LoginControlButtonVisibility { get; set; }
        public string LoginExitVisibility { get; set; }
        
        public string BlogTitle { get; set; }
        public int BlogId { get; set; }
        public string BlogHeader { get; set; }
        public string BlogDescription { get; set; }
        public string BlogShortDescription { get; set; }
        public BitmapImage BlogImage { get; set; }
        public string BlogFileName { get; set; }
        public string BlogVisibility { get; set; }
        public bool BlogIsReadOnly { get; set; }
        public string BlogVisibilityContextMenu { get; set; }
        public string BlogError { get; set; }

        public string ApplicationVisibilityDesktop { get; set; }
        public string ApplicationAllApplication { get; set; }
        public string ApplicationViewApplication { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationUserFullName { get; set; }
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserMessage { get; set; }
        public DateTime ApplicationDate { get; set; }

        public string ContactVisibilityContextMenu { get; set; }
        public string ContactVisibilityCreateMenu { get; set; }
        public string ContactCreateHeader { get; set; }
        public string ContactId { get; set; }
        public string ContactHeader { get; set; }
        public string ContactValue { get; set; }
        public int ContactTag { get; set; }
        public string ContactVisibilityButtonDelete { get; set; }
        public string LoginServer { get; set; }
        public string ContactError { get; set; }
        public string LoginControlError { get; set; }
        public string GlobalErrorVisibility { get; set; }
        public string GlobalError { get; set; }
    }
}
