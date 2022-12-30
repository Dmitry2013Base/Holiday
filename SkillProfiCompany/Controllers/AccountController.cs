using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Models;
using SkillProfiCompany.Users;

namespace SkillProfiCompany.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLogin()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect($"~/MyHome/Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(new UserRegistration());
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistration model)
        {

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.LoginProp, Email = model.Email };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "MyHome");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "MyHome");
        }

        [HttpPost]
        [Route("Account/LoginWpf")]
        public async Task<string> LoginWpf([FromBody]UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    User user = await _userManager.FindByNameAsync(model.LoginProp);
                    return user.Id;
                }
            }

            return String.Empty;
        }

        [HttpGet]
        [HttpPost]
        [Route("Account/RegistrationWpf")]
        public async Task RegistrationWpf([FromBody] UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = userRegistration.LoginProp, Email = userRegistration.Email};
                var createResult = await _userManager.CreateAsync(user, userRegistration.Password);

                if (createResult.Succeeded)
                {
                    if (userRegistration.LoginProp == "admin" && userRegistration.Password == "admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Guest");
                    }

                    await _signInManager.SignInAsync(user, false);
                }
            }
        }
    }
}

