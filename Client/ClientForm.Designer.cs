namespace Client
{
    partial class ClientForm
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.healthBox = new System.Windows.Forms.PictureBox();
            this.healthLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.speedBox = new System.Windows.Forms.PictureBox();
            this.mineCountLabel = new System.Windows.Forms.Label();
            this.mineCountBox = new System.Windows.Forms.PictureBox();
            this.bombCountLabel = new System.Windows.Forms.Label();
            this.bombCountBox = new System.Windows.Forms.PictureBox();
            this.superMineCountLabel = new System.Windows.Forms.Label();
            this.superMineCountBox = new System.Windows.Forms.PictureBox();
            this.superBombCountLabel = new System.Windows.Forms.Label();
            this.superBombCountBox = new System.Windows.Forms.PictureBox();
            this.superBombRadiusLabel = new System.Windows.Forms.Label();
            this.superBombRadiusBox = new System.Windows.Forms.PictureBox();
            this.bombRadiusLabel = new System.Windows.Forms.Label();
            this.bombRadiusBox = new System.Windows.Forms.PictureBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timeBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.healthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bombCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superMineCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superBombCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superBombRadiusBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bombRadiusBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(244, 225);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 43);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(725, 475);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(213, 150);
            this.textBox1.MaxLength = 15;
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Enter username";
            this.textBox1.Size = new System.Drawing.Size(300, 43);
            this.textBox1.TabIndex = 6;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Player";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.richTextBox1.Location = new System.Drawing.Point(0, 476);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox1.Size = new System.Drawing.Size(725, 150);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(2, 630);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(645, 23);
            this.textBox2.TabIndex = 8;
            this.textBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(649, 629);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 9;
            this.button2.TabStop = false;
            this.button2.Text = "Send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.Enabled = false;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(250, 395);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(225, 43);
            this.button3.TabIndex = 10;
            this.button3.TabStop = false;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 12);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(0, 23);
            this.textBox3.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.BurlyWood;
            this.label1.Location = new System.Drawing.Point(21, 518);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.BurlyWood;
            this.label2.Location = new System.Drawing.Point(21, 533);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.BurlyWood;
            this.label3.Location = new System.Drawing.Point(21, 548);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.BurlyWood;
            this.label4.Location = new System.Drawing.Point(21, 563);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.BurlyWood;
            this.label5.Location = new System.Drawing.Point(21, 491);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Round scores:";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.BurlyWood;
            this.label6.Location = new System.Drawing.Point(153, 491);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "Match scores:";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.BurlyWood;
            this.label7.Location = new System.Drawing.Point(153, 518);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.BurlyWood;
            this.label8.Location = new System.Drawing.Point(153, 533);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 15);
            this.label8.TabIndex = 21;
            this.label8.Text = "label8";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.BurlyWood;
            this.label9.Location = new System.Drawing.Point(153, 548);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "label9";
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.BurlyWood;
            this.label10.Location = new System.Drawing.Point(153, 563);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "label10";
            this.label10.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox4.Location = new System.Drawing.Point(213, 87);
            this.textBox4.MaxLength = 15;
            this.textBox4.Name = "textBox4";
            this.textBox4.PlaceholderText = "Enter ip";
            this.textBox4.Size = new System.Drawing.Size(300, 43);
            this.textBox4.TabIndex = 24;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "localhost";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // healthBox
            // 
            this.healthBox.BackColor = System.Drawing.Color.Transparent;
            this.healthBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.healthBox.Location = new System.Drawing.Point(583, 8);
            this.healthBox.Margin = new System.Windows.Forms.Padding(0);
            this.healthBox.Name = "healthBox";
            this.healthBox.Size = new System.Drawing.Size(25, 25);
            this.healthBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.healthBox.TabIndex = 25;
            this.healthBox.TabStop = false;
            this.healthBox.Visible = false;
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.BackColor = System.Drawing.Color.Transparent;
            this.healthLabel.Location = new System.Drawing.Point(609, 5);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(44, 15);
            this.healthLabel.TabIndex = 26;
            this.healthLabel.Text = "label11";
            this.healthLabel.Visible = false;
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.BackColor = System.Drawing.Color.Transparent;
            this.speedLabel.Location = new System.Drawing.Point(680, 5);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(44, 15);
            this.speedLabel.TabIndex = 28;
            this.speedLabel.Text = "label11";
            this.speedLabel.Visible = false;
            // 
            // speedBox
            // 
            this.speedBox.BackColor = System.Drawing.Color.Transparent;
            this.speedBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.speedBox.Location = new System.Drawing.Point(654, 8);
            this.speedBox.Margin = new System.Windows.Forms.Padding(0);
            this.speedBox.Name = "speedBox";
            this.speedBox.Size = new System.Drawing.Size(25, 25);
            this.speedBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.speedBox.TabIndex = 27;
            this.speedBox.TabStop = false;
            this.speedBox.Visible = false;
            // 
            // mineCountLabel
            // 
            this.mineCountLabel.AutoSize = true;
            this.mineCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.mineCountLabel.Location = new System.Drawing.Point(680, 36);
            this.mineCountLabel.Name = "mineCountLabel";
            this.mineCountLabel.Size = new System.Drawing.Size(44, 15);
            this.mineCountLabel.TabIndex = 32;
            this.mineCountLabel.Text = "label11";
            this.mineCountLabel.Visible = false;
            // 
            // mineCountBox
            // 
            this.mineCountBox.BackColor = System.Drawing.Color.Transparent;
            this.mineCountBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mineCountBox.Location = new System.Drawing.Point(657, 40);
            this.mineCountBox.Margin = new System.Windows.Forms.Padding(0);
            this.mineCountBox.Name = "mineCountBox";
            this.mineCountBox.Size = new System.Drawing.Size(25, 25);
            this.mineCountBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mineCountBox.TabIndex = 31;
            this.mineCountBox.TabStop = false;
            this.mineCountBox.Visible = false;
            // 
            // bombCountLabel
            // 
            this.bombCountLabel.AutoSize = true;
            this.bombCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.bombCountLabel.Location = new System.Drawing.Point(609, 36);
            this.bombCountLabel.Name = "bombCountLabel";
            this.bombCountLabel.Size = new System.Drawing.Size(44, 15);
            this.bombCountLabel.TabIndex = 30;
            this.bombCountLabel.Text = "label11";
            this.bombCountLabel.Visible = false;
            // 
            // bombCountBox
            // 
            this.bombCountBox.BackColor = System.Drawing.Color.Transparent;
            this.bombCountBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bombCountBox.Location = new System.Drawing.Point(585, 37);
            this.bombCountBox.Margin = new System.Windows.Forms.Padding(0);
            this.bombCountBox.Name = "bombCountBox";
            this.bombCountBox.Size = new System.Drawing.Size(25, 25);
            this.bombCountBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bombCountBox.TabIndex = 29;
            this.bombCountBox.TabStop = false;
            this.bombCountBox.Visible = false;
            // 
            // superMineCountLabel
            // 
            this.superMineCountLabel.AutoSize = true;
            this.superMineCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.superMineCountLabel.Location = new System.Drawing.Point(680, 67);
            this.superMineCountLabel.Name = "superMineCountLabel";
            this.superMineCountLabel.Size = new System.Drawing.Size(44, 15);
            this.superMineCountLabel.TabIndex = 36;
            this.superMineCountLabel.Text = "label11";
            this.superMineCountLabel.Visible = false;
            // 
            // superMineCountBox
            // 
            this.superMineCountBox.BackColor = System.Drawing.Color.Transparent;
            this.superMineCountBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.superMineCountBox.Location = new System.Drawing.Point(657, 71);
            this.superMineCountBox.Margin = new System.Windows.Forms.Padding(0);
            this.superMineCountBox.Name = "superMineCountBox";
            this.superMineCountBox.Size = new System.Drawing.Size(25, 25);
            this.superMineCountBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.superMineCountBox.TabIndex = 35;
            this.superMineCountBox.TabStop = false;
            this.superMineCountBox.Visible = false;
            // 
            // superBombCountLabel
            // 
            this.superBombCountLabel.AutoSize = true;
            this.superBombCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.superBombCountLabel.Location = new System.Drawing.Point(609, 67);
            this.superBombCountLabel.Name = "superBombCountLabel";
            this.superBombCountLabel.Size = new System.Drawing.Size(44, 15);
            this.superBombCountLabel.TabIndex = 34;
            this.superBombCountLabel.Text = "label11";
            this.superBombCountLabel.Visible = false;
            // 
            // superBombCountBox
            // 
            this.superBombCountBox.BackColor = System.Drawing.Color.Transparent;
            this.superBombCountBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.superBombCountBox.Location = new System.Drawing.Point(585, 68);
            this.superBombCountBox.Margin = new System.Windows.Forms.Padding(0);
            this.superBombCountBox.Name = "superBombCountBox";
            this.superBombCountBox.Size = new System.Drawing.Size(25, 25);
            this.superBombCountBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.superBombCountBox.TabIndex = 33;
            this.superBombCountBox.TabStop = false;
            this.superBombCountBox.Visible = false;
            // 
            // superBombRadiusLabel
            // 
            this.superBombRadiusLabel.AutoSize = true;
            this.superBombRadiusLabel.BackColor = System.Drawing.Color.Transparent;
            this.superBombRadiusLabel.Location = new System.Drawing.Point(680, 98);
            this.superBombRadiusLabel.Name = "superBombRadiusLabel";
            this.superBombRadiusLabel.Size = new System.Drawing.Size(44, 15);
            this.superBombRadiusLabel.TabIndex = 40;
            this.superBombRadiusLabel.Text = "label11";
            this.superBombRadiusLabel.Visible = false;
            // 
            // superBombRadiusBox
            // 
            this.superBombRadiusBox.BackColor = System.Drawing.Color.Transparent;
            this.superBombRadiusBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.superBombRadiusBox.Location = new System.Drawing.Point(654, 100);
            this.superBombRadiusBox.Margin = new System.Windows.Forms.Padding(0);
            this.superBombRadiusBox.Name = "superBombRadiusBox";
            this.superBombRadiusBox.Size = new System.Drawing.Size(25, 25);
            this.superBombRadiusBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.superBombRadiusBox.TabIndex = 39;
            this.superBombRadiusBox.TabStop = false;
            this.superBombRadiusBox.Visible = false;
            // 
            // bombRadiusLabel
            // 
            this.bombRadiusLabel.AutoSize = true;
            this.bombRadiusLabel.BackColor = System.Drawing.Color.Transparent;
            this.bombRadiusLabel.Location = new System.Drawing.Point(609, 98);
            this.bombRadiusLabel.Name = "bombRadiusLabel";
            this.bombRadiusLabel.Size = new System.Drawing.Size(44, 15);
            this.bombRadiusLabel.TabIndex = 38;
            this.bombRadiusLabel.Text = "label11";
            this.bombRadiusLabel.Visible = false;
            // 
            // bombRadiusBox
            // 
            this.bombRadiusBox.BackColor = System.Drawing.Color.Transparent;
            this.bombRadiusBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bombRadiusBox.Location = new System.Drawing.Point(583, 100);
            this.bombRadiusBox.Margin = new System.Windows.Forms.Padding(0);
            this.bombRadiusBox.Name = "bombRadiusBox";
            this.bombRadiusBox.Size = new System.Drawing.Size(25, 25);
            this.bombRadiusBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bombRadiusBox.TabIndex = 37;
            this.bombRadiusBox.TabStop = false;
            this.bombRadiusBox.Visible = false;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Location = new System.Drawing.Point(645, 129);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(44, 15);
            this.timeLabel.TabIndex = 42;
            this.timeLabel.Text = "label11";
            this.timeLabel.Visible = false;
            // 
            // timeBox
            // 
            this.timeBox.BackColor = System.Drawing.Color.Transparent;
            this.timeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.timeBox.Location = new System.Drawing.Point(619, 131);
            this.timeBox.Margin = new System.Windows.Forms.Padding(0);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(25, 25);
            this.timeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.timeBox.TabIndex = 41;
            this.timeBox.TabStop = false;
            this.timeBox.Visible = false;
            // 
            // ClientForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(725, 655);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.timeBox);
            this.Controls.Add(this.superBombRadiusLabel);
            this.Controls.Add(this.superBombRadiusBox);
            this.Controls.Add(this.bombRadiusLabel);
            this.Controls.Add(this.bombRadiusBox);
            this.Controls.Add(this.superMineCountLabel);
            this.Controls.Add(this.superMineCountBox);
            this.Controls.Add(this.superBombCountLabel);
            this.Controls.Add(this.superBombCountBox);
            this.Controls.Add(this.mineCountLabel);
            this.Controls.Add(this.mineCountBox);
            this.Controls.Add(this.bombCountLabel);
            this.Controls.Add(this.bombCountBox);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.speedBox);
            this.Controls.Add(this.healthLabel);
            this.Controls.Add(this.healthBox);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.ShowIcon = false;
            this.Text = "BMAN";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.healthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bombCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superMineCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superBombCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superBombRadiusBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bombRadiusBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.PictureBox healthBox;
        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.PictureBox speedBox;
        private System.Windows.Forms.Label mineCountLabel;
        private System.Windows.Forms.PictureBox mineCountBox;
        private System.Windows.Forms.Label bombCountLabel;
        private System.Windows.Forms.PictureBox bombCountBox;
        private System.Windows.Forms.Label superMineCountLabel;
        private System.Windows.Forms.PictureBox superMineCountBox;
        private System.Windows.Forms.Label superBombCountLabel;
        private System.Windows.Forms.PictureBox superBombCountBox;
        private System.Windows.Forms.Label superBombRadiusLabel;
        private System.Windows.Forms.PictureBox superBombRadiusBox;
        private System.Windows.Forms.Label bombRadiusLabel;
        private System.Windows.Forms.PictureBox bombRadiusBox;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.PictureBox timeBox;
    }
}

