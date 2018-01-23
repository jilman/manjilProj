using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Connection
{
   public class ConnectionDB:DbContext
    {
        public ConnectionDB()
        {

        }
        public ConnectionDB(DbContextOptions<ConnectionDB> options) : base(options)
        {
        }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<PortfolioAccount> PortfolioAccount { get; set; }
        public DbSet<StockReceiveMast> StockReceiveMast { get; set; }
        public DbSet<StockReceiveDetl> StockReceiveDetl { get; set; }
    }
}
