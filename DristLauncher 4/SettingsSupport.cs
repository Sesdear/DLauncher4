using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DristLauncher_4
{
    internal class SettingsSupport
    {
        public void FindJavaButtonOnPc_Click(object sender, EventArgs e, TextBox JavaPathTextBox)
        {
            List<string> javaPaths = new List<string>();

            // 1. Поиск в реестре (JRE и JDK)
            string[] registryKeys = new[]
            {
        @"SOFTWARE\JavaSoft\Java Runtime Environment",
        @"SOFTWARE\JavaSoft\Java Development Kit",
        @"SOFTWARE\WOW6432Node\JavaSoft\Java Runtime Environment",
        @"SOFTWARE\WOW6432Node\JavaSoft\Java Development Kit"
    };

            foreach (string keyPath in registryKeys)
            {
                using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey(keyPath))
                {
                    if (baseKey == null) continue;

                    foreach (string version in baseKey.GetSubKeyNames())
                    {
                        using (RegistryKey subKey = baseKey.OpenSubKey(version))
                        {
                            if (subKey == null) continue;

                            var javaHome = subKey.GetValue("JavaHome") as string;
                            if (!string.IsNullOrEmpty(javaHome))
                            {
                                var javaExe = Path.Combine(javaHome, "bin", "java.exe");
                                if (File.Exists(javaExe) && !javaPaths.Contains(javaExe))
                                {
                                    javaPaths.Add(javaExe);
                                }
                            }
                        }
                    }
                }
            }

            // 2. Поиск в переменной среды PATH
            string pathEnv = Environment.GetEnvironmentVariable("PATH");
            if (!string.IsNullOrEmpty(pathEnv))
            {
                foreach (string path in pathEnv.Split(';'))
                {
                    string javaExe = Path.Combine(path.Trim(), "java.exe");
                    if (File.Exists(javaExe) && !javaPaths.Contains(javaExe))
                    {
                        javaPaths.Add(javaExe);
                    }
                }
            }

            // 3. Если нашли — показать пользователю
            if (javaPaths.Count > 0)
            {
                using (Form selectForm = new Form())
                {
                    selectForm.Text = "Выберите Java";
                    selectForm.Size = new System.Drawing.Size(500, 150);
                    ComboBox comboBox = new ComboBox()
                    {
                        DataSource = javaPaths,
                        Dock = DockStyle.Top,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };

                    Button okButton = new Button()
                    {
                        Text = "OK",
                        Dock = DockStyle.Bottom
                    };

                    okButton.Click += (s, args) =>
                    {
                        string selected = comboBox.SelectedItem.ToString();
                        MessageBox.Show($"Выбрана Java:\n{selected}", "Java выбрана", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // TODO: например, сохранить в TextBox:
                        // yourTextBox.Text = selected;
                        JavaPathTextBox.Text = selected;

                        selectForm.DialogResult = DialogResult.OK;
                        selectForm.Close();
                    };

                    selectForm.Controls.Add(comboBox);
                    selectForm.Controls.Add(okButton);
                    selectForm.StartPosition = FormStartPosition.CenterParent;

                    selectForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Java не найдена на этом ПК.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
