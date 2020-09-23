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
        const int xsize = 20;
        const int ysize = 20;
        private Game game;

        private string username;
        private int clientId;
        private int timePlayed = 0;
        private bool _keyTop, _keyLeft, _keyRight, _keyBot, _keyBomb;

        public int startspeed = 1;
        public const int playerSize = 15;
        public static readonly int[] background = { 98, 65, 8 };
        Bitmap playerPicture = new Bitmap("p1.png");

        public Form1()
        {
            //FORM SHIT
            InitializeComponent();

            connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/gamehub").Build();

            //RECEIVING MESSAGES

            //Someone has logged in
            connection.On<string>("LoggedinMessage", (username) =>
            {
                richTextBox1.AppendText(username + " has logged in\n", Color.Green);
            });

            //Someone sent a message
            connection.On<string, string>("ReceiveMessage", (username, message) =>
            {
                richTextBox1.AppendText(username + ": " + message + "\n");
            });

            List<PlayerSimple> playersSimple = new List<PlayerSimple>();
            //Game has started info of players sent
            connection.On<List<string>>("InitializePlayers", (players) =>
            {
                playersSimple.Clear();
                PlayerSimple ps = new PlayerSimple();
                foreach (var player in players)
                {
                    string[] playerInfo = player.Split('|');
                    /*
                    foreach (var item in playerInfo)
                    {
                        richTextBox1.AppendText(item + "\n");
                    }
                    richTextBox1.AppendText("\n");*/
                    ps.username = playerInfo[0];
                    ps.id = playerInfo[1];
                    ps.x = int.Parse(playerInfo[2]);
                    ps.y = int.Parse(playerInfo[3]);
                    playersSimple.Add(ps);
                }
                richTextBox1.AppendText(playersSimple.Count + "\n");
                /*
                foreach (var p in playersSimple)
                {
                    richTextBox1.AppendText(p.username + "  " + p.id + "  " + p.x + "  " + p.y + "\n");
                }*/
                pictureBox1.Image = DrawPlayersSimple(game, playersSimple);
            });

            game = new Game();
            initialiseValues();
            //pictureBox1.Image = game.getGame();
            //pictureBox1.Image = DrawPlayersSimple(game, playersSimple);
        }

        public class PlayerSimple 
        {
            public string username;
            public string id;
            public int x, y;

            public PlayerSimple()
            {
            }

            public PlayerSimple(string username, string id, int x, int y)
            {
                this.username = username;
                this.id = id;
                this.x = x;
                this.y = y;
            }
        }

        public Bitmap DrawPlayersSimple(Game game, List<PlayerSimple> players)
        {
            Bitmap newMap = game.getMap();
            PlayerSimple[] simplePlayers = players.ToArray();
            for (int i = 0; i < players.Count; i++)
            {
                PlayerSimple player = simplePlayers[i];
                int[] xy = new int[] { player.x, player.y };
                for (int x = 0; x < playerSize; x++)
                {
                    for (int y = 0; y < playerSize; y++)
                    {
                        newMap.SetPixel(x + xy[0], y + xy[1], playerPicture.GetPixel(x, y));
                    }
                }
            }
            return newMap;
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
                MessageBox.Show("There was an error.");
            }
            /*try
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
            }*/
        }
        private void update_Map_Slow()
        {
            Bitmap back = game.getGame();
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
                await connection.InvokeAsync("StartMessage");
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
