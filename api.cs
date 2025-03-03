using mass;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json;
using static mass.Author;
//using static mass.Formats;
using static mass.Translator;
using System.Xml.Serialization;

namespace Test
{

    public class Gutendex
    {

        // define class for base result from api call 
        public class Rootobject
        {
            public int count { get; set; }
            public string next { get; set; }
            public object previous { get; set; }
            public Result[] results { get; set; }
        }

        //change the name of the book class for convenience
        public class Result : Book
        {
            
        }

        
    }
}
//https://gutendex.com/books?author_year_start=1900&author_year_end=1910&languages=en&page=1&copyright=true