using System.Net;
using System;
using Test;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace mass
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
        }
        private string contstructLink()
        {
            if (checkBox1.Checked & !checkBox2.Checked)
            {
                return "/?author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=true";
            }
            else if (checkBox2.Checked & !checkBox1.Checked)
            {
                return "/?author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text + "&copyright=false";
            }
            else
            {
                return "/?author_year_start=" + author_year_start.Text + "&author_year_end=" + author_year_end.Text;
            }
             
        }

        private void mainform_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            callapi(contstructLink());
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
    }
}
