using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillProfiCompany.Users
{
    public class UserLogin
    {
        public string LoginProp { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
