using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DristLauncher_4
{
    public partial class SimpleProgressBar : Form
    {
        public SimpleProgressBar()
        {
            InitializeComponent();
            ApplyTheme();
        }
        private void ApplyTheme()
        {
            this.BackColor = ParseColor(LauncherSettings.Default.MainColor);
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
        public void SetLabel(string label)
        {
            label1.Text = label;
        }
        public void SetProgress(int value, int max)
        {
            if (ProgressBar.InvokeRequired)
                ProgressBar.Invoke(new Action(() =>
                {
                    ProgressBar.Maximum = max;
                    ProgressBar.Value = Math.Min(value, max);
                }));
            else
            {
                ProgressBar.Maximum = max;
                ProgressBar.Value = Math.Min(value, max);
            }
        }
    }
}
