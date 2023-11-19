
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocomecApp.Models;
using SocomecApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SocomecApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> _personManager;
        private SignInManager<User> _signInManager;
        private IPasswordHasher<User> _passwordHasher;
        public AccountController(UserManager<User> userMgr, SignInManager<User> signinMgr, IPasswordHasher<User> passwordHasher)
        {
            _personManager = userMgr;
            _passwordHasher = passwordHasher;
            _signInManager = signinMgr;
        }
        
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                User appUser = await _personManager.FindByNameAsync(login.UserName);
                if (appUser != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                    if (result.Succeeded)
                    {
                       return Redirect(login.ReturnUrl ?? "/");
                    }
                       
                }
                ModelState.AddModelError(nameof(login.UserName), "Login Failed: Invalid Username or password");
            }
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //[AllowAnonymous]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword([Required] string email)
        //{
        //    if (!ModelState.IsValid)
        //        return View(email);

        //    var user = await _personManager.FindByEmailAsync(email);
        //    if (user == null)
        //        return RedirectToAction(nameof(ForgotPasswordConfirmation));

        //    var token = await _personManager.GeneratePasswordResetTokenAsync(user);
        //    var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            
        //    bool emailResponse = _emailRepository.SendEmailPasswordReset(user.Email, link);

        //    if (emailResponse)
        //        return RedirectToAction("ForgotPasswordConfirmation");
        //    else
        //    {
        //        // log email failed 
        //    }
        //    return View(email);
        //}

        //[AllowAnonymous]
        //public IActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}

        //[AllowAnonymous]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    var model = new ResetPassword { Token = token, Email = email };
        //    return View(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        //{
        //    if (!ModelState.IsValid)
        //        return View(resetPassword);

        //    var user = await _personManager.FindByEmailAsync(resetPassword.Email);
        //    if (user == null)
        //        RedirectToAction("ResetPasswordConfirmation");

        //    var resetPassResult = await _personManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
        //    if (!resetPassResult.Succeeded)
        //    {
        //        foreach (var error in resetPassResult.Errors)
        //            ModelState.AddModelError(error.Code, error.Description);
        //        return View();
        //    }
        //    return RedirectToAction("ResetPasswordConfirmation");
        //}
        //public IActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}
        public IActionResult AccessDenied()
        {
            return View();
        }
        
    }
}
