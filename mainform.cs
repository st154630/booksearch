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
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace mass
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
        }
        static BookContext db = new BookContext();
        private void mainform_Load(object sender, EventArgs e)
        {
            author_year_start.Text = authorstartbar.Value.ToString();

            author_year_end.Text = authorendbar.Value.ToString();


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await db.Books.ExecuteDeleteAsync();
            //set up constraints since doing them in constructlink every time it is called would take forever
            int page = 1;
            int start = authorstartbar.Value;
            int end = authorendbar.Value;
            int diff = end - start;
            end = end + (diff * ((10 - (int)numericEnd.Value) / 10));
            start = start - (diff * ((10 - (int)numericStart.Value) / 10));
            if (start < authorstartbar.Minimum) start = authorstartbar.Minimum;
            if (end > authorendbar.Maximum) end = authorendbar.Maximum;
            Rootobject? current = callapi(contstructLink(page, start, end));
            //call api for the first 10 pages since any more would take too long
            while (page < 10 && current.results[1].title != null)
            {
                DBadd(callapi(contstructLink(page, start, end)).results);
                page++;
                current = callapi(contstructLink(page, start, end));
            }
            var bookList = await db.Books.ToListAsync();
            foreach (var book in bookList)
            {
                richTextBox1.Text += (book.download_count) + "\n";
                if (book.authors.Count != 0)
                {
                    richTextBox1.Text += (book.authors[0].birth_year) + "\n";
                    richTextBox1.Text += (book.authors[0].death_year) + "\n";
                }
                richTextBox1.Text += (book.title) + "\n";
            }

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




        private string contstructLink(int page, int start, int end)
        {
            //ignore 
            //if (checkBox1.Checked & !checkBox2.Checked)
            //{
            //    return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=true" + "&page=" + page;
            //}
            //else if (checkBox2.Checked & !checkBox1.Checked)
            //{
            //    return "?pretty=1&author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=false" + "&page=" + page;
            //}


            //returns the formatted uri, its better to keep this minimal for performance
            return "?pretty=1&author_year_start=" + start + "&author_year_end=" + end + "&page=" + page;
        }



        public Rootobject callapi(string url)
        {
            //call the api and deserialize into the defined object type
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

        private async void DBadd(Result[] results)
        {
            var books = new List<Book>();
            // adds each book to the database, currently missing formats
            foreach (var result in results)
            {
                var bk = new Book
                {
                    id = result.id,
                    authors = result.authors,
                    title = result.title,
                    summaries = result.summaries,
                    translators = result.translators,
                    subjects = result.subjects,
                    bookshelves = result.bookshelves,
                    languages = result.languages,
                    copyright = result.copyright,
                    media_type = result.media_type,
                    download_count = result.download_count,
                };
                books.Add(bk);
            }
            db.AddRange(books);
            db.SaveChanges();
        }

        private void authorendbar_Scroll(object sender, EventArgs e)
        {
            //let user see the value of the bar
            author_year_end.Text = authorendbar.Value.ToString();
            //ensure end year is not before start year
            if (authorstartbar.Value > authorendbar.Value)
            {
                authorstartbar.Value = authorendbar.Value;
                author_year_start.Text = authorstartbar.Value.ToString();

            }
        }

        private void authorstartbar_Scroll(object sender, EventArgs e)
        {
            //see above comment
            author_year_start.Text = authorstartbar.Value.ToString();
            //same basic idea as the end bar
            if (authorstartbar.Value > authorendbar.Value)
            {
                authorendbar.Value = authorstartbar.Value;
                author_year_end.Text = authorendbar.Value.ToString();
            }

        }

        private void author_year_end_TextChanged(object sender, EventArgs e)
        {
            //very jank way of ensuring textbox is only numbers
            // TODO : doesnt work with negative year values 
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
            //same as above
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void ScoreBook(ref Book book)
        {
            if (book != null)
            {
                if (book.copyright != null)
                {
                    if ((bool)(checkBox1.Checked & !checkBox2.Checked & book.copyright))
                    {
                        book.score += (float)(numericCopy.Value / 10);

                    }
                    else if ((bool)(checkBox2.Checked & !checkBox1.Checked & !book.copyright))
                    {

                        book.score += (float)(numericCopy.Value / 10);
                    }
                }
                if (book.authors != null)
                {
                    foreach (Author author in book.authors)
                    {
                        int tempScore = 0;
                        if (author != null)
                        {


                        }
                    }
                }
                if (book.title.Contains(searchBox.Text))
                {
                    book.score += (float)(numericSearch.Value / 10);
                }
                else
                {
                    foreach (string summ in book.summaries)
                    {
                        if (summ.Contains(searchBox.Text))
                        {
                            book.score += (float)(numericSearch.Value / 20);
                        }
                    }
                }

            }


        }
    }
}
