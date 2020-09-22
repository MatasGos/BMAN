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
using Microsoft.AspNetCore.SignalR.Client;

namespace prototype
{
    public partial class Form1 : Form
    {
        HubConnection connection;
        const int xsize = 20;
        public const int playerSize = 15;
        const int ysize = 20;
        private int clientId;
        private int timePlayed;
        public static readonly int[] background = { 98, 65, 8 };
        private bool _keyTop, _keyLeft, _keyRight, _keyBot, _keyBomb;
        private Game game;
        public int startspeed = 1;
        public Form1()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/gamehub").Build();
            connection.On<int, string>("ReceiveMessage", (user, message) =>
            {
                richTextBox1.Text = richTextBox1.Text + user + ": " + message + "\n";
            });
            game = new Game();
            initialiseValues();
            pictureBox1.Image = game.getGame();
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
        
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                clientId = game.join(textBox1.Text);
                label1.Text = clientId.ToString();

                pictureBox1.Image = game.getGame();

                richTextBox1.Text = richTextBox1.Text + "Connecting..." + "\n";
                await connection.StartAsync();
                richTextBox1.Text = richTextBox1.Text + "Connected to the server" + "\n";
            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error." );
            }
        }
        private void update_Map_Slow()
        {
            Bitmap back = game.getGame();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", clientId, textBox2.Text);
            }
            catch (Exception ex)
            {

                richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
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
            checkButtonClicks();
            timePlayed++;
            update_Map_Slow();
            if (clientId != 404)
            {
            }
        }
        private async void checkButtonClicks()
        {
            if (clientId != 404)
            {
                if (_keyTop) 
                {
                    label1.Text = game.Move(clientId, 0, -1);
                    try
                    {
                        await connection.InvokeAsync("Move", clientId, 0, -1);
                    }
                    catch (Exception ex)
                    {

                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }
                }
                if (_keyLeft) 
                {
                    try
                    {
                        await connection.InvokeAsync("Move", clientId, -1, 0);
                    }
                    catch (Exception ex)
                    {

                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }
                    game.Move(clientId, -1, 0);
                }
                if (_keyBot) 
                {
                    try
                    {
                        await connection.InvokeAsync("Move", clientId, 0, 1);
                    }
                    catch (Exception ex)
                    {

                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }
                    game.Move(clientId, 0, 1);
                }
                if (_keyRight) 
                {
                    try
                    {
                        await connection.InvokeAsync("Move", clientId, 1, 0);
                    }
                    catch (Exception ex)
                    {

                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }
                    game.Move(clientId, 1, 0);
                }
                if (_keyBomb)
                {
                    _keyBomb = false;
                    game.addBomb(clientId);
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", clientId, textBox1.Text);
            }
            catch (Exception ex)
            {

                richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
            }
        }

    }

}
