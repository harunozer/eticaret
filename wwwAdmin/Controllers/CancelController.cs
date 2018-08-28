using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Services;
using HelperLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using wwwAdmin.Models;
using wwwAdmin.Models.FormModel;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class CancelController : BaseController
    {
        readonly CancelService cancelService;

        public CancelController(CancelService cancelService_)
        {
            cancelService = cancelService_;
        }

        [HttpGet]
        public IActionResult Index()
        {
            CancelListModel model = new CancelListModel();
            model.InsertPermission = cancelService.CheckInsertPermission();

            ListFilterItem NameItem = new ListFilterItem
            {
                ColName = "CancelName",
                LabelName = "İptal Nedeni",
                FilterType = ListFilterType.Contains,
                FilterDataType = ListFilterDataType.String,
                FilterInputType = ListFilterInputType.Textbox
            };
            model.ListProps.Filters.Add(NameItem);

            model.BuildQuery(HttpContext, cancelService.getListQuery());
            cancelService.BindPermissionsList(model.DataList);

            base.Model_ = model;
            return View(model);
        }

        [HttpGet]
        public IActionResult Form(int? id)
        {
            CancelFormModel model = new CancelFormModel();
            model.FormType = id != null ? FormType.Update : FormType.Insert;

            if (model.FormType == FormType.Insert)
            {
                if (!cancelService.CheckInsertPermission())
                    throw new PermissionException();

            }
            else if (model.FormType == FormType.Update)
            {
                model.Data = cancelService.Get((int)id);
                if (!model.Data.Permissions.Update)
                    throw new PermissionException();
            }

            BindFormModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Form(CancelFormModel model)
        {
            if (!ModelState.IsValid)
            {
                BindFormModel(model);
                return View(model);
            }

            model.FormType = model.Data.ID == 0 ? FormType.Insert : FormType.Update;
            try
            {
                if (model.FormType == FormType.Insert)
                {
                    cancelService.Add(model.Data);
                }
                else if (model.FormType == FormType.Update)
                {
                    Cancel updatedData = cancelService.Get(model.Data.ID);
                    updatedData.CancelName = model.Data.CancelName;
                    updatedData.CancelID = model.Data.CancelID;

                    cancelService.Update(updatedData);
                }
            }
            catch (ValidationException exValidation)
            {
                setErrorMesaage(exValidation.Message);
                BindFormModel(model);
                return View(model);
            }

            setSuccessMesaage("Kayıt başarılı");
            return RedirectToAction("Index", "Cancel");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            return View(cancelService.Get(id));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            cancelService.Delete(id);
            setSuccessMesaage("Kayıt silindi");
            return RedirectToAction("Index", "Cancel");
        }

        private void BindFormModel(CancelFormModel model)
        {
            model.CancelList = cancelService.getList();
            base.Model_ = model;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}