using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace mass
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string[] Languages { get; set; }
        public int Downloads { get; set; }
        public string Link {  get; set; }
    }
}
