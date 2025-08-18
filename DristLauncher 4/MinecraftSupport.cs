using CmlLib.Core;
using CmlLib.Core.Installer.Forge;
using CmlLib.Core.Installer.Forge.Versions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
///////////////////////////////////////////////////////////////////////////
///
///              Сделать скачивание forge и майна
///              Cделать запуск игры
///
///////////////////////////////////////////////////////////////////////////
namespace DristLauncher_4
{
    public class MinecraftSupport
    {
        public async Task DownloadForgeMinecraft()
        {
            var versionLoader = new ForgeVersionLoader(new HttpClient());
            var versions = await versionLoader.GetForgeVersions(MinecraftOptions.Default.MVersion);
            var launcher = new MinecraftLauncher(MinecraftOptions.Default.Path);

            var forge = new ForgeInstaller(launcher);
            await forge.Install(MinecraftOptions.Default.MVersion, MinecraftOptions.Default.MModLoaderVersion);
        }
    }
    public class ManifestBuilder
    {
        public List<string> GetModsList(string modsPath)
        {
            List<string> modList = new List<string>();

            if (!Directory.Exists(modsPath))
            {
                Console.WriteLine("Папка модов не найдена.");
                return modList;
            }

            foreach (string filePath in Directory.GetFiles(modsPath))
            {
                modList.Add(Path.GetFileName(filePath));
            }

            return modList;
        }
        public List<string> GetFilesList(string filesPath)
        {
            List<string> modList = new List<string>();

            if (!Directory.Exists(filesPath))
            {
                Console.WriteLine("Папка модов не найдена.");
                return modList;
            }

            foreach (string filePath in Directory.GetFiles(filesPath))
            {
                modList.Add(Path.GetFileName(filePath));
            }

            return modList;
        }
    }
    public class FileDownloader
    {
        public static async Task DownloadFileAsync(string url, string filePath)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();

                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                            {
                                await stream.CopyToAsync(fileStream);
                            }
                        }
                    }
                    Console.WriteLine($"Файл успешно загружен: {filePath}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Ошибка HTTP запроса: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка при скачивании файла: {e.Message}");
                }
            }
        }
    }
    public class ModsFunctions
    {
        private string ModManifestUrlBuilder()
        {
            string url = $"http://{serversUrls.Default.ServerIp}:{serversUrls.Default.ServerPort}/DristPunk4/modsManifest.json";
            return url;
        }

        private string ModPathUrl(string file)
        {
            string url = $"http://{serversUrls.Default.ServerIp}:{serversUrls.Default.ServerPort}/DristPunk4/minecraft/mods/{file}";
            return url;
        }
        private async Task DownloadMod(string url, string pathToSave)
        {
            await FileDownloader.DownloadFileAsync(url, pathToSave);
        }



        public async Task StartModsChecker()
        {
            using (var progressForm = new ProgressForm())
            {
                progressForm.Show();
                progressForm.Log("▶ Начинаем проверку модов...");

                ResponseMethods responseMethods = new ResponseMethods();
                ManifestBuilder manifestBuilder = new ManifestBuilder();
                ChecksForValid checksForValid = new ChecksForValid();
                FilesFunctions filesFunctions = new FilesFunctions();

                string url = ModManifestUrlBuilder();
                string modsPath = checksForValid.CheckExistsDir("minecraft/mods");

                List<string> clientMods = manifestBuilder.GetModsList(modsPath);

                try
                {
                    HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url);
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    ModsManifest serverInfo = JsonConvert.DeserializeObject<ModsManifest>(jsonResponse);
                    if (serverInfo?.mods == null)
                    {
                        progressForm.Log("❌ Ошибка: список модов пуст.");
                        return;
                    }

                    // какие моды удалить и скачать
                    var modsToDelete = clientMods.Except(serverInfo.mods).ToList();
                    var modsToDownload = serverInfo.mods.Except(clientMods).ToList();

                    int totalSteps = modsToDelete.Count + modsToDownload.Count;
                    int currentStep = 0;

                    foreach (string mod in modsToDelete)
                    {
                        string modPath = Path.Combine(modsPath, mod);
                        if (File.Exists(modPath))
                        {
                            File.Delete(modPath);
                            progressForm.Log($"🗑 Удалён мод: {mod}");
                        }
                        currentStep++;
                        progressForm.SetProgress(currentStep, totalSteps);
                    }

                    foreach (string mod in modsToDownload)
                    {
                        string modPathOnServer = ModPathUrl(mod);
                        string modPathClient = Path.Combine(modsPath, mod);
                        await DownloadMod(modPathOnServer, modPathClient);

                        progressForm.Log($"⬇ Загружен мод: {mod}");
                        currentStep++;
                        progressForm.SetProgress(currentStep, totalSteps);
                    }

                    progressForm.Log("✅ Синхронизация модов завершена.");

                    // запускаем проверку файлов и передаем туда progressForm
                    await filesFunctions.StartFilesChecker(progressForm);
                }
                catch (Exception e)
                {
                    progressForm.Log($"❌ Ошибка при загрузке модов: {e.Message}");
                }
            }
        }


    }

    public class FilesFunctions
    {
        private string FilesManifestUrlBuilder()
        {
            string url = $"http://{serversUrls.Default.ServerIp}:{serversUrls.Default.ServerPort}/DristPunk4/filesManifest.json";
            return url;
        }

        private string FilePathUrl(string relativePath)
        {
            string url = $"http://{serversUrls.Default.ServerIp}:{serversUrls.Default.ServerPort}/{relativePath.Replace("\\", "/")}";
            return url;
        }

        private async Task DownloadFile(string url, string pathToSave)
        {
            await FileDownloader.DownloadFileAsync(url, pathToSave);
        }

        public async Task StartFilesChecker(ProgressForm progressForm)
        {
            ResponseMethods responseMethods = new ResponseMethods();
            ChecksForValid checksForValid = new ChecksForValid();

            string basePath = checksForValid.CheckExistsDir("./");
            string url = FilesManifestUrlBuilder();

            progressForm.Log("▶ Начинаем проверку файлов...");
            progressForm.Log($"URL: {url}");

            try
            {
                HttpResponseMessage response = await responseMethods.ResponseUrlAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                FilesManifest serverManifest = JsonConvert.DeserializeObject<FilesManifest>(jsonResponse);
                if (serverManifest?.files_hashes == null)
                {
                    progressForm.Log("❌ Ошибка: список файлов пуст.");
                    return;
                }

                var serverFiles = new Dictionary<string, string>();
                foreach (var line in serverManifest.files_hashes)
                {
                    var parts = line.Split(new string[] { " : " }, StringSplitOptions.None);
                    if (parts.Length == 2)
                        serverFiles[parts[0]] = parts[1];
                }

                List<string> localFiles = Directory.GetFiles(basePath, "*", SearchOption.AllDirectories)
                    .Select(f => f.Substring(basePath.Length + 1).Replace('\\', '/'))
                    .ToList();

                int totalSteps = serverFiles.Count + localFiles.Count;
                int currentStep = 0;

                // удаление лишних
                foreach (var file in localFiles.Except(serverFiles.Keys).ToList())
                {
                    string fullPath = Path.Combine(basePath, file.Replace('/', Path.DirectorySeparatorChar));
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        progressForm.Log($"🗑 Удалён файл: {file}");
                    }
                    currentStep++;
                    progressForm.SetProgress(currentStep, totalSteps);
                }

                // загрузка новых/обновленных
                foreach (var kvp in serverFiles)
                {
                    string relativePath = kvp.Key;
                    string serverHash = kvp.Value;

                    const string prefixToRemove = "DristPunk4/";
                    if (relativePath.StartsWith(prefixToRemove))
                        relativePath = relativePath.Substring(prefixToRemove.Length);

                    string fullPath = Path.Combine(basePath, relativePath.Replace('/', Path.DirectorySeparatorChar));
                    bool needDownload = true;

                    if (File.Exists(fullPath))
                    {
                        string localHash = FilesManifestGenerator.ComputeSHA256(fullPath);
                        if (localHash == serverHash)
                            needDownload = false;
                    }

                    if (needDownload)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                        string fileUrl = FilePathUrl(kvp.Key);

                        await DownloadFile(fileUrl, fullPath);
                        progressForm.Log($"⬇ Загружен файл: {relativePath}");
                    }

                    currentStep++;
                    progressForm.SetProgress(currentStep, totalSteps);
                }

                progressForm.Log("✅ Синхронизация файлов завершена.");
                
                var smine = new StartMinecraft();
                await smine.FStartMinecraft(progressForm);
            }
            catch (Exception ex)
            {
                progressForm.Log($"❌ Ошибка при загрузке файлов: {ex.Message}");
            }

            
            

        }

    }

    public class FilesManifestGenerator
    {
        // Список папок, которые исключаем (относительно basePath)
        private static readonly List<string> excludeFolders = new List<string>
    {
        "mods",
        "logs",
        ".git",
        "assets",
        "defaultconfigs",
        "libares",
        "versions",
        "saves",
        "runtime",
        "resourcepacks"
    };

        // Список файлов, которые исключаем по имени
        private static readonly List<string> excludeFiles = new List<string>
    {
        "filesManifest.json",
        "desktop.ini",
        "optinos.txt"
    };

        public static string ComputeSHA256(string filePath)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке {filePath}: {ex.Message}");
                return null;
            }
        }


        public static List<string> GenerateHashes(string basePath)
        {
            var result = new List<string>();

            var files = Directory.GetFiles(basePath, "*", SearchOption.AllDirectories)
                .Where(file =>
                {
                    // Проверка на исключённые папки
                    string relative = GetRelativePath(basePath, file);
                    foreach (var folder in excludeFolders)
                    {
                        if (relative.StartsWith(folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                            return false;
                    }

                    // Проверка на исключённые файлы
                    string fileName = Path.GetFileName(relative);
                    if (excludeFiles.Contains(fileName, StringComparer.OrdinalIgnoreCase))
                        return false;

                    return true;
                });

            foreach (var file in files)
            {
                string relativePath = GetRelativePath(basePath, file).Replace('\\', '/');
                string hash = ComputeSHA256(file);
                if (hash != null)
                {
                    result.Add($"{relativePath} : {hash}");
                }
            }

            return result;
        }

        public void GenerateManifest(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Указанный путь не является папкой.");
                return;
            }

            string outputFile = Path.Combine(folderPath, "filesManifest.json");
            var newHashes = GenerateHashes(folderPath);
            int version = 1;

            if (File.Exists(outputFile))
            {
                try
                {
                    var oldContent = File.ReadAllText(outputFile);
                    var oldManifest = System.Text.Json.JsonSerializer.Deserialize<FilesManifest>(oldContent);
                    if (oldManifest != null && !oldManifest.files_hashes.SequenceEqual(newHashes))
                    {
                        version = oldManifest.version + 1;
                    }
                    else
                    {
                        version = oldManifest?.version ?? 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка чтения {outputFile}: {ex.Message}");
                    Console.WriteLine("Начинаем с версии 1.");
                }
            }
            else
            {
                Console.WriteLine("Файл манифеста не найден. Создаём новый с версией 1.");
            }

            var manifest = new FilesManifest
            {
                version = version,
                files_hashes = newHashes
            };

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            File.WriteAllText(outputFile, System.Text.Json.JsonSerializer.Serialize(manifest, jsonOptions));
            Console.WriteLine($"JSON файл успешно создан/обновлён: {outputFile} (версия: {version})");
        }

        public static string GetRelativePath(string basePath, string fullPath)
        {
            basePath = Path.GetFullPath(basePath);
            fullPath = Path.GetFullPath(fullPath);

            Uri baseUri = new Uri(basePath.EndsWith(Path.DirectorySeparatorChar.ToString())
                ? basePath
                : basePath + Path.DirectorySeparatorChar);

            Uri fullUri = new Uri(fullPath);

            return Uri.UnescapeDataString(
                baseUri.MakeRelativeUri(fullUri)
                       .ToString()
                       .Replace('/', Path.DirectorySeparatorChar)
            );
        }
    }
}
    

