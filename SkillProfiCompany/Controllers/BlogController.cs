using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace SkillProfiCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public BlogController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("items")]
        public List<Blog> GetBlogss()
        {
            return _databaseCont.Blogs.ToList();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items/view")]
        public async Task<ActionResult> GetBlogsAsync()
        {
            await HelperCss.CreateFileBlogCss();
            return View("~/Views/Blog/Index.cshtml", _databaseCont.Blogs.ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("edit/{id}")]
        [Route("edit/{id}/{view}")]
        public async Task<ActionResult> EditAsync(int id, bool view = false)
        {
            var blog = await _databaseCont.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            return View("~/Views/Blog/Edit.cshtml", (blog, view));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task CreateBlogAsync([FromBody] Blog blog)
        {
            blog.Date = DateTime.Today.ToString("d");
            _databaseCont.Blogs.Add(blog);
            await _databaseCont.SaveChangesAsync();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task UpdateBlogAsync([FromBody] Blog blogToUpdate)
        {
            var blog = _databaseCont.Blogs.AsNoTracking().FirstOrDefault(e => e.Id == blogToUpdate.Id);

            _databaseCont.Blogs.Update(blogToUpdate);
            await _databaseCont.SaveChangesAsync();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("items/{id}")]
        public async Task DeleteBlogAsync(int id)
        {
            var blog = _databaseCont.Blogs.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (blog != null)
            {
                _databaseCont.Blogs.Remove(blog);
                await _databaseCont.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
