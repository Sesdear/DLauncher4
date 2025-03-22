using System.Security.Policy;
using System.Text.Json;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json.Serialization;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Xml.Linq;
using CmlLib.Core.FileExtractors;
using CmlLib.Core.Java;



namespace DristLauncher4
{
    public partial class exectServers : Form
    {
        int PW;

        bool hide; 

        public exectServers()
        {
            InitializeComponent();
            
            var support = new SupportsMethods();

            ramUpDown.Minimum = 2;
            ramUpDown.Maximum = 24;


            support.setCurrentOptions(textBox1, ramUpDown, usernameBox);
            support.getFileServerUrl(serverCheckedListBox);

            PW = 320;
            hide = true;

        }
        private void settingsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void settingsButton_Click(object sender, EventArgs e)
        {
            tmrSettingsPanel.Start();
        }

        private void tmrSettingsPanel_Tick(object sender, EventArgs e)
        {
            if (hide)
            {
                settingsPanel.Width = settingsPanel.Width + 20;
                if (settingsPanel.Width >= PW)
                {
                    tmrSettingsPanel.Stop();
                    hide = false;
                    this.Refresh();
                }
            }
            else
            {
                settingsPanel.Width = settingsPanel.Width - 20;
                if (settingsPanel.Width <= 0)
                {
                    tmrSettingsPanel.Stop();
                    hide = true;
                    this.Refresh();
                }
            }
        }

        private void serverCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void startButton_Click(object sender, EventArgs e)
        {
            
            MinecraftMethods minecraftMethods = new MinecraftMethods();
            string selectedText;
            selectedText = "";

            


            string nickname = usernameBox.Text;
            if (minecraftMethods.checkNickname(nickname))
            {
                minecraftMethods.saveNickname(nickname);
                if (serverCheckedListBox.SelectedItem != null)
                {
                    selectedText = serverCheckedListBox.SelectedItem.ToString();

                }

                string JsonString = File.ReadAllText("servers_list.json");
                Dictionary<string, ServerInfo> serverInfo = JsonSerializer.Deserialize<Dictionary<string, ServerInfo>>(JsonString);
                if (serverInfo != null && serverInfo.ContainsKey(selectedText))
                {
                    ServerInfo current_server = serverInfo[selectedText];
                    string version = current_server.Version;
                    string modloader = current_server.Modloader;
                    string modloaderVersion = current_server.ModloaderVersion;

                    minecraftMethods.InstallAndLaunchMinecraft(selectedText, version, progressBar1, startButton, progressLabel, modloader, modloaderVersion);
                }
                else
                {
                    MessageBox.Show("Выбранный сервер не найден в файле servers_list.json!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            
            
            
            
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            string new_java_path = textBox1.Text;
            int new_ram = Convert.ToInt32(ramUpDown.Value);
            MinecraftStartParametrs.Default.XmxGb = new_ram;
            MinecraftStartParametrs.Default.JavaPath = new_java_path;
            MinecraftStartParametrs.Default.Save();

        }
        private void findButton_Click(Object sender, EventArgs e)
        {
            

        }
    }
}
