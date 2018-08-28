using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace wwwAdmin.Controllers
{
    public class LogoutController : BaseController
    {
        readonly UserService userService;
        public LogoutController(UserService userService_)
        {
            userService = userService_;
        }

        public IActionResult Index()
        {
            userService.Logout();
            return Redirect("/Login/");
        }
    }
}