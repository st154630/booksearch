using System;
using System.Net;
using System.Net.Http;
using System.Xml.Serialization;

namespace Test
{
    public class gutendex
    {


        static string waidon(string[] args)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://gutendex.com/books");
                HttpResponseMessage response = client.GetAsync("?author_year_start=1900&author_year_end=1910&languages=en&page=1&copyright=true").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }           
        }

        private void DBadd(string[] args, string result) 
        { 

        }


    }
}
//https://gutendex.com/books?author_year_start=1900&author_year_end=1910&languages=en&page=1&copyright=true