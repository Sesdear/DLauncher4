using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DristLauncher4
{
    
    public class SupportsMethods
    {
        

        public async Task<bool> serverFilesDownload(string serverName)
        {
            string manifestUrl = $"{IPCONFIGs.Default.file_server_url}/{serverName}/{IPCONFIGs.Default.manifest_file_name}";
            var responseMethods = new ResponseMethods();
            string downloadDir = Path.Combine(serverName);

            try
            {
                
                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(manifestUrl);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Manifest data = JsonSerializer.Deserialize<Manifest>(jsonResponse);

                
                string jsonString = File.ReadAllText("servers_list.json");
                var servers = JsonSerializer.Deserialize<Dictionary<string, ServerInfo>>(jsonString);

                if (!servers.TryGetValue(serverName, out ServerInfo serverInfo))
                {
                    MessageBox.Show($"Сервер '{serverName}' не найден в servers_list.json.");
                    return false;
                }

                string filesUrl = serverInfo.FilesUrl;
                using HttpClient client = new HttpClient();

                
                foreach (var folder in data.folders)
                {
                    string folderPath = Path.Combine(downloadDir, folder);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                }

                
                foreach (var file in data.Files)
                {
                    string fileUrl = $"{filesUrl}/{file}";
                    string filePath = Path.Combine(downloadDir, file);

                    HttpResponseMessage fileResponse = await client.GetAsync(fileUrl);
                    if (!fileResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Ошибка загрузки {file}: {fileResponse.StatusCode}");
                        return false;
                    }

                    byte[] fileBytes = await fileResponse.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(filePath, fileBytes);

                    
                    if (data.file_hashes.TryGetValue(file, out string expectedHash))
                    {
                        string actualHash = ComputeSHA256(fileBytes);
                        if (!actualHash.Equals(expectedHash, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show($"Файл {file} повреждён: хеш не совпадает!");
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (JsonException e)
            {
                MessageBox.Show($"Ошибка JSON 'serverFilesDownload': {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Неизвестная ошибка 'serverFilesDownload': {e.Message}");
            }

            return false;
        }

    // Функция для вычисления SHA-256
    private static string ComputeSHA256(byte[] fileBytes)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(fileBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    public void setCurrentOptions(TextBox JavaTextBox, NumericUpDown NumericRamUpDown, TextBox usernameTextBox)
        {
            string nickname = MinecraftUser.Default.username;
            int ram = MinecraftStartParametrs.Default.XmxGb;
            string java_path = MinecraftStartParametrs.Default.JavaPath;

            
            JavaTextBox.Text = java_path;
            NumericRamUpDown.Value = ram;
            usernameTextBox.Text = nickname;
        }
        public async Task getFileServerUrl(CheckedListBox serverCheckedListBox)
        {
            string url = IPCONFIGs.Default.url_to_build;
            var responseMethods = new ResponseMethods();

            try
            {
                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url);

                string jsonResponse = await response.Content.ReadAsStringAsync();


                JsonPortIpD data = JsonSerializer.Deserialize<JsonPortIpD>(jsonResponse);


                JsonDocument doc = JsonDocument.Parse(jsonResponse);
                string server_ip = doc.RootElement.GetProperty("defaultIp").GetString();
                int port = doc.RootElement.GetProperty("defaultPort").GetInt32();
                string type_protocol = doc.RootElement.GetProperty("type").GetString();
                string server_list_dir = doc.RootElement.GetProperty("serverListDir").GetString();
                string server_list_file = doc.RootElement.GetProperty("serverListFile").GetString();
                string manifest_list_dir = doc.RootElement.GetProperty("manifestListDir").GetString();
                string manifest_list_file = doc.RootElement.GetProperty("manifestListFile").GetString();

                string FileServerUrl = $"{type_protocol}://{server_ip}:{port}";
                string ServerListUrl = $"{FileServerUrl}/{server_list_dir}/{server_list_file}";
                string ManifestServerUrl = $"{FileServerUrl}/{manifest_list_dir}";

                IPCONFIGs.Default.manifest_server_url = ManifestServerUrl;
                IPCONFIGs.Default.manifest_file_name = manifest_list_file;
                IPCONFIGs.Default.servers_info_url = ServerListUrl;
                IPCONFIGs.Default.file_server_url = FileServerUrl;
                IPCONFIGs.Default.Save();

                GetServerInfoJsonAndSetCheckedListBox(serverCheckedListBox);


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
        private async Task GetServerInfoJsonAndSetCheckedListBox(CheckedListBox serverCheckedListBox)
        {
            string url = IPCONFIGs.Default.servers_info_url;
            var responseMethods = new ResponseMethods();

            try
            {

                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url);

                string jsonResponse = await response.Content.ReadAsStringAsync();
                MessageBox.Show(jsonResponse);

                File.WriteAllText("servers_list.json", jsonResponse);

                Dictionary<string, ServerInfo> servers = JsonSerializer.Deserialize<Dictionary<string, ServerInfo>>(jsonResponse);


                if (servers == null || servers.Count == 0)
                {
                    MessageBox.Show("Ошибка: Не удалось десериализовать JSON или список серверов пуст.");
                    return;
                }


                foreach (var server in servers)
                {
                    string serverName = server.Key;
                    serverCheckedListBox.Items.Add(serverName);

                }
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
    }
    public class ResponseMethods
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<HttpResponseMessage> ResponseUrlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (HttpRequestException httpEx)
            {

                MessageBox.Show($"HTTP Request Error: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"General Error: {ex.Message}");
                throw;
            }
        }
    }
}
