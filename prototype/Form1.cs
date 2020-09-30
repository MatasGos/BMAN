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
using Model;
using Newtonsoft.Json;
using System.Diagnostics;

namespace prototype
{
    public partial class Form1 : Form
    {
        private HubConnection connection;   //SignalR Hub connection object to connect to the server
        private Game game;                  //Game logic object for user-side

        private string username;                                        //Player's chosen username
        private bool _keyTop, _keyLeft, _keyRight, _keyBot, _keyBomb;   //Booleans to see if the key was pressed at a specific time frame

        public Form1()
        {
            // FORM AND DATA INITIALIZATION
            InitializeComponent();  //Initialize form components
            initializeValues();     //Initialize keypress booleans
            connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/gamehub").Build();   //Set up the hub connection
            game = new Game();      //Initialize the game logic object


            // RECEIVING MESSAGES
            //Receive another player's login message
            connection.On<string>("ReceiveLoginMessage", (username) =>
            {
                richTextBox1.AppendText(username + " has logged in\n", Color.Green);
            });

            //Receive another player's sent message
            connection.On<string, string>("ReceiveMessage", (username, message) =>
            {
                richTextBox1.AppendText(username + ": " + message + "\n");
            });

            //Game has started info of players sent
            connection.On<string, string>("SendData", (players, map) =>
            {

            });

            //TODO: GAME LOGIC
            //Game has started info of players sent
            connection.On<List<string>>("InitializePlayers", (players) =>
            {
                //game.update(players);
                checkButtonClicksSERVER();
            });

            //Receive the map
            connection.On<Map>("ReceiveMap", (map) =>
            {
                richTextBox1.AppendText("mapas kraunamas");
                //game.setMap(map);
                richTextBox1.AppendText("mapas pakrautas");
            });
        }

        //Initialize boolean keypress values
        public void initializeValues()
        {
            _keyTop = false;
            _keyBot = false;
            _keyLeft = false;
            _keyRight = false;
            _keyBomb = false;
        }
        
        //Login button
        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    await connection.StartAsync();
                    username = textBox1.Text;
                    await connection.InvokeAsync("SendLoginMessage", username);
                    richTextBox1.AppendText("Connected to the server\n", Color.Green);
                    textBox1.Enabled = false;
                    button1.Enabled = false;
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                }
            }
        }

        //Send message button
        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {
                    await connection.InvokeAsync("SendMessage", username, textBox2.Text);
                    textBox2.Clear();
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                }
            }
        }

        //Start game button
        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //game.uploadGame();
                await connection.InvokeAsync("StartMessage");
            }
            catch (Exception ex)
            {

                richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
            }
        }

        private void update_Map_Slow()
        {
            //Bitmap back = game.getGame();
            Bitmap back = game.getMap();
            pictureBox1.Image = back;          
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
