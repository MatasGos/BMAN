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
        private bool _keyTop, _keyLeft, _keyRight, _keyBot, _keyBomb;
        public Form1()
        {
            InitializeComponent();

            map = new Map(xsize, ysize, background);
            initialiseValues();

            pictureBox1.Image = map.getMap();
        }
        public void initialiseValues()
        {
            clientId = 404;
            timePlayed = 0;
            _keyTop = false;
            _keyBot = false;
            _keyLeft = false;
            _keyRight = false;
            _keyBomb = false;
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'q')
            {
                _keyBomb = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                _keyTop = false;
            }
            if (e.KeyCode == Keys.A)
            {
                _keyLeft = false;
            }
            if (e.KeyCode == Keys.S)
            {
                _keyBot = false;
            }
            if (e.KeyCode == Keys.D)
            {
                _keyRight = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.W)
            {
                _keyTop = true;                        
            }
            if (e.KeyCode == Keys.A)
            {
                _keyLeft = true;
            }
            if (e.KeyCode == Keys.S)
            {
                _keyBot = true;
            }
            if (e.KeyCode == Keys.D)
            {
                _keyRight = true;
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
            checkButtonClicks();
            timePlayed++;
            update_Map_Slow();
        }
        private void checkButtonClicks()
        {
            if (clientId != 404)
            {
                if (_keyTop) 
                {
                    int[] a = map.Move(clientId, 0, -1);
                }
                if (_keyLeft) 
                {  
                    int[] a = map.Move(clientId, -1, 0);
                }
                if (_keyBot) 
                {
                    int[] a = map.Move(clientId, 0, 1);
                }
                if (_keyRight) 
                {
                    int[] a = map.Move(clientId, 1, 0);
                }
                if (_keyBomb)
                {
                    _keyBomb = false;
                    map.addBomb(clientId);
                }
            }
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
