using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillProfiCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public ServiceController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("items")]
        public List<Service> GetServices()
        {
            return _databaseCont.Services.ToList();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items/view")]
        public ActionResult ServicesView()
        {
            return View("~/Views/Service/Index.cshtml", _databaseCont.Services.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task CreateServiceAsync([FromBody] Service service)
        {
            _databaseCont.Services.Add(service);
            await _databaseCont.SaveChangesAsync();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task UpdateServiceAsync([FromBody] Service serviceToUpdate)
        {
            var service = _databaseCont.Services.AsNoTracking().FirstOrDefault(e => e.Id == serviceToUpdate.Id);

            _databaseCont.Services.Update(serviceToUpdate);
            var result = await _databaseCont.SaveChangesAsync();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("items/{id}")]
        public async Task DeleteServiceAsync(int id)
        {
            var project = _databaseCont.Services.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (project != null)
            {
                _databaseCont.Services.Remove(project);
                var result = await _databaseCont.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
