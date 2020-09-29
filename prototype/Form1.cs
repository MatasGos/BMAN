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
using System.Threading;

namespace prototype
{
    public partial class Form1 : Form
    {
        HubConnection connection;
        private Game game;

        private string username;
        private int clientId; // sito gali but kad nereikia, bet bijau trint
        private bool _keyTop, _keyLeft, _keyRight, _keyBot, _keyBomb;


        public Form1()
        {
            int count = 0;
            //FORM 
            InitializeComponent();

            connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/gamehub").Build();

            //RECEIVING MESSAGES

            //Someone has logged in
            connection.On<string>("LoggedinMessage", (username) =>
            {

                richTextBox1.AppendText(username + " has logged in\n", Color.Green);
            });

            //Send map
            connection.On<Map>("ReceiveMap", (map) =>
            {
                richTextBox1.AppendText("mapas kraunamas");
                game.setMap(map);
                richTextBox1.AppendText("mapas pakrautas");
            });


            //Someone sent a message
            connection.On<string, string>("ReceiveMessage", (username, message) =>
            {
                richTextBox1.AppendText(username + ": " + message + "\n");
            });

            //Game has started info of players sent
            connection.On<List<string>>("InitializePlayers", (players) =>
            {
                game.update(players);
                label1.Text = count++.ToString();
                checkButtonClicksSERVER();
            });

            game = new Game();
            initialiseValues();
        }

        public void initialiseValues()
        {
            clientId = 404;
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
                await connection.StartAsync();
                username = textBox1.Text;
                await connection.InvokeAsync("LoginMessage", username);
                richTextBox1.AppendText("Connected to the server\n", Color.Green);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error."+ex.ToString());
            }
        }
        private void update_Map_Slow()
        {
            Bitmap back = game.getGame();
            pictureBox1.Image = back;
            label1.Text = new Player("aaaa", 10,10).getString();            
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

        private async void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                game.uploadGame();
                await connection.InvokeAsync("StartMessage");
            }
            catch (Exception ex)
            {

                richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
            update_Map_Slow();
        }

        private async void checkButtonClicksSERVER()
        {
           
                if (_keyTop)
                {
                    try
                    {
                        await connection.InvokeAsync("Move", 0, -1);
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
                        await connection.InvokeAsync("Move", -1, 0);
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }
                }
                if (_keyBot)
                {
                    try
                    {
                        await connection.InvokeAsync("Move", 0, 1);
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }
                }
                if (_keyRight)
                {
                    try
                    {
                        await connection.InvokeAsync("Move", 1, 0);
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }
                }
                if (_keyBomb)
                {
                    _keyBomb = false;
                    //game.addBomb(clientId);
                    /*try
                    {
                        await connection.InvokeAsync("Move", 1, 0);
                    }
                    catch (Exception ex)
                    {

                        richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                    }*/ // kazkas tokio turetu buti tik vietoj move place bomb or something idk lol
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

    public static class RichTextBoxExtension
    {
        //For colored text
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }

}
