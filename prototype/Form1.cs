using prototype.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace prototype
{
    public partial class Form1 : Form
    {
        const int xsize = 20;
        const int ysize = 20;
        private int clientId;
        private int timePlayed;
        public static readonly int[] background = { 98, 65, 8 };
        public Form1()
        {
            InitializeComponent();

            map = new Map(xsize, ysize, background);
            clientId = 404;
            timePlayed = 0;

            pictureBox1.Image = map.getMap();
        }
        public int startspeed = 1;
        Map map;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                timePlayed = 0;
                clientId = map.join();
                label1.Text = clientId.ToString();

                pictureBox1.Image = map.getMap();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error." );
            }
        }
        private void update_Map_Slow()
        {
            Bitmap back = map.updatedMap();
            pictureBox2.Image = back;
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (clientId != 404)
            {
                if (e.KeyCode == Keys.W)
                {
                    int[] a = map.Move(clientId, 0, -1);
                    label1.Text = a[0].ToString();
                    label2.Text = a[1].ToString();
                }
                if (e.KeyCode == Keys.A)
                {  
                    int[] a = map.Move(clientId, -1, 0);
                    label1.Text = a[0].ToString();
                    label2.Text = a[1].ToString();
                }
                if (e.KeyCode == Keys.S)
                {
                    int[] a = map.Move(clientId, 0, 1);
                    label1.Text = a[0].ToString();
                    label2.Text = a[1].ToString();
                }
                if (e.KeyCode == Keys.D)
                {
                    int[] a = map.Move(clientId, 1, 0);
                    label1.Text = a[0].ToString();
                    label2.Text = a[1].ToString();
                }
                if (e.KeyCode == Keys.Q)
                {
                    map.addBomb(clientId);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(clientId != 404)
            {
                int[] xy = map.getPlayers()[0].getPos();
                label1.Text = xy[0].ToString();
                label2.Text = xy[1].ToString();
            }
            timePlayed++;
            update_Map_Slow();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                map = new Map(xsize, ysize, background);

                pictureBox1.Image = map.getMap();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error.");
            }
        }

    }

}
