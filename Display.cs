using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using mass;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace mass
{
    internal class Display
    {
        public Display(int count, Book book,ref Panel panel) 
        {
            //title label
            Label title = new Label();
            title.Size = new Size(500, 15);
            title.Location = new Point(0, count*200);
            title.Text = "Title: " + book.title;
            
            //author label, scales with the amount of authors
            Label authors = new Label();
            authors.Size = new Size(500, 15 * (book.authors.Count+1));
            authors.Text += "Authors: ";
            foreach (var bookAuthor in book.authors)
            {
                authors.Text += "\n" + bookAuthor.name + " " + bookAuthor.birth_year + "-" + bookAuthor.death_year;
            }
            authors.Location = new Point(0, 160 - (book.authors.Count * 15) + (count * 200));
            
            //copyright label
            Label copyright = new Label();
            copyright.Text = "Copyright: " + book.copyright.ToString();
            copyright.Location = new Point(600, count * 200);
            
            //score label 
            Label score = new Label();
            score.Text = "Score: " + book.score.ToString();
            score.Location = new Point(600, 160 + count * 200);

            //creates a break in between displays
            Label lineBreak = new Label();
            lineBreak.Size = new Size(750, 15);
            lineBreak.Location = new Point(0, 180 + count * 200);
            lineBreak.Text = "_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
            
            //display number of downloads for the book
            Label downloads = new Label();
            downloads.Text = "Downloads: " + book.download_count.ToString();
            downloads.Location = new Point(300, 160 + count * 200);
            downloads.Size = new Size(200, 15);

            LinkLabel bookLink = new LinkLabel();
            bookLink.Location = new Point(0, 80 + count * 200);
            bookLink.Text = "https://www.gutenberg.org/ebooks/" + book.id.ToString();
            bookLink.Size = new Size(600, 15);
            bookLink.LinkArea = new LinkArea(0, bookLink.Text.Length + 20);
            bookLink.LinkColor = Color.LightSeaGreen;
            bookLink.LinkClicked += delegate
            {
                try
                {
                    System.Diagnostics.Process.Start(bookLink.Text);
                }
                catch (Exception ex) 
                {
                    
                }
            };
            
        
        



        panel.Controls.Add(bookLink);
            panel.Controls.Add(downloads);
            panel.Controls.Add(lineBreak);
            panel.Controls.Add(score);
            panel.Controls.Add(copyright);
            panel.Controls.Add(authors);
            panel.Controls.Add(title);
            
        }

       
    }
}
