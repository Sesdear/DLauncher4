using System;
using System.Windows.Forms;

namespace DristLauncher_4
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
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
    }
}
