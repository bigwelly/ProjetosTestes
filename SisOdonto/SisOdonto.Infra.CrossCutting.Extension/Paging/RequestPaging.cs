using System.Collections.Generic;

namespace SiSisOdonto.Infra.CrossCutting.Extension.Paging
{
    public class RequestPaging
    {
        public List<string> Orders { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}
