namespace mass
{
    partial class mainform
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            trackBar1 = new TrackBar();
            panel1 = new Panel();
            bindingSource1 = new BindingSource(components);
            searchBox = new TextBox();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            author_year_start = new TextBox();
            label2 = new Label();
            label3 = new Label();
            author_year_end = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            label4 = new Label();
            authorstartbar = new TrackBar();
            authorendbar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)authorstartbar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)authorendbar).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(12, 73);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(104, 45);
            trackBar1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Location = new Point(527, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(549, 546);
            panel1.TabIndex = 5;
            // 
            // searchBox
            // 
            searchBox.Location = new Point(12, 124);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(100, 23);
            searchBox.TabIndex = 6;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(205, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(123, 109);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 106);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 8;
            label1.Text = "Search terms";
            // 
            // author_year_start
            // 
            author_year_start.Location = new Point(12, 183);
            author_year_start.Name = "author_year_start";
            author_year_start.Size = new Size(100, 23);
            author_year_start.TabIndex = 9;
            author_year_start.TextChanged += author_year_start_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 165);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 10;
            label2.Text = "Author year start";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 281);
            label3.Name = "label3";
            label3.Size = new Size(92, 15);
            label3.TabIndex = 12;
            label3.Text = "Author year end";
            // 
            // author_year_end
            // 
            author_year_end.Location = new Point(16, 319);
            author_year_end.Name = "author_year_end";
            author_year_end.Size = new Size(100, 23);
            author_year_end.TabIndex = 11;
            author_year_end.TextChanged += author_year_end_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 514);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(43, 19);
            checkBox1.TabIndex = 13;
            checkBox1.Text = "Yes";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(12, 539);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(42, 19);
            checkBox2.TabIndex = 14;
            checkBox2.Text = "No";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 496);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 15;
            label4.Text = "Copyright ";
            // 
            // authorstartbar
            // 
            authorstartbar.Location = new Point(12, 221);
            authorstartbar.Maximum = 2025;
            authorstartbar.Minimum = -1105;
            authorstartbar.Name = "authorstartbar";
            authorstartbar.Size = new Size(316, 45);
            authorstartbar.TabIndex = 16;
            authorstartbar.Scroll += authorstartbar_Scroll;
            // 
            // authorendbar
            // 
            authorendbar.Location = new Point(12, 363);
            authorendbar.Maximum = 2025;
            authorendbar.Minimum = -1105;
            authorendbar.Name = "authorendbar";
            authorendbar.Size = new Size(316, 45);
            authorendbar.TabIndex = 17;
            authorendbar.Scroll += authorendbar_Scroll;
            // 
            // mainform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1088, 641);
            Controls.Add(authorendbar);
            Controls.Add(authorstartbar);
            Controls.Add(label4);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(author_year_end);
            Controls.Add(label2);
            Controls.Add(author_year_start);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(searchBox);
            Controls.Add(panel1);
            Controls.Add(trackBar1);
            Controls.Add(button1);
            Name = "mainform";
            Text = "Form1";
            Load += mainform_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)authorstartbar).EndInit();
            ((System.ComponentModel.ISupportInitialize)authorendbar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TrackBar trackBar1;
        private Panel panel1;
        private BindingSource bindingSource1;
        private TextBox searchBox;
        private RichTextBox richTextBox1;
        private Label label1;
        private TextBox author_year_start;
        private Label label2;
        private Label label3;
        private TextBox author_year_end;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label label4;
        private TrackBar authorstartbar;
        private TrackBar authorendbar;
    }
}
