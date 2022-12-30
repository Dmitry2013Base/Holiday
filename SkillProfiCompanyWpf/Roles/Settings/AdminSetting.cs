﻿using SkillProfiCompanyWpf.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkillProfiCompanyWpf.Roles.Settings
{
    public class AdminSetting : ISetting
    {
        public Dictionary<string, object> Values()
        {
            var settings = new Dictionary<string, object>()
            {
                { "LoginAbortRegistrationVisibility", Visibility.Collapsed.ToString() },
                { "LoginAuthorizationContent", "Авторизация" },
                { "LoginAuthorizationVisibility", Visibility.Visible.ToString() },
                { "LoginUserName", String.Empty },
                { "LoginUserLogin", String.Empty },
                { "LoginLoginVisibility", Visibility.Visible.ToString() },
                { "LoginEmailVisibility", Visibility.Collapsed.ToString() },
                { "LoginUserEmail", String.Empty },
                { "LoginErrorContent", String.Empty },
                { "LoginEnterButtonVisibility", Visibility.Visible.ToString() },
                { "LoginRegistrationButtonVisibility", Visibility.Visible.ToString() },
                { "LoginRegistrationVisibility", Visibility.Collapsed.ToString() },
                { "LoginControlButtonVisibility", Visibility.Collapsed.ToString() },
                { "LoginExitVisibility", Visibility.Collapsed.ToString() },
                { "LoginServer", String.Empty },
                { "LoginControlError", String.Empty },
                { "GlobalErrorVisibility", Visibility.Collapsed.ToString() },
                { "GlobalError", String.Empty },

                { "BlogTitle", "Блог" },
                { "BlogId", 0 },
                { "BlogHeader", String.Empty },
                { "BlogDescription", String.Empty },
                { "BlogShortDescription", String.Empty },
                { "BlogImage", null },
                { "BlogFileName", String.Empty },
                { "BlogVisibility", Visibility.Visible.ToString() },
                { "BlogIsReadOnly", false },
                { "BlogVisibilityContextMenu", Visibility.Visible.ToString() },
                { "BlogError", String.Empty },

                { "ProjectTitle", "Проект" },
                { "ProjectId", 0 },
                { "ProjectHeader", String.Empty },
                { "ProjectDescription", String.Empty },
                { "ProjectImage", null },
                { "ProjectFileName", String.Empty },
                { "ProjectVisibility", Visibility.Visible.ToString() },
                { "ProjectIsReadOnly", false },
                { "ProjectVisibilityContextMenu", Visibility.Visible.ToString() },
                { "ProjectError", String.Empty },

                { "ApplicationDesktopHeader", "Управление" },
                { "ApplicationUserName", String.Empty },
                { "ApplicationEmailText", String.Empty },
                { "ApplicationEmailRead", false },
                { "ApplicationMessageText", String.Empty },
                { "ApplicationMessageRead", false },
                { "ButtonAddApplicationTooltip", "Отправить" },
                { "ApplicationError", String.Empty },

                { "ServiceId", 0  },
                { "ServiceHeader", String.Empty  },
                { "ServiceDescription", String.Empty },
                { "ServiceVisibilityButtonCancel", Visibility.Visible.ToString() },
                { "ServiceVisibilityContextMenu", Visibility.Visible.ToString() },
                { "ServiceError", String.Empty },

                { "ApplicationVisibilityDesktop", Visibility.Visible.ToString() },
                { "ApplicationAllApplication", String.Empty },
                { "ApplicationViewApplication", String.Empty},
                { "ApplicationId", 0 },
                { "ApplicationUserFullName", String.Empty },
                { "ApplicationUserEmail", String.Empty },
                { "ApplicationUserMessage", String.Empty },
                { "ApplicationDate", DateTime.MinValue },

                { "ContactVisibilityContextMenu", Visibility.Visible.ToString() },
                { "ContactVisibilityCreateMenu", Visibility.Collapsed.ToString() },
                { "ContactCreateHeader", String.Empty },
                { "ContactId", "0" },
                { "ContactHeader", String.Empty },
                { "ContactValue", String.Empty },
                { "ContactTag", 0 },
                { "ContactError", String.Empty },
                { "ContactVisibilityButtonDelete", Visibility.Collapsed.ToString() },
            };
            return settings;
        }
    }
}
