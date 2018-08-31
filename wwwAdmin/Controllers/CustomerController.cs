using DataLayer.Models;
using DataLayer.Services;
using HelperLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using wwwAdmin.Models;
using wwwAdmin.Models.FormModel;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerService customerService;
        private readonly CancelService cancelService;

        public CustomerController(CustomerService customerService_,CancelService cancelService_)
        {
            customerService = customerService_;
            cancelService = cancelService_;
        }

        [HttpGet]
        public IActionResult Index()
        {
            CustomerListModel model = new CustomerListModel();
            model.InsertPermission = customerService.CheckInsertPermission();

            ListFilterItem filterName = new ListFilterItem {
                ColName = "Name",
                LabelName = "Ad",
                FilterType = ListFilterType.StartsWith
            };
            model.ListProps.Filters.Add(filterName);

            ListFilterItem filterSurname = new ListFilterItem
            {
                ColName = "Surname",
                LabelName = "Soyad",
                FilterType = ListFilterType.StartsWith
            };
            model.ListProps.Filters.Add(filterSurname);

            ListFilterItem filterMail = new ListFilterItem
            {
                ColName = "EMail",
                LabelName = "E-Posta",
                FilterType = ListFilterType.Equal
            };
            model.ListProps.Filters.Add(filterMail);

            ListFilterItem filterGsm = new ListFilterItem
            {
                ColName = "Gsm",
                LabelName = "Gsm",
                FilterType = ListFilterType.Equal
            };
            model.ListProps.Filters.Add(filterGsm);

            model.BuildQuery(HttpContext, customerService.getListQuery());
            customerService.BindPermissionsList(model.DataList);

            base.Model_ = model;
            return View(model);
        }
        [HttpGet]
        public IActionResult Form(int? id)
        {
            //UserAdd - UserUpdate Form
            CustomerFormModel model = new CustomerFormModel();
            model.FormType = id != null ? FormType.Update : FormType.Insert;

            if (model.FormType == FormType.Insert)
            {
                if (!customerService.CheckInsertPermission())
                    throw new PermissionException();

            }
            else if (model.FormType == FormType.Update)
            {
                model.Data = customerService.Get((int)id);
                if (!model.Data.Permissions.Update)
                    throw new PermissionException();
            }

            BindFormModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Form(CustomerFormModel model)
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
                    customerService.Add(model.Data);
                }
                else if (model.FormType == FormType.Update)
                {
                    Customer updatedData = customerService.Get(model.Data.ID);
                    updatedData.Name = model.Data.Name;
                    updatedData.Surname = model.Data.Surname;
                    updatedData.EMail = model.Data.EMail;
                    updatedData.Gsm = model.Data.Gsm;
                    updatedData.Password = model.Data.Password;
                    updatedData.Gender = model.Data.Gender;
                    updatedData.DateOfBirth = model.Data.DateOfBirth;
                    updatedData.CancelID = model.Data.CancelID;

                    customerService.Update(updatedData);
                }
            }
            catch (ValidationException exValidation)
            {
                setErrorMesaage(exValidation.Message);
                BindFormModel(model);
                return View(model);
            }

            setSuccessMesaage("Kayıt başarılı");
            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            return View(customerService.Get(id));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            customerService.Delete(id);
            setSuccessMesaage("Kayıt silindi");
            return RedirectToAction("Index", "Customer");
        }
        private void BindFormModel(CustomerFormModel model)
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