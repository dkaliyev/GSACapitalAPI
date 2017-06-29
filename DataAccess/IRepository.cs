using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataAccess
{
    public interface IRepository<T>
    {
        T Get(Expression<Func<T, bool>> exp);
        IQueryable<T> GetQuery();
        T Create(T entity);
        void Delete(T entity);
    }
}
