using Microsoft.AspNetCore.Mvc;
using System;
using wwwAdmin.Filters;

namespace wwwAdmin.Controllers
{
    [AuthFilter]
    public class BaseController : Controller 
    {
        public IDisposable Model_;
        
        public void setErrorMesaage(string message)
        {
            TempData["errorMessage"] = message;
        }

        public void setSuccessMesaage(string message)
        {
            TempData["successMessage"] = message;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (Model_ != null) Model_.Dispose();
        }
    }
}
