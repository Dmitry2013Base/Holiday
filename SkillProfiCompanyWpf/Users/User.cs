using SkillProfiCompanyWpf.Interface;
using SkillProfiCompanyWpf.Models;
using SkillProfiCompanyWpf.Roles;
using SkillProfiCompanyWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Users
{
    public class User
    {
        private List<string> _roles;


        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ClientViewModel ClientViewModel { get; set; }
        public List<Role> Roles { get; set; }



        public User(List<string> roles, string login)
        {
            Login = login;
            _roles = roles;
            ClientViewModel = GetViewModel();
        }

        private ClientViewModel GetViewModel()
        {
            ClientViewModel viewModel = new ClientViewModel();
            var properties = viewModel.GetType().GetProperties().ToList();

            List<Role> roles1 = new List<Role>();
            List<string> roles2 = new List<string>();

            for (int i = 0; i < _roles.Count; i++)
            {
                Type type = Assembly.GetExecutingAssembly().GetTypes()
                  .Where(e => e.Namespace == "SkillProfiCompanyWpf.Roles" && !e.Name.Contains("<>") && !e.IsAbstract && e.Name == _roles[i])
                  .FirstOrDefault();

                var role = Role.GetRole(type);
                roles1.Add(role);
            }

            roles1 = roles1.OrderBy(e => e.Weight).ToList();

            for (int i = 0; i < roles1.Count; i++)
            {
                var str = roles1[i].ToString().Split('.');
                var role = Role.GetRole(roles1[i].GetType(), str[str.Length - 1]);
                roles1[i] = role;
                viewModel = roles1[i].GetViewModel();
            }

            Roles = roles1;
            return viewModel;
        }

        public void Update(Dictionary<string, object> values)
        {
            var properties = ClientViewModel.GetType().GetProperties().ToList();
            var keys = values.Keys.ToList();
            var value = values.Values.ToList();

            int num = 0;

            keys.ForEach(i =>
            {
                properties.FirstOrDefault(e => e.Name == i).SetValue(ClientViewModel, values[i]);
                num++;
            });
        }

        public bool GetPrivilege(int weight)
        {
            bool check = false;
            Roles.ForEach(i =>
            {
                if (i.Weight >= weight)
                {
                    check = true;
                }
            });
            return check;
        }
    }
}
