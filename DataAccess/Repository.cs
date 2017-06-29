using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess
{
    public class Repository<T>: IRepository<T>
        where T: class
    {
        public TradeContext _ctx;

        public Repository(TradeContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<T> GetQuery()
        {
            return _ctx.Set<T>();
        }

        public T Get(Expression<Func<T, bool>> exp)
        {
            T result = _ctx.Set<T>().FirstOrDefault(exp);
            return result;
        }

        public T Create(T entity)
        {
            var result = _ctx.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _ctx.Remove(entity);
        }
    }
}
