using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Models;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkillProfiCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public UIController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("items/{page}")]
        public List<UIElement> GetAllUIByPage(string page)
        {
            return _databaseCont.UIElements.Where(e => e.Page == page).ToList();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items")]
        public List<UIElement> GetAllUI()
        {
            return _databaseCont.UIElements.ToList();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task UpdateUIAsync([FromBody] dynamic UIUpdate)
        {
            dynamic app = JsonConvert.DeserializeObject<dynamic>(UIUpdate.ToString());

            
            for (int i = 0; i < app.newElements.Count; i++)
            {
                string key = app.newElements[i].key;
                string value = app.newElements[i].value;

                var ui = _databaseCont.UIElements.AsNoTracking().FirstOrDefault(e => e.Key == key);
                
                if (ui != null)
                {
                    ui.Value = value;
                    _databaseCont.UIElements.Update(ui);
                }
                else
                {
                    throw new Exception();
                }
            }

            await _databaseCont.SaveChangesAsync();
        }
    }
}
