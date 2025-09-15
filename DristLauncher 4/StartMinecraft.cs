using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Installer.Forge;
using CmlLib.Core.Installers;
using CmlLib.Core.ProcessBuilder;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DristLauncher_4
{
    internal class StartMinecraft
    {
        public async Task Start(DebugForm debugForm, Guna.UI2.WinForms.Guna2Button button)
        {
            ModsFunctions modsFunctions = new ModsFunctions();
            FilesFunctions filesFunctions = new FilesFunctions();
            using (var progressForm = new ProgressForm())
            {
                await modsFunctions.StartModsChecker(debugForm, progressForm, button);
                await filesFunctions.StartFilesChecker(progressForm, debugForm, button);

            }

        }
        public async Task FStartMinecraft(ProgressForm progressForm, DebugForm debugForm, Guna.UI2.WinForms.Guna2Button button)
        {
            progressForm.Log("Запуск майнкрафта.......");
            var mcPath = new MinecraftPath($"./{MinecraftOptions.Default.SelectedServer}");
            var launcher = new MinecraftLauncher(mcPath);

            // оффлайн-сессия (валидные UUID и AccessToken будут сгенерированы автоматически)
            var session = MSession.CreateOfflineSession(MinecraftOptions.Default.Nickname);

            var launchOption = new MLaunchOption
            {
                Session = session,

                JavaPath = MinecraftOptions.Default.JavaPath,
                MaximumRamMb = MinecraftOptions.Default.MaximumRamMb,
                MinimumRamMb = MinecraftOptions.Default.MinimumRamMb,

                ScreenWidth = 1600,
                ScreenHeight = 900,
                FullScreen = MinecraftOptions.Default.FullScreen,

                VersionType = MinecraftOptions.Default.VersionType,
                GameLauncherName = MinecraftOptions.Default.GameLauncherName,
                GameLauncherVersion = MinecraftOptions.Default.GameLauncherVersion
            };
            if (string.IsNullOrEmpty(launchOption.Session?.AccessToken) ||
                string.IsNullOrEmpty(launchOption.Session?.UUID) ||
                string.IsNullOrEmpty(launchOption.Session?.Username))
            {
                if (debugForm != null)
                {
                    debugForm.Log($"Сессия невалидна! Проверь данные (AccessToken: {launchOption.Session?.AccessToken}, UUID: {launchOption.Session?.UUID}, Username: {launchOption.Session?.Username})");
                }

                progressForm.Log($"Сессия невалидна! Проверь данные (AccessToken: {launchOption.Session?.AccessToken}, UUID: {launchOption.Session?.UUID}, Username: {launchOption.Session?.Username})");
                button.Enabled = true;
                return;
            }
            if (debugForm != null)
            {

                debugForm.Log($"JavaPath {launchOption.JavaPath}\nMaximumRamMb {launchOption.MaximumRamMb}\nMinimumRamMb {launchOption.MinimumRamMb}\nVersionType {launchOption.VersionType}\nGameLauncherVersion {launchOption.GameLauncherVersion}");

            }
            bool isInstalled = await CheckForInstall(launchOption, progressForm, debugForm, mcPath);

            if (!isInstalled)
            {
                await DownloadAndStart(launchOption, launcher, progressForm, debugForm, button);
            }
            else
            {
                await StartMinecraftWithForge(launchOption, launcher, progressForm, debugForm, mcPath, button);
            }



        }


        public async Task<bool> CheckForInstall(
    MLaunchOption mLaunchOption,
    ProgressForm progressForm,
    DebugForm debugForm,
    MinecraftPath minecraftPath)
        {
            // проверяем базовые папки
            if (!Directory.Exists(minecraftPath.Versions) ||
                !Directory.Exists(minecraftPath.Library) ||
                !Directory.Exists(minecraftPath.Assets) ||
                !Directory.Exists(minecraftPath.Resource) ||
                !Directory.Exists(minecraftPath.Runtime))
            {
                return false;
            }
            if (!File.Exists($"./minecraft/versions/{MinecraftOptions.Default.MVersion}-{MinecraftOptions.Default.MModLoader}-{MinecraftOptions.Default.MModLoaderVersion}/{MinecraftOptions.Default.MVersion}-{MinecraftOptions.Default.MModLoader}-{MinecraftOptions.Default.MModLoaderVersion}.jar"))
            {
                return false;
            }
            progressForm.Log($"Minecraft {MinecraftOptions.Default.MVersion} найден и готов к запуску.");
            return true;
        }

        public async Task StartMinecraftWithForge(
    MLaunchOption mLaunchOption,
    MinecraftLauncher minecraftLauncher,
    ProgressForm progressForm,
    DebugForm debugForm,
    MinecraftPath minecraftPath,
    Guna.UI2.WinForms.Guna2Button button)
        {
            try
            {
                // формируем имя forge-версии
                var forgeVersionId = $"{MinecraftOptions.Default.MVersion}-forge-{MinecraftOptions.Default.MModLoaderVersion}";

                // грузим список версий напрямую из лаунчера
                var versions = await minecraftLauncher.GetAllVersionsAsync();

                // проверяем, установлен ли Forge
                if (!versions.Any(v => v.Name == forgeVersionId))
                {
                    MessageBox.Show($"Forge {forgeVersionId} не установлен!");
                    button.Enabled = true;
                    return;
                }

                progressForm.Log($"Запуск Minecraft {forgeVersionId}...");

                // создаём процесс
                var process = await minecraftLauncher.CreateProcessAsync(forgeVersionId, mLaunchOption);

                if (process == null)
                {
                    MessageBox.Show("Не удалось собрать процесс запуска!");
                    button.Enabled = true;
                    return;
                }

                // подключаем обработчики вывода
                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        debugForm?.Log("[OUT] " + e.Data);
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        debugForm?.Log("[ERR] " + e.Data);
                };

                // запускаем и включаем асинхронное чтение вывода
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                button.Enabled = true;

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запуска Minecraft с Forge: " + ex.Message);
                button.Enabled = true;
                return;
            }
        }



        public async Task DownloadAndStart(
            MLaunchOption mLaunchOption,
            MinecraftLauncher minecraftLauncher,
            ProgressForm progressForm, DebugForm debugForm, Guna.UI2.WinForms.Guna2Button button)
        {


            // прогресс в байтах
            var fileProgress = new Progress<InstallerProgressChangedEventArgs>(e =>
            {
                progressForm.Log($"[{e.EventType}] {e.Name} ({e.ProgressedTasks}/{e.TotalTasks})");
                progressForm.SetProgress(e.ProgressedTasks, e.TotalTasks);
            });

            var byteProgress = new Progress<ByteProgress>(e =>
            {
                int percent = (int)(e.ToRatio() * 100);
                progressForm.Log($"Скачано {e.ProgressedBytes / 1024 / 1024} МБ из {e.TotalBytes / 1024 / 1024} МБ ({percent}%)");
            });

            var forgeInstaller = new ForgeInstaller(minecraftLauncher);

            try
            {
                string forgeVersionName = null;

                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        forgeVersionName = await forgeInstaller.Install(
                            MinecraftOptions.Default.MVersion,
                            MinecraftOptions.Default.MModLoaderVersion,
                            new ForgeInstallOptions { ByteProgress = byteProgress, FileProgress = fileProgress });

                        if (debugForm != null)
                        {
                            debugForm.Log("Forge installed: " + forgeVersionName);
                        }
                        progressForm.Log("Forge installed: " + forgeVersionName);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (debugForm != null)
                        {
                            debugForm.Log($"Ошибка установки Forge (попытка {i + 1}): {ex.Message}");
                        }
                        progressForm.Log($"Ошибка установки Forge (попытка {i + 1}): {ex.Message}");
                        if (i == 2) throw; // после 3 попыток окончательно падаем
                    }
                }

                if (forgeVersionName == null)
                {
                    MessageBox.Show("Forge так и не удалось установить!");
                    button.Enabled = true;
                    return;
                }

                var process = await minecraftLauncher.CreateProcessAsync(forgeVersionName, mLaunchOption);
                if (process == null)
                {

                    MessageBox.Show("Не удалось собрать процесс запуска!");
                    button.Enabled = true;

                    return;
                }

                process.Start();
                button.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка установки/запуска Forge: " + ex);
                button.Enabled = true;
            }





        }
    }
}
