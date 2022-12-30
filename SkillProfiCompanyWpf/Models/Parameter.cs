using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkillProfiCompanyWpf.Models
{
    public class Parameter
    {
        public static Dictionary<string, object> ViewRegistration()
        {
            Dictionary<string, object> registration = new Dictionary<string, object>()
            {
                { "LoginAbortRegistrationVisibility", Visibility.Visible.ToString() },
                { "LoginAuthorizationContent", "Регистрация" },
                { "LoginEmailVisibility", Visibility.Visible.ToString() },
                { "LoginEnterButtonVisibility", Visibility.Collapsed.ToString() },
                { "LoginRegistrationButtonVisibility", Visibility.Collapsed.ToString() },
                { "LoginRegistrationVisibility", Visibility.Visible.ToString() },
                { "LoginErrorContent", String.Empty },
            };

            return registration;
        }

        public static Dictionary<string, object> ViewLogin()
        {
            Dictionary<string, object> registration = new Dictionary<string, object>()
            {
                { "ApplicationUserName", String.Empty },
                { "LoginAbortRegistrationVisibility", Visibility.Collapsed.ToString() },
                { "LoginAuthorizationContent", "Авторизация" },
                { "LoginLoginVisibility", Visibility.Visible.ToString() },
                { "LoginEmailVisibility", Visibility.Collapsed.ToString() },
                { "LoginEnterButtonVisibility", Visibility.Visible.ToString() },
                { "LoginRegistrationButtonVisibility", Visibility.Visible.ToString() },
                { "LoginRegistrationVisibility", Visibility.Collapsed.ToString() },
                { "LoginUserName", String.Empty },
                { "LoginUserLogin", String.Empty },
                { "LoginUserEmail", String.Empty },
                { "LoginErrorContent", String.Empty },
            };

            return registration;
        }

        public static Dictionary<string, object> ViewAuthorized(string login, string role = "")
        {
            string visible = Visibility.Collapsed.ToString();
            if (role != String.Empty)
            {
                visible = Visibility.Visible.ToString();
            }

            Dictionary<string, object> registration = new Dictionary<string, object>()
            {
                { "ApplicationUserName", login },
                { "LoginAbortRegistrationVisibility", Visibility.Collapsed.ToString() },
                { "LoginAuthorizationContent", $"{login} в сети!" },
                { "LoginAuthorizationVisibility", Visibility.Visible.ToString() },
                { "LoginUserName", String.Empty },
                { "LoginUserLogin", String.Empty },
                { "LoginLoginVisibility", Visibility.Collapsed.ToString() },
                { "LoginEmailVisibility", Visibility.Collapsed.ToString() },
                { "LoginUserEmail", String.Empty },
                { "LoginErrorContent", String.Empty },
                { "LoginEnterButtonVisibility", Visibility.Collapsed.ToString() },
                { "LoginRegistrationButtonVisibility", Visibility.Collapsed.ToString() },
                { "LoginRegistrationVisibility", Visibility.Collapsed.ToString() },
                { "LoginControlButtonVisibility", visible },
                { "LoginExitVisibility", Visibility.Visible.ToString() },
            };

            return registration;
        }
    }
}
