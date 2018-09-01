using DataLayer.Models;
using DataLayer.Services;
using System;
using wwwAdmin.Models;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class CustomerController : BaseModelController<Customer,CustomerService,CustomerFormModel,CustomerListModel>
    {
        private readonly CancelService cancelService;

        public CustomerController(CustomerService customerService_,CancelService cancelService_):base(customerService_)
        {
            cancelService = cancelService_;
        }

        public override void bindFilterList(CustomerListModel model)
        {
            ListFilterItem filterName = new ListFilterItem
            {
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
        }

        public override Customer getUpdatedData(CustomerFormModel model)
        {
            Customer updatedData = service.Get(model.Data.ID);
            updatedData.Name = model.Data.Name;
            updatedData.Surname = model.Data.Surname;
            updatedData.EMail = model.Data.EMail;
            updatedData.Gsm = model.Data.Gsm;
            updatedData.Password = model.Data.Password;
            updatedData.Gender = model.Data.Gender;
            updatedData.DateOfBirth = model.Data.DateOfBirth;
            updatedData.CancelID = model.Data.CancelID;
            return updatedData;
        }

        public override void bindFormModel(CustomerFormModel model)
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