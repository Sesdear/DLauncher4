using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Security.Policy;
using System.Text.RegularExpressions;


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
        public void generateCrackedUUID()
        {
            /*
             Check if uuid in file MinecraftUser.settings is empty generated new uuid
             */
            string currentUUID = MinecraftOptions.Default.Uuid;
            if (string.IsNullOrEmpty(currentUUID))
            {
                Guid randomUuid = Guid.NewGuid();

                string hexUuid = randomUuid.ToString("N");

                Console.WriteLine($"New uuid created: {hexUuid}");

                MinecraftOptions.Default.Uuid = hexUuid;
                MinecraftOptions.Default.Save();

            }

        }
        public void generateCrackedAccessToken()
        {
            /*
             Check if uuid in file MinecraftUser.settings is empty generated new uuid
             */
            string currentUUID = MinecraftOptions.Default.AccessToken;
            if (string.IsNullOrEmpty(currentUUID))
            {
                Guid randomUuid = Guid.NewGuid();

                string hexUuid = randomUuid.ToString("N");

                Console.WriteLine($"New uuid created: {hexUuid}");

                MinecraftOptions.Default.Uuid = hexUuid;
                MinecraftOptions.Default.Save();

            }

        }
        public void generateCrackedClientToken()
        {
            /*
             Check if uuid in file MinecraftUser.settings is empty generated new uuid
             */
            string currentUUID = MinecraftOptions.Default.ClientToken;
            if (string.IsNullOrEmpty(currentUUID))
            {
                Guid randomUuid = Guid.NewGuid();

                string hexUuid = randomUuid.ToString("N");

                Console.WriteLine($"New uuid created: {hexUuid}");

                MinecraftOptions.Default.Uuid = hexUuid;
                MinecraftOptions.Default.Save();

            }

        }
    }
    public class GetServerInfo
    {
        public void SetPropInfo(
            Label PropServerNameLabel,
            Label PropDescriptionLabel,
            Label PropMVersionLabel,
            Label PropMModLoaderLabel,
            Label PropodPackSizeLabel,
            Label PropModPackVersionLabel
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
                PropMVersionLabel.Text = serverinfo.MVersion;
                PropMModLoaderLabel.Text = serverinfo.MModLoader;
                PropodPackSizeLabel.Text = serverinfo.ModPackSize;
                PropModPackVersionLabel.Text = serverinfo.ModPackVersion;

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
        public void StartSetServersPanels(Label MainServerName1, Label MainDescription1, PictureBox MainImage1)
        {
            SetServerIndex(MainServerName1, MainDescription1, MainImage1);
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

        private async Task SetServersPanels(Label MainServerName1, Label MainDescription1, PictureBox MainImage1)
        {

            var responseMethods = new ResponseMethods();


            foreach (PropertyInfo property in ServersList.Default.GetType().GetProperties())
            {
                // Получаем значение свойства из ServersList.Default
                object value = property.GetValue(ServersList.Default);

                string strValue = value?.ToString() ?? "null";

                // Условие: имя свойства содержит "Server", значение не пустое и не является типом класса
                if (property.Name.Contains("Server1") &&
                    strValue != "DristLauncher_4.ServersList" &&
                    !string.IsNullOrEmpty(strValue))
                {
                    string url = ServersUrlBuilder(strValue);
                    Console.WriteLine($"{property.Name}: {url}");

                    try
                    {
                        HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url);
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        File.WriteAllText("data.json", jsonResponse);

                        ServerInfo serverinfo = JsonConvert.DeserializeObject<ServerInfo>(jsonResponse);

                        if (serverinfo == null)
                        {
                            MessageBox.Show("Ошибка: Не удалось десериализовать JSON или список серверов пуст.");
                            return;
                        }
                        if (LauncherSettings.Default.DebugMode == true)
                        {
                            MessageBox.Show($"ServerName: {serverinfo.ServerName}\n" +
                                            $"Description: {serverinfo.Description}\n" +
                                            $"Image: {serverinfo.Image}\n" +
                                            $"MVersion: {serverinfo.MVersion}\n" +
                                            $"MModLoader: {serverinfo.MModLoader}\n" +
                                            $"MModLoaderVersion: {serverinfo.MModLoaderVersion}\n" +
                                            $"ModPackSize: {serverinfo.ModPackSize}\n" +
                                            $"ModPackVersion: {serverinfo.ModPackVersion}\n" +
                                            $"ModPackFiles: {serverinfo.ModPackFiles}");
                        }

                        MinecraftOptions.Default.MVersion = serverinfo.MVersion;
                        MinecraftOptions.Default.MModLoaderVersion = serverinfo.MModLoaderVersion;
                        MinecraftOptions.Default.MModLoader = serverinfo.MModLoader;
                        MinecraftOptions.Default.Path = serverinfo.ServerName;
                        MinecraftOptions.Default.Save();

                        MainServerName1.Text = serverinfo.ServerName;
                        MainDescription1.Text = serverinfo.Description;
                        MainImage1.LoadCompleted += (s, e) =>
                        {
                            if (LauncherSettings.Default.DebugMode == true)
                            {
                                MessageBox.Show("Картинка загружена успешно.");
                            }
                        };

                        MainImage1.LoadAsync(serverinfo.Image);


                    }
                    catch (JsonException e)
                    {
                        MessageBox.Show($"Ошибка JSON при загрузке серверов: {e.Message}");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Неизвестная ошибка при загрузке серверов {url}: {e.Message}");
                    }
                }
            }



        }
        private async Task SetServerIndex(Label MainServerName1, Label MainDescription1, PictureBox MainImage1)
        {
            string url = ServerIndexUrlBuilder(serversUrls.Default.ServersListFile);
            var responseMethods = new ResponseMethods();

            try
            {
                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                File.WriteAllText("serverList.json", jsonResponse);

                ServerIndexes servers = JsonConvert.DeserializeObject<ServerIndexes>(jsonResponse);

                if (servers == null)
                {
                    MessageBox.Show("Ошибка: Не удалось десериализовать JSON или список серверов пуст.");
                    return;
                }

                ServersList.Default.Server1 = servers.Server1 ?? string.Empty;
                ServersList.Default.Server2 = servers.Server2 ?? string.Empty;
                ServersList.Default.Server3 = servers.Server3 ?? string.Empty;
                ServersList.Default.Server4 = servers.Server4 ?? string.Empty;

                ServersList.Default.Save();

                if (LauncherSettings.Default.DebugMode == true)
                {
                    MessageBox.Show($"Успешно загружены сервера:\n" +
                               $"Server1: {ServersList.Default.Server1}\n" +
                               $"Server2: {ServersList.Default.Server2}\n" +
                               $"Server3: {ServersList.Default.Server3}\n" +
                               $"Server4: {ServersList.Default.Server4}");
                }
                await SetServersPanels(MainServerName1, MainDescription1, MainImage1);
            }
            catch (JsonException e)
            {
                MessageBox.Show($"Ошибка JSON при загрузке серверов: {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Неизвестная ошибка при загрузке серверов {url}: {e.Message}");
            }
        }


        private async Task GetGithubInfo(Label MainServerName1, Label MainDescription1, PictureBox MainImage1)
        {
            string url = serversUrls.Default.GithubUrl;
            var responseMethods = new ResponseMethods();

            try
            {
                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                //MessageBox.Show(jsonResponse); // Можно закомментировать после отладки
                File.WriteAllText("githubIps.json", jsonResponse);

                // Десериализуем JSON в объект GithubInfo (а не Dictionary)
                GithubInfo info = JsonConvert.DeserializeObject<GithubInfo>(jsonResponse);

                if (info == null)
                {
                    MessageBox.Show("Ошибка: Не удалось десериализовать JSON или полученные данные пусты.");
                    return;
                }

                // Используем данные из десериализованного объекта
                serversUrls.Default.ServerIp = info.DefaultIp;
                serversUrls.Default.ServerPort = info.DefaultPort;
                serversUrls.Default.ServerProtocol = info.TypeOfProtocol;
                serversUrls.Default.ServersListFile = info.ServersListFile;
                serversUrls.Default.Save();
                await SetServerIndex(MainServerName1, MainDescription1, MainImage1);

                if (LauncherSettings.Default.DebugMode == true)
                {
                    MessageBox.Show("Данные успешно получены и сохранены.");
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

            public async Task<HttpResponseMessage> ResponseUrlAsync(string url)
            {
                if (LauncherSettings.Default.DebugMode == true)
                {
                    MessageBox.Show(url);
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
