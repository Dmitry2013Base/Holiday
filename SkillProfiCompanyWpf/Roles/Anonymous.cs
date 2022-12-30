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
    public class Anonymous : Role
    {
        public override int Weight => 0;


        public Anonymous() : base() { }
        public Anonymous(string roleName) : base(roleName) { }
    }
}
