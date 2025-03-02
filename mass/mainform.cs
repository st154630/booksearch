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

        static BookContext db = new BookContext();

        static int person = 0;

        private void mainform_Load(object sender, EventArgs e)
        {
            //author_year_start.Text = authorstartbar.Value.ToString();
            //author_year_end.Text = authorendbar.Value.ToString();
            db.Database.EnsureCreated();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int page = 1;
            Rootobject? current = callapi(contstructLink(page));
            while (page < 10 && current.results[1].title != null)
            {
                DBadd(callapi(contstructLink(page)).results);
                page++;
                current = callapi(contstructLink(page));
                richTextBox1.Text += page.ToString();
            }
            var bookList = await db.BooksTest.ToListAsync();
            richTextBox1.Text += bookList.Count.ToString();
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




        private string contstructLink(int page)
        {
            if (checkBox1.Checked & !checkBox2.Checked)
            {
                return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=true" + "&page=" + page;
            }
            else if (checkBox2.Checked & !checkBox1.Checked)
            {
                return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=false" + "&page=" + page;
            }

            else
            {
                return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&page=" + page;
            };


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
                foreach (var book in final.results)
                {
                    //foreach (Person author in book.authors)
                    //{
                    //    author.Personid = person;
                    //    person++;
                    //}
                }
                return final;
            }
        }

        private async void DBadd(Result[] results)
        {
            //var db = new BookContext();
            //string[] strings = { "" };
            foreach (var result in results)
            {

                var bk = new Book() {id = result.id, authors = result.authors, title = result.title, summaries = result.summaries, translators = result.translators, subjects = result.subjects, bookshelves = result.bookshelves, languages = result.languages, copyright = result.copyright, media_type = result.media_type, download_count = result.download_count, };
                db.Add(bk);
                //foreach (var author in result.authors)
                //{
                //    bk.authors.Add(author);
                //}

            }
            richTextBox1.Text = person.ToString();
            await db.SaveChangesAsync();
        }

        private void authorendbar_Scroll(object sender, EventArgs e)
        {
            author_year_end.Text = authorendbar.Value.ToString();
            if (authorstartbar.Value < authorendbar.Value)
            {
                authorstartbar.Value = authorendbar.Value;
                author_year_start.Text = authorstartbar.Value.ToString();

            }
        }

        private void authorstartbar_Scroll(object sender, EventArgs e)
        {
            author_year_start.Text = authorstartbar.Value.ToString();
            if (authorstartbar.Value < authorendbar.Value)
            {
                authorendbar.Value = authorstartbar.Value;
                author_year_end.Text = authorendbar.Value.ToString();
            }

        }

        private void author_year_end_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(author_year_end.Text, out int result))
            {
                if (int.Parse(author_year_end.Text) >= authorendbar.Minimum && int.Parse(author_year_end.Text) <= authorendbar.Maximum)
                {
                    authorendbar.Value = int.Parse(author_year_end.Text);
                }
            }
            else
            {
                author_year_end.Text = "";
            }
            
        }

        private void author_year_start_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(author_year_start.Text, out int result))
            {
                if (int.Parse(author_year_start.Text) >= authorstartbar.Minimum && int.Parse(author_year_start.Text) <= authorstartbar.Maximum)
                {
                    authorstartbar.Value = int.Parse(author_year_start.Text);
                }
            }
            else
            {
                author_year_start.Text = "";
            }
        }
    }
}
