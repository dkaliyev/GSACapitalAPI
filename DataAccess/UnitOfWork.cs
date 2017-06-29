using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private TradeContext _ctx;

        public UnitOfWork()
        {
            _ctx = new TradeContext();
            _ctx.Database.EnsureCreated();
        }

        ~UnitOfWork()
        {
            _ctx.Dispose();
        }

        public void Commit()
        {
            _ctx.SaveChanges();
        }

        public IRepository<T> GetRepository<T>()
            where T: class
        {
            return new Repository<T>(_ctx);
        }
    }
}
