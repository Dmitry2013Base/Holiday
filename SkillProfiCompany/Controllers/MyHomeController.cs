using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfiCompany.DatabaseContext;
using System.Linq;

namespace SkillProfiCompany.Controllers
{
    public class MyHomeController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public MyHomeController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_databaseCont.Images.ToList());
        }
    }
}
