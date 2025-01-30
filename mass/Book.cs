using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection.Metadata;
using static Test.gutendex;

namespace mass
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public Author[] authors { get; set; }
        public string[] summaries { get; set; }
        public Translator[] translators { get; set; }
        public string[] subjects { get; set; }
        public string[] bookshelves { get; set; }
        public string[] languages { get; set; }
        public bool copyright { get; set; }
        public string media_type { get; set; }
        public Formats formats { get; set; }
        public int download_count { get; set; }
    }
}
