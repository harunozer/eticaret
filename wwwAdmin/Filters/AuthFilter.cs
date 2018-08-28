using DataLayer.Models;
using HelperLayer.web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace wwwAdmin.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller.GetType() == typeof(Controllers.ErrorController))
            {
                //Error da yetki sınırlaması yok
            }
            else if (context.Controller.GetType() != typeof(Controllers.LoginController))
            {
                //Add formda ID validation error ignore
                if (!context.ModelState.IsValid) context.ModelState.Remove("Data.ID");

                //sadece login controller actionlarda login olmamış olabilir.
                if (context.HttpContext.SessionGetLoginUser<User>() == null)
                    context.Result = new RedirectResult("/Login/");
            }
            else
            {
                //zaten login ise login sayfasına giremez
                if (context.HttpContext.SessionGetLoginUser<User>() != null)
                    context.Result = new RedirectResult("/Home/");
            }
        }
    }
}
