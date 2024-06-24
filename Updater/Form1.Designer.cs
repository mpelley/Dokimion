namespace Updater
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
            DokimionSettingsButton = new Button();
            ProjectsListBox = new ListBox();
            DownloadButton = new Button();
            UploadButton = new Button();
            RepoPathTextBox = new TextBox();
            BrowseGitHubButton = new Button();
            groupBox1 = new GroupBox();
            SelectAllButton = new Button();
            ClearAllButton = new Button();
            GetTestCasesButton = new Button();
            ExitButton = new Button();
            label1 = new Label();
            ProgressBar = new ProgressBar();
            TestCaseListBox = new CheckedListBox();
            StatusTextBox = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // DokimionSettingsButton
            // 
            DokimionSettingsButton.Location = new Point(12, 11);
            DokimionSettingsButton.Name = "DokimionSettingsButton";
            DokimionSettingsButton.Size = new Size(126, 23);
            DokimionSettingsButton.TabIndex = 0;
            DokimionSettingsButton.Text = "Dokimion Settings";
            DokimionSettingsButton.UseVisualStyleBackColor = true;
            DokimionSettingsButton.Click += DokimionSettingsButton_Click;
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.ItemHeight = 15;
            ProjectsListBox.Location = new Point(12, 59);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(204, 259);
            ProjectsListBox.TabIndex = 2;
            ProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // DownloadButton
            // 
            DownloadButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DownloadButton.Location = new Point(537, 162);
            DownloadButton.Name = "DownloadButton";
            DownloadButton.Size = new Size(173, 23);
            DownloadButton.TabIndex = 3;
            DownloadButton.Text = "Download from Dokimion";
            DownloadButton.UseVisualStyleBackColor = true;
            DownloadButton.Click += DownloadButton_Click;
            // 
            // UploadButton
            // 
            UploadButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UploadButton.Location = new Point(537, 200);
            UploadButton.Name = "UploadButton";
            UploadButton.Size = new Size(173, 23);
            UploadButton.TabIndex = 4;
            UploadButton.Text = "Upload to Dokimion";
            UploadButton.UseVisualStyleBackColor = true;
            UploadButton.Click += UploadButton_Click;
            // 
            // RepoPathTextBox
            // 
            RepoPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RepoPathTextBox.Location = new Point(13, 48);
            RepoPathTextBox.Name = "RepoPathTextBox";
            RepoPathTextBox.Size = new Size(174, 23);
            RepoPathTextBox.TabIndex = 5;
            // 
            // BrowseGitHubButton
            // 
            BrowseGitHubButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BrowseGitHubButton.Location = new Point(13, 19);
            BrowseGitHubButton.Name = "BrowseGitHubButton";
            BrowseGitHubButton.Size = new Size(75, 23);
            BrowseGitHubButton.TabIndex = 7;
            BrowseGitHubButton.Text = "Browse";
            BrowseGitHubButton.UseVisualStyleBackColor = true;
            BrowseGitHubButton.Click += BrowseGitHubButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(BrowseGitHubButton);
            groupBox1.Controls.Add(RepoPathTextBox);
            groupBox1.Location = new Point(524, 15);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(202, 95);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "GitHub Repo";
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(361, 27);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(75, 23);
            SelectAllButton.TabIndex = 10;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Location = new Point(442, 27);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(75, 23);
            ClearAllButton.TabIndex = 11;
            ClearAllButton.Text = "Clear All";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // GetTestCasesButton
            // 
            GetTestCasesButton.Location = new Point(245, 27);
            GetTestCasesButton.Name = "GetTestCasesButton";
            GetTestCasesButton.Size = new Size(111, 23);
            GetTestCasesButton.TabIndex = 12;
            GetTestCasesButton.Text = "Get For Project";
            GetTestCasesButton.UseVisualStyleBackColor = true;
            GetTestCasesButton.Click += GetTestCasesButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ExitButton.Location = new Point(623, 336);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(75, 23);
            ExitButton.TabIndex = 13;
            ExitButton.Text = "Exit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += Button8_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(245, 9);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 14;
            label1.Text = "Test Cases";
            // 
            // ProgressBar
            // 
            ProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ProgressBar.Location = new Point(537, 241);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(173, 23);
            ProgressBar.Step = 1;
            ProgressBar.Style = ProgressBarStyle.Continuous;
            ProgressBar.TabIndex = 15;
            // 
            // TestCaseListBox
            // 
            TestCaseListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TestCaseListBox.FormattingEnabled = true;
            TestCaseListBox.Location = new Point(244, 59);
            TestCaseListBox.Name = "TestCaseListBox";
            TestCaseListBox.Size = new Size(274, 238);
            TestCaseListBox.TabIndex = 16;
            // 
            // StatusTextBox
            // 
            StatusTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StatusTextBox.Location = new Point(10, 322);
            StatusTextBox.Margin = new Padding(3, 2, 3, 2);
            StatusTextBox.MaxLength = 1000000;
            StatusTextBox.Multiline = true;
            StatusTextBox.Name = "StatusTextBox";
            StatusTextBox.ReadOnly = true;
            StatusTextBox.ScrollBars = ScrollBars.Both;
            StatusTextBox.Size = new Size(584, 48);
            StatusTextBox.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = ExitButton;
            ClientSize = new Size(737, 379);
            Controls.Add(StatusTextBox);
            Controls.Add(TestCaseListBox);
            Controls.Add(ProgressBar);
            Controls.Add(label1);
            Controls.Add(ExitButton);
            Controls.Add(GetTestCasesButton);
            Controls.Add(ClearAllButton);
            Controls.Add(SelectAllButton);
            Controls.Add(UploadButton);
            Controls.Add(DownloadButton);
            Controls.Add(ProjectsListBox);
            Controls.Add(DokimionSettingsButton);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Updater";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button DokimionSettingsButton;
        private ListBox ProjectsListBox;
        private Button DownloadButton;
        private Button UploadButton;
        private TextBox RepoPathTextBox;
        private Button BrowseGitHubButton;
        private GroupBox groupBox1;
        private Button SelectAllButton;
        private Button ClearAllButton;
        private Button GetTestCasesButton;
        private Button ExitButton;
        private Label label1;
        private ProgressBar ProgressBar;
        private CheckedListBox TestCaseListBox;
        private TextBox StatusTextBox;
    }
}