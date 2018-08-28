namespace wwwAdmin.Models.ListModel
{
    public enum ListFilterType
    {
        Equal = 1, //=
        GreaterThan = 2, //>
        LessThan = 3, //<
        GreaterThanOrEqual = 4, //>=
        LessThanOrEqual = 5, //<=
        StartsWith = 6,
        Contains = 7,
        Between = 8,
        MultipleInValue = 9,
        SingleInValue = 10
    };
}