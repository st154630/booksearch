using System.Net;
using System;
using Test;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using static Test.Gutendex;
using System.Text.Json;
using System.Net.Http;
using System.Xml.Serialization;

namespace mass
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
        }
        

        private void mainform_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DBadd(callapi(contstructLink()).results);
            
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }




        private string contstructLink()
        {
            if (checkBox1.Checked & !checkBox2.Checked)
            {
                return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=true";
            }
            else if (checkBox2.Checked & !checkBox1.Checked)
            {
                return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=false";
            }
            else
            {
                return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text;
            }

        }

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

        private void DBadd(Result[] results)
        {
            var db = new BookContext();
            string[] strings = {""};
            foreach (var result in results) {

                var bk = new Book() { id = result.id, authors = result.authors, title = result.title, summaries = result.summaries, translators = result.translators, subjects = result.subjects, bookshelves = result.bookshelves, languages = result.languages, copyright = result.copyright, media_type = result.media_type, download_count = result.download_count};
                db.Add(bk);
                
               
            }
            db.SaveChangesAsync();
        }
    }
}
