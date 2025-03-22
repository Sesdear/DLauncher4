using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CmlLib.Core;

using CmlLib.Core.Auth;
using CmlLib.Core.Installer.Forge.Versions;
using CmlLib.Core.Installer.Forge;
using CmlLib.Core.ModLoaders;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core.Installers;
using CmlLib.Core.ModLoaders.FabricMC;
using CmlLib.Core.Version;
using CmlLib.Core.ModLoaders.QuiltMC;
using System.Diagnostics;
using System.Threading;
using static System.Windows.Forms.Design.AxImporter;


namespace DristLauncher4
{
    public class MinecraftMethods
    {
        private MLaunchOption BuildMLOption()
        {
            MinecraftUser userInfo = new MinecraftUser();
            MinecraftStartParametrs minecraftStartParametrs = new MinecraftStartParametrs();
            var launchOption = new MLaunchOption
            {
                Session = MSession.CreateOfflineSession(userInfo.username),


                JavaPath = minecraftStartParametrs.JavaPath,
                MaximumRamMb = minecraftStartParametrs.XmxGb * 1024,
                MinimumRamMb = minecraftStartParametrs.XmsGb * 1024,

                IsDemo = minecraftStartParametrs.IsDemo,
                ScreenWidth = 1600,
                ScreenHeight = 900,
                FullScreen = minecraftStartParametrs.FullScreen,
                /*
                QuickPlayPath = "/path/quickplay",
                QuickPlaySingleplayer = "world name",
                QuickPlayRealms = "realm id",
                ServerIp = "mc.hypixel.net",
                ServerPort = 25565,
                */


                VersionType = minecraftStartParametrs.VersionType,
                GameLauncherName = minecraftStartParametrs.GameLauncherName,
                GameLauncherVersion = minecraftStartParametrs.GameLaucherVersion,


            };
            return launchOption;
        }
        private MinecraftLauncher BuildMLauncher(string path)
        {
            var launcher = new MinecraftLauncher(path);
            return launcher;
        }
        public void generateCrackedUUID()
        {
            /*
             Check if uuid in file MinecraftUser.settings is empty generated new uuid
             */
            string currentUUID = MinecraftUser.Default.uuid;
            if (string.IsNullOrEmpty(currentUUID))
            {
                Guid randomUuid = Guid.NewGuid();

                string hexUuid = randomUuid.ToString("N");

                Console.WriteLine($"New uuid created: {hexUuid}");

                MinecraftUser.Default.uuid = hexUuid;
                MinecraftUser.Default.Save();

            }

        }
        public void saveNickname(string nickname)
        {

            MinecraftUser.Default.username = nickname;
            MinecraftUser.Default.Save();


        }
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

        
        public async void InstallAndLaunchMinecraft(string serverName, string version, ProgressBar progressBar, Button startButton, Label progressLable, string modloader = "vanilla", string modloader_version = "")
        {
            Download download = new Download();
            Launch launch = new Launch();
            string mcpath = $"./{serverName}";
            var launcher = BuildMLauncher(mcpath);
            var options = BuildMLOption();
            startButton.Enabled = false;
            


            if (string.IsNullOrEmpty(version))
            {
                Console.WriteLine("Error: Minecraft version cannot be null or empty.");

            }
            else
            { 


                switch (modloader.ToLower())
                {
                    case "vanilla":

                        
                        bool vResult = await download.VanillaAsync(mcpath, version, launcher, progressBar, progressLable);
                        startButton.Enabled = true;
                        if (vResult)
                        {
                            launch.StartMinecraftVanila(version ,launcher, options);
                            
                        }

                        break;

                    case "forge":
                        if (string.IsNullOrEmpty(modloader_version))
                        {
                            MessageBox.Show(
            "Mod Loader Error",
            "Forge version must be specified.",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
                            
                        }

                        bool vResult2 = await download.ForgeAsync(mcpath, version, modloader_version, launcher, progressBar, progressLable, options);
                        startButton.Enabled = true;
                        // Call start method contains in ForgeAsync
                        break;


                    case "fabric":
                        if (string.IsNullOrEmpty(modloader_version))
                        {
                            MessageBox.Show(
            "Mod Loader Error",
            "Fabric version must be specified.",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
                            
                        }
                        MessageBox.Show(
            "Info",
            "Fabric installer in progress.",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

                        startButton.Enabled = true;
                        break;


                    case "quilt":
                        if (string.IsNullOrEmpty(modloader_version))
                        {
                            MessageBox.Show(
            "Mod Loader Error",
            "Quilt version must be specified.",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
                            
                        }
                        MessageBox.Show(
            "Info",
            "Quilt installer in progress.",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

                        startButton.Enabled = true;
                        break;


                    default:
                        Console.WriteLine($"Error: Unsupported modloader '{modloader}'.");
                        startButton.Enabled = true;
                        break;
                }
            }
        }
    }

    public class Download
    {

        private CancellationTokenSource cancellationToken;
        private ByteProgress byteProgress;

        private void Launcher_ProgressChanged(ByteProgress e)
        {
            byteProgress = e;
        }

        public async Task<bool> VanillaAsync(string path,
            string version,
            MinecraftLauncher launcher,
            ProgressBar progressBar,
            Label progressLabel)



        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(version))
                return false;

            cancellationToken = new CancellationTokenSource();

            var mcPath = new MinecraftPath(path);
            var byteProgress = new SyncProgress<ByteProgress>(Launcher_ProgressChanged);

            launcher.ByteProgressChanged += (sender, args) =>
            {
                int downloaded = (int)(args.ProgressedBytes / 1024 / 1024); // В МБ
                int total = (int)(args.TotalBytes / 1024 / 1024); // В МБ

                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke(new Action(() =>
                    {
                        progressBar.Maximum = total > 0 ? total : 1;
                        progressBar.Value = downloaded;
                        progressLabel.Text = $"{downloaded} MB / {total} MB";
                        progressBar.Update();
                    }));
                }
                else
                {
                    progressBar.Maximum = total > 0 ? total : 1;
                    progressBar.Value = downloaded;
                    progressLabel.Text = $"{downloaded} MB / {total} MB";
                    progressBar.Update();
                }
            };

            await launcher.InstallAsync(version);

            return true;
        }

        public async Task<bool> ForgeAsync(string path,
            string version,
            string modloaderVersion,
            MinecraftLauncher launcher,
            ProgressBar progressBar,
            Label progressLabel,
            MLaunchOption options)



        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(version) || string.IsNullOrEmpty(modloaderVersion))
                return false;

            var mcPath = new MinecraftPath(path);
            
            var byteProgress = new SyncProgress<ByteProgress>(Launcher_ProgressChanged);
            var forgeInstaller = new ForgeInstaller(launcher);
            var launch = new Launch();

            launcher.ByteProgressChanged += (sender, args) =>
            {
                int downloaded = (int)(args.ProgressedBytes / 1024 / 1024); // В МБ
                int total = (int)(args.TotalBytes / 1024 / 1024); // В МБ

                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke(new Action(() =>
                    {
                        progressBar.Maximum = total > 0 ? total : 1;
                        progressBar.Value = downloaded;
                        progressLabel.Text = $"{downloaded} MB / {total} MB";
                        progressBar.Update();
                    }));
                }
                else
                {
                    progressBar.Maximum = total > 0 ? total : 1;
                    progressBar.Value = downloaded;
                    progressLabel.Text = $"{downloaded} MB / {total} MB";
                    progressBar.Update();
                }
            };

            var version_name = await forgeInstaller.Install(version, modloaderVersion, new ForgeInstallOptions { ByteProgress = byteProgress });
            launch.StartMinecraftForge(version_name, launcher, options);

            return true;
        }

        

        public async Task<bool> QuiltAsync(string path, string version, string modloaderVersion, MinecraftLauncher launcher)
        {
            
            return true;
        }
    }

    public class Launch
    {
        
        public async void StartMinecraftVanila(string version ,MinecraftLauncher launcher, MLaunchOption options)
        {
            var process = await launcher.BuildProcessAsync(version, options);
            process.Start();
        }
        public async void StartMinecraftForge(string version, MinecraftLauncher launcher, MLaunchOption options)
        {
            
            try
            {
                
                var process = await launcher.BuildProcessAsync(version, options);
                process.Start();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }
        }
    }




}
