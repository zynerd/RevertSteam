namespace RevertSteam
{
    partial class RevertSteamWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevertSteamWindow));
            this.browseButton = new System.Windows.Forms.Button();
            this.revertButton = new System.Windows.Forms.Button();
            this.patchButton = new System.Windows.Forms.Button();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.browseTextBox = new System.Windows.Forms.TextBox();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.steamDirBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.browseLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(12, 25);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.TabStop = false;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // revertButton
            // 
            this.revertButton.Location = new System.Drawing.Point(12, 161);
            this.revertButton.Name = "revertButton";
            this.revertButton.Size = new System.Drawing.Size(75, 23);
            this.revertButton.TabIndex = 1;
            this.revertButton.TabStop = false;
            this.revertButton.Text = "Revert";
            this.revertButton.UseVisualStyleBackColor = true;
            this.revertButton.Click += new System.EventHandler(this.revertButton_Click);
            // 
            // patchButton
            // 
            this.patchButton.Enabled = false;
            this.patchButton.Location = new System.Drawing.Point(93, 161);
            this.patchButton.Name = "patchButton";
            this.patchButton.Size = new System.Drawing.Size(75, 23);
            this.patchButton.TabIndex = 2;
            this.patchButton.TabStop = false;
            this.patchButton.Text = "Patch";
            this.patchButton.UseVisualStyleBackColor = true;
            this.patchButton.Click += new System.EventHandler(this.patchButton_Click);
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Location = new System.Drawing.Point(12, 190);
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(250, 23);
            this.downloadProgressBar.TabIndex = 3;
            // 
            // browseTextBox
            // 
            this.browseTextBox.Location = new System.Drawing.Point(93, 27);
            this.browseTextBox.Name = "browseTextBox";
            this.browseTextBox.Size = new System.Drawing.Size(169, 20);
            this.browseTextBox.TabIndex = 4;
            this.browseTextBox.TabStop = false;
            // 
            // statusTextBox
            // 
            this.statusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusTextBox.Enabled = false;
            this.statusTextBox.Location = new System.Drawing.Point(12, 226);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ShortcutsEnabled = false;
            this.statusTextBox.Size = new System.Drawing.Size(250, 13);
            this.statusTextBox.TabIndex = 5;
            this.statusTextBox.TabStop = false;
            this.statusTextBox.Text = "Ready.";
            this.statusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // steamDirBrowser
            // 
            this.steamDirBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // browseLabel
            // 
            this.browseLabel.AutoSize = true;
            this.browseLabel.Location = new System.Drawing.Point(9, 9);
            this.browseLabel.Name = "browseLabel";
            this.browseLabel.Size = new System.Drawing.Size(138, 13);
            this.browseLabel.TabIndex = 6;
            this.browseLabel.Text = "Steam Installation Directory:";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(19, 81);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(236, 39);
            this.infoLabel.TabIndex = 7;
            this.infoLabel.Text = "This tool automatically reverts Steam back to the\r\nprevious version of the Librar" +
    "y UI, undoing the\r\nSteam Library update from October 30th 2019.";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RevertSteamWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 251);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.browseLabel);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.browseTextBox);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.patchButton);
            this.Controls.Add(this.revertButton);
            this.Controls.Add(this.browseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RevertSteamWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RevertSteam";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RevertSteamWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button revertButton;
        private System.Windows.Forms.Button patchButton;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.TextBox browseTextBox;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.FolderBrowserDialog steamDirBrowser;
        private System.Windows.Forms.Label browseLabel;
        private System.Windows.Forms.Label infoLabel;
    }
}

