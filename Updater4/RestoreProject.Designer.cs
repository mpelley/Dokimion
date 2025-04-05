namespace Updater4
{
    partial class RestoreProject
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
            BrowseButton = new Button();
            FolderPathTextBox = new TextBox();
            RestoreButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(284, 5);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(94, 29);
            BrowseButton.TabIndex = 0;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // FolderPathTextBox
            // 
            FolderPathTextBox.Location = new Point(12, 40);
            FolderPathTextBox.Name = "FolderPathTextBox";
            FolderPathTextBox.Size = new Size(504, 27);
            FolderPathTextBox.TabIndex = 1;
            // 
            // RestoreButton
            // 
            RestoreButton.DialogResult = DialogResult.OK;
            RestoreButton.Location = new Point(12, 73);
            RestoreButton.Name = "RestoreButton";
            RestoreButton.Size = new Size(94, 29);
            RestoreButton.TabIndex = 2;
            RestoreButton.Text = "Restore";
            RestoreButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(246, 20);
            label1.TabIndex = 3;
            label1.Text = "Path to folder with project info files:";
            // 
            // RestoreProject
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(526, 109);
            Controls.Add(label1);
            Controls.Add(RestoreButton);
            Controls.Add(FolderPathTextBox);
            Controls.Add(BrowseButton);
            Name = "RestoreProject";
            Text = "RestoreProject";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BrowseButton;
        public TextBox FolderPathTextBox;
        private Button RestoreButton;
        private Label label1;
    }
}