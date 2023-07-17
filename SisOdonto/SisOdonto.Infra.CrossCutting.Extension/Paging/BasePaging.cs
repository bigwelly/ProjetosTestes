using SisOdonto.Infra.CrossCutting.Extension.Generic;

namespace SisOdonto.Infra.CrossCutting.Extension.Paging
{
    public abstract class BasePaging
    {
        public ResultPaging<TInstance> GetResultPaging<TInstance>(IList<TInstance> instance, int pageCount, int pageSize,
            List<string> orders = null) where TInstance : class
        {
            if (orders != null) instance = instance.AsQueryable().OrderBy(orders.ToArray()).ToList();

            return ConvertToObjectPaginated(instance, pageCount, pageSize);
        }

        public ResultPaging<TInstance> GetResultPaging<TInstance>(IQueryable<TInstance> instance, int pageCount, int pageSize,
            List<string> orders = null) where TInstance : class
        {
            if (orders != null) instance = instance.OrderBy(orders.ToArray());

            return ConvertToObjectPaginated(instance, pageCount, pageSize);
        }

        private ResultPaging<TInstance> ConvertToObjectPaginated<TInstance>(IEnumerable<TInstance> query, 
            int page, int pageSize) where TInstance : class
        {
            page = page == 0 ? 1 : page;
            pageSize = pageSize == 0 ? 10 : pageSize;

            var result = new ResultPaging<TInstance>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Rows = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
