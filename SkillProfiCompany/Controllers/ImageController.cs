using Microsoft.AspNetCore.Mvc;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SkillProfiCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public ImageController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("items")]
        public List<CompanyImage> GetImages()
        {
            return _databaseCont.Images.ToList();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task CreateImageAsync([FromBody] CompanyImage image)
        {
            _databaseCont.Images.Add(image);
            await _databaseCont.SaveChangesAsync();
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("items/{id}")]
        public async Task DeleteImageAsync(int id)
        {
            var image = _databaseCont.Images.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (image != null)
            {
                _databaseCont.Images.Remove(image);
                await _databaseCont.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
