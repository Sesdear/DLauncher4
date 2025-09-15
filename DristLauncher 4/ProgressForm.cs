using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DristLauncher_4
{
    public partial class ProgressForm : Form
    {
        private readonly string logFile = "start_log.txt";

        public ProgressForm()
        {
            InitializeComponent();
            ApplyTheme();
            this.FormClosing += ProgressForm_FormClosing; // подписка на событие
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
            textBox1.FillColor = ParseColor(LauncherSettings.Default.PanelsColor);


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

        public void SetProgress(int value, int max)
        {
            if (progressBar1.InvokeRequired)
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Maximum = max;
                    progressBar1.Value = Math.Min(value, max);
                }));
            else
            {
                progressBar1.Maximum = max;
                progressBar1.Value = Math.Min(value, max);
            }
        }

        public void Log(string message)
        {
            if (textBox1.InvokeRequired)
                textBox1.Invoke(new Action(() => textBox1.AppendText(message + Environment.NewLine)));
            else
                textBox1.AppendText(message + Environment.NewLine);
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                File.WriteAllText(logFile, textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения лога: " + ex.Message,
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
