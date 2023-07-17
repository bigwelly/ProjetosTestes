using System.Collections.Generic;

namespace SisOdonto.Infra.CrossCutting.Extension.Paging
{
    public class ResultPaging<TInstance>
    {
        public IList<TInstance> Rows { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
}
