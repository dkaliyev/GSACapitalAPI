using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TradeContext : DbContext
    {
        public DbSet<Strategy> Strategy { get; set; }
        public DbSet<Capital> Capitals { get; set; }
        public DbSet<ProfitNLoss> ProfitNLoss { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;Initial Catalog=MyDatabase;Trusted_Connection=True;");
        }

    }
}
