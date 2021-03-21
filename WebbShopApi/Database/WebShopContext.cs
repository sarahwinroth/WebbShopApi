using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebbShopApi.Models;

namespace WebbShopApi.Database
{
    public class WebShopContext : DbContext
    {
        private const string DatabaseName = "WebbShopSarahWinroth";
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<SoldBook> SoldBooks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer($@"Server = .\SQLEXPRESS;Database={DatabaseName};trusted_connection=true");
        }
    }
}
