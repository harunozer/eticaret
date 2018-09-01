using DataLayer.Models;
using DataLayer.Services;
using System;
using wwwAdmin.Models;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class CountryController
        : BaseModelController<Country, CountryService, CountryFormModel, CountryListModel>
    {
        private readonly CancelService cancelService;
        public CountryController(CountryService service_,CancelService cancelService_) : base(service_)
        {
            cancelService = cancelService_;
        }

        public override void bindFilterList(CountryListModel model)
        {
            ListFilterItem filterName = new ListFilterItem {
                ColName = "CountryName",
                LabelName = "Ülke Adı",
                FilterType = ListFilterType.Contains
            };
            model.ListProps.Filters.Add(filterName);

        }

        public override Country getUpdatedData(CountryFormModel model)
        {
            Country updatedData = service.Get(model.Data.ID);
            updatedData.CountryName = model.Data.CountryName;
            updatedData.CancelID = model.Data.CancelID;
            return updatedData;
        }

        public override void bindFormModel(CountryFormModel model)
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