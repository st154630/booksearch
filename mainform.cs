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
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            Enabled = false;
            //clear any existing data or display to allow for repeated searches 
            panel1.Controls.Clear();
            foreach (var book in db.Books)
            {
                book.authors.Clear();
                book.translators.Clear();
                db.Remove(book);
            }
            db.SaveChanges();

            //set up constraints for link, saves time over setting up in constructLink
            int page = 1;
            int start = authorstartbar.Value;
            int end = authorendbar.Value;
            int diff = end - start;
            end = end + (diff * ((10 - (int)numericEnd.Value) / 10));
            start = start - (diff * ((10 - (int)numericStart.Value) / 10));
            if (start < authorstartbar.Minimum) start = authorstartbar.Minimum;
            if (end > authorendbar.Maximum) end = authorendbar.Maximum;
            Rootobject? current = callapi(contstructLink(page, start, end));

            //call api for the first 10 pages
            if (current != null)
            {
                while (page < 10 && current != null && current.results != null)
                {
                    DBadd(callapi(contstructLink(page, start, end)).results);
                    page++;
                    if (current.count < page * 20)
                    {
                        break;
                    }
                    current = callapi(contstructLink(page, start, end));

                }


                //get all books from the database and calculate score
                var bookList = db.Books
                    .OrderByDescending(e => e.download_count)
                    .ThenBy(e => e.id)
                    .ToList();

                int max = bookList[0].download_count;
                foreach (var book in bookList)
                {
                    book.score = ScoreBook(book, max);
                    await db.SaveChangesAsync();
                }


                List<Display> DisplayList = new List<Display>();
                //sort list by score and display all results
                //timing here for displaying all results is miniscule compared to calling the api
                var sortedList = db.Books
                    .OrderByDescending(e => e.score)
                    .ThenBy(e => e.id)
                    .ToList();
                Enabled = true; //reenable form now that search is complete
                for (int i = 0; i < sortedList.Count; i++)
                {
                    var book = sortedList[i];
                    DisplayList.Add(new Display(i, book, ref panel1));

                }
            }
            else
            {
                Enabled = true;
                Label label = new Label();
                label.Location = new Point(350, 225);
                label.Text = "No results";
                panel1.Controls.Add(label);
            }

        }



        private string contstructLink(int page, int start, int end)
        {
            //returns the formatted uri, its better to keep this minimal for performance
            string searchLink = searchBox.Text.Replace(" ", "%20");
            return "?pretty=1&author_year_start=" + start + "&author_year_end=" + end + "&page=" + page + "&search=" + searchLink;
        }



        public Rootobject callapi(string url)
        {
            //call the api and deserialize into the defined object type
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://gutendex.com/books");
                HttpResponseMessage response = client.GetAsync(url).Result;
                //this is the easiest way to ensure the program doesn't crash when a result is returned with 0 books in it
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Rootobject? final = JsonSerializer.Deserialize<Rootobject>(result);
                    if (final.results.Count() > 0)
                    {
                        return final;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private async void DBadd(Result[] results)
        {
            var books = new List<Book>();
            //adds each book to the database
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
                    score = result.score,
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
            //let user see the value of the bar
            author_year_start.Text = authorstartbar.Value.ToString();
            //ensure end year is not before start year
            if (authorstartbar.Value > authorendbar.Value)
            {
                authorendbar.Value = authorstartbar.Value;
                author_year_end.Text = authorendbar.Value.ToString();
            }

        }

        private void author_year_end_TextChanged(object sender, EventArgs e)
        {
            //ensure textbox is either only numbers or just a - sign
            if (author_year_end.Text == "-")
            {
                authorendbar.Value = 0;
            }
            if (int.TryParse(author_year_end.Text, out int result))
            {

                if (int.Parse(author_year_end.Text) >= authorendbar.Minimum && int.Parse(author_year_end.Text) <= authorendbar.Maximum)
                {
                    authorendbar.Value = int.Parse(author_year_end.Text);
                }
            }
            else if (author_year_end.Text != "-")
            {
                author_year_end.Text = "";
            }

        }

        private void author_year_start_TextChanged(object sender, EventArgs e)
        {
            //ensure textbox is either only numbers or just a - sign
            if (author_year_start.Text == "-")
            {
                authorstartbar.Value = 0;
            }
            if (int.TryParse(author_year_start.Text, out int result))
            {


                if (int.Parse(author_year_start.Text) >= authorstartbar.Minimum && int.Parse(author_year_start.Text) <= authorstartbar.Maximum)
                {
                    authorstartbar.Value = int.Parse(author_year_start.Text);
                }

            }
            else if (author_year_start.Text != "-")
            {
                author_year_start.Text = "";
            }
        }


        public float ScoreBook(Book book, int max)
        {
            float score = 0;
            if (book != null)
            {
                //add score based on copyright and weigh by user input
                if (book.copyright != null)
                {
                    if ((bool)(checkBox1.Checked & !checkBox2.Checked & book.copyright))
                    {
                        score += (float)(numericCopy.Value);

                    }
                    else if ((bool)(checkBox2.Checked & !checkBox1.Checked & !book.copyright))
                    {

                        score += (float)(numericCopy.Value);
                    }
                }
                //if the book has listed authors that conform to the requirements set by the user add score
                if (book.authors != null)
                {
                    int i = 1;
                    foreach (Author author in book.authors)
                    {
                        if (author != null)
                        {
                            if (author.death_year < authorendbar.Value)
                            {
                                score += (float)(numericEnd.Value / (2 * i * i));
                            }
                            if (author.birth_year > authorstartbar.Value)
                            {
                                score += (float)(numericEnd.Value / (2 * i * i));
                            }
                            i++;
                        }
                    }
                }
                //if title contains text from the search add score
                if (searchBox.Text != null && book.title.Contains(searchBox.Text))
                {
                    score += (float)(numericSearch.Value);
                }
                else
                {
                    //extra case for if one of the summaries contains search text
                    foreach (string summ in book.summaries)
                    {
                        if (summ.Contains(searchBox.Text))
                        {
                            score += (float)(numericSearch.Value / 2);
                        }
                    }
                }
                //score downloads based on fraction of the most downloaded book in the list
                if (book.download_count != null)
                {
                    score += (float)numericDown.Value * (float)book.download_count / (float)max;
                }


            }
            return score;

        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //assigns 4 base colours to the ui depending on if it is set to dark or light mode
            if (listBox1.SelectedIndex == 0)
            {
                listBox1.ForeColor = button1.ForeColor = searchBox.ForeColor = numericSearch.ForeColor = author_year_start.ForeColor = author_year_end.ForeColor = numericStart.ForeColor = numericEnd.ForeColor = numericCopy.ForeColor = numericDown.ForeColor = System.Drawing.SystemColors.ControlText;
                panel1.BackColor = button1.BackColor = BackColor = System.Drawing.SystemColors.Control;
                ForeColor = System.Drawing.SystemColors.ControlText;
                listBox1.BackColor = button1.BackColor = searchBox.BackColor = numericSearch.BackColor = author_year_start.BackColor = author_year_end.BackColor = numericStart.BackColor = numericEnd.BackColor = numericCopy.BackColor = numericDown.BackColor = System.Drawing.SystemColors.Window;
            }
            if (listBox1.SelectedIndex == 1)
            {
                listBox1.ForeColor = button1.ForeColor = searchBox.ForeColor = numericSearch.ForeColor = author_year_start.ForeColor = author_year_end.ForeColor = numericStart.ForeColor = numericEnd.ForeColor = numericCopy.ForeColor = numericDown.ForeColor = System.Drawing.SystemColors.Control;
                panel1.BackColor = button1.BackColor = BackColor = Color.FromArgb(48, 48, 48);
                ForeColor = System.Drawing.SystemColors.Control;
                listBox1.BackColor = button1.BackColor = searchBox.BackColor = numericSearch.BackColor = author_year_start.BackColor = author_year_end.BackColor = numericStart.BackColor = numericEnd.BackColor = numericCopy.BackColor = numericDown.BackColor = Color.DimGray;
            }
        }

       
    }
}
