using mass;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml.Serialization;

namespace Test
{

    public class Gutendex
    {


        public Rootobject callapi(string url)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://gutendex.com/books");
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;

                Rootobject? final = JsonSerializer.Deserialize<Rootobject>(result);
                return final;
            }
        }


        public class Rootobject
        {
            public int count { get; set; }
            public string next { get; set; }
            public object previous { get; set; }
            public Result[] results { get; set; }
        }

        public class Result
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

        public class Formats
        {
            public int id { get; set; }
            public string texthtml { get; set; }
            public string applicationepubzip { get; set; }
            public string applicationxmobipocketebook { get; set; }
            public string textplaincharsetusascii { get; set; }
            public string textplaincharsetutf8 { get; set; }
            public string applicationrdfxml { get; set; }
            public string imagejpeg { get; set; }
            public string applicationoctetstream { get; set; }
            public string applicationpdf { get; set; }
            public string applicationprstex { get; set; }
            public string texthtmlcharsetutf8 { get; set; }
            public string textplaincharsetiso88591 { get; set; }
        }

        public class Author
        {
            [Key]
            public int id { get; set; }
            public string name { get; set; }
            public int? birth_year { get; set; }
            public int? death_year { get; set; }
        }

        public class Translator
        {
            [Key]
            public int id { get; set; }
            public string name { get; set; }
            public int? birth_year { get; set; }
            public int? death_year { get; set; }
        }


        private void DBadd(string[] args, Result[] results)
        {
            var db = new BookContext();
            for (int i = 0; i < results.Length; i++) 
            {
                var bk = new Book() { id = results[i].id, authors = results[i].authors };
                db.Add(bk);
            }
        }


    }
}
//https://gutendex.com/books?author_year_start=1900&author_year_end=1910&languages=en&page=1&copyright=true