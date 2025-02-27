using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace mass
{
    public class BookContext : DbContext
    {
        public DbSet<Book> BooksTest { get; set; }
        public string DbPath { get; }
        public BookContext()
        {
            DbPath = "booktest.db";
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        
            => options.UseSqlite("Data Source=booktest.db");
        
    }
}
