using System;
using System.Drawing;
using System.Windows.Forms;

namespace DristLauncher_4
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
            ApplyTheme();
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
            textBox.FillColor = ParseColor(LauncherSettings.Default.PanelsColor);


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
        public void Log(string message)
        {
            if (textBox.InvokeRequired)
                textBox.Invoke(new Action(() => textBox.AppendText(message + Environment.NewLine)));
            else
                textBox.AppendText(message + Environment.NewLine);
        }
    }
}
