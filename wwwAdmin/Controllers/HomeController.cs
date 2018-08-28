using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace wwwAdmin.Controllers
{
    public class HomeController : BaseController
    {
        readonly UserService userService;
        public HomeController(UserService userService_)
        {
            userService = userService_;
        }

        public IActionResult Index()
        {
            //AdminAnasayfaView
            //userService.UserAddTest(20);
            return View();
        }
    }
}