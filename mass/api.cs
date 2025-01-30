using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Serialization;

namespace Test
{
    public class gutendex
    {


        static Rootobject APIcall(string url)
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

        public class Formats
        {
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
            public string name { get; set; }
            public int? birth_year { get; set; }
            public int? death_year { get; set; }
        }

        public class Translator
        {
            public string name { get; set; }
            public int? birth_year { get; set; }
            public int? death_year { get; set; }
        }


        private void DBadd(string[] args, string result)
        {

        }


    }
}
//https://gutendex.com/books?author_year_start=1900&author_year_end=1910&languages=en&page=1&copyright=true
