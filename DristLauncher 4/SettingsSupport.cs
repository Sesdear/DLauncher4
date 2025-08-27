using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DristLauncher_4
{
    internal class SettingsSupport
    {
        private class JavaEntry
        {
            public string DisplayName { get; set; }
            public string Path { get; set; }
            public override string ToString() => DisplayName;
        }

        public void FindJavaButtonOnPc_Click(object sender, EventArgs e, TextBox JavaPathTextBox, DebugForm debugForm)
        {
            List<string> foundJavaPaths = new List<string>();

            // Только Program Files\Java и Program Files (x86)\Java
            string[] javaProgramDirs = new[]
{
    @"C:\Program Files\Java",      // 64-bit Java
    @"C:\Program Files (x86)\Java" // 32-bit Java
};

            foreach (string javaDir in javaProgramDirs)
            {
                if (!Directory.Exists(javaDir)) continue;

                try
                {
                    // Берём только подпапки верхнего уровня
                    foreach (string subDir in Directory.GetDirectories(javaDir))
                    {
                        string javawPath = Path.Combine(subDir, "bin", "javaw.exe");
                        if (!foundJavaPaths.Contains(javawPath))
                        {
                            foundJavaPaths.Add(javawPath);
                            debugForm?.Log(javawPath);
                        }
                    }
                }
                catch { }
            }

            // Подготовка списка для ComboBox
            List<JavaEntry> javaEntries = new List<JavaEntry>();
            foreach (var path in foundJavaPaths)
            {
                string versionName;
                try
                {
                    versionName = Directory.GetParent(Directory.GetParent(path).FullName).Name;
                }
                catch
                {
                    versionName = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(path)));
                }

                javaEntries.Add(new JavaEntry
                {
                    DisplayName = $"Java {versionName} ({path})",
                    Path = path
                });
            }

            // Показ пользователю
            if (javaEntries.Count > 0)
            {
                using (Form selectForm = new Form())
                {
                    selectForm.Text = "Выберите Java";
                    selectForm.Size = new System.Drawing.Size(600, 180);

                    ComboBox comboBox = new ComboBox
                    {
                        DataSource = javaEntries,
                        Dock = DockStyle.Top,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };

                    Button okButton = new Button
                    {
                        Text = "OK",
                        Dock = DockStyle.Bottom
                    };

                    okButton.Click += (s, args) =>
                    {
                        var selected = (JavaEntry)comboBox.SelectedItem;
                        MessageBox.Show($"Выбрана Java:\n{selected.Path}", "Java выбрана", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        JavaPathTextBox.Text = selected.Path;
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
