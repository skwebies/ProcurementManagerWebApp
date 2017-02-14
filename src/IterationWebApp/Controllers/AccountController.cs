using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using IterationWebApp.ViewModels;
using Microsoft.AspNet.Authorization;
using IterationWebApp.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IterationWebApp.Controllers
{
    [RequireHttps]
    public class AccountController : Controller
    {
        private IdentityDbContext _identityContext;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private IIterationRepository _repository;
        private ILoggerFactory _logger;

        public AccountController(IdentityDbContext identityContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IIterationRepository repository, ILoggerFactory logger)
        {
            _identityContext = identityContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var users = _repository.GetAllUsers();

            return View(users);
        }


        [Authorize]
        public ViewResult Register()
        {
            if (TempData["created"] != null)
            {
                string msg = TempData["created"].ToString();
                ViewData["Registered"] = msg;
            }
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Register(RegisterUserViewModel vmUser)
        {
            if (!ModelState.IsValid)
                return View(vmUser);

            await _identityContext.Database.EnsureCreatedAsync();
            var user = new IdentityUser();
            user.UserName = vmUser.Email;
            user.Email = vmUser.Email;

            var result = await _userManager.CreateAsync(user, vmUser.Password);

            if (result.Succeeded)
            {
                TempData["success"] = "New User " + vmUser.Email + " is created successfully!";
                //await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }



            TempData["error"] = "Registration Failed! try again...";
            return View();






        }
        //[HttpPost]
        //public JsonResult regUser(RegisterUserViewModel vmUser)
        //{
        //    var user = new IdentityUser();
        //    user.Email = vmUser.Email;

        //    return Json(vmUser);

        //}

        //User Login form load...
        [AllowAnonymous]
        public ViewResult Login(string returnUrl = "")
        {
            var model = new LoginUserViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        #region Login User
        //User Login form action
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserViewModel vmUser)
        {
            if (ModelState.IsValid)
            {
                //await _identityContext.Database.EnsureCreatedAsync();

                var result = await _signInManager.PasswordSignInAsync(
                   vmUser.Email,
                   vmUser.Password,
                   vmUser.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(vmUser.ReturnUrl) && Url.IsLocalUrl(vmUser.ReturnUrl))
                    {
                        return Redirect(vmUser.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
            }

            ModelState.AddModelError("", "Invalid login attempt!");
            return View(vmUser);

        }
        #endregion

        #region Logout user
        //User Logout
        [HttpPost]
        public async Task<IActionResult> Logout(LoginUserViewModel userLoginViewModel, string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl);


        }
        #endregion

        public ActionResult ForgotPassword()
        {

            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                new { UserId = user.Id, code = code }, protocol: Request.Path.Value);
                //await _userManager.SetEmailAsync(user.Id, "Reset Password",
                //"Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ViewResult ForgotPasswordConfirmation()
        {
            return View();
        }


        #region Reset Password
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string code=null)
        {
            var user = await _userManager.FindByNameAsync(email);
            var Code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var model = new ResetPasswordViewModel();
            model.Code = Code;
            return model == null ? View("Error") : View();
            //return View(new ResetPasswordViewModel());
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            model.Code = code;
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
           // model.Code = model.Email;
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion



        public IActionResult Delete(string id)
        {
            var user = _repository.GetUser(id);
            _identityContext.Users.Remove(user);
            _identityContext.SaveChanges();
            TempData["Delete"] = "User " + user.Email + " has been deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
