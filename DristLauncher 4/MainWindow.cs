
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DristLauncher_4
{
/// <summary>
/// ToDo
/// 1. Сделать проверку на наличие чтобы каждый раз не перегружать майнкрафт
/// 
/// 
/// </summary>
    public partial class MainWindow : Form
    {
        
        DebugForm debugForm;
        public MainWindow()
        {
            
            InitializeComponent();
            ApplyTheme();




            GetServerInfo getServerInfo = new GetServerInfo();
            ChecksForValid checksForValid = new ChecksForValid();

            string currentVersion = FileVersionInfo
    .GetVersionInfo(Assembly.GetExecutingAssembly().Location)
    .FileVersion;

            vLabel.Text = currentVersion;

            
            if (LauncherSettings.Default.DebugMode)
            {
                debugForm = new DebugForm();
                debugForm.Show();
            }

            getServerInfo.StartSetServersPanels(ServerLabel1, ServerDescriptionLabel1, ServerPictureBox1, debugForm);
            checksForValid.CheckExistsDir("minecraft");
            
            NicknameTextBox.Text = MinecraftOptions.Default.Nickname;

            LoadNewsFromServer("http://217.114.10.85:30752/news.json");

            // Настраиваем таймер
            newsTimer.Interval = 9000;
            newsTimer.Tick += (s, e) => ShowNews((currentNewsIndex + 1) % newsList.Count);
            newsTimer.Start();

            // Отображаем первую новость
            if (newsList.Count > 0)
                ShowNews(0);








        }

        private void ApplyTheme()
        {
            // Устанавливаем цвет фона формы
            this.BackColor = ParseColor(LauncherSettings.Default.MainColor);

            // Перебираем все элементы формы рекурсивно
            ApplyColorsToControls(this);
        }

        private void ApplyColorsToControls(Control parent)
        {
            PlayButton.FillColor = ParseColor(LauncherSettings.Default.ButtonsColor);
            SettingsButton.FillColor = ParseColor(LauncherSettings.Default.ButtonsColor);
            FolderButton.FillColor = ParseColor(LauncherSettings.Default.ButtonsColor);

            panel1.BackColor = ParseColor(LauncherSettings.Default.PanelsColor);
            guna2Panel1.BackColor = ParseColor(LauncherSettings.Default.PanelsColor);
            panel3.BackColor = ParseColor(LauncherSettings.Default.PanelsColor);
            panel4.BackColor = ParseColor(LauncherSettings.Default.PanelsColor);

            splitContainer2.BackColor = ParseColor(LauncherSettings.Default.ContainersColor);
            splitContainer3.BackColor = ParseColor(LauncherSettings.Default.ContainersColor);
            splitContainer4.BackColor = ParseColor(LauncherSettings.Default.ContainersColor);

        }

        private Color ParseColor(string rgbString)
        {
            // "159,87,39"
            var parts = rgbString.Split(',');
            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int r) &&
                int.TryParse(parts[1], out int g) &&
                int.TryParse(parts[2], out int b))
            {
                return Color.FromArgb(r, g, b);
            }
            return Color.Gray; // fallback
        }




        private class NewsItem
        {
            public string Text { get; set; }
            public string ImageUrl { get; set; }
        }

        private List<NewsItem> newsList = new List<NewsItem>();
        private int currentNewsIndex = 0;
        private Timer newsTimer = new Timer();

        private void LoadNewsFromServer(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8; // важно для кириллицы
                    string json = client.DownloadString(url);

                    JObject parsed = JObject.Parse(json);
                    JArray newsArray = (JArray)parsed["news"];

                    foreach (var item in newsArray)
                    {
                        string[] parts = item.ToString().Split(';');
                        string text = parts[0];
                        string imageUrl = parts.Length > 1 ? parts[1] : null;

                        newsList.Add(new NewsItem { Text = text, ImageUrl = imageUrl });
                    }
                }

                // Создаём "точки"
                for (int i = 0; i < newsList.Count; i++)
                {
                    Label dot = new Label();
                    dot.Text = "●"; // текущая точка
                    dot.Font = new Font("Segoe UI Symbol", 9, FontStyle.Regular);
                    dot.ForeColor = Color.Gray;
                    dot.AutoSize = true;
                    dot.Margin = new Padding(2, 0, 2, 0); // отступы для компактности
                    dot.Tag = i;

                    // кликабельность
                    dot.Cursor = Cursors.Hand;
                    dot.Click += Dot_Click;

                    DotsPanel.Controls.Add(dot);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки новостей: " + ex.Message);
            }
        }

        private void ShowNews(int index)
        {
            if (newsList.Count == 0) return;

            currentNewsIndex = index;
            var news = newsList[currentNewsIndex];

            NewsLabel.Text = news.Text;

            if (!string.IsNullOrEmpty(news.ImageUrl))
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        byte[] imgData = client.DownloadData(news.ImageUrl);
                        using (var ms = new MemoryStream(imgData))
                        {
                            NewsPictureBox.Image = Image.FromStream(ms);
                        }
                    }
                    catch
                    {
                        NewsPictureBox.Image = null; // если не удалось загрузить картинку
                    }
                }
            }
            else
            {
                NewsPictureBox.Image = null;
            }


            // Обновляем точки
            for (int i = 0; i < DotsPanel.Controls.Count; i++)
            {
                Label dot = (Label)DotsPanel.Controls[i];
                dot.ForeColor = (i == currentNewsIndex) ? Color.White : Color.Gray;
            }

        }

        private void Dot_Click(object sender, EventArgs e)
        {
            Label clicked = sender as Label;   // меняем Button → Label
            if (clicked == null) return;

            int index = (int)clicked.Tag;
            ShowNews(index);
        }





        private void splitContainer5_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Process.Start("explorer.exe", currentDirectory);
        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void ServerButton1_Click(object sender, EventArgs e)
        {
            GetServerInfo getServerInfo1 = new GetServerInfo();
            getServerInfo1.SetPropInfo(
                ServerNameLabel,
                ServerDescriptionLabel
                
                );
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            ChecksForValid checks = new ChecksForValid();
            StartMinecraft startMinecraft = new StartMinecraft();
            CrackedUUID crackedUUID = new CrackedUUID();
            

            string uuid = MinecraftOptions.Default.Uuid;
            string accessToken = MinecraftOptions.Default.AccessToken;
            string clientToken = MinecraftOptions.Default.ClientToken;
            string nickname = NicknameTextBox.Text;
            MinecraftOptions.Default.Nickname = nickname;
            MinecraftOptions.Default.Save();

            
            if (uuid == string.Empty)
            {
                crackedUUID.generateCrackedUUID(debugForm);
            }
            if (accessToken == string.Empty)
            {
                crackedUUID.generateCrackedAccessToken(debugForm);
            }
            if (clientToken == string.Empty)
            {
                crackedUUID.generateCrackedClientToken(debugForm);
            }

            if (!checks.CheckJavaPath())
            {
                return;
            }

            if (!checks.checkNickname(nickname) || nickname == string.Empty)
            {
                return;
            }
            try
            {
                PlayButton.Enabled = false;


                startMinecraft.Start(debugForm, PlayButton);
            }
            catch { PlayButton.Enabled = true; }
            


            
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DiscordButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/462A2hxhYY");
        }

        private void GithubButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Sesdear/DLauncher4/releases");
        }
    }
}
