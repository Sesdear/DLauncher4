using System;
using System.Drawing;
using System.Windows.Forms;

namespace DristLauncher_4
{
    public class ServerItemControl : UserControl
    {
        private PictureBox picture;
        private Label label;
        private Label label2;
        private Timer timer;
        public string ServerName { get; private set; }
        public string ServerIp { get; private set; }

        public event EventHandler ServerSelected;


        public ServerItemControl(string serverName, Image serverIcon, string serverIp)
        {
            Initialize(serverName, serverIp);
            picture.Image = serverIcon;
        }


        public ServerItemControl(string serverName, string imageUrl, string serverIp)
        {
            Initialize(serverName, serverIp);

            if (!string.IsNullOrEmpty(imageUrl))
            {
                try
                {
                    picture.LoadAsync(imageUrl);
                }
                catch
                {
                    picture.BackColor = Color.Gray;
                }
            }
            else
            {
                picture.BackColor = Color.Gray;
            }
        }

        private void Initialize(string serverName, string serverIp)
        {
            this.ServerName = serverName;
            this.ServerIp = serverIp;


            timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
            timer.Start();

            this.Width = 470;
            this.Height = 80;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(5);
            this.BackColor = Color.FromArgb(159, 87, 39);
            this.ForeColor = Color.White;

            picture = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 70,
                Height = 70,
                Location = new Point(5, 5)
            };

            label = new Label
            {
                Text = serverName,
                Font = new Font("Consolas", 12, FontStyle.Bold),
                Location = new Point(75, 30),
                AutoSize = true
            };

            label2 = new Label
            {
                Text = "0/0",
                Font = new Font("Consolas", 10, FontStyle.Bold),
                Location = new Point(400, 30),
                AutoSize = true
            };

            this.Controls.Add(picture);
            this.Controls.Add(label);
            this.Controls.Add(label2);


            UpdateOnlineStatus();


            this.Click += (s, e) => OnServerSelected();
            picture.Click += (s, e) => OnServerSelected();
            label.Click += (s, e) => OnServerSelected();
            label2.Click += (s, e) => OnServerSelected();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateOnlineStatus();
        }

        private void UpdateOnlineStatus()
        {
            try
            {
                string[] parts = ServerIp.Split(':');
                if (parts.Length >= 2)
                {
                    string ip = parts[0];
                    int port = int.Parse(parts[1]);
                    var status = MinecraftPing.GetStatus(ip, port);


                    if (label2.InvokeRequired)
                    {
                        label2.Invoke(new Action(() =>
                        {
                            label2.Text = $"{status.OnlinePlayers}/{status.MaxPlayers}";
                        }));
                    }
                    else
                    {
                        label2.Text = $"{status.OnlinePlayers}/{status.MaxPlayers}";
                    }
                }
            }
            catch (Exception ex)
            {

                if (label2.InvokeRequired)
                {
                    label2.Invoke(new Action(() =>
                    {
                        label2.Text = "Ошибка";
                    }));
                }
                else
                {
                    label2.Text = "Ошибка";
                }


                Console.WriteLine($"Ошибка получения статуса сервера: {ex.Message}");
            }
        }

        private void OnServerSelected()
        {
            ServerSelected?.Invoke(this, EventArgs.Empty);
        }


        public void SetSelected(bool selected)
        {
            this.BackColor = selected ? Color.LightBlue : Color.FromArgb(159, 87, 39);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer?.Stop();
                timer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}