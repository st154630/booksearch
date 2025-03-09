using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mass
{
    internal class Display
    {
        public Display(int count, Book book,ref Panel panel) 
        {
            Label title = new Label();
            title.Size = new Size(500, 15);
            title.Location = new Point(0, count*200);
            title.Text = "Title: " + book.title;
            Label authors = new Label();
            authors.Size = new Size(500, 30);
            authors.Text += "Authors: ";
            foreach (var bookAuthor in book.authors)
            {
                authors.Text += "\n" + bookAuthor.name + " " + bookAuthor.birth_year + "-" + bookAuthor.death_year;
            }
            authors.Location = new Point(0, 160 - (book.authors.Count * 15) + (count * 200));
            Label copyright = new Label();
            copyright.Text = "Copyright: " + book.copyright.ToString();
            copyright.Location = new Point(600, count * 200);
            Label score = new Label();
            score.Text = "Score: " + book.score.ToString();
            score.Location = new Point(600, 160 + count * 200);
            Label lineBreak = new Label();
            lineBreak.Size = new Size(750, 15);
            lineBreak.Location = new Point(0, 180 + count * 200);
            lineBreak.Text = "_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
            Label downloads = new Label();
            downloads.Text = "Downloads: " + book.download_count.ToString();
            downloads.Location = new Point(300, 160 + count * 200);
            panel.Controls.Add(downloads);
            panel.Controls.Add(lineBreak);
            panel.Controls.Add(score);
            panel.Controls.Add(copyright);
            panel.Controls.Add(authors);
            panel.Controls.Add(title);
            
        }
    }
}
