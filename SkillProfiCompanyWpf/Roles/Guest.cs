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
    public class Guest : Role
    {
        public override int Weight => 1;


        public Guest() : base() { }
        public Guest(string roleName) : base(roleName) { }
    }
}
