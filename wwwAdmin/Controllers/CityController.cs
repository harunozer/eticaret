using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Linq;
using wwwAdmin.Models;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class CityController
        : BaseModelController<City, CityService, CityFormModel, CityListModel>
    {
        private readonly CancelService cancelService;
        private readonly CountryService countryService;

        public CityController(CityService service_, CancelService cancelService_, CountryService countryService_) : base(service_)
        {
            cancelService = cancelService_;
            countryService = countryService_;
        }

        public override void bindFilterList(CityListModel model)
        {
            ListFilterItem itemCountry = new ListFilterItem
            {
                ColName = "Country",
                LabelName = "Ülke",
                FilterDataType = ListFilterDataType.Numeric,
                FilterInputType = ListFilterInputType.ListBox,
                FilterType = ListFilterType.Equal
            };
            itemCountry.InputValues.AddRange(
                countryService.getList().Select(i => new InputListItem { Text = i.CountryName, Value = i.ID.ToString() })
            );
            model.ListProps.Filters.Add(itemCountry);
        }

        public override City getUpdatedData(CityFormModel model)
        {
            City updatedData = service.Get(model.Data.ID);
            updatedData.CityName = model.Data.CityName;
            updatedData.CountryID = model.Data.CountryID;
            updatedData.CancelID = model.Data.CancelID;
            return updatedData;
        }

        public override void bindFormModel(CityFormModel model)
        {
            model.CancelList = cancelService.getList();
            model.CountryList = countryService.getList();
            base.Model_ = model;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}