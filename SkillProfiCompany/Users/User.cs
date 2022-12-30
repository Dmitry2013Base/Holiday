using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Models;

namespace SkillProfiCompany.Users
{
    public class User : IdentityUser
    {
        public static User GetUser(string login)
        {
            using (var db = new DatabaseCont())
            {
                return db.Users.First(e => e.UserName == login);
            }
        }
    }
}
