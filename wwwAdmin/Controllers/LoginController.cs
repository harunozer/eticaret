using DataLayer.Models.NotMapped;
using DataLayer.Services;
using HelperLayer;
using HelperLayer.web;
using Microsoft.AspNetCore.Mvc;

namespace wwwAdmin.Controllers
{
    public class LoginController : BaseController
    {
        readonly UserService userService;
        public LoginController(UserService userService_)
        {
            userService = userService_;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Login form view
            return View(HttpContext.CookieObjectGet<LoginForm>(Consts.CookieNameLoginUser));
        }

        [HttpPost]
        public IActionResult Index(LoginForm model)
        {
            if (ModelState.IsValid)
            {
                if (userService.Login(model) != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Kullanıcı bulunamadı.
                    ViewData["Login-err"] = "Kullanıcı Bulunamadı";
                }
            }

            //LoginFormAction
            return View(model);
        }
    }
}