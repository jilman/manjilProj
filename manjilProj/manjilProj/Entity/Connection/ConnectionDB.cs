using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Entity.Connection
{
   public class ConnectionDB:DbContext
    {
       
        public ConnectionDB(DbContextOptions<ConnectionDB> options) : base(options)
        {
        }

        public ConnectionDB()
        {

        }

        private IConfiguration Configuration { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = configuration.Build();
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DELL\\SQL2014;Database=Portfolio;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
            
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<PortfolioAccount> PortfolioAccount { get; set; }
        public DbSet<StockReceiveMast> StockReceiveMast { get; set; }
        public DbSet<StockReceiveDetl> StockReceiveDetl { get; set; }
    }
}
