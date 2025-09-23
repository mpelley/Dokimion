namespace Updater5
{
    partial class Updater
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
            PrevButton = new Button();
            NextButton = new Button();
            QuitButton = new Button();
            StepTextBox = new TextBox();
            StepProgressBar = new ProgressBar();
            FeedbackTextBox = new TextBox();
            panelLogin = new Panel();
            label8 = new Label();
            UrlTextBox = new TextBox();
            UseHttpsCheckBox = new CheckBox();
            ShowPasswordCheckBox = new CheckBox();
            label9 = new Label();
            label10 = new Label();
            LoginButton = new Button();
            PasswordTextBox = new TextBox();
            UserNameTextBox = new TextBox();
            panelDownloadChangedMetadata = new Panel();
            ChangedMetadataProgressBar = new ProgressBar();
            DownloadChangedMetadataButton = new Button();
            ClearAllChangedMetadataButton = new Button();
            SelectAllChangedMetadataButton = new Button();
            ChangedMetadataDataGridView = new DataGridView();
            NewTestCaseSelect = new DataGridViewCheckBoxColumn();
            NewTestCaseId = new DataGridViewTextBoxColumn();
            NewTestCaseName = new DataGridViewTextBoxColumn();
            NewTestCaseNotes = new DataGridViewTextBoxColumn();
            panelSelectProject = new Panel();
            ProjectsListBox = new ListBox();
            label2 = new Label();
            panelSelectRepo = new Panel();
            FolderForAllProjectsCheckBox = new CheckBox();
            FolderTextBox = new TextBox();
            BrowseFileSystemButton = new Button();
            label3 = new Label();
            panelDownloadNewTestCases = new Panel();
            RescanButton = new Button();
            ClearAllButton = new Button();
            SelectAllButton = new Button();
            DownloadTestCasesButton = new Button();
            NewTestCasesDataGridView = new DataGridView();
            MetadataSelect = new DataGridViewCheckBoxColumn();
            MetadataID = new DataGridViewTextBoxColumn();
            MetadataName = new DataGridViewTextBoxColumn();
            Issue = new DataGridViewTextBoxColumn();
            MetadataProgressBar = new ProgressBar();
            panelHandleDifferences = new Panel();
            label6 = new Label();
            panelDone = new Panel();
            label7 = new Label();
            panelLogin.SuspendLayout();
            panelDownloadChangedMetadata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ChangedMetadataDataGridView).BeginInit();
            panelSelectProject.SuspendLayout();
            panelSelectRepo.SuspendLayout();
            panelDownloadNewTestCases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NewTestCasesDataGridView).BeginInit();
            panelHandleDifferences.SuspendLayout();
            panelDone.SuspendLayout();
            SuspendLayout();
            // 
            // PrevButton
            // 
            PrevButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PrevButton.Location = new Point(10, 384);
            PrevButton.Margin = new Padding(3, 2, 3, 2);
            PrevButton.Name = "PrevButton";
            PrevButton.Size = new Size(82, 22);
            PrevButton.TabIndex = 0;
            PrevButton.Text = "Prev";
            PrevButton.UseVisualStyleBackColor = true;
            PrevButton.Click += PrevButton_Click;
            // 
            // NextButton
            // 
            NextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NextButton.Location = new Point(120, 384);
            NextButton.Margin = new Padding(3, 2, 3, 2);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(82, 22);
            NextButton.TabIndex = 1;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            NextButton.Click += NextButton_Click;
            // 
            // QuitButton
            // 
            QuitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            QuitButton.Location = new Point(592, 384);
            QuitButton.Margin = new Padding(3, 2, 3, 2);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(82, 22);
            QuitButton.TabIndex = 2;
            QuitButton.Text = "Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // StepTextBox
            // 
            StepTextBox.Location = new Point(10, 9);
            StepTextBox.Margin = new Padding(3, 2, 3, 2);
            StepTextBox.Name = "StepTextBox";
            StepTextBox.ReadOnly = true;
            StepTextBox.Size = new Size(295, 23);
            StepTextBox.TabIndex = 3;
            // 
            // StepProgressBar
            // 
            StepProgressBar.Location = new Point(341, 9);
            StepProgressBar.Margin = new Padding(3, 2, 3, 2);
            StepProgressBar.Name = "StepProgressBar";
            StepProgressBar.Size = new Size(332, 22);
            StepProgressBar.TabIndex = 4;
            // 
            // FeedbackTextBox
            // 
            FeedbackTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FeedbackTextBox.Location = new Point(10, 337);
            FeedbackTextBox.Margin = new Padding(3, 2, 3, 2);
            FeedbackTextBox.Multiline = true;
            FeedbackTextBox.Name = "FeedbackTextBox";
            FeedbackTextBox.ReadOnly = true;
            FeedbackTextBox.ScrollBars = ScrollBars.Vertical;
            FeedbackTextBox.Size = new Size(664, 44);
            FeedbackTextBox.TabIndex = 5;
            // 
            // panelLogin
            // 
            panelLogin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelLogin.Controls.Add(label8);
            panelLogin.Controls.Add(UrlTextBox);
            panelLogin.Controls.Add(UseHttpsCheckBox);
            panelLogin.Controls.Add(ShowPasswordCheckBox);
            panelLogin.Controls.Add(label9);
            panelLogin.Controls.Add(label10);
            panelLogin.Controls.Add(LoginButton);
            panelLogin.Controls.Add(PasswordTextBox);
            panelLogin.Controls.Add(UserNameTextBox);
            panelLogin.Location = new Point(10, 40);
            panelLogin.Margin = new Padding(3, 2, 3, 2);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(663, 292);
            panelLogin.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(123, 104);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 29;
            label8.Text = "Server Address:";
            // 
            // UrlTextBox
            // 
            UrlTextBox.Location = new Point(225, 101);
            UrlTextBox.Margin = new Padding(3, 2, 3, 2);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.Size = new Size(406, 23);
            UrlTextBox.TabIndex = 22;
            // 
            // UseHttpsCheckBox
            // 
            UseHttpsCheckBox.AutoSize = true;
            UseHttpsCheckBox.Location = new Point(38, 102);
            UseHttpsCheckBox.Margin = new Padding(3, 2, 3, 2);
            UseHttpsCheckBox.Name = "UseHttpsCheckBox";
            UseHttpsCheckBox.Size = new Size(75, 19);
            UseHttpsCheckBox.TabIndex = 21;
            UseHttpsCheckBox.Text = "Use https";
            UseHttpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowPasswordCheckBox
            // 
            ShowPasswordCheckBox.AutoSize = true;
            ShowPasswordCheckBox.Location = new Point(574, 128);
            ShowPasswordCheckBox.Margin = new Padding(3, 2, 3, 2);
            ShowPasswordCheckBox.Name = "ShowPasswordCheckBox";
            ShowPasswordCheckBox.Size = new Size(55, 19);
            ShowPasswordCheckBox.TabIndex = 28;
            ShowPasswordCheckBox.Text = "Show";
            ShowPasswordCheckBox.UseVisualStyleBackColor = true;
            ShowPasswordCheckBox.CheckedChanged += ShowPasswordCheckBox_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(310, 128);
            label9.Name = "label9";
            label9.Size = new Size(60, 15);
            label9.TabIndex = 26;
            label9.Text = "Password:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(31, 130);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 24;
            label10.Text = "User name:";
            // 
            // LoginButton
            // 
            LoginButton.DialogResult = DialogResult.OK;
            LoginButton.Location = new Point(43, 160);
            LoginButton.Margin = new Padding(3, 2, 3, 2);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(80, 29);
            LoginButton.TabIndex = 27;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(377, 128);
            PasswordTextBox.Margin = new Padding(3, 2, 3, 2);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(183, 23);
            PasswordTextBox.TabIndex = 25;
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Location = new Point(105, 126);
            UserNameTextBox.Margin = new Padding(3, 2, 3, 2);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(197, 23);
            UserNameTextBox.TabIndex = 23;
            // 
            // panelDownloadChangedMetadata
            // 
            panelDownloadChangedMetadata.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDownloadChangedMetadata.Controls.Add(ChangedMetadataProgressBar);
            panelDownloadChangedMetadata.Controls.Add(DownloadChangedMetadataButton);
            panelDownloadChangedMetadata.Controls.Add(ClearAllChangedMetadataButton);
            panelDownloadChangedMetadata.Controls.Add(SelectAllChangedMetadataButton);
            panelDownloadChangedMetadata.Controls.Add(ChangedMetadataDataGridView);
            panelDownloadChangedMetadata.Location = new Point(0, 0);
            panelDownloadChangedMetadata.Margin = new Padding(3, 2, 3, 2);
            panelDownloadChangedMetadata.Name = "panelDownloadChangedMetadata";
            panelDownloadChangedMetadata.Size = new Size(663, 292);
            panelDownloadChangedMetadata.TabIndex = 0;
            panelDownloadChangedMetadata.Visible = false;
            // 
            // ChangedMetadataProgressBar
            // 
            ChangedMetadataProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ChangedMetadataProgressBar.Location = new Point(299, 265);
            ChangedMetadataProgressBar.Margin = new Padding(3, 2, 3, 2);
            ChangedMetadataProgressBar.Name = "ChangedMetadataProgressBar";
            ChangedMetadataProgressBar.Size = new Size(359, 22);
            ChangedMetadataProgressBar.TabIndex = 4;
            // 
            // DownloadChangedMetadataButton
            // 
            DownloadChangedMetadataButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DownloadChangedMetadataButton.Location = new Point(8, 266);
            DownloadChangedMetadataButton.Margin = new Padding(3, 2, 3, 2);
            DownloadChangedMetadataButton.Name = "DownloadChangedMetadataButton";
            DownloadChangedMetadataButton.Size = new Size(271, 22);
            DownloadChangedMetadataButton.TabIndex = 3;
            DownloadChangedMetadataButton.Text = "Download Selected Metadata from Dokimion";
            DownloadChangedMetadataButton.UseVisualStyleBackColor = true;
            DownloadChangedMetadataButton.Click += DownloadChangedMetadataButton_Click;
            // 
            // ClearAllChangedMetadataButton
            // 
            ClearAllChangedMetadataButton.Location = new Point(91, 6);
            ClearAllChangedMetadataButton.Margin = new Padding(3, 2, 3, 2);
            ClearAllChangedMetadataButton.Name = "ClearAllChangedMetadataButton";
            ClearAllChangedMetadataButton.Size = new Size(82, 22);
            ClearAllChangedMetadataButton.TabIndex = 2;
            ClearAllChangedMetadataButton.Text = "Clear All";
            ClearAllChangedMetadataButton.UseVisualStyleBackColor = true;
            ClearAllChangedMetadataButton.Click += ClearAllChangedMetadataButton_Click;
            // 
            // SelectAllChangedMetadataButton
            // 
            SelectAllChangedMetadataButton.Location = new Point(4, 6);
            SelectAllChangedMetadataButton.Margin = new Padding(3, 2, 3, 2);
            SelectAllChangedMetadataButton.Name = "SelectAllChangedMetadataButton";
            SelectAllChangedMetadataButton.Size = new Size(82, 22);
            SelectAllChangedMetadataButton.TabIndex = 1;
            SelectAllChangedMetadataButton.Text = "Select All";
            SelectAllChangedMetadataButton.UseVisualStyleBackColor = true;
            SelectAllChangedMetadataButton.Click += SelectAllChangedMetadataButton_Click;
            // 
            // ChangedMetadataDataGridView
            // 
            ChangedMetadataDataGridView.AllowUserToAddRows = false;
            ChangedMetadataDataGridView.AllowUserToDeleteRows = false;
            ChangedMetadataDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ChangedMetadataDataGridView.Columns.AddRange(new DataGridViewColumn[] { NewTestCaseSelect, NewTestCaseId, NewTestCaseName, NewTestCaseNotes });
            ChangedMetadataDataGridView.Location = new Point(3, 32);
            ChangedMetadataDataGridView.Margin = new Padding(3, 2, 3, 2);
            ChangedMetadataDataGridView.Name = "ChangedMetadataDataGridView";
            ChangedMetadataDataGridView.RowHeadersWidth = 51;
            ChangedMetadataDataGridView.Size = new Size(655, 228);
            ChangedMetadataDataGridView.TabIndex = 0;
            // 
            // NewTestCaseSelect
            // 
            NewTestCaseSelect.HeaderText = "Select";
            NewTestCaseSelect.MinimumWidth = 6;
            NewTestCaseSelect.Name = "NewTestCaseSelect";
            NewTestCaseSelect.Width = 50;
            // 
            // NewTestCaseId
            // 
            NewTestCaseId.HeaderText = "ID";
            NewTestCaseId.MinimumWidth = 6;
            NewTestCaseId.Name = "NewTestCaseId";
            NewTestCaseId.ReadOnly = true;
            NewTestCaseId.Width = 50;
            // 
            // NewTestCaseName
            // 
            NewTestCaseName.HeaderText = "Name";
            NewTestCaseName.MinimumWidth = 6;
            NewTestCaseName.Name = "NewTestCaseName";
            NewTestCaseName.ReadOnly = true;
            NewTestCaseName.Width = 200;
            // 
            // NewTestCaseNotes
            // 
            NewTestCaseNotes.HeaderText = "Notes";
            NewTestCaseNotes.MinimumWidth = 6;
            NewTestCaseNotes.Name = "NewTestCaseNotes";
            NewTestCaseNotes.ReadOnly = true;
            NewTestCaseNotes.Width = 200;
            // 
            // panelSelectProject
            // 
            panelSelectProject.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSelectProject.Controls.Add(ProjectsListBox);
            panelSelectProject.Controls.Add(label2);
            panelSelectProject.Location = new Point(10, 40);
            panelSelectProject.Margin = new Padding(3, 2, 3, 2);
            panelSelectProject.Name = "panelSelectProject";
            panelSelectProject.Size = new Size(663, 292);
            panelSelectProject.TabIndex = 0;
            panelSelectProject.Visible = false;
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.ItemHeight = 15;
            ProjectsListBox.Location = new Point(14, 44);
            ProjectsListBox.Margin = new Padding(3, 2, 3, 2);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(644, 244);
            ProjectsListBox.TabIndex = 32;
            ProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 13);
            label2.Name = "label2";
            label2.Size = new Size(187, 15);
            label2.TabIndex = 0;
            label2.Text = "Select One Project from Dokimion";
            // 
            // panelSelectRepo
            // 
            panelSelectRepo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSelectRepo.Controls.Add(FolderForAllProjectsCheckBox);
            panelSelectRepo.Controls.Add(FolderTextBox);
            panelSelectRepo.Controls.Add(BrowseFileSystemButton);
            panelSelectRepo.Controls.Add(label3);
            panelSelectRepo.Location = new Point(10, 40);
            panelSelectRepo.Margin = new Padding(3, 2, 3, 2);
            panelSelectRepo.Name = "panelSelectRepo";
            panelSelectRepo.Size = new Size(663, 292);
            panelSelectRepo.TabIndex = 0;
            panelSelectRepo.Visible = false;
            // 
            // FolderForAllProjectsCheckBox
            // 
            FolderForAllProjectsCheckBox.AutoSize = true;
            FolderForAllProjectsCheckBox.Location = new Point(124, 78);
            FolderForAllProjectsCheckBox.Margin = new Padding(3, 2, 3, 2);
            FolderForAllProjectsCheckBox.Name = "FolderForAllProjectsCheckBox";
            FolderForAllProjectsCheckBox.Size = new Size(329, 19);
            FolderForAllProjectsCheckBox.TabIndex = 5;
            FolderForAllProjectsCheckBox.Text = "Use one folder for all projects, each project in a subfolder.";
            FolderForAllProjectsCheckBox.UseVisualStyleBackColor = true;
            // 
            // FolderTextBox
            // 
            FolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FolderTextBox.Location = new Point(37, 102);
            FolderTextBox.Margin = new Padding(3, 2, 3, 2);
            FolderTextBox.Name = "FolderTextBox";
            FolderTextBox.Size = new Size(622, 23);
            FolderTextBox.TabIndex = 4;
            FolderTextBox.TextChanged += FolderTextBox_TextChanged;
            // 
            // BrowseFileSystemButton
            // 
            BrowseFileSystemButton.Location = new Point(37, 76);
            BrowseFileSystemButton.Margin = new Padding(3, 2, 3, 2);
            BrowseFileSystemButton.Name = "BrowseFileSystemButton";
            BrowseFileSystemButton.Size = new Size(82, 22);
            BrowseFileSystemButton.TabIndex = 3;
            BrowseFileSystemButton.Text = "Browse...";
            BrowseFileSystemButton.UseVisualStyleBackColor = true;
            BrowseFileSystemButton.Click += BrowseFileSystemButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 44);
            label3.Name = "label3";
            label3.Size = new Size(232, 15);
            label3.TabIndex = 0;
            label3.Text = "Select the folder holding the repo or repos.";
            // 
            // panelDownloadNewTestCases
            // 
            panelDownloadNewTestCases.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDownloadNewTestCases.Controls.Add(RescanButton);
            panelDownloadNewTestCases.Controls.Add(ClearAllButton);
            panelDownloadNewTestCases.Controls.Add(SelectAllButton);
            panelDownloadNewTestCases.Controls.Add(DownloadTestCasesButton);
            panelDownloadNewTestCases.Controls.Add(NewTestCasesDataGridView);
            panelDownloadNewTestCases.Controls.Add(MetadataProgressBar);
            panelDownloadNewTestCases.Location = new Point(10, 40);
            panelDownloadNewTestCases.Margin = new Padding(3, 2, 3, 2);
            panelDownloadNewTestCases.Name = "panelDownloadNewTestCases";
            panelDownloadNewTestCases.Size = new Size(663, 292);
            panelDownloadNewTestCases.TabIndex = 0;
            panelDownloadNewTestCases.Visible = false;
            // 
            // RescanButton
            // 
            RescanButton.Location = new Point(221, 16);
            RescanButton.Margin = new Padding(3, 2, 3, 2);
            RescanButton.Name = "RescanButton";
            RescanButton.Size = new Size(115, 22);
            RescanButton.TabIndex = 7;
            RescanButton.Text = "Rescan test cases";
            RescanButton.UseVisualStyleBackColor = true;
            RescanButton.Click += RescanButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Location = new Point(118, 15);
            ClearAllButton.Margin = new Padding(3, 2, 3, 2);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(82, 22);
            ClearAllButton.TabIndex = 6;
            ClearAllButton.Text = "Clear All";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(14, 16);
            SelectAllButton.Margin = new Padding(3, 2, 3, 2);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(82, 22);
            SelectAllButton.TabIndex = 5;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // DownloadTestCasesButton
            // 
            DownloadTestCasesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DownloadTestCasesButton.Location = new Point(12, 266);
            DownloadTestCasesButton.Margin = new Padding(3, 2, 3, 2);
            DownloadTestCasesButton.Name = "DownloadTestCasesButton";
            DownloadTestCasesButton.Size = new Size(220, 22);
            DownloadTestCasesButton.TabIndex = 3;
            DownloadTestCasesButton.Text = "Download selected test cases";
            DownloadTestCasesButton.UseVisualStyleBackColor = true;
            DownloadTestCasesButton.Click += DownloadTestCasesButton_Click;
            // 
            // NewTestCasesDataGridView
            // 
            NewTestCasesDataGridView.AllowUserToAddRows = false;
            NewTestCasesDataGridView.AllowUserToDeleteRows = false;
            NewTestCasesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NewTestCasesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            NewTestCasesDataGridView.Columns.AddRange(new DataGridViewColumn[] { MetadataSelect, MetadataID, MetadataName, Issue });
            NewTestCasesDataGridView.Location = new Point(11, 44);
            NewTestCasesDataGridView.Margin = new Padding(3, 2, 3, 2);
            NewTestCasesDataGridView.Name = "NewTestCasesDataGridView";
            NewTestCasesDataGridView.RowHeadersVisible = false;
            NewTestCasesDataGridView.RowHeadersWidth = 51;
            NewTestCasesDataGridView.Size = new Size(647, 218);
            NewTestCasesDataGridView.TabIndex = 2;
            // 
            // MetadataSelect
            // 
            MetadataSelect.HeaderText = "Select";
            MetadataSelect.MinimumWidth = 6;
            MetadataSelect.Name = "MetadataSelect";
            MetadataSelect.Width = 50;
            // 
            // MetadataID
            // 
            MetadataID.HeaderText = "ID";
            MetadataID.MinimumWidth = 6;
            MetadataID.Name = "MetadataID";
            MetadataID.ReadOnly = true;
            MetadataID.Width = 50;
            // 
            // MetadataName
            // 
            MetadataName.HeaderText = "Name";
            MetadataName.MinimumWidth = 6;
            MetadataName.Name = "MetadataName";
            MetadataName.ReadOnly = true;
            MetadataName.Width = 400;
            // 
            // Issue
            // 
            Issue.HeaderText = "Issue";
            Issue.Name = "Issue";
            Issue.ReadOnly = true;
            Issue.Width = 200;
            // 
            // MetadataProgressBar
            // 
            MetadataProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MetadataProgressBar.Location = new Point(243, 265);
            MetadataProgressBar.Margin = new Padding(3, 2, 3, 2);
            MetadataProgressBar.Name = "MetadataProgressBar";
            MetadataProgressBar.Size = new Size(415, 22);
            MetadataProgressBar.Style = ProgressBarStyle.Continuous;
            MetadataProgressBar.TabIndex = 1;
            // 
            // panelHandleDifferences
            // 
            panelHandleDifferences.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelHandleDifferences.Controls.Add(label6);
            panelHandleDifferences.Location = new Point(10, 40);
            panelHandleDifferences.Margin = new Padding(3, 2, 3, 2);
            panelHandleDifferences.Name = "panelHandleDifferences";
            panelHandleDifferences.Size = new Size(663, 292);
            panelHandleDifferences.TabIndex = 0;
            panelHandleDifferences.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 13);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 0;
            label6.Text = "label6";
            // 
            // panelDone
            // 
            panelDone.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDone.Controls.Add(label7);
            panelDone.Location = new Point(10, 40);
            panelDone.Margin = new Padding(3, 2, 3, 2);
            panelDone.Name = "panelDone";
            panelDone.Size = new Size(663, 292);
            panelDone.TabIndex = 0;
            panelDone.Visible = false;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 16F);
            label7.Location = new Point(258, 124);
            label7.Name = "label7";
            label7.Size = new Size(71, 30);
            label7.TabIndex = 0;
            label7.Text = "Done!";
            // 
            // Updater
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 415);
            Controls.Add(panelDownloadNewTestCases);
            Controls.Add(panelDownloadChangedMetadata);
            Controls.Add(panelHandleDifferences);
            Controls.Add(panelDone);
            Controls.Add(panelSelectRepo);
            Controls.Add(panelLogin);
            Controls.Add(panelSelectProject);
            Controls.Add(FeedbackTextBox);
            Controls.Add(StepProgressBar);
            Controls.Add(StepTextBox);
            Controls.Add(QuitButton);
            Controls.Add(NextButton);
            Controls.Add(PrevButton);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Updater";
            Text = "Updater";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelDownloadChangedMetadata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ChangedMetadataDataGridView).EndInit();
            panelSelectProject.ResumeLayout(false);
            panelSelectProject.PerformLayout();
            panelSelectRepo.ResumeLayout(false);
            panelSelectRepo.PerformLayout();
            panelDownloadNewTestCases.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NewTestCasesDataGridView).EndInit();
            panelHandleDifferences.ResumeLayout(false);
            panelHandleDifferences.PerformLayout();
            panelDone.ResumeLayout(false);
            panelDone.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Button PrevButton;
        public Button NextButton;
        private Button QuitButton;
        private TextBox StepTextBox;
        private ProgressBar StepProgressBar;
        public TextBox FeedbackTextBox;

        private StepLogin StepLogin;
        private StepSelectProject StepSelectProject;
        private StepSelectRepo StepSelectRepo;
        private StepDownloadNewTestCases StepDownloadNewTestCases;
        private StepHandleDifferences StepHandleDifferences;
        private StepDownloadChangedMetadata StepDownloadChangedMetadata;
        private StepDone StepDone;

        private StepCode ActiveStepCode;

        private Panel panelLogin;

        private Panel panelSelectProject;

        private Panel panelSelectRepo;
        private Label label3;

        private Panel panelDownloadNewTestCases;

        private Panel panelDownloadChangedMetadata;

        private Panel panelHandleDifferences;
        private Label label6;

        private Panel panelDone;
        private Label label7;

        private Data Data;

        // Controls for Step Login
        private Label label8;
        public TextBox UrlTextBox;
        public CheckBox UseHttpsCheckBox;
        public CheckBox ShowPasswordCheckBox;
        private Label label9;
        private Label label10;
        private Button LoginButton;
        public TextBox PasswordTextBox;
        public TextBox UserNameTextBox;
        public ListBox ProjectsListBox;
        private Label label2;

        // Controls for Select Repo
        public CheckBox FolderForAllProjectsCheckBox;
        public TextBox FolderTextBox;
        private Button BrowseFileSystemButton;
        public ProgressBar MetadataProgressBar;
        public DataGridView NewTestCasesDataGridView;
        private Button DownloadTestCasesButton;
        private Button ClearAllButton;
        private Button SelectAllButton;
        private Button RescanButton;
        private ProgressBar ChangedMetadataProgressBar;
        private Button DownloadChangedMetadataButton;
        private Button ClearAllChangedMetadataButton;
        private Button SelectAllChangedMetadataButton;
        private DataGridViewCheckBoxColumn NewTestCaseSelect;
        private DataGridViewTextBoxColumn NewTestCaseId;
        private DataGridViewTextBoxColumn NewTestCaseName;
        private DataGridViewTextBoxColumn NewTestCaseNotes;
        public DataGridView ChangedMetadataDataGridView;
        private DataGridViewCheckBoxColumn MetadataSelect;
        private DataGridViewTextBoxColumn MetadataID;
        private DataGridViewTextBoxColumn MetadataName;
        private DataGridViewTextBoxColumn Issue;
    }
}
