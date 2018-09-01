using DataLayer.Services;
using HelperLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using wwwAdmin.Models.FormModel;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class BaseModelController<M, S, F, L> : BaseController
        where M : class, new() //Model 
        where S : BaseService<M> //Service
        where F : BaseFormModel<M>, new() //FormModel
        where L : BaseListModel<M>, new() //ListModel
    {
        public readonly S service;

        public BaseModelController(S service_)
        {
            service = service_;
        }

        public virtual void bindFilterList(L model) { }
        public virtual void bindFormModel(F model) { }
        public virtual M getUpdatedData(F model) { return new M(); }


        [HttpGet]
        public virtual IActionResult Index()
        {
            L model = new L();
            model.InsertPermission = service.CheckInsertPermission();

            bindFilterList(model);

            model.BuildQuery(HttpContext, service.getListQuery());
            service.BindPermissionsList(model.DataList);

            base.Model_ = model;
            return View(model);
        }

        [HttpGet]
        public virtual IActionResult Form(int? id)
        {
            //UserAdd - UserUpdate Form
            F model = new F();
            model.FormType = id != null ? FormType.Update : FormType.Insert;

            if (model.FormType == FormType.Insert)
            {
                if (!service.CheckInsertPermission())
                    throw new PermissionException();

            }
            else if (model.FormType == FormType.Update)
            {
                model.Data = service.Get((int)id);
                if (!service.GetDataPermission(model.Data).Update)
                    throw new PermissionException();
            }
            
            bindFormModel(model);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Form(F model)
        {
            if (!ModelState.IsValid)
            {
                bindFormModel(model);
                return View(model);
            }

            model.FormType = ((int)model.Data.GetType().GetProperty("ID").GetValue(model.Data)) == 0 ? FormType.Insert : FormType.Update;

            try
            {
                if (model.FormType == FormType.Insert)
                    service.Add(model.Data);
                else if (model.FormType == FormType.Update)
                    service.Update(getUpdatedData(model));
            }
            catch (ValidationException exValidation)
            {
                setErrorMesaage(exValidation.Message);
                bindFormModel(model);
                return View(model);
            }
            setSuccessMesaage("Kayıt başarılı");
            
            return RedirectToAction("Index", ControllerContext.RouteData.Values["controller"].ToString());
        }

        [HttpGet]
        public virtual IActionResult View(int id)
        {
            return View(service.Get(id));
        }

        [HttpGet]
        public virtual IActionResult Delete(int id)
        {
            service.Delete(id);
            setSuccessMesaage("Kayıt silindi");
            return RedirectToAction("Index", ControllerContext.RouteData.Values["controller"].ToString());
        }
    }
}