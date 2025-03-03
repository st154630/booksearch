using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace mass
{
    public class BookContext : DbContext
    {
        //i dont remember why i need all 3 of these but im pretty sure i do
        // *maybe not
        public DbSet<Book> Books { get; set; }
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Translator> Translators { get; set; }
        public string DbPath { get; }
        public BookContext()
        {
            //define the basic stuff
            DbPath = "booktest.db";
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // TEMPORARY, THE DATA LOGGING SHOULD NOT BE IN THE FINAL CODE
            options.EnableSensitiveDataLogging();
            options.UseSqlite("Data Source=booktest.db");
            options.LogTo(message => Debug.WriteLine(message));
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //just in case the [Key] in the class definition didnt work

            modelBuilder.Entity<Author>()
                .HasKey(e => new{e.auid});

            modelBuilder.Entity<Translator>()
                .HasKey(e => new {e.trid});
        }
    }


    //model for how the gutendex db is roughly structured
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<Author> authors { get; set; }
        public List<string>? summaries { get; set; }
        public List<Translator> translators { get; set; }
        public List<string>? subjects { get; set; }
        public List<string>? bookshelves { get; set; }
        public List<string>? languages { get; set; }
        public bool copyright { get; set; }
        public string media_type { get; set; }
        //public Formats formats { get; set; }
        public int download_count { get; set; }
        public int score {  get; set; }
    }
    //public class Formats
    //{
    //    [Key]
    //    public int id { get; set; }
    //    public string texthtml { get; set; }
    //    public string applicationepubzip { get; set; }
    //    public string applicationxmobipocketebook { get; set; }
    //    public string textplaincharsetusascii { get; set; }
    //    public string textplaincharsetutf8 { get; set; }
    //    public string applicationrdfxml { get; set; }
    //    public string imagejpeg { get; set; }
    //    public string applicationoctetstream { get; set; }
    //    public string applicationpdf { get; set; }
    //    public string applicationprstex { get; set; }
    //    public string texthtmlcharsetutf8 { get; set; }
    //    public string textplaincharsetiso88591 { get; set; }
    //}

    public class Author
    {
        [Key]
        public int auid { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int? birth_year { get; set; }
        public int? death_year { get; set; }
    }

    public class Translator
    {
        [Key]
        public int trid { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int? birth_year { get; set; }
        public int? death_year { get; set; }
    }
}
