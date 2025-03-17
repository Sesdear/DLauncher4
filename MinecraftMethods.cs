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
using DLauncher4Rebuild;
System.Net.ServicePointManager.DefaultConnectionLimit = 256;

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

        
        public async void InstallAndLaunchMinecraft(string serverName, string version, string modloader = "vanilla", string modloader_version = "")
        {
            Download download = new Download();
            Launch launch = new Launch();
            string mcpath = $"./{serverName}";
            var launcher = BuildMLauncher(mcpath);
            var options = BuildMLOption();
            
            if (string.IsNullOrEmpty(version))
            {
                Console.WriteLine("Error: Minecraft version cannot be null or empty.");

            }
            else
            { 


                switch (modloader.ToLower())
                {
                    case "vanilla":
                       
                        bool vResult = await download.VanillaAsync(mcpath, version, launcher);
                        if (vResult)
                        {
                            launch.StartMinecraft(launcher, options);
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
                        break;


                    default:
                        Console.WriteLine($"Error: Unsupported modloader '{modloader}'.");
                        break;
                }
            }
        }
    }

    public class Download
    {

        CancellationTokenSource? cancellationToken;


        ByteProgress byteProgress;
        private void Launcher_ProgressChanged(ByteProgress e)
        {
            byteProgress = e;
        }

        public async Task<bool> VanillaAsync(string path, string version, MinecraftLauncher launcher)
        {

            
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(version))
                return false;

            cancellationToken = new CancellationTokenSource();


            var mcPath = new MinecraftPath(path);
            
            var byteProgress = new SyncProgress<ByteProgress>(Launcher_ProgressChanged);

            launcher.ByteProgressChanged += (sender, args) =>
            {
                Console.WriteLine($"{args.ProgressedBytes} bytes / {args.TotalBytes} bytes");
            };


            if (version == null)
                return false;

            await launcher.InstallAsync(version);


            return true;
        }

        public async Task<bool> ForgeAsync(string path, string version, string modloaderVersion, MinecraftLauncher launcher)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(version) || string.IsNullOrEmpty(modloaderVersion))
                return false;

            var mcPath = new MinecraftPath(path);
            
            var byteProgress = new SyncProgress<ByteProgress>(Launcher_ProgressChanged);
            var forgeInstaller = new ForgeInstaller(launcher);
            var stopwatch = new Stopwatch();

            await forgeInstaller.Install(version, modloaderVersion);
            return true;
        }

        public async Task<bool> FabricAsync(string path, string version, string modloaderVersion, MinecraftLauncher launcher)
        {
            
            return true;
        }

        public async Task<bool> QuiltAsync(string path, string version, string modloaderVersion, MinecraftLauncher launcher)
        {
            
            return true;
        }
    }

    public class Launch
    {
        
        public async void StartMinecraft(MinecraftLauncher launcher, MLaunchOption options)
        {
            
            var process = await launcher.BuildProcessAsync("1.20.4", options);
            process.Start();
        }
    }




}
