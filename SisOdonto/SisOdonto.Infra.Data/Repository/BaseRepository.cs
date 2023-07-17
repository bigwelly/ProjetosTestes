using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SisOdonto.Infra.Data.Context;
using SisOdonto.Infra.Data.Interfaces;
using System.Data.Common;
using System.Linq.Expressions;

namespace SisOdonto.Infra.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SisOdontoContext _context;
        protected IDbContextTransaction _transaction;

        public int? Page { get; set; }
        public int? Limit { get; set; }

        SqlConnection cn = new SqlConnection();

        public BaseRepository(SisOdontoContext context)
        {
            _context = context;
            Page = Limit = null;
        }

        public void SetPagination<E>(IQueryable<E> query)
        {
            if (Page != null && Limit != null)
            {
                var skip = ((int)Page - 1) * (int)Limit;
                query = query.Skip(skip).Take((int)Limit);
            }
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DetachObject(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        protected bool BooleanTypeFromDataReader(DbDataReader reader)
        {
            return GetValue<bool>(reader[0]);
        }

        protected static G GetValue<G>(object data)
        {
            G returnValue = default(G);
            if (data != null && data != DBNull.Value)
            {
                var typeCodeIn = System.Convert.GetTypeCode(data);
                var typeCodeOut = System.Convert.GetTypeCode(returnValue);
                if (typeCodeOut != TypeCode.Object && typeCodeOut != TypeCode.Empty && typeCodeIn != typeCodeOut)
                    returnValue = (G)System.Convert.ChangeType(data, typeCodeOut);
                else
                {
                    Type typeOfResult = typeof(G);
                    Type genericType = Nullable.GetUnderlyingType(typeOfResult);
                    if (genericType != null)
                        returnValue = (G)System.Convert.ChangeType(data, genericType);
                    else
                        returnValue = (G)data;
                }
            }
            return returnValue;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().Where(predicate);
            SetPagination(query);
            return query;
        }

        public IQueryable<E> Find<E>(Expression<Func<E, bool>> predicate) where E : class
        {
            var query = _context.Set<E>().Where(predicate);
            SetPagination(query);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T FindById(params object[] id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> FindPage(int skip, int size)
        {
            return _context.Set<T>().Skip(skip).Take(size);
        }

        public virtual void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public int CountByRawSql(string sql, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
