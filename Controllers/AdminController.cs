
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocomecApp.Models;
using SocomecApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SocomecApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private UserManager<User> _personManager;
        private IPasswordHasher<User> _passwordHasher;
        private RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<User> prsMgr , IPasswordHasher<User> passwordHasher, RoleManager<IdentityRole> roleMgr)
        {
            _personManager = prsMgr;
            _passwordHasher = passwordHasher;
            roleManager = roleMgr;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CeatePerson person)
        {
            User person1 = new User(); 
            if (ModelState.IsValid)
            {
                person1.UserName = person.initial;
                person1.PhoneNumber = person.phone;
                person1.Email = person.mail;
                IdentityResult result = await _personManager.CreateAsync(person1, person.password);
                if (result.Succeeded)
                {
                    //return View("Index");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View(person);
                }
            }
            else
            {
                return View(person);
            }
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    return View("Error");
            }
            return View(name);
        }
        public IActionResult AffectRole()
        {
            ViewBag.user = _personManager.Users;
            ViewBag.role = roleManager.Roles;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AffectPersonRole(AffectRole affectRole)
        {
            IdentityResult result = new IdentityResult();
            if (ModelState.IsValid)
            {
                User user = await _personManager.FindByNameAsync(affectRole.Persons);
                if (user != null)
                {
                    result = await _personManager.AddToRoleAsync(user, affectRole.Roles);
                    if (!result.Succeeded)
                        Errors(result);
                }
            }
            //return View("Index");
            return RedirectToAction("Index", "Home");
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
