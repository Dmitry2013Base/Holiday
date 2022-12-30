using SkillProfiCompanyWpf.Interface;
using SkillProfiCompanyWpf.Models;
using SkillProfiCompanyWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf.Roles
{
    public class Admin : Role
    {
        public override int Weight => 10;


        public Admin() : base() { }
        public Admin(string roleName) : base(roleName) { }
    }
}
