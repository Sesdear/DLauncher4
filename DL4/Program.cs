using System;
using System.Diagnostics;

var psi = new ProcessStartInfo
{
    FileName = "DristLauncher 4.exe",
    UseShellExecute = true,
    Verb = "runas" // запуск от имени администратора
};

try
{
    Process.Start(psi);
}
catch
{
    Console.WriteLine("Пользователь отказался от прав администратора.");
    // Можно добавить задержку, чтобы увидеть сообщение
    Console.ReadKey();
}

// Если хотите, чтобы окно закрылось сразу после успешного запуска,
// просто не делайте Console.ReadLine/ReadKey, программа завершится сама.
