using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SkillProfiCompany.Models
{
    public class FilterByDate: IFilter<UserApplication>
    {
        private readonly DateTime[] _dates;


        public FilterByDate(DateTime[] dates)
        {
            _dates = dates;
        }

        public List<UserApplication> Filter()
        {
            using (DatabaseCont database = new DatabaseCont())
            {
                Array.Sort(_dates);

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
                    .Where(e => e.Date >= _dates[0] && e.Date <= _dates[1])
                    .OrderBy(e => e.Date)
                    .ToList();

                return applications;
            }
        }
    }
}
