using System;
using System.Collections.Generic;

namespace wwwAdmin.Models.ListModel
{
    public class BaseListPartialModel
    {
        //Sort
        public string SortField { get; set; } = "ID";
        public string SortType { get; set; } = "asc";
        public readonly string SortFieldName = "x_sort_field";
        public readonly string SortTypeName = "x_sort_type";

        //Pageing
        public int PageRecordCount { get; set; } = 20;
        public int ActivePageNumber { get; set; } = 1;
        public int TotalRecordCount { get; set; }
        public int PageCount
        {
            get
            {
                if (TotalRecordCount == 0) return 0;
                return (int)Math.Round((1.0 * TotalRecordCount / PageRecordCount) + 0.5);
            }
        }
        public readonly string PageRecordCountName = "x_page_rc";
        public readonly string ActivePageNumberName = "x_active_pn";

        //filters
        //TODO: FilterString or ile FilterFields alanlarda filtrelenecek
        public string FilterString { get; set; } //SearchString
        public string FilterFields { get; set; } //Field1,Field2

        public List<ListFilterItem> Filters { get; set; } = new List<ListFilterItem>();
    }
}