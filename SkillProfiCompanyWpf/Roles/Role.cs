using SkillProfiCompanyWpf.Interface;
using SkillProfiCompanyWpf.Models;
using SkillProfiCompanyWpf.Roles.Settings;
using SkillProfiCompanyWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Roles
{
    public abstract class Role
    {
        private readonly ISetting _setting;
        private readonly ClientViewModel _clientViewModel;

        public abstract int Weight { get; }

        public Role()
        {

        }

        public Role(string roleName)
        {
            _clientViewModel = new ClientViewModel();
            _setting = GetSetting(roleName);
            Start();
        }

        public ClientViewModel GetViewModel()
        {
            return _clientViewModel;
        }

        private void Start()
        {
            var properties = _clientViewModel.GetType().GetProperties().ToList();
            var values = _setting.Values();

            properties.ForEach(e =>
            {
                e.SetValue(_clientViewModel, values[e.Name.ToString()]);
            });
        }

        private ISetting GetSetting(string roleName)
        {
            var set = Assembly.GetExecutingAssembly().GetTypes()
                .Where(r => r.Namespace == "SkillProfiCompanyWpf.Roles.Settings" && r.GetInterfaces().Contains(typeof(ISetting)) && r.Name.Remove(r.Name.Length - 7, 7) == roleName)
                .ToList();

            ISetting setting = (ISetting)Activator.CreateInstance(set[0]);

            return setting;
        }

        public static Role GetRole(Type type, string roleName = "")
        {
            if (roleName == String.Empty)
            {
                return (Role)Activator.CreateInstance(type);
            }

            return (Role)Activator.CreateInstance(type, roleName);
        }
    }
}
