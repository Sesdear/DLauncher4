using CmlLib.Core;

namespace DristLauncher4
{
    
    partial class exectServers
    {
        int lastCheckedIndex = -1;
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void serverCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index != lastCheckedIndex)
            {
                if (lastCheckedIndex != -1)
                    serverCheckedListBox.SetItemCheckState(lastCheckedIndex, CheckState.Unchecked);
                lastCheckedIndex = e.Index;
            }
        }
        //To register event


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            startButton = new Button();
            label1 = new Label();
            settingsPanel = new Panel();
            saveButton = new Button();
            panel1 = new Panel();
            textBox2 = new TextBox();
            label5 = new Label();
            mirrorPanel = new Panel();
            mirrorsComboBox = new ComboBox();
            label4 = new Label();
            ramPanel = new Panel();
            ramUpDown = new NumericUpDown();
            label3 = new Label();
            javaPanel = new Panel();
            findJavaButton = new Button();
            downloadJavaButton = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            settingsButton = new FontAwesome.Sharp.IconButton();
            progressBar1 = new ProgressBar();
            usernameBox = new TextBox();
            tmrSettingsPanel = new System.Windows.Forms.Timer(components);
            serversPanel = new Panel();
            serverCheckedListBox = new CheckedListBox();
            panel2 = new Panel();
            settingsPanel.SuspendLayout();
            panel1.SuspendLayout();
            mirrorPanel.SuspendLayout();
            ramPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ramUpDown).BeginInit();
            javaPanel.SuspendLayout();
            serversPanel.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.AutoSize = true;
            startButton.BackColor = Color.FromArgb(76, 49, 107);
            startButton.FlatAppearance.BorderSize = 0;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Font = new Font("Myanmar Text", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            startButton.ForeColor = SystemColors.Control;
            startButton.Location = new Point(12, 240);
            startButton.Name = "startButton";
            startButton.Size = new Size(111, 44);
            startButton.TabIndex = 0;
            startButton.Text = "Play";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            // 
            // label1
            // 
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Myanmar Text", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(-1, 0);
            label1.Name = "label1";
            label1.Size = new Size(176, 38);
            label1.TabIndex = 1;
            label1.Text = "DLauncher4";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // settingsPanel
            // 
            settingsPanel.BackColor = Color.FromArgb(58, 38, 82);
            settingsPanel.Controls.Add(saveButton);
            settingsPanel.Controls.Add(panel1);
            settingsPanel.Controls.Add(mirrorPanel);
            settingsPanel.Controls.Add(ramPanel);
            settingsPanel.Controls.Add(javaPanel);
            settingsPanel.Dock = DockStyle.Right;
            settingsPanel.Location = new Point(363, 0);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(0, 293);
            settingsPanel.TabIndex = 3;
            settingsPanel.Paint += settingsPanel_Paint;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Left;
            saveButton.BackColor = Color.FromArgb(76, 49, 107);
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Font = new Font("Unispace", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            saveButton.ForeColor = SystemColors.Control;
            saveButton.Location = new Point(13, 251);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 30);
            saveButton.TabIndex = 4;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left;
            panel1.BackColor = Color.FromArgb(61, 42, 84);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label5);
            panel1.Location = new Point(13, 115);
            panel1.Name = "panel1";
            panel1.Size = new Size(294, 36);
            panel1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(76, 49, 107);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Location = new Point(102, 10);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(189, 16);
            textBox2.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Unispace", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(0, 11);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 0;
            label5.Text = "SL Password";
            // 
            // mirrorPanel
            // 
            mirrorPanel.Anchor = AnchorStyles.Left;
            mirrorPanel.BackColor = Color.FromArgb(61, 42, 84);
            mirrorPanel.Controls.Add(mirrorsComboBox);
            mirrorPanel.Controls.Add(label4);
            mirrorPanel.Location = new Point(133, 73);
            mirrorPanel.Name = "mirrorPanel";
            mirrorPanel.Size = new Size(174, 36);
            mirrorPanel.TabIndex = 2;
            // 
            // mirrorsComboBox
            // 
            mirrorsComboBox.BackColor = Color.FromArgb(76, 49, 107);
            mirrorsComboBox.FlatStyle = FlatStyle.Flat;
            mirrorsComboBox.ForeColor = SystemColors.Menu;
            mirrorsComboBox.FormattingEnabled = true;
            mirrorsComboBox.Location = new Point(63, 8);
            mirrorsComboBox.Name = "mirrorsComboBox";
            mirrorsComboBox.Size = new Size(108, 23);
            mirrorsComboBox.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Unispace", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(0, 11);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 0;
            label4.Text = "Mirrors";
            // 
            // ramPanel
            // 
            ramPanel.Anchor = AnchorStyles.Left;
            ramPanel.BackColor = Color.FromArgb(61, 42, 84);
            ramPanel.Controls.Add(ramUpDown);
            ramPanel.Controls.Add(label3);
            ramPanel.Location = new Point(13, 73);
            ramPanel.Name = "ramPanel";
            ramPanel.Size = new Size(114, 36);
            ramPanel.TabIndex = 1;
            // 
            // ramUpDown
            // 
            ramUpDown.BackColor = Color.FromArgb(76, 49, 107);
            ramUpDown.BorderStyle = BorderStyle.None;
            ramUpDown.ForeColor = SystemColors.Menu;
            ramUpDown.Location = new Point(61, 9);
            ramUpDown.Name = "ramUpDown";
            ramUpDown.Size = new Size(40, 19);
            ramUpDown.TabIndex = 1;
            ramUpDown.TextAlign = HorizontalAlignment.Center;
            ramUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Unispace", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(0, 11);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 0;
            label3.Text = "Ram GB";
            // 
            // javaPanel
            // 
            javaPanel.Anchor = AnchorStyles.Left;
            javaPanel.BackColor = Color.FromArgb(61, 42, 84);
            javaPanel.Controls.Add(findJavaButton);
            javaPanel.Controls.Add(downloadJavaButton);
            javaPanel.Controls.Add(textBox1);
            javaPanel.Controls.Add(label2);
            javaPanel.Location = new Point(13, 9);
            javaPanel.Name = "javaPanel";
            javaPanel.Size = new Size(294, 58);
            javaPanel.TabIndex = 0;
            // 
            // findJavaButton
            // 
            findJavaButton.BackColor = Color.FromArgb(76, 49, 107);
            findJavaButton.FlatAppearance.BorderSize = 0;
            findJavaButton.FlatStyle = FlatStyle.Flat;
            findJavaButton.Font = new Font("Unispace", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            findJavaButton.ForeColor = SystemColors.Control;
            findJavaButton.Location = new Point(120, 34);
            findJavaButton.Name = "findJavaButton";
            findJavaButton.Size = new Size(82, 23);
            findJavaButton.TabIndex = 3;
            findJavaButton.Text = "Find Java";
            findJavaButton.UseVisualStyleBackColor = false;
            // 
            // downloadJavaButton
            // 
            downloadJavaButton.BackColor = Color.FromArgb(76, 49, 107);
            downloadJavaButton.FlatAppearance.BorderSize = 0;
            downloadJavaButton.FlatStyle = FlatStyle.Flat;
            downloadJavaButton.Font = new Font("Unispace", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            downloadJavaButton.ForeColor = SystemColors.Control;
            downloadJavaButton.Location = new Point(4, 34);
            downloadJavaButton.Name = "downloadJavaButton";
            downloadJavaButton.Size = new Size(110, 23);
            downloadJavaButton.TabIndex = 2;
            downloadJavaButton.Text = "Downolad Java";
            downloadJavaButton.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(76, 49, 107);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.ForeColor = SystemColors.Menu;
            textBox1.Location = new Point(85, 11);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(206, 16);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Unispace", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(0, 11);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 0;
            label2.Text = "Java Path";
            // 
            // settingsButton
            // 
            settingsButton.BackColor = Color.FromArgb(76, 49, 107);
            settingsButton.Dock = DockStyle.Top;
            settingsButton.FlatAppearance.BorderSize = 0;
            settingsButton.FlatStyle = FlatStyle.Flat;
            settingsButton.IconChar = FontAwesome.Sharp.IconChar.Cog;
            settingsButton.IconColor = Color.White;
            settingsButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            settingsButton.IconSize = 42;
            settingsButton.Location = new Point(0, 0);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(40, 38);
            settingsButton.TabIndex = 4;
            settingsButton.UseVisualStyleBackColor = false;
            settingsButton.Click += settingsButton_Click;
            // 
            // progressBar1
            // 
            progressBar1.BackColor = Color.FromArgb(76, 49, 107);
            progressBar1.Location = new Point(12, 220);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(111, 14);
            progressBar1.TabIndex = 5;
            // 
            // usernameBox
            // 
            usernameBox.BackColor = Color.FromArgb(76, 49, 107);
            usernameBox.Font = new Font("Myanmar Text", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            usernameBox.ForeColor = SystemColors.Menu;
            usernameBox.Location = new Point(12, 44);
            usernameBox.MaxLength = 16;
            usernameBox.Name = "usernameBox";
            usernameBox.Size = new Size(104, 32);
            usernameBox.TabIndex = 6;
            usernameBox.Text = "UserName";
            // 
            // tmrSettingsPanel
            // 
            tmrSettingsPanel.Interval = 20;
            tmrSettingsPanel.Tick += tmrSettingsPanel_Tick;
            // 
            // serversPanel
            // 
            serversPanel.BackColor = Color.FromArgb(58, 38, 82);
            serversPanel.Controls.Add(serverCheckedListBox);
            serversPanel.Location = new Point(129, 44);
            serversPanel.Name = "serversPanel";
            serversPanel.Size = new Size(188, 237);
            serversPanel.TabIndex = 7;
            // 
            // serverCheckedListBox
            // 
            serverCheckedListBox.BackColor = Color.FromArgb(58, 38, 82);
            serverCheckedListBox.BorderStyle = BorderStyle.None;
            serverCheckedListBox.CheckOnClick = true;
            serverCheckedListBox.Dock = DockStyle.Fill;
            serverCheckedListBox.Font = new Font("Myanmar Text", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            serverCheckedListBox.ForeColor = SystemColors.Menu;
            serverCheckedListBox.FormattingEnabled = true;
            serverCheckedListBox.Items.AddRange(new object[] { "server1", "server2" });
            serverCheckedListBox.Location = new Point(0, 0);
            serverCheckedListBox.Name = "serverCheckedListBox";
            serverCheckedListBox.Size = new Size(188, 237);
            serverCheckedListBox.TabIndex = 0;
            serverCheckedListBox.ItemCheck += serverCheckedListBox_ItemCheck;
            serverCheckedListBox.SelectedIndexChanged += serverCheckedListBox_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(settingsButton);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(323, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(40, 293);
            panel2.TabIndex = 8;
            // 
            // exectServers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(54, 35, 77);
            ClientSize = new Size(363, 293);
            Controls.Add(panel2);
            Controls.Add(settingsPanel);
            Controls.Add(label1);
            Controls.Add(startButton);
            Controls.Add(progressBar1);
            Controls.Add(usernameBox);
            Controls.Add(serversPanel);
            MaximumSize = new Size(379, 332);
            MinimumSize = new Size(379, 332);
            Name = "exectServers";
            Text = "DLauncher4";
            settingsPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            mirrorPanel.ResumeLayout(false);
            mirrorPanel.PerformLayout();
            ramPanel.ResumeLayout(false);
            ramPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ramUpDown).EndInit();
            javaPanel.ResumeLayout(false);
            javaPanel.PerformLayout();
            serversPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private Label label1;
        private Panel settingsPanel;
        private FontAwesome.Sharp.IconButton settingsButton;
        public ProgressBar progressBar1;
        private Panel javaPanel;
        private Label label2;
        private TextBox usernameBox;
        private Button findJavaButton;
        private Button downloadJavaButton;
        private TextBox textBox1;
        private Panel ramPanel;
        private Panel mirrorPanel;
        private Label label4;
        private NumericUpDown ramUpDown;
        private Label label3;
        private Button saveButton;
        private Panel panel1;
        private TextBox textBox2;
        private Label label5;
        private ComboBox mirrorsComboBox;
        private System.Windows.Forms.Timer tmrSettingsPanel;
        private Panel serversPanel;
        private Panel panel2;
        private CheckedListBox serverCheckedListBox;
    }
}
