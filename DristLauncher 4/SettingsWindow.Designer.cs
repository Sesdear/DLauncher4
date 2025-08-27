namespace DristLauncher_4
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveButton = new Guna.UI2.WinForms.Guna2Button();
            this.DebugCheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.FindJavaButton = new Guna.UI2.WinForms.Guna2Button();
            this.RamNumericUpDown = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.ChooseJavaPathButton = new Guna.UI2.WinForms.Guna2Button();
            this.DownloadJavaButton = new Guna.UI2.WinForms.Guna2Button();
            this.JavaPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RamNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(87)))), ((int)(((byte)(39)))));
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.DebugCheckBox);
            this.panel1.Controls.Add(this.guna2HtmlLabel1);
            this.panel1.Controls.Add(this.FindJavaButton);
            this.panel1.Controls.Add(this.RamNumericUpDown);
            this.panel1.Controls.Add(this.ChooseJavaPathButton);
            this.panel1.Controls.Add(this.DownloadJavaButton);
            this.panel1.Controls.Add(this.JavaPathTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 105);
            this.panel1.TabIndex = 0;
            // 
            // SaveButton
            // 
            this.SaveButton.BorderRadius = 5;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.SaveButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SaveButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SaveButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SaveButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SaveButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.SaveButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveButton.ForeColor = System.Drawing.Color.Black;
            this.SaveButton.Location = new System.Drawing.Point(269, 69);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(57, 28);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.AutoSize = true;
            this.DebugCheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.DebugCheckBox.CheckedState.BorderRadius = 0;
            this.DebugCheckBox.CheckedState.BorderThickness = 0;
            this.DebugCheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.DebugCheckBox.CheckMarkColor = System.Drawing.Color.Black;
            this.DebugCheckBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold);
            this.DebugCheckBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.DebugCheckBox.Location = new System.Drawing.Point(104, 69);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(129, 26);
            this.DebugCheckBox.TabIndex = 2;
            this.DebugCheckBox.Text = "Debug Mode";
            this.DebugCheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.DebugCheckBox.UncheckedState.BorderRadius = 0;
            this.DebugCheckBox.UncheckedState.BorderThickness = 0;
            this.DebugCheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(7, 69);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(33, 24);
            this.guna2HtmlLabel1.TabIndex = 6;
            this.guna2HtmlLabel1.Text = "Ram\r\n";
            // 
            // FindJavaButton
            // 
            this.FindJavaButton.BorderRadius = 5;
            this.FindJavaButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.FindJavaButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.FindJavaButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.FindJavaButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.FindJavaButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.FindJavaButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.FindJavaButton.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FindJavaButton.ForeColor = System.Drawing.Color.Black;
            this.FindJavaButton.Location = new System.Drawing.Point(7, 37);
            this.FindJavaButton.Name = "FindJavaButton";
            this.FindJavaButton.Size = new System.Drawing.Size(91, 26);
            this.FindJavaButton.TabIndex = 7;
            this.FindJavaButton.Text = "Find Java";
            this.FindJavaButton.Click += new System.EventHandler(this.FindJavaButton_Click);
            // 
            // RamNumericUpDown
            // 
            this.RamNumericUpDown.BackColor = System.Drawing.Color.Transparent;
            this.RamNumericUpDown.BorderRadius = 5;
            this.RamNumericUpDown.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RamNumericUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RamNumericUpDown.Location = new System.Drawing.Point(52, 69);
            this.RamNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.RamNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.RamNumericUpDown.Name = "RamNumericUpDown";
            this.RamNumericUpDown.Size = new System.Drawing.Size(46, 25);
            this.RamNumericUpDown.TabIndex = 5;
            this.RamNumericUpDown.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.RamNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // ChooseJavaPathButton
            // 
            this.ChooseJavaPathButton.BorderRadius = 5;
            this.ChooseJavaPathButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.ChooseJavaPathButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ChooseJavaPathButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ChooseJavaPathButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ChooseJavaPathButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ChooseJavaPathButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.ChooseJavaPathButton.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.ChooseJavaPathButton.ForeColor = System.Drawing.Color.Black;
            this.ChooseJavaPathButton.Location = new System.Drawing.Point(99, 37);
            this.ChooseJavaPathButton.Name = "ChooseJavaPathButton";
            this.ChooseJavaPathButton.Size = new System.Drawing.Size(105, 25);
            this.ChooseJavaPathButton.TabIndex = 7;
            this.ChooseJavaPathButton.Text = "Choose path";
            this.ChooseJavaPathButton.Click += new System.EventHandler(this.ChooseJavaPathButton_Click);
            // 
            // DownloadJavaButton
            // 
            this.DownloadJavaButton.BorderRadius = 5;
            this.DownloadJavaButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.DownloadJavaButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.DownloadJavaButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.DownloadJavaButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DownloadJavaButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.DownloadJavaButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(201)))), ((int)(((byte)(173)))));
            this.DownloadJavaButton.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DownloadJavaButton.ForeColor = System.Drawing.Color.Black;
            this.DownloadJavaButton.Location = new System.Drawing.Point(205, 37);
            this.DownloadJavaButton.Name = "DownloadJavaButton";
            this.DownloadJavaButton.Size = new System.Drawing.Size(121, 25);
            this.DownloadJavaButton.TabIndex = 7;
            this.DownloadJavaButton.Text = "Download Java";
            this.DownloadJavaButton.Click += new System.EventHandler(this.DownloadJavaButton_Click);
            // 
            // JavaPathTextBox
            // 
            this.JavaPathTextBox.Location = new System.Drawing.Point(99, 4);
            this.JavaPathTextBox.Name = "JavaPathTextBox";
            this.JavaPathTextBox.Size = new System.Drawing.Size(227, 20);
            this.JavaPathTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Java path";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(87)))), ((int)(((byte)(39)))));
            this.panel2.Location = new System.Drawing.Point(12, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 0);
            this.panel2.TabIndex = 1;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(40)))), ((int)(((byte)(19)))));
            this.ClientSize = new System.Drawing.Size(350, 127);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(366, 166);
            this.MinimumSize = new System.Drawing.Size(366, 166);
            this.Name = "SettingsWindow";
            this.Text = "DL4 Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RamNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button SaveButton;
        private Guna.UI2.WinForms.Guna2CheckBox DebugCheckBox;
        private Guna.UI2.WinForms.Guna2Button DownloadJavaButton;
        private System.Windows.Forms.TextBox JavaPathTextBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2NumericUpDown RamNumericUpDown;
        private Guna.UI2.WinForms.Guna2Button FindJavaButton;
        private Guna.UI2.WinForms.Guna2Button ChooseJavaPathButton;
    }
}