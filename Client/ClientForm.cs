using Microsoft.AspNetCore.SignalR.Client;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        private HubConnection connection;   //SignalR Hub connection object to connect to the server
        private Game<Bitmap, Color> game;                  //Game logic object for user-side

        private string username;                                        //Player's chosen username
        private bool _keyTop, _keyLeft, _keyRight, _keyBot, _keyBomb, _keyMine, _keyUndo, _keySuperMine, _keySuperBomb, _keyMessaging;   //Booleans to see if the key was pressed at a specific time frame

        //Json settings
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public ClientForm()
        {
            // FORM AND DATA INITIALIZATION
            InitializeComponent();  //Initialize form components
            initializeValues();     //Initialize keypress booleans

            connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/gamehub").Build();   //Set up the hub connection

            game = new Game<Bitmap, Color>();      //Initialize the game logic object
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

                    //Disable things
                    button3.Enabled = false;
                    button3.Visible = false;
                    pictureBox1.BackgroundImage = null;

                    //Enable things
                    button2.Enabled = true;
                    textBox2.Enabled = true;

                    textBox3.Focus();

                    game.drawBackground();
                }
                game.drawMap();
                pictureBox1.Image = game.GetField().GetImage();
            });

            //Update player images after changing their appearance
            connection.On<string>("UpdatePlayerImages", (jsonPlayers) =>
            {
                game.players = JsonConvert.DeserializeObject<List<Player>>(jsonPlayers, settings);
                game.FormPlayerImages();
            });
        }

        //Initialize default values
        public void initializeValues()
        {
            _keyTop = false;
            _keyBot = false;
            _keyLeft = false;
            _keyRight = false;
            _keyBomb = false;
            _keySuperMine = false;
            _keySuperBomb = false;
            _keyMessaging = false;

            pictureBox1.BackgroundImage = Images.menuBackground;

            button1.Parent = pictureBox1;
            button1.BackgroundImage = Images.loginButton;
            button1.MouseEnter += Button1_MouseEnter;
            button1.MouseLeave += Button1_MouseLeave;

            button3.Parent = pictureBox1;
            button3.BackgroundImage = Images.startButton;
            button3.MouseEnter += Button3_MouseEnter;
            button3.MouseLeave += Button3_MouseLeave;

            textBox2.KeyPress += TextBox2_KeyPress;
            richTextBox1.TextChanged += RichTextBox1_TextChanged;
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '`')
            {
                e.Handled = true;
            }
        }

        private void Button1_MouseEnter(object sender, System.EventArgs e)
        {
            button1.BackgroundImage = Images.loginButtonHover;
            button1.Refresh();
        }

        private void Button1_MouseLeave(object sender, System.EventArgs e)
        {
            button1.BackgroundImage = Images.loginButton;
            button1.Refresh();
        }

        private void Button3_MouseEnter(object sender, System.EventArgs e)
        {
            button3.BackgroundImage = Images.startButtonHover;
            button3.Refresh();
        }

        private void Button3_MouseLeave(object sender, System.EventArgs e)
        {
            button3.BackgroundImage = Images.startButton;
            button3.Refresh();
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
                    button1.Enabled = false;
                    button1.Visible = false;
                    textBox1.Enabled = false;
                    textBox1.Visible = false;
                    button3.Enabled = true;
                    button3.Visible = true;
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
            if (e.KeyCode == Keys.Q)
            {
                _keyBomb = false;
            }
            if (e.KeyCode == Keys.M)
            {
                _keyMine = false;
            }
            if (e.KeyCode == Keys.T)
            {
                _keyUndo = false;
            }
            if (e.KeyCode == Keys.B)
            {
                _keySuperMine = false;
            }
            if (e.KeyCode == Keys.N)
            {
                _keySuperBomb = false;
            }
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
            if (e.KeyCode == Keys.Q)
            {
                _keyBomb = true;
            }
            if (e.KeyCode == Keys.M)
            {
                _keyMine = true;
            }
            if (e.KeyCode == Keys.T)
            {
                _keyUndo = true;
            }
            if (e.KeyCode == Keys.B)
            {
                _keySuperMine = true;
            }
            if (e.KeyCode == Keys.N)
            {
                _keySuperBomb = true;
            }
            if (e.KeyCode == Keys.Oemtilde)
            {
                _keyMessaging = !_keyMessaging;
            }
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(null, null);
                e.SuppressKeyPress = true;
            }
        }

        //Timer that checks button presses
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (game.gameStarted)
            {
                CheckButtonPresses();
                textBox3.Clear();
            }
        }

        //Checking which buttons were pressed
        private void CheckButtonPresses()
        {
            int x = 0;
            int y = 0;
            string action = "";
            string command = "";

            if (!_keyMessaging)
            {
                //Movement directions
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

                //Actions
                if (_keyBomb)
                {
                    action = "placeBomb";
                }
                if (_keyMine)
                {
                    action = "placeMine";
                }
                if (_keyUndo)
                {
                    action = "undo";
                }
                if (_keySuperMine)
                {
                    action = "placeMineS";
                }
                if (_keySuperBomb)
                {
                    action = "placeBombS";
                }

                //Movement
                if (x == -1 && y == 0)
                {
                    command = "moveleft";
                }
                if (x == 1 && y == 0)
                {
                    command = "moveright";
                }
                if (x == 0 && y == -1)
                {
                    command = "moveup";
                }
                if (x == 0 && y == 1)
                {
                    command = "movedown";
                }
                if (x == -1 && y == -1)
                {
                    command = "moveleftup";
                }
                if (x == -1 && y == 1)
                {
                    command = "moveleftdown";
                }
                if (x == 1 && y == -1)
                {
                    command = "moverightup";
                }
                if (x == 1 && y == 1)
                {
                    command = "moverightdown";
                }

                SendActionCommand(action);
                SendMoveCommand(command);
            }

            //Check if player wants to use messaging
            if (_keyMessaging)
            {
                textBox2.Focus();
            }
            else
            {
                textBox3.Focus();
            }
        }

        //Send a move command to the server
        public async void SendMoveCommand(string command)
        {
            if(command == "")
            {
                return;
            }

            try
            {
                await connection.InvokeAsync("SendMoveMessage", command);
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
            }
        }

        //Send an action command to the server
        public async void SendActionCommand(string action)
        {
            if (action == "")
            {
                return;
            }

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
