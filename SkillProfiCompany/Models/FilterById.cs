using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SkillProfiCompany.Models
{
    public class FilterById : IFilter<UserApplication>
    {
        private readonly int _id;

        public FilterById(int id)
        {
            _id = id;
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
                    .Where(e => e.Id == _id)
                    .ToList();

                return applications;
            }
        }
    }
}
