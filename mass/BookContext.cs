﻿using System;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
            => optionsBuilder.UseSqlite("Data Source=booktest.db");
        
    }
}
