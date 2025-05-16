namespace JavaServerLauncher
{
    partial class Form1
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxVersions = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            label2 = new Label();
            numericUpDown1 = new NumericUpDown();
            button3 = new Button();
            label3 = new Label();
            openFileDialog = new OpenFileDialog();
            button4 = new Button();
            buttonOpenPluginBrowser = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // comboBoxVersions
            // 
            comboBoxVersions.FormattingEnabled = true;
            comboBoxVersions.Location = new Point(12, 12);
            comboBoxVersions.Name = "comboBoxVersions";
            comboBoxVersions.Size = new Size(311, 38);
            comboBoxVersions.TabIndex = 0;
            comboBoxVersions.SelectedIndexChanged += comboBoxVersions_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(329, 12);
            button1.Name = "button1";
            button1.Size = new Size(167, 38);
            button1.TabIndex = 1;
            button1.Text = "Download";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 53);
            label1.Name = "label1";
            label1.Size = new Size(67, 30);
            label1.TabIndex = 2;
            label1.Text = "Type: ";
            // 
            // button2
            // 
            button2.Location = new Point(329, 56);
            button2.Name = "button2";
            button2.Size = new Size(167, 38);
            button2.TabIndex = 3;
            button2.Text = "Start Server";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(554, 15);
            label2.Name = "label2";
            label2.Size = new Size(194, 30);
            label2.TabIndex = 5;
            label2.Text = "RAM Amount (MB):";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(754, 15);
            numericUpDown1.Maximum = new decimal(new int[] { 1215752192, 23, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 35);
            numericUpDown1.TabIndex = 6;
            numericUpDown1.Value = new decimal(new int[] { 1024, 0, 0, 0 });
            // 
            // button3
            // 
            button3.Location = new Point(12, 248);
            button3.Name = "button3";
            button3.Size = new Size(130, 38);
            button3.TabIndex = 7;
            button3.Text = "Change";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 215);
            label3.Name = "label3";
            label3.Size = new Size(765, 30);
            label3.TabIndex = 8;
            label3.Text = "Java Path: \"C:\\Program Files\\Eclipse Adoptium\\jre-21.0.5.11-hotspot\\bin\\java.exe\"";
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // button4
            // 
            button4.Location = new Point(329, 100);
            button4.Name = "button4";
            button4.Size = new Size(167, 38);
            button4.TabIndex = 9;
            button4.Text = "Install Paper";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // buttonOpenPluginBrowser
            // 
            buttonOpenPluginBrowser.Location = new Point(329, 144);
            buttonOpenPluginBrowser.Name = "buttonOpenPluginBrowser";
            buttonOpenPluginBrowser.Size = new Size(167, 38);
            buttonOpenPluginBrowser.TabIndex = 10;
            buttonOpenPluginBrowser.Text = "Plugin Browser";
            buttonOpenPluginBrowser.UseVisualStyleBackColor = true;
            buttonOpenPluginBrowser.Click += buttonOpenPluginBrowser_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 583);
            Controls.Add(buttonOpenPluginBrowser);
            Controls.Add(button4);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(numericUpDown1);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(comboBoxVersions);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 6, 5, 6);
            Name = "Form1";
            Text = "Server Launcher";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxVersions;
        private Button button1;
        private Label label1;
        private Button button2;
        private Label label2;
        private NumericUpDown numericUpDown1;
        private Button button3;
        private Label label3;
        private OpenFileDialog openFileDialog;
        private Button button4;
        private Button buttonOpenPluginBrowser;
    }
}
