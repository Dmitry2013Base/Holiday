using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;
using SkillProfiCompany.Models;
using SkillProfiCompany.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SkillProfiCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : Controller
    {

        private readonly DatabaseCont _databaseCont;

        public ApplicationController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("items/{date1}/{date2}")]
        public ActionResult FilterApplicationByDate(string date1, string date2)
        {
            IFilter<UserApplication> filter = new FilterByDate(new DateTime[] { Convert.ToDateTime(date1), Convert.ToDateTime(date2) });
            return View("~/Views/Application/Index.cshtml", filter.Filter());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("itemsCount")]
        public int ApplicationCount()
        {
            IFilter<UserApplication> filter = new FilterByDate(new DateTime[] { DateTime.MinValue, DateTime.Now });
            return filter.Filter().Count;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("items/{name}")]
        public List<UserApplication> FilterApplicationByName(string name)
        {
            IFilter<UserApplication> filter = new FilterByName(name);
            return filter.Filter();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("messages/{id}")]
        public async Task<ActionResult> GetMessages(int id)
        {
            var app = _databaseCont.UserApplications
                .Join(_databaseCont.ApplicationStatuses,
                    a => a.ApplicationStatus.Id,
                    s => s.Id,
                    (a, s) => new UserApplication
                    {
                        Id = a.Id,
                        Date = a.Date,
                        UserFullName = a.UserFullName,
                        UserEmail = a.UserEmail,
                        UserMessage = a.UserMessage,
                        ApplicationStatus = s,
                        User = a.User,
                    })
                .Join(_databaseCont.Users,
                    a => a.User.Id,
                    u => u.Id,
                    (a, u) => new UserApplication
                    {
                        Id = a.Id,
                        Date = a.Date,
                        UserFullName = a.UserFullName,
                        UserEmail = a.UserEmail,
                        UserMessage = a.UserMessage,
                        ApplicationStatus = a.ApplicationStatus,
                        User = u,
                    });

            var application = await app.FirstOrDefaultAsync(e => e.Id == id);
            return View("~/Views/Application/Message.cshtml", application);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("items/statuses")]
        public List<string> GetApplicationStatuses()
        {
            return _databaseCont.ApplicationStatuses.Select(e => e.StatusName).ToList();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("items/allStatuses")]
        public List<ApplicationStatus> GetStatuses()
        {
            return _databaseCont.ApplicationStatuses.ToList();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public List<UserApplication> GetApplication()
        {
            IFilter<UserApplication> filter = new FilterByDate(new DateTime[] { DateTime.MinValue, DateTime.MaxValue });
            return filter.Filter();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("items")]
        public async Task CreateApplcationAsync([FromBody] UserApplication application)
        {
            string userName = application.UserFullName;
            if (application.UserFullName.StartsWith("_"))
            {
                application.UserFullName = application.UserFullName.Remove(1, application.UserFullName.Length - 1);
            }

            User user = await _databaseCont.Users.FirstOrDefaultAsync(e => e.UserName.StartsWith(application.UserFullName));

            await _databaseCont.Database.ExecuteSqlRawAsync($"Insert Into UserApplications([Date], UserFullName, UserEmail, UserMessage, ApplicationStatusId, UserId) " +
                $"Values('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', N'{userName}', N'{application.UserEmail}', N'{application.UserMessage}', 1, '{user.Id}')");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task UpdateApplicationAsync([FromBody] object param)
        {
            dynamic app = JsonConvert.DeserializeObject<dynamic>(param.ToString());
            await _databaseCont.Database.ExecuteSqlRawAsync($"Update UserApplications Set ApplicationStatusId = {(int)app.newStatusId} Where Id = {(int)app.applicationId}");
        }
    }
}
