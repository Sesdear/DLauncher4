using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DristLauncher_4
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            GetServerInfo getServerInfo = new GetServerInfo();
            ChecksForValid checksForValid = new ChecksForValid();
            string nickname = MinecraftOptions.Default.Nickname;

            getServerInfo.StartSetServersPanels(ServerLabel1, ServerDescriptionLabel1, ServerPictureBox1);
            checksForValid.CheckExistsDir("minecraft");
            Form1_Load();
            if (nickname == string.Empty)
            {
                NicknameTextBox.Enter += NicknameTextBox_Enter;
                NicknameTextBox.Leave += NicknameTextBox_Leave;
            }
            else
            {
                NicknameTextBox.Text = nickname;
            }
            

            
            
          
           
             
        }

        
        

        private void splitContainer5_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //------------PlaceHolder--------------
        private void NicknameTextBox_Enter(object sender, EventArgs e)
        {
            if (NicknameTextBox.Text == "Enter Nickname...")
            {
                NicknameTextBox.Text = "";
                NicknameTextBox.ForeColor = Color.Black; // ��� ��� ���� �� ���������
            }
        }

        private void NicknameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NicknameTextBox.Text))
            {
                NicknameTextBox.Text = "Enter Nickname...";
                NicknameTextBox.ForeColor = Color.Gray; // ���� ������������
            }
        }

        private void Form1_Load()
        {
            NicknameTextBox.Text = "Enter Nickname...";
            NicknameTextBox.ForeColor = Color.Gray; // ���� ������������
        }

        //-------------------------------------

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
                ServerDescriptionLabel,
                PropMVersionLabel,
                PropMModLoaderLabel,
                PropodPackSizeLabel,
                PropModPackVersionLabel
                );
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            ChecksForValid checks = new ChecksForValid();
            MinecraftSupport minecraftSupport = new MinecraftSupport();
            ModsFunctions modFunctions = new ModsFunctions();
            CrackedUUID crackedUUID = new CrackedUUID();

            string uuid = MinecraftOptions.Default.Uuid;
            string accessToken = MinecraftOptions.Default.AccessToken;
            string clientToken = MinecraftOptions.Default.ClientToken;
            string nickname = NicknameTextBox.Text;
            MinecraftOptions.Default.Nickname = nickname;
            MinecraftOptions.Default.Save();

            if (uuid == string.Empty)
            {
                crackedUUID.generateCrackedUUID();
            }
            if (accessToken == string.Empty)
            {
                crackedUUID.generateCrackedAccessToken();
            }
            if (clientToken == string.Empty)
            {
                crackedUUID.generateCrackedClientToken();
            }

            if (checks.checkNickname(nickname) == false || nickname == string.Empty)
            {
                return;
            }
            try
            {
                PlayButton.Enabled = false;
                //modFunctions.StartModsChecker();
                
                modFunctions.StartModsChecker();
            }
            catch { PlayButton.Enabled = true; }
            


            
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}
