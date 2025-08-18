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
            this.panel1 = new System.Windows.Forms.Panel();
            this.DownloadJavaButton = new System.Windows.Forms.Button();
            this.ChooseJavaPathButton = new System.Windows.Forms.Button();
            this.FindJavaButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.JavaPathTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DebugCheckBox = new System.Windows.Forms.CheckBox();
            this.RamNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RamNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(137)))), ((int)(((byte)(140)))));
            this.panel1.Controls.Add(this.DownloadJavaButton);
            this.panel1.Controls.Add(this.ChooseJavaPathButton);
            this.panel1.Controls.Add(this.FindJavaButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.JavaPathTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 65);
            this.panel1.TabIndex = 0;
            // 
            // DownloadJavaButton
            // 
            this.DownloadJavaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadJavaButton.AutoSize = true;
            this.DownloadJavaButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DownloadJavaButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(209)))), ((int)(((byte)(210)))));
            this.DownloadJavaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadJavaButton.Location = new System.Drawing.Point(233, 37);
            this.DownloadJavaButton.Name = "DownloadJavaButton";
            this.DownloadJavaButton.Size = new System.Drawing.Size(93, 25);
            this.DownloadJavaButton.TabIndex = 4;
            this.DownloadJavaButton.Text = "Download Java";
            this.DownloadJavaButton.UseVisualStyleBackColor = false;
            // 
            // ChooseJavaPathButton
            // 
            this.ChooseJavaPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseJavaPathButton.AutoSize = true;
            this.ChooseJavaPathButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ChooseJavaPathButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(209)))), ((int)(((byte)(210)))));
            this.ChooseJavaPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChooseJavaPathButton.Location = new System.Drawing.Point(74, 37);
            this.ChooseJavaPathButton.Name = "ChooseJavaPathButton";
            this.ChooseJavaPathButton.Size = new System.Drawing.Size(79, 25);
            this.ChooseJavaPathButton.TabIndex = 3;
            this.ChooseJavaPathButton.Text = "Choose path";
            this.ChooseJavaPathButton.UseVisualStyleBackColor = false;
            this.ChooseJavaPathButton.Click += new System.EventHandler(this.ChooseJavaPathButton_Click);
            // 
            // FindJavaButton
            // 
            this.FindJavaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FindJavaButton.AutoSize = true;
            this.FindJavaButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FindJavaButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(209)))), ((int)(((byte)(210)))));
            this.FindJavaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindJavaButton.Location = new System.Drawing.Point(3, 37);
            this.FindJavaButton.Name = "FindJavaButton";
            this.FindJavaButton.Size = new System.Drawing.Size(65, 25);
            this.FindJavaButton.TabIndex = 2;
            this.FindJavaButton.Text = "Find Java";
            this.FindJavaButton.UseVisualStyleBackColor = false;
            this.FindJavaButton.Click += new System.EventHandler(this.FindJavaButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Java path";
            // 
            // JavaPathTextBox
            // 
            this.JavaPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JavaPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JavaPathTextBox.Location = new System.Drawing.Point(86, 3);
            this.JavaPathTextBox.Name = "JavaPathTextBox";
            this.JavaPathTextBox.Size = new System.Drawing.Size(241, 20);
            this.JavaPathTextBox.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(137)))), ((int)(((byte)(140)))));
            this.panel2.Controls.Add(this.SaveButton);
            this.panel2.Controls.Add(this.DebugCheckBox);
            this.panel2.Controls.Add(this.RamNumericUpDown);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 98);
            this.panel2.TabIndex = 1;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.AutoSize = true;
            this.SaveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(209)))), ((int)(((byte)(210)))));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveButton.Location = new System.Drawing.Point(3, 68);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(45, 27);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DebugCheckBox.AutoSize = true;
            this.DebugCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DebugCheckBox.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DebugCheckBox.Location = new System.Drawing.Point(3, 30);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(118, 25);
            this.DebugCheckBox.TabIndex = 2;
            this.DebugCheckBox.Text = "Debug Mode";
            this.DebugCheckBox.UseVisualStyleBackColor = true;
            // 
            // RamNumericUpDown
            // 
            this.RamNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RamNumericUpDown.AutoSize = true;
            this.RamNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(209)))), ((int)(((byte)(210)))));
            this.RamNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RamNumericUpDown.Location = new System.Drawing.Point(50, 4);
            this.RamNumericUpDown.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.RamNumericUpDown.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.RamNumericUpDown.Name = "RamNumericUpDown";
            this.RamNumericUpDown.Size = new System.Drawing.Size(35, 20);
            this.RamNumericUpDown.TabIndex = 1;
            this.RamNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ram";
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(350, 189);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SettingsWindow";
            this.Text = "DL4 Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RamNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox JavaPathTextBox;
        private System.Windows.Forms.Button FindJavaButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DownloadJavaButton;
        private System.Windows.Forms.Button ChooseJavaPathButton;
        private System.Windows.Forms.NumericUpDown RamNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox DebugCheckBox;
    }
}