using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection.Metadata;
using static Test.Gutendex;


namespace mass
{
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
        public Formats formats { get; set; }
        public int download_count { get; set; }
    }

}
