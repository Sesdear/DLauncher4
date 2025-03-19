using System.Security.Policy;
using System.Text.Json;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json.Serialization;
using System;


namespace DristLauncher4
{
    public partial class exectServers : Form
    {
        int PW;

        bool hide;

        string FileServerUrl,
            CurrentStartMVersion,
            CurrentServer,
            CurrentModLoader,
            CurrentModLoaderVersion,
            CurrentJavaVersion;

        public exectServers()
        {
            InitializeComponent();
            ramUpDown.Minimum = 2;
            ramUpDown.Maximum = 24;

            setCurrentOptions();

            

            getFileServerUrl();


            PW = 320;
            hide = true;




        }

        private void settingsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void setCurrentOptions()
        {
            string nickname = MinecraftUser.Default.username;
            int ram = MinecraftStartParametrs.Default.XmxGb;
            string java_path = MinecraftStartParametrs.Default.JavaPath;

            textBox1.Text = java_path;
            ramUpDown.Value = ram;
            usernameBox.Text = nickname;
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
                
               minecraftMethods.InstallAndLaunchMinecraft(selectedText, "1.20.4");
            }
            
            
            
            
        }
        private class JsonPortIpD
        {
            public string defaultIp { get; set; }
            public int defaultPort { get; set; }
            public string type { get; set; }
            public string serverListDir { get; set; }
            public string serverListFile { get; set; }

        }
        private async Task getFileServerUrl()
        {
            string ping_url = IPCONFIGs.Default.url_to_build;
            
            try
            {

                using HttpClient client = new HttpClient();


                HttpResponseMessage response = await client.GetAsync(ping_url);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                
                
                JsonPortIpD data = JsonSerializer.Deserialize<JsonPortIpD>(jsonResponse);

                
                JsonDocument doc = JsonDocument.Parse(jsonResponse);
                string server_ip = doc.RootElement.GetProperty("defaultIp").GetString();
                int port = doc.RootElement.GetProperty("defaultPort").GetInt32();
                string type_protocol = doc.RootElement.GetProperty("type").GetString();
                string server_list_dir = doc.RootElement.GetProperty("serverListDir").GetString();
                string server_list_file = doc.RootElement.GetProperty("serverListFile").GetString();

                FileServerUrl = $"{type_protocol}://{server_ip}:{port}";
                string ServerListUrl = $"{FileServerUrl}/{server_list_dir}/{server_list_file}";
                
                
                IPCONFIGs.Default.servers_info_url = ServerListUrl;
                IPCONFIGs.Default.file_server_url = FileServerUrl;
                IPCONFIGs.Default.Save();

                GetServerInfo();
                
                
            }
            catch (HttpRequestException f)
            {
                MessageBox.Show($"HttpError 'getFileServerUrl': {f.Message}");
            }
            catch (JsonException f)
            {
                MessageBox.Show($"JsonError 'getFileServerUrl': {f.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Неизвестная ошибка 'getFileServerUrl': {e.Message}");
            }

        }

        private async Task GetServerInfo()
        {
            string url = IPCONFIGs.Default.servers_info_url;
            
            

            try
            {
                using HttpClient client = new HttpClient();

                
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); 

                
                string jsonResponse = await response.Content.ReadAsStringAsync();

                
                Dictionary<string, ServerInfo> servers = JsonSerializer.Deserialize<Dictionary<string, ServerInfo>>(jsonResponse);

                
                if (servers == null || servers.Count == 0)
                {
                    MessageBox.Show("Ошибка: Не удалось десериализовать JSON.");
                    return;
                }

                
                foreach (var server in servers)
                {
                    string serverName = server.Key; 
                    ServerInfo serverInfo = server.Value;

                    
                    
                    serverCheckedListBox.Items.Add(serverName);

                    CurrentStartMVersion = serverInfo.Version;
                    CurrentModLoader = serverInfo.Modloader;
                    CurrentModLoaderVersion = serverInfo.ModloaderVersion;
                    CurrentJavaVersion = serverInfo.JavaVersion;
                }

                
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Ошибка HTTP 'GetServerInfo': {e.Message}");
            }
            catch (JsonException e)
            {
                MessageBox.Show($"Ошибка JSON 'GetServerInfo': {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Неизвестная ошибка 'GetServerInfo' {url}: {e.Message}");
                
            }
        }
        private class ServerInfo
        {
            public string Version { get; set; }
            public string Modloader { get; set; }
            public string ModloaderVersion { get; set; }
            public string JavaVersion { get; set; }
            public int Serverversion { get; set; }
            public string FilesUrl { get; set; }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string new_java_path = textBox1.Text;
            int new_ram = Convert.ToInt32(ramUpDown.Value);
            MinecraftStartParametrs.Default.XmxGb = new_ram;
            MinecraftStartParametrs.Default.JavaPath = new_java_path;
            MinecraftStartParametrs.Default.Save();

        }
    }
    
}
