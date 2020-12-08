using Microsoft.AspNetCore.SignalR.Client;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
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

        //Custom font
        private static PrivateFontCollection pfc = new PrivateFontCollection();
        private static uint cFonts = 0;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private static void AddFont(byte[] fontdata)
        {
            System.IntPtr dataPointer = Marshal.AllocCoTaskMem(fontdata.Length);
            Marshal.Copy(fontdata, 0, dataPointer, (int)fontdata.Length);
            AddFontMemResourceEx(dataPointer, (uint)fontdata.Length, IntPtr.Zero, ref cFonts);
            cFonts += 1;
            pfc.AddMemoryFont(dataPointer, (int)fontdata.Length);
        }

        public ClientForm()
        {
            // FORM AND DATA INITIALIZATION
            InitializeComponent();  //Initialize form components
            InitializeValues();     //Initialize keypress booleans

            game = new Game<Bitmap, Color>();      //Initialize the game logic object
            timer1.Enabled = true;  //Enable timer that draws the map

            richTextBox1.AppendText("BMAN v1", Color.Purple);
        }

        public void InitializeConnection()
        {
            connection = new HubConnectionBuilder().WithUrl($"http://{ textBox4.Text }:5000/gamehub").Build();   //Set up the hub connection

            // RECEIVING MESSAGES
            //Receive another player's login message
            connection.On<string>("ReceiveLoginMessage", (username) =>
            {
                richTextBox1.AppendText("\n" + username + " has logged in", Color.Green);
            });

            //Receive another player's sent message
            connection.On<string, string>("ReceiveMessage", (username, message) =>
            {
                richTextBox1.AppendText("\n" + username + ": " + message, Color.Black);
            });

            //Game has started info of players sent
            connection.On<string, string, int, string, int>("SendData", (jsonPlayers, jsonMap, playerHealth, scoreboard, roundEnded) =>
            {
                game.players = JsonConvert.DeserializeObject<List<Player>>(jsonPlayers, settings);
                game.map = JsonConvert.DeserializeObject<Map>(jsonMap, settings);

                switch (roundEnded)
                {
                    case 0:
                        PrintScoreboardRound(new ScoreboardTemplateProxy(JsonConvert.DeserializeObject<ScoreboardRound>(scoreboard, settings)));
                        break;
                    case 1:
                        PrintScoreboardMatch(new ScoreboardTemplateProxy(JsonConvert.DeserializeObject<ScoreboardMatch>(scoreboard, settings)));
                        break;
                    default:
                        throw new NotImplementedException();
                }
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

                    labelHealth.Visible = true;
                    labelHealthValue.Visible = true;
                    label5.Visible = true;


                    game.drawBackground();
                }
                labelHealthValue.Text = playerHealth.ToString();
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
        public void InitializeValues()
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

            //Use custom font
            AddFont(Images.arcadeClassicFont);
            labelHealth.Font = new Font(pfc.Families[0], 16);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Login button
        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "")
            {
                try
                {
                    InitializeConnection();
                    await connection.StartAsync();
                    username = textBox1.Text;
                    await connection.InvokeAsync("SendLoginMessage", username);
                    richTextBox1.AppendText("\nConnected to the server", Color.Green);
                    button1.Enabled = false;
                    button1.Visible = false;
                    textBox1.Enabled = false;
                    textBox1.Visible = false;
                    button3.Enabled = true;
                    button3.Visible = true;
                    textBox4.Enabled = false;
                    textBox4.Visible = false;
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = richTextBox1.Text + ex.Message + "\n";
                }
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

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

        private void PrintScoreboardRound(IScoreboardTemplate scoreboard)
        {
            foreach(var x in scoreboard.FormTable())
            {
                switch(x.Item1)
                { //red blue green yellow
                    case 0:
                        label1.ForeColor = Color.Red;
                        label1.Text = x.Item2.ToString();
                        label1.Visible = true;
                        break;
                    case 1:
                        label2.ForeColor = Color.Blue;
                        label2.Text = x.Item2.ToString();
                        label2.Visible = true;
                        break;
                    case 2:
                        label3.ForeColor = Color.Green;
                        label3.Text = x.Item2.ToString();
                        label3.Visible = true;
                        break;
                    case 3:
                        label4.ForeColor = Color.Yellow;
                        label4.Text = x.Item2.ToString();
                        label4.Visible = true;
                        break;
                    default:
                        throw new NotImplementedException();                   
                }
            }
        }

        private void PrintScoreboardMatch(IScoreboardTemplate scoreboard)
        {
            label6.Visible = true;
            foreach (var x in scoreboard.FormTable())
            {
                switch (x.Item1)
                { //red blue green yellow
                    case 0:
                        label7.ForeColor = Color.Red;
                        label7.Text = x.Item2.ToString();
                        label7.Visible = true;
                        if (x.Item3 == "bold")
                        {
                            label7.Font = new Font(label7.Font.Name, label7.Font.Size, FontStyle.Bold);
                        }
                        else
                        {
                            label7.Font = new Font(label7.Font.Name, label7.Font.Size);
                        }
                        break;
                    case 1:
                        label8.ForeColor = Color.Blue;
                        label8.Text = x.Item2.ToString();
                        label8.Visible = true;
                        if (x.Item3 == "bold")
                        {
                            label8.Font = new Font(label8.Font.Name, label8.Font.Size, FontStyle.Bold);
                        }
                        else
                        {
                            label8.Font = new Font(label8.Font.Name, label8.Font.Size);
                        }
                        break;
                    case 2:
                        label9.ForeColor = Color.Green;
                        label9.Text = x.Item2.ToString();
                        label9.Visible = true;
                        if (x.Item3 == "bold")
                        {
                            label9.Font = new Font(label9.Font.Name, label9.Font.Size, FontStyle.Bold);
                        }
                        else
                        {
                            label9.Font = new Font(label9.Font.Name, label9.Font.Size);
                        }
                        break;
                    case 3:
                        label10.ForeColor = Color.Yellow;
                        label10.Text = x.Item2.ToString();
                        label10.Visible = true;
                        if (x.Item3 == "bold")
                        {
                            label10.Font = new Font(label10.Font.Name, label10.Font.Size, FontStyle.Bold);
                        }
                        else
                        {
                            label10.Font = new Font(label10.Font.Name, label10.Font.Size);
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
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
