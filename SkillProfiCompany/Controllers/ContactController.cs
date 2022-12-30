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
    public class ContactController : Controller
    {
        private readonly DatabaseCont _databaseCont;

        public ContactController(DatabaseCont databaseCont)
        {
            _databaseCont = databaseCont;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("items")]
        public List<Contact> GetContacts()
        {
            return _databaseCont.Contacts.ToList();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items/{id}")]
        public Task<Contact> GetContactAsync(int id)
        {
            return _databaseCont.Contacts.FirstOrDefaultAsync(e => e.Id == id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items/social")]
        public List<Contact> GetSocialContactAsync()
        {
            return _databaseCont.Contacts.ToList().FindAll(e => e.Tag == Contact.TagContact.Social);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items/tel")]
        public List<Contact> GetTelContactAsync()
        {
            return _databaseCont.Contacts.ToList().FindAll(e => e.Tag == Contact.TagContact.Telephone);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items/view")]
        public ActionResult GetContactsView()
        {
            return View("~/Views/Contact/Index.cshtml", _databaseCont.Contacts.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task CreateContactAsync([FromBody] Contact contact)
        {
            _databaseCont.Contacts.Add(contact);
            await _databaseCont.SaveChangesAsync();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("items")]
        public async Task UpdateContactAsync([FromBody] Contact contactToUpdate)
        {
            var contact = _databaseCont.Contacts.AsNoTracking().FirstOrDefault(e => e.Id == contactToUpdate.Id);

            _databaseCont.Contacts.Update(contactToUpdate);
            await _databaseCont.SaveChangesAsync();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("items/{id}")]
        public async Task DeleteContactAsync(int id)
        {
            var contact = _databaseCont.Contacts.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (contact != null)
            {
                _databaseCont.Contacts.Remove(contact);
                await _databaseCont.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
