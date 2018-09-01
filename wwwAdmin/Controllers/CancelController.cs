using DataLayer.Models;
using DataLayer.Services;
using System;
using wwwAdmin.Models;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class CancelController : BaseModelController<Cancel, CancelService, CancelFormModel, CancelListModel>
    {
        public CancelController(CancelService cancelService_) : base(cancelService_) { }

        public override void bindFilterList(CancelListModel model)
        {
            ListFilterItem NameItem = new ListFilterItem
            {
                ColName = "CancelName",
                LabelName = "İptal Nedeni",
                FilterType = ListFilterType.Contains,
                FilterDataType = ListFilterDataType.String,
                FilterInputType = ListFilterInputType.Textbox
            };
            model.ListProps.Filters.Add(NameItem);
        }

        public override Cancel getUpdatedData(CancelFormModel model)
        {
            Cancel updatedData = service.Get(model.Data.ID);
            updatedData.CancelName = model.Data.CancelName;
            updatedData.CancelID = model.Data.CancelID;
            return updatedData;
        }

        public override void bindFormModel(CancelFormModel model)
        {
            base.bindFormModel(model);
            model.CancelList = service.getList();
            base.Model_ = model;
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}