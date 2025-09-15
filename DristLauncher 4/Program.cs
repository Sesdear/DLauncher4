using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DristLauncher_4
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                Console.WriteLine($"[Resolve] Пытаемся загрузить: {args.Name}");

                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libs");
                string assemblyName = new AssemblyName(args.Name).Name + ".dll";
                string assemblyPath = Path.Combine(basePath, assemblyName);

                if (File.Exists(assemblyPath))
                {
                    try
                    {
                        Console.WriteLine($"[Resolve] Загружаем из: {assemblyPath}");
                        return Assembly.LoadFrom(assemblyPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Resolve] Ошибка при загрузке {assemblyPath}: {ex}");
                    }
                }

                return null;
            };

            // 🔥 Глобальный перехват всех исключений
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                MessageBox.Show(((Exception)e.ExceptionObject).ToString(), "UnhandledException");
            };

            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show(e.Exception.ToString(), "ThreadException");
            };

            Updater updater = new Updater();
            updater.StartUpdater("https://raw.githubusercontent.com/Sesdear/DLauncher4/refs/heads/main/update.xml");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Updater.StartUpdate();
            Application.Run(new MainWindow());
        }
    }
}
