using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Test.Gutendex;
namespace mass
{
    public class BookContext : DbContext
    {
        public DbSet<Book> BooksTest { get; set; }
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Translator> Translators { get; set; }
        public string DbPath { get; }
        public BookContext()
        {
            DbPath = "booktest.db";
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging();
            options.UseSqlite("Data Source=booktest.db");
            options.LogTo(message => Debug.WriteLine(message));
        }
           
            

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(e => e.authors)
                .WithOne()
                .HasForeignKey(e => e.id);
            modelBuilder.Entity<Book>()
                .HasMany(e => e.translators)
                .WithOne()
                .HasForeignKey(e => e.id);
        }
    }
}
