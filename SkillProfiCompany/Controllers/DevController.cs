using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Models;
using SkillProfiCompany.Users;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Web.Helpers;
using System.Xml.Linq;

namespace SkillProfiCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public DevController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public void GetDB()
        {
            string projects = JsonConvert.SerializeObject(_databaseCont.Projects.ToList(), Formatting.Indented);
            string services = JsonConvert.SerializeObject(_databaseCont.Services.ToList(), Formatting.Indented);
            string blogs = JsonConvert.SerializeObject(_databaseCont.Blogs.ToList(), Formatting.Indented);
            string contacts = JsonConvert.SerializeObject(_databaseCont.Contacts.ToList(), Formatting.Indented);
            string ui = JsonConvert.SerializeObject(_databaseCont.UIElements.ToList(), Formatting.Indented);
            string images = JsonConvert.SerializeObject(_databaseCont.Images.ToList(), Formatting.Indented);
            string statuses = JsonConvert.SerializeObject(_databaseCont.ApplicationStatuses.ToList(), Formatting.Indented);

            System.IO.File.WriteAllText(@"..\SkillProfiCompany\DbStart\Projects.json", projects);
            System.IO.File.WriteAllText(@"..\SkillProfiCompany\DbStart\Services.json", services);
            System.IO.File.WriteAllText(@"..\SkillProfiCompany\DbStart\Blogs.json", blogs);
            System.IO.File.WriteAllText(@"..\SkillProfiCompany\DbStart\Contacts.json", contacts);
            System.IO.File.WriteAllText(@"..\SkillProfiCompany\DbStart\UI.json", ui);
            System.IO.File.WriteAllText(@"..\SkillProfiCompany\DbStart\Images.json", images);
            System.IO.File.WriteAllText(@"..\SkillProfiCompany\DbStart\Statuses.json", statuses);
        }
    }
}
