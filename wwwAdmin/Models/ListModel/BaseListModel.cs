using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using HelperLayer.web;
using DataLayer;

namespace wwwAdmin.Models.ListModel
{
    public class BaseListModel<T> : IDisposable
    {
        //TODO: global filtre kaydetme yapılacak.
        //Db de Filters tablosu açılır ID - User - ListName - FiltersName - Data
        //Liste formuna "Filtre Kaydet" butonu eklenir. (popup ile isim istenir, ajax ile çalışır)
        //Listelere global isim verilir, Filter listesi json serilize olarak Data alanına basılır. (seçili value değerleri ile)
        //Kayıtlı filtre varsa filtre formuna selectbox eklenir
        public bool InsertPermission { get; set; }

        public List<T> DataList { get; set; }

        public IQueryable<T> query { get; set; }

        public BaseListPartialModel ListProps { get; set; } = new BaseListPartialModel();

        public void BindValues(HttpContext httpContext)
        {
            //SortValues
            if (httpContext.GetRequestItem(ListProps.SortFieldName) != "")
                ListProps.SortField = httpContext.GetRequestItem(ListProps.SortFieldName);

            if (httpContext.GetRequestItem(ListProps.SortTypeName) != "")
                ListProps.SortType = httpContext.GetRequestItem(ListProps.SortTypeName);

            //PagerValues
            try
            {
                ListProps.PageRecordCount = Convert.ToInt32(httpContext.GetRequestItem(ListProps.PageRecordCountName));
            }
            catch (Exception) { }

            try
            {
                ListProps.ActivePageNumber = Convert.ToInt32(httpContext.GetRequestItem(ListProps.ActivePageNumberName));
            }
            catch (Exception) { }

            //FiltersValue
            foreach (ListFilterItem item in ListProps.Filters)
            {
                string DataVal = httpContext.GetRequestItem(item.FilterName);
                item.FilterValue1 = DataVal != "" ? DataVal : item.FilterValue1;

                DataVal = httpContext.GetRequestItem(item.FilterName2);
                item.FilterValue2 = DataVal != "" ? DataVal : item.FilterValue2;
            }
        }

        public void BuildQuery(HttpContext httpContext)
        {
            BindValues(httpContext);

            #region Filters
            foreach (var item in ListProps.Filters)
            {
                if (string.IsNullOrEmpty(item.FilterValue1))
                    continue;

                ListFilterDataType dataType = item.FilterDataType;
                if (dataType == ListFilterDataType.None) continue;

                switch (item.FilterType)
                {
                    case ListFilterType.Equal: // =
                        //AllType
                        switch (dataType)
                        {
                            case ListFilterDataType.Numeric:
                                query = query.Where(u => EF.Property<int>(u, item.ColName) == Convert.ToInt32(item.FilterValue1));
                                break;
                            case ListFilterDataType.Date:
                                query = query.Where(u => EF.Property<DateTime>(u, item.ColName) == Convert.ToDateTime(item.FilterValue1));
                                break;
                            case ListFilterDataType.String:
                                query = query.Where(u => EF.Property<string>(u, item.ColName) == item.FilterValue1);
                                break;
                            case ListFilterDataType.Bool:
                                query = query.Where(u => EF.Property<bool>(u, item.ColName) == Convert.ToBoolean(item.FilterValue1));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.GreaterThan: // >
                        //Numeric - Date
                        switch (dataType)
                        {
                            case ListFilterDataType.Numeric:
                                query = query.Where(u => EF.Property<int>(u, item.ColName) > Convert.ToInt32(item.FilterValue1));
                                break;
                            case ListFilterDataType.Date:
                                query = query.Where(u => EF.Property<DateTime>(u, item.ColName) > Convert.ToDateTime(item.FilterValue1));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.LessThan: // <
                        //Numeric - Date
                        switch (dataType)
                        {
                            case ListFilterDataType.Numeric:
                                query = query.Where(u => EF.Property<int>(u, item.ColName) < Convert.ToInt32(item.FilterValue1));
                                break;
                            case ListFilterDataType.Date:
                                query = query.Where(u => EF.Property<DateTime>(u, item.ColName) < Convert.ToDateTime(item.FilterValue1));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.GreaterThanOrEqual: // >=
                        //Numeric - Date 
                        switch (dataType)
                        {
                            case ListFilterDataType.Numeric:
                                query = query.Where(u => EF.Property<int>(u, item.ColName) >= Convert.ToInt32(item.FilterValue1));
                                break;
                            case ListFilterDataType.Date:
                                query = query.Where(u => EF.Property<DateTime>(u, item.ColName) >= Convert.ToDateTime(item.FilterValue1));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.LessThanOrEqual: // <=
                        //Numeric - Date - DateTime
                        switch (dataType)
                        {
                            case ListFilterDataType.Numeric:
                                query = query.Where(u => EF.Property<int>(u, item.ColName) <= Convert.ToInt32(item.FilterValue1));
                                break;
                            case ListFilterDataType.Date:
                                query = query.Where(u => EF.Property<DateTime>(u, item.ColName) <= Convert.ToDateTime(item.FilterValue1));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.StartsWith: // like 'XX%'
                        //string
                        switch (dataType)
                        {
                            case ListFilterDataType.String:
                                query = query.Where(u => EF.Property<string>(u, item.ColName).StartsWith(item.FilterValue1));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.Contains: // like '%XX%'
                        //string
                        switch (dataType)
                        {
                            case ListFilterDataType.String:
                                query = query.Where(u => EF.Property<string>(u, item.ColName).Contains(item.FilterValue1));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.Between: // >= XX and <= XX
                        //Numeric - Date - DateTime
                        switch (dataType)
                        {
                            case ListFilterDataType.Numeric:
                                query = query.Where(u => EF.Property<int>(u, item.ColName) >= Convert.ToInt32(item.FilterValue1) && EF.Property<int>(u, item.ColName) <= Convert.ToInt32(item.FilterValue2));
                                break;
                            case ListFilterDataType.Date:
                                query = query.Where(u => EF.Property<DateTime>(u, item.ColName) >= Convert.ToDateTime(item.FilterValue1) && EF.Property<DateTime>(u, item.ColName) <= Convert.ToDateTime(item.FilterValue2));
                                break;
                            default:
                                break;
                        }
                        break;
                    case ListFilterType.MultipleInValue:
                        //Int (ID INs)
                        switch (dataType)
                        {
                            case ListFilterDataType.Numeric:
                                query = query.Where(u => item.FilterValue1.Contains("," + EF.Property<int>(u, item.ColName).ToString() + ","));
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            #endregion

            ListProps.TotalRecordCount = query.Count();

            #region Order
            if (!String.IsNullOrEmpty(ListProps.SortField) && !String.IsNullOrEmpty(ListProps.SortType))
            {

                var propertyInfo = typeof(T).GetProperty(ListProps.SortField);
                if (propertyInfo != null)
                {
                    if (ListProps.SortType == "desc")
                        query = query.OrderByDescending(i => propertyInfo.GetValue(i, null));
                    else
                        query = query.OrderBy(i => propertyInfo.GetValue(i, null));
                }
            }

            #endregion

            #region Pager
            if (ListProps.PageRecordCount > 0 && ListProps.ActivePageNumber > 0)
                query = query.Pageing(ListProps.ActivePageNumber, ListProps.PageRecordCount);
            #endregion

            DataList = query.ToList();
        }

        public void BuildQuery(HttpContext httpContext, IQueryable<T> query)
        {
            this.query = query;
            BuildQuery(httpContext);
        }
        public virtual void Dispose()
        {
            DataList.Clear();
            DataList = null;

            ListProps.Filters.Clear();
            ListProps.Filters = null;

            ListProps.SortField = null;
            ListProps.SortType = null;
            ListProps.FilterString = null;
            ListProps.FilterFields = null;
            ListProps = null;

            GC.SuppressFinalize(this);
        }
    }
}