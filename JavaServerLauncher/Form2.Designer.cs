namespace JavaServerLauncher
{
    partial class Form2
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
            listBoxPlugins = new ListBox();
            textBoxSearch = new TextBox();
            buttonSearch = new Button();
            labelDescription = new RichTextBox();
            buttonDownload = new Button();
            SuspendLayout();
            // 
            // listBoxPlugins
            // 
            listBoxPlugins.FormattingEnabled = true;
            listBoxPlugins.ItemHeight = 30;
            listBoxPlugins.Location = new Point(14, 56);
            listBoxPlugins.Margin = new Padding(5, 6, 5, 6);
            listBoxPlugins.Name = "listBoxPlugins";
            listBoxPlugins.Size = new Size(203, 574);
            listBoxPlugins.TabIndex = 0;
            listBoxPlugins.SelectedIndexChanged += listBoxPlugins_SelectedIndexChanged;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(14, 12);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(203, 35);
            textBoxSearch.TabIndex = 1;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(223, 12);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(91, 35);
            buttonSearch.TabIndex = 2;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // labelDescription
            // 
            labelDescription.Location = new Point(225, 56);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(537, 574);
            labelDescription.TabIndex = 3;
            labelDescription.Text = "";
            // 
            // buttonDownload
            // 
            buttonDownload.Location = new Point(629, 12);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(133, 35);
            buttonDownload.TabIndex = 4;
            buttonDownload.Text = "Download";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 638);
            Controls.Add(buttonDownload);
            Controls.Add(labelDescription);
            Controls.Add(buttonSearch);
            Controls.Add(textBoxSearch);
            Controls.Add(listBoxPlugins);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            Text = "Plugin Browser";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBoxPlugins;
        private TextBox textBoxSearch;
        private Button buttonSearch;
        private RichTextBox labelDescription;
        private Button buttonDownload;
    }
}