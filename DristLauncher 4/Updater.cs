using AutoUpdaterDotNET;
using System.Windows.Forms;

namespace DristLauncher_4
{
    internal class Updater
    {
        public void StartUpdater(string url)
        {
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
            AutoUpdater.Start("https://raw.githubusercontent.com/Sesdear/DLauncher4/main/update.xml");

        }
        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                DialogResult dialogResult = MessageBox.Show(
                    $"Новая версия {args.CurrentVersion} доступна. Скачать?",
                    "Обновление",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    AutoUpdater.DownloadUpdate(args);
                }
            }
        }
        private void AutoUpdater_ApplicationExitEvent()
        {
            Application.Exit();
        }
    }
}
