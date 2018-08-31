using System.Collections.Generic;

namespace wwwAdmin.Models.ListModel
{
    public class ListFilterItem
    {
        /* =, >, <, >=, <=, start like, like, between, InMultipleValue (like) */

        public string ColName { get; set; }
        public string LabelName { get; set; }
        public string FilterName { get { return "xflt_" + ColName; } }
        public string FilterName2 { get { return "xflt2_" + ColName; } }

        public ListFilterType FilterType { get; set; }
        public ListFilterDataType FilterDataType { get; set; } = ListFilterDataType.String;
        public ListFilterInputType FilterInputType { get; set; } = ListFilterInputType.Textbox;

        //parametre
        public string FilterValue1 { get; set; }
        public string FilterValue2 { get; set; }

        //ListBox, Radio da dolu olur
        public List<InputListItem> InputValues { get; set; } = new List<InputListItem>();

        public void BindDefaultBoolInputValues()
        {
            InputValues.Add(new InputListItem { Value = true.ToString(), Text = "Evet" });
            InputValues.Add(new InputListItem { Value = false.ToString(), Text = "Hayır" });
        }
        public string HtmlInputType
        {
            get
            {
                switch (FilterDataType)
                {
                    case ListFilterDataType.Numeric:
                        return "number";
                    case ListFilterDataType.Date:
                        return "date";
                }
                return "text";
            }
        }
    }
}
