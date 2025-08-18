using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Installer.Forge;
using CmlLib.Core.Installers;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core.Version;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DristLauncher_4
{
    internal class StartMinecraft
    {
        public async Task FStartMinecraft(ProgressForm progressForm)
        {
            progressForm.Log("Запуск майнкрафта.......");
            var mcPath = new MinecraftPath("./minecraft");
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
                progressForm.Log($"Сессия невалидна! Проверь данные (AccessToken: {launchOption.Session?.AccessToken}, UUID: {launchOption.Session?.UUID}, Username: {launchOption.Session?.Username})");
                return;
            }

            await DownloadAndStart(launchOption, launcher, progressForm);
        }



        public async Task DownloadAndStart(
            MLaunchOption mLaunchOption,
            MinecraftLauncher minecraftLauncher,
            ProgressForm progressForm)
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
                var forgeVersionName = await forgeInstaller.Install(
                    MinecraftOptions.Default.MVersion,
                    MinecraftOptions.Default.MModLoaderVersion,
                    new ForgeInstallOptions { ByteProgress = byteProgress, FileProgress = fileProgress });

                progressForm.Log("Forge installed: " + forgeVersionName);

                var process = await minecraftLauncher.CreateProcessAsync(forgeVersionName, mLaunchOption);
                if (process == null)
                {
                    progressForm.Log("Не удалось собрать процесс запуска!");
                    return;
                }

                process.Start();
            }
            catch (Exception ex)
            {
                progressForm.Log("Ошибка установки/запуска Forge: " + ex);
            }





        }
    }
}
