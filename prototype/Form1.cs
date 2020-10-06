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
using System.Xml;

namespace prototype
{
    public partial class Form1 : Form
    {
        private HubConnection connection;   //SignalR Hub connection object to connect to the server
        private Game game;                  //Game logic object for user-side

        private string username;                                        //Player's chosen username
        private bool _keyTop, _keyLeft, _keyRight, _keyBot, _keyBomb, _keyMine;   //Booleans to see if the key was pressed at a specific time frame

        //Json settings
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public Form1()
        {
            // FORM AND DATA INITIALIZATION
            InitializeComponent();  //Initialize form components
            initializeValues();     //Initialize keypress booleans

            connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/gamehub").Build();   //Set up the hub connection

            game = new Game();      //Initialize the game logic object
            timer1.Enabled = true;  //Enable timer that draws the map


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
            connection.On<string, string>("SendData", (jsonPlayers, jsonMap) =>
            {
                game.players = JsonConvert.DeserializeObject<List<Player>>(jsonPlayers, settings);
                game.map = JsonConvert.DeserializeObject<Map>(jsonMap, settings);
                if (game.gameStarted == false)
                {
                    game.gameStarted = true;
                    button3.Enabled = false;
                    game.drawBackground();
                }
                game.drawMap();
                pictureBox1.Image = game.field;
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
                await connection.InvokeAsync("StartMessage");
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
            }
        }

        //Form key press event handler
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
            if (e.KeyCode == Keys.Space)
            {
                _keyBomb = false;
            }
            if (e.KeyCode == Keys.M)
            {
                _keyMine = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!game.gameStarted)
                return;
            connection.InvokeAsync("DebugAddBoost");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (!game.gameStarted)
                return;
            connection.InvokeAsync("DebugRemoveBoost");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        //Form key press event handler
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
            if (e.KeyCode == Keys.Space)
            {
                _keyBomb = true;
            }
            if (e.KeyCode == Keys.M)
            {
                _keyMine = true;
            }
        }

        //Timer that checks button presses
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (game.gameStarted)
            {
                CheckButtonPresses();
            }
        }

        //Checking which buttons were pressed
        private void CheckButtonPresses()
        {
            int x = 0;
            int y = 0;
            string action = "";

            if (_keyLeft)
            {
                x -= 1;
            }
            if (_keyRight)
            {
                x += 1;
            }
            if (_keyTop)
            {
                y -= 1;
            }
            if (_keyBot)
            {
                y += 1;
            }
            if (_keyBomb)
            {
                action = "placeBomb";
            }
            if (_keyMine)
            {
                action = "placeMine";
            }
            SendActionCommand(action);
            SendMoveCommand(x, y);
            
        }

        //Send a move command to the server
        public async void SendMoveCommand(int x, int y)
        {
            try
            {
                await connection.InvokeAsync("SendMoveMessage", x, y);
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
            }
        }

        //Send a place bomb command to the server
        public async void SendActionCommand(string action)
        {
            try
            {
                await connection.InvokeAsync("SendActionMessage", action);
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
