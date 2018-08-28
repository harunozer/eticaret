using HelperLayer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace wwwAdmin.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                string routeWhereExceptionOccurred = exceptionFeature.Path;

                Exception exceptionThatOccurred = exceptionFeature.Error;

                ExceptionDetail detail = new ExceptionDetail
                {
                    Message = exceptionThatOccurred.Message,
                    StackTrace = exceptionThatOccurred.StackTrace,
                    Path = exceptionThatOccurred.Source
                };

                //Mail gönderme, log alma vs.. burada yapılabilir.
                if (exceptionThatOccurred.GetType() == typeof(PermissionException))
                {
                    HttpContext.Response.StatusCode = 401; //Yetkisiz istek
                }
                else if (exceptionThatOccurred.GetType() == typeof(NotFoundDataException))
                {
                    HttpContext.Response.StatusCode = 404; //kayıt bulunamadı
                }
                else
                {
                    //Burada mail vs. atılabilir log tutulabilir.
                    HttpContext.Response.StatusCode = 500;
                }

                return View(detail);
            }

            return RedirectToAction("Index", "Home");

        }
    }
}