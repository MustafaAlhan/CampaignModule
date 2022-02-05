using CM.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Context
{
    public class CMDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.Json");
            IConfiguration configuration = configBuilder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Campaigns> Campaigns { get; set; }
        public DbSet<TotalAddedHour> TotalAddedHour { get; set; }
    }
}
