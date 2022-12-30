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
    public class ProjectController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public ProjectController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("items")]
        public List<Project> GetProjects()
        {
            return _databaseCont.Projects.ToList();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items/view")]
        public async Task<ActionResult> GetProjectsAsync()
        {
            await HelperCss.CreateFileProjectCss();
            return View("~/Views/Project/Index.cshtml", _databaseCont.Projects.ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("edit/{id}")]
        [Route("edit/{id}/{view}")]
        public async Task<ActionResult> EditAsync(int id, bool view = false)
        {
            var project = await _databaseCont.Projects.FirstOrDefaultAsync(x => x.Id == id);
            return View("~/Views/Project/Edit.cshtml", (project, view));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task CreateProjectAsync([FromBody] Project project)
        {
            _databaseCont.Projects.Add(project);
            await _databaseCont.SaveChangesAsync();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task UpdateProjectAsync([FromBody] Project projectToUpdate)
        {
            var project = _databaseCont.Projects.AsNoTracking().FirstOrDefault(e => e.Id == projectToUpdate.Id);

            _databaseCont.Projects.Update(projectToUpdate);
            await _databaseCont.SaveChangesAsync();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("items/{id}")]
        public async Task DeleteProjectAsync(int id)
        {
            var project = _databaseCont.Projects.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (project != null)
            {
                _databaseCont.Projects.Remove(project);
                await _databaseCont.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
