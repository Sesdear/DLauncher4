using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DristLauncher_4
{
    public class ChecksForValid
    {
        public bool checkNickname(string nickname)
        {
            Regex regex = new Regex("^[A-Za-z0-9_]+$");
            if (string.IsNullOrEmpty(nickname) || nickname.Length < 3 || nickname.Length > 16 || !regex.IsMatch(nickname))
            {
                MessageBox.Show(
        "Error",
        "Nickname is Invalid",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error,
        MessageBoxDefaultButton.Button1,
        MessageBoxOptions.DefaultDesktopOnly);
                return false;


            }
            else
            {
                return true;
            }

        }
        public bool CheckJavaPath()
        {
            if (!File.Exists(MinecraftOptions.Default.JavaPath))
            {
                MessageBox.Show($"Java 17 не найдена по пути:\n{MinecraftOptions.Default.JavaPath}\nИзмените в настройках путь", "Ошибка");
                return false;
            }
            else { return true; }

        }
        public string CheckExistsDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return path;
            }
            else
            {
                return path;
            }
        }

    }
    public class CrackedUUID
    {
        public void generateCrackedUUID(DebugForm debugForm)
        {
            string currentUUID = MinecraftOptions.Default.Uuid;
            if (string.IsNullOrEmpty(currentUUID))
            {
                Guid randomUuid = Guid.NewGuid();
                string hexUuid = randomUuid.ToString("N");
                if (debugForm != null)
                {
                    debugForm.Log("Generate new uuid: " + hexUuid);
                }
                MinecraftOptions.Default.Uuid = hexUuid;
                MinecraftOptions.Default.Save();
            }
        }
        public void generateCrackedAccessToken(DebugForm debugForm)
        {

            string currentUUID = MinecraftOptions.Default.AccessToken;
            if (string.IsNullOrEmpty(currentUUID))
            {
                Guid randomUuid = Guid.NewGuid();

                string hexUuid = randomUuid.ToString("N");
                if (debugForm != null)
                {

                    debugForm.Log("Generate new AccessToken: " + hexUuid);

                }

                MinecraftOptions.Default.Uuid = hexUuid;
                MinecraftOptions.Default.Save();

            }

        }
        public void generateCrackedClientToken(DebugForm debugForm)
        {

            string currentUUID = MinecraftOptions.Default.ClientToken;
            if (string.IsNullOrEmpty(currentUUID))
            {
                Guid randomUuid = Guid.NewGuid();

                string hexUuid = randomUuid.ToString("N");
                if (debugForm != null)
                {


                    debugForm.Log("Generate new ClientToken: " + hexUuid);

                }
                MinecraftOptions.Default.Uuid = hexUuid;
                MinecraftOptions.Default.Save();

            }

        }
    }
    public class GetServerInfo
    {
        public void SetPropInfo(
            Label PropServerNameLabel,
            Label PropDescriptionLabel
            )
        {
            try
            {
                string data = File.ReadAllText("data.json");
                ServerInfo serverinfo = JsonConvert.DeserializeObject<ServerInfo>(data);

                if (serverinfo == null)
                {
                    MessageBox.Show("Ошибка: Не удалось десериализовать JSON или список серверов пуст.");
                    return;
                }

                PropServerNameLabel.Text = serverinfo.ServerName;
                PropDescriptionLabel.Text = serverinfo.Description;

            }
            catch (JsonException e)
            {
                MessageBox.Show($"Ошибка JSON при загрузке: {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Неизвестная ошибка: {e.Message}");
            }


        }
        public void StartSupport(DebugForm debugForm, FlowLayoutPanel flowLayoutPanel, Label ServerNameLabel, Label ServerDescriptionLabel)
        {
            SetServerIndex(debugForm, flowLayoutPanel, ServerNameLabel, ServerDescriptionLabel);
        }

        public string ServerIndexUrlBuilder(string path_to_serverList)
        {
            string url = $"{serversUrls.Default.ServerProtocol}://{serversUrls.Default.ServerIp}:{serversUrls.Default.ServerPort}/{path_to_serverList}";
            return url;
        }
        public string ServersUrlBuilder(string path_to_serverData)
        {
            string url = $"{serversUrls.Default.ServerProtocol}://{serversUrls.Default.ServerIp}:{serversUrls.Default.ServerPort}/{path_to_serverData}/data.json";
            return url;
        }

        private async Task SetServersPanels(List<string> serverUrls, FlowLayoutPanel panel, DebugForm debugForm, Label ServerNameLabel, Label ServerDescriptionLabel)
        {
            var responseMethods = new ResponseMethods();

            panel.Controls.Clear();

            foreach (string serverUrl in serverUrls)
            {
                if (string.IsNullOrEmpty(serverUrl))
                    continue;

                string url = ServersUrlBuilder(serverUrl);

                if (debugForm != null)
                    debugForm.Log($"Загрузка сервера: {url}");

                try
                {
                    HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url, debugForm);
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    File.WriteAllText("data.json", jsonResponse);


                    ServerInfo serverInfo = JsonConvert.DeserializeObject<ServerInfo>(jsonResponse);

                    if (serverInfo == null)
                    {
                        if (debugForm != null)
                            debugForm.Log($"Ошибка: не удалось десериализовать {url}");
                        continue;
                    }

                    if (debugForm != null)
                    {
                        debugForm.Log($"DATA.JSON\n" + $"ServerName: {serverInfo.ServerName}\n" +
                            $"Description: {serverInfo.Description}\n" +
                            $"Image: {serverInfo.Image}\n" +
                            $"MVersion: {serverInfo.MVersion}\n" +
                            $"MModLoader: {serverInfo.MModLoader}\n" +
                            $"MModLoaderVersion: {serverInfo.MModLoaderVersion}\n" +
                            $"ModPackSize: {serverInfo.ModPackSize}\n" +
                            $"ModPackVersion: {serverInfo.ModPackVersion}\n" +
                            $"ModPackFiles: {serverInfo.ModPackFiles}");
                    }
                    MinecraftOptions.Default.MVersion = serverInfo.MVersion;
                    MinecraftOptions.Default.MModLoaderVersion = serverInfo.MModLoaderVersion;
                    MinecraftOptions.Default.MModLoader = serverInfo.MModLoader;
                    MinecraftOptions.Default.Path = serverInfo.ServerName;
                    MinecraftOptions.Default.Save();



                    ServerItemControl serverControl = new ServerItemControl(serverInfo.ServerName, serverInfo.Image, serverInfo.MServerIp);
                    serverControl.ServerSelected += (s, e) =>
                    {
                        ServerNameLabel.Text = serverInfo.ServerName;
                        ServerDescriptionLabel.Text = serverInfo.Description;
                        MinecraftOptions.Default.SelectedServer = serverInfo.ServerName;
                        MinecraftOptions.Default.Save();
                    };

                    // Добавляем в FlowLayoutPanel
                    panel.Controls.Add(serverControl);

                }
                catch (JsonException e)
                {
                    MessageBox.Show($"Ошибка JSON при загрузке сервера {serverUrl}: {e.Message}");
                }
                catch (Exception e)
                {
                    if (debugForm != null)
                        debugForm.Log($"Неизвестная ошибка при загрузке сервера {url}: {e.Message}");
                }
            }
        }

        private async Task SetServerIndex(DebugForm debugForm, FlowLayoutPanel flowLayoutPanel, Label ServerNameLabel, Label ServerDescriptionLabel)
        {
            string url = ServerIndexUrlBuilder(serversUrls.Default.ServersListFile);
            var responseMethods = new ResponseMethods();

            try
            {
                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url, debugForm);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                File.WriteAllText("servers_index.json", jsonResponse);


                ServerIndexes servers = JsonConvert.DeserializeObject<ServerIndexes>(jsonResponse);

                if (servers == null || servers.Servers == null || servers.Servers.Count == 0)
                {
                    MessageBox.Show("Ошибка: Не удалось десериализовать JSON или список серверов пуст.");
                    return;
                }

                await SetServersPanels(servers.Servers, flowLayoutPanel, debugForm, ServerNameLabel, ServerDescriptionLabel);


                if (debugForm != null)
                {
                    debugForm.Log("Успешно загружены сервера:\n");
                    for (int i = 0; i < servers.Servers.Count; i++)
                    {
                        debugForm.Log($"Server{i + 1}: {servers.Servers[i]}");
                    }
                }
            }
            catch (JsonException e)
            {
                MessageBox.Show($"Ошибка JSON при загрузке серверов: {e.Message}");
            }
            catch (Exception e)
            {
                if (debugForm != null)
                {
                    debugForm.Log($"Неизвестная ошибка при загрузке серверов {url}: {e.Message}");
                }
            }
        }


        private async Task GetGithubInfo(DebugForm debugForm, FlowLayoutPanel flowLayoutPanel)
        {
            string url = serversUrls.Default.GithubUrl;
            var responseMethods = new ResponseMethods();

            try
            {
                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url, debugForm);
                string jsonResponse = await response.Content.ReadAsStringAsync();


                File.WriteAllText("githubIps.json", jsonResponse);


                GithubInfo info = JsonConvert.DeserializeObject<GithubInfo>(jsonResponse);

                if (info == null)
                {
                    MessageBox.Show("Ошибка: Не удалось десериализовать JSON или полученные данные пусты.");
                    return;
                }


                serversUrls.Default.ServerIp = info.DefaultIp;
                serversUrls.Default.ServerPort = info.DefaultPort;
                serversUrls.Default.ServerProtocol = info.TypeOfProtocol;
                serversUrls.Default.ServersListFile = info.ServersListFile;
                serversUrls.Default.Save();

                if (debugForm != null)
                {

                    debugForm.Log("Данные успешно получены и сохранены.");

                }
            }
            catch (JsonException e)
            {
                MessageBox.Show($"Ошибка JSON 'GithubInfo': {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Неизвестная ошибка 'GithubInfo' {url}: {e.Message}");
            }
        }
    }
    public class ResponseMethods
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public async Task<HttpResponseMessage> ResponseUrlAsync(string url, DebugForm debugForm)
        {
            if (debugForm != null)
            {
                debugForm.Log(url);
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            try
            {

                if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                {
                    throw new ArgumentException("Invalid URL format");
                }


                if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
                {
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", "DristLauncher/4.0");
                }

                HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Server returned {response.StatusCode}: {response.ReasonPhrase}, {url}");
                }

                return response;
            }
            catch (HttpRequestException httpEx)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Ошибка подключения к серверу:\n{httpEx.Message}",
                    "Ошибка сети",
                    System.Windows.Forms.MessageBoxButtons.OK
                    );
                throw;
            }
            catch (TaskCanceledException)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Превышено время ожидания ответа от сервера",
                    "Таймаут",
                    System.Windows.Forms.MessageBoxButtons.OK);
                throw;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Произошла непредвиденная ошибка:\n{ex.Message}",
                    "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK);
                throw;
            }
        }
    }



}
