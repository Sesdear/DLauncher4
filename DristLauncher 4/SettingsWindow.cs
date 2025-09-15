using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace DristLauncher_4
{
    public partial class SettingsWindow : Form
    {
        DebugForm debugForm;
        public SettingsWindow()
        {
            InitializeComponent();
            ApplyTheme();
            /*if (LauncherSettings.Default.DebugMode)
            {
                debugForm = new DebugForm();
                debugForm.Show();
            }
            */
            string javaPath = MinecraftOptions.Default.JavaPath;
            if (javaPath == string.Empty)
            {

                JavaPathTextBox.Text = "C:\\Program Files\\Java\\jdk-17\\bin\\javaw.exe";
                MinecraftOptions.Default.JavaPath = "C:\\Program Files\\Java\\jdk-17\\bin\\javaw.exe";
            }
            else
            {
                JavaPathTextBox.Text = MinecraftOptions.Default.JavaPath;
            }
            int ramGb = MinecraftOptions.Default.MaximumRamMb / 1024;

            // Проверяем диапазон
            if (ramGb < (int)RamNumericUpDown.Minimum)
                ramGb = (int)RamNumericUpDown.Minimum;
            if (ramGb > (int)RamNumericUpDown.Maximum)
                ramGb = (int)RamNumericUpDown.Maximum;

            RamNumericUpDown.Value = ramGb;

            DebugCheckBox.Checked = LauncherSettings.Default.DebugMode;




        }

        private void ApplyTheme()
        {
            // Устанавливаем цвет фона формы
            this.BackColor = ParseColor(LauncherSettings.Default.MainColor);

            // Перебираем все элементы формы рекурсивно
            ApplyColorsToControls(this);
        }

        private void ApplyColorsToControls(Control parent)
        {
            ChooseJavaPathButton.FillColor = ParseColor(LauncherSettings.Default.ButtonsColor);
            FindJavaButton.FillColor = ParseColor(LauncherSettings.Default.ButtonsColor);
            DownloadJavaButton.FillColor = ParseColor(LauncherSettings.Default.ButtonsColor);
            SaveButton.FillColor = ParseColor(LauncherSettings.Default.ButtonsColor);
            RamNumericUpDown.UpDownButtonFillColor = ParseColor(LauncherSettings.Default.ButtonsColor);

        }

        private Color ParseColor(string rgbString)
        {
            // "159,87,39"
            var parts = rgbString.Split(',');
            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int r) &&
                int.TryParse(parts[1], out int g) &&
                int.TryParse(parts[2], out int b))
            {
                return Color.FromArgb(r, g, b);
            }
            return Color.Gray; // fallback
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            MinecraftOptions.Default.JavaPath = JavaPathTextBox.Text;
            MinecraftOptions.Default.MaximumRamMb = Convert.ToInt32(RamNumericUpDown.Value) * 1024;
            LauncherSettings.Default.DebugMode = DebugCheckBox.Checked;

            MinecraftOptions.Default.Save();
            LauncherSettings.Default.Save();


            MessageBox.Show("Settings Save", "Setiings Saved");



        }

        private void FindJavaButton_Click(object sender, EventArgs e)
        {
            SettingsSupport settingsSupport = new SettingsSupport();
            settingsSupport.FindJavaButtonOnPc_Click(sender, e, JavaPathTextBox, debugForm);
        }

        private void ChooseJavaPathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл java.exe";
            openFileDialog.Filter = "Java Executables|java.exe;javaw.exe";
            openFileDialog.InitialDirectory = "C:\\";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string javaPath = openFileDialog.FileName;
                MessageBox.Show("Выбранный путь к Java:\n" + javaPath, "Java Path");

                // Можешь сохранить путь или использовать его дальше
                // Например, записать в TextBox:
                JavaPathTextBox.Text = javaPath;
            }
        }

        private async void DownloadJavaButton_Click(object sender, EventArgs e)
        {
            string url = "https://download.oracle.com/java/17/archive/jdk-17.0.12_windows-x64_bin.exe";
            string filePath = Path.Combine(Path.GetTempPath(), "jdk-17.0.12_windows-x64_bin.exe");

            using (SimpleProgressBar progressForm = new SimpleProgressBar())
            {
                progressForm.SetLabel("Скачивание Java...");
                progressForm.Show();

                try
                {
                    using (HttpClient client = new HttpClient())
                    using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();
                        var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                        var canReportProgress = totalBytes != -1;

                        using (var contentStream = await response.Content.ReadAsStreamAsync())
                        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                        {
                            var totalRead = 0L;
                            var buffer = new byte[8192];
                            int read;
                            while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await fileStream.WriteAsync(buffer, 0, read);
                                totalRead += read;
                                if (canReportProgress)
                                    progressForm.SetProgress((int)totalRead, (int)totalBytes);
                            }
                        }
                    }

                    progressForm.SetLabel("Скачивание завершено!");
                    MessageBox.Show("Java успешно скачана. Запуск установщика...");

                    // Запуск скачанного файла
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при скачивании: " + ex.Message);
                }
                finally
                {
                    progressForm.Close();
                }
            }
        }
    }
}
