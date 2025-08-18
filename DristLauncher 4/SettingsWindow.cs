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
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();

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
            RamNumericUpDown.Value = MinecraftOptions.Default.MaximumRamMb / 1024;
            DebugCheckBox.Checked = LauncherSettings.Default.DebugMode;
            


        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            
            MinecraftOptions.Default.JavaPath = JavaPathTextBox.Text;
            MinecraftOptions.Default.MaximumRamMb = Convert.ToInt32(RamNumericUpDown.Value) * 1024;
            LauncherSettings.Default.DebugMode = DebugCheckBox.Checked;

            MinecraftOptions.Default.Save();
            LauncherSettings.Default.Save();
            
            
            MessageBox.Show("Suc My dick", "Setiings Saved");
            
            

        }

        private void FindJavaButton_Click(object sender, EventArgs e)
        {
            SettingsSupport settingsSupport = new SettingsSupport();
            settingsSupport.FindJavaButtonOnPc_Click(sender, e, JavaPathTextBox);
        }

        private void ChooseJavaPathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл java.exe";
            openFileDialog.Filter = "Java Executable (java.exe)|java.exe";
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
    }
}
