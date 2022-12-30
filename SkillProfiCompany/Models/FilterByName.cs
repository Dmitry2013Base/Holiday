using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SkillProfiCompany.Models
{

    public class FilterByName : IFilter<UserApplication>
    {
        private readonly string _name;

        public FilterByName(string name)
        {
            _name = name;
        }
 
        public List<UserApplication> Filter()
        {
            using (DatabaseCont database = new DatabaseCont())
            {
                List<UserApplication> applications = database.UserApplications
                    .Join(database.ApplicationStatuses,
                        u => u.ApplicationStatus.Id,
                        a => a.Id,
                        (u, a) => new UserApplication
                        {
                            Id = u.Id,
                            Date = u.Date,
                            UserFullName = u.UserFullName,
                            UserMessage = u.UserMessage,
                            UserEmail = u.UserEmail,
                            User = u.User,
                            ApplicationStatus = a,
                        })
                    .Join(database.Users,
                        a => a.User.Id,
                        u => u.Id,
                        (a, u) => new UserApplication
                        {
                            Id = a.Id,
                            Date = a.Date,
                            UserFullName = a.UserFullName,
                            UserMessage = a.UserMessage,
                            UserEmail = a.UserEmail,
                            User = u,
                            ApplicationStatus = a.ApplicationStatus,
                        }
                    )
                    .Where(e => e.User.UserName.Contains(_name))
                    .OrderBy(e => e.Date)
                    .ToList();

                return applications;
            }
        }
    }
}
