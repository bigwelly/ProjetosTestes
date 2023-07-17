using System.Linq.Expressions;

namespace SisOdonto.Infra.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        int? Page { get; set; }
        int? Limit { get; set; }

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Find(Expression<Func<T, Boolean>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> FindPage(int skip, int size);
        T FindById(params object[] id);
        void DetachObject(T entity);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        int CountByRawSql(string sql, Dictionary<string, string> parameters);
    }
}
