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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            PrevButton = new Button();
            NextButton = new Button();
            QuitButton = new Button();
            StepTextBox = new TextBox();
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
            splitContainer1 = new SplitContainer();
            ChangedMetadataDataGridView = new DataGridView();
            NewTestCaseSelect = new DataGridViewCheckBoxColumn();
            NewTestCaseId = new DataGridViewTextBoxColumn();
            NewTestCaseName = new DataGridViewTextBoxColumn();
            NewTestCaseNotes = new DataGridViewTextBoxColumn();
            ChangedMetadataDiffViewer = new DiffPlex.WindowsForms.Controls.DiffViewer();
            ChangedMetadataProgressBar = new ProgressBar();
            DownloadChangedMetadataButton = new Button();
            ClearAllChangedMetadataButton = new Button();
            SelectAllChangedMetadataButton = new Button();
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
            splitContainer2 = new SplitContainer();
            HandleDiffDataGridView = new DataGridView();
            dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            HandleDiffDiffViewer = new DiffPlex.WindowsForms.Controls.DiffViewer();
            HandleDiffRescanTestCasesButton = new Button();
            HandleDiffProgressBar = new ProgressBar();
            UploadDiffToDokimionButton = new Button();
            HandleDiffClearAllButton = new Button();
            HandleDiffSelectAllButton = new Button();
            panelDone = new Panel();
            label7 = new Label();
            panelLogin.SuspendLayout();
            panelDownloadChangedMetadata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ChangedMetadataDataGridView).BeginInit();
            panelSelectProject.SuspendLayout();
            panelSelectRepo.SuspendLayout();
            panelDownloadNewTestCases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NewTestCasesDataGridView).BeginInit();
            panelHandleDifferences.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HandleDiffDataGridView).BeginInit();
            panelDone.SuspendLayout();
            SuspendLayout();
            // 
            // PrevButton
            // 
            PrevButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PrevButton.Location = new Point(11, 512);
            PrevButton.Name = "PrevButton";
            PrevButton.Size = new Size(94, 29);
            PrevButton.TabIndex = 0;
            PrevButton.Text = "Prev";
            PrevButton.UseVisualStyleBackColor = true;
            PrevButton.Click += PrevButton_Click;
            // 
            // NextButton
            // 
            NextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NextButton.Location = new Point(137, 512);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(94, 29);
            NextButton.TabIndex = 1;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            NextButton.Click += NextButton_Click;
            // 
            // QuitButton
            // 
            QuitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            QuitButton.Location = new Point(677, 512);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(94, 29);
            QuitButton.TabIndex = 2;
            QuitButton.Text = "Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // StepTextBox
            // 
            StepTextBox.Location = new Point(11, 12);
            StepTextBox.Name = "StepTextBox";
            StepTextBox.ReadOnly = true;
            StepTextBox.Size = new Size(337, 27);
            StepTextBox.TabIndex = 3;
            // 
            // FeedbackTextBox
            // 
            FeedbackTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FeedbackTextBox.Location = new Point(11, 449);
            FeedbackTextBox.Multiline = true;
            FeedbackTextBox.Name = "FeedbackTextBox";
            FeedbackTextBox.ReadOnly = true;
            FeedbackTextBox.ScrollBars = ScrollBars.Vertical;
            FeedbackTextBox.Size = new Size(758, 57);
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
            panelLogin.Location = new Point(11, 53);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(758, 389);
            panelLogin.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(141, 139);
            label8.Name = "label8";
            label8.Size = new Size(110, 20);
            label8.TabIndex = 29;
            label8.Text = "Server Address:";
            // 
            // UrlTextBox
            // 
            UrlTextBox.Location = new Point(257, 135);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.Size = new Size(463, 27);
            UrlTextBox.TabIndex = 22;
            // 
            // UseHttpsCheckBox
            // 
            UseHttpsCheckBox.AutoSize = true;
            UseHttpsCheckBox.Location = new Point(43, 136);
            UseHttpsCheckBox.Name = "UseHttpsCheckBox";
            UseHttpsCheckBox.Size = new Size(92, 24);
            UseHttpsCheckBox.TabIndex = 21;
            UseHttpsCheckBox.Text = "Use https";
            UseHttpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowPasswordCheckBox
            // 
            ShowPasswordCheckBox.AutoSize = true;
            ShowPasswordCheckBox.Location = new Point(656, 171);
            ShowPasswordCheckBox.Name = "ShowPasswordCheckBox";
            ShowPasswordCheckBox.Size = new Size(67, 24);
            ShowPasswordCheckBox.TabIndex = 28;
            ShowPasswordCheckBox.Text = "Show";
            ShowPasswordCheckBox.UseVisualStyleBackColor = true;
            ShowPasswordCheckBox.CheckedChanged += ShowPasswordCheckBox_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(354, 171);
            label9.Name = "label9";
            label9.Size = new Size(73, 20);
            label9.TabIndex = 26;
            label9.Text = "Password:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(35, 173);
            label10.Name = "label10";
            label10.Size = new Size(82, 20);
            label10.TabIndex = 24;
            label10.Text = "User name:";
            // 
            // LoginButton
            // 
            LoginButton.DialogResult = DialogResult.OK;
            LoginButton.Location = new Point(49, 213);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(91, 39);
            LoginButton.TabIndex = 27;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(431, 171);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(209, 27);
            PasswordTextBox.TabIndex = 25;
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Location = new Point(120, 168);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(225, 27);
            UserNameTextBox.TabIndex = 23;
            // 
            // panelDownloadChangedMetadata
            // 
            panelDownloadChangedMetadata.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDownloadChangedMetadata.Controls.Add(splitContainer1);
            panelDownloadChangedMetadata.Controls.Add(ChangedMetadataProgressBar);
            panelDownloadChangedMetadata.Controls.Add(DownloadChangedMetadataButton);
            panelDownloadChangedMetadata.Controls.Add(ClearAllChangedMetadataButton);
            panelDownloadChangedMetadata.Controls.Add(SelectAllChangedMetadataButton);
            panelDownloadChangedMetadata.Location = new Point(11, 53);
            panelDownloadChangedMetadata.Name = "panelDownloadChangedMetadata";
            panelDownloadChangedMetadata.Size = new Size(758, 389);
            panelDownloadChangedMetadata.TabIndex = 0;
            panelDownloadChangedMetadata.Visible = false;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(1, 40);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ChangedMetadataDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(ChangedMetadataDiffViewer);
            splitContainer1.Size = new Size(757, 305);
            splitContainer1.SplitterDistance = 130;
            splitContainer1.TabIndex = 5;
            // 
            // ChangedMetadataDataGridView
            // 
            ChangedMetadataDataGridView.AllowUserToAddRows = false;
            ChangedMetadataDataGridView.AllowUserToDeleteRows = false;
            ChangedMetadataDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            ChangedMetadataDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            ChangedMetadataDataGridView.ColumnHeadersHeight = 29;
            ChangedMetadataDataGridView.Columns.AddRange(new DataGridViewColumn[] { NewTestCaseSelect, NewTestCaseId, NewTestCaseName, NewTestCaseNotes });
            ChangedMetadataDataGridView.Location = new Point(-1, -3);
            ChangedMetadataDataGridView.Name = "ChangedMetadataDataGridView";
            ChangedMetadataDataGridView.RowHeadersVisible = false;
            ChangedMetadataDataGridView.RowHeadersWidth = 51;
            ChangedMetadataDataGridView.Size = new Size(754, 128);
            ChangedMetadataDataGridView.TabIndex = 6;
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
            // ChangedMetadataDiffViewer
            // 
            ChangedMetadataDiffViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ChangedMetadataDiffViewer.BorderColor = Color.FromArgb(0, 0, 0, 0);
            ChangedMetadataDiffViewer.BorderWidth = new Padding(0);
            ChangedMetadataDiffViewer.ChangeTypeForeColor = Color.FromArgb(128, 128, 128);
            ChangedMetadataDiffViewer.CollapseUnchangedSectionsToggleTitle = "_Collapse unchanged sections";
            ChangedMetadataDiffViewer.ContextLinesMenuItemsTitle = "_Lines for context";
            ChangedMetadataDiffViewer.DeletedBackColor = Color.FromArgb(64, 216, 32, 32);
            ChangedMetadataDiffViewer.DeletedForeColor = Color.FromArgb(0, 0, 0);
            ChangedMetadataDiffViewer.FontFamilyNames = "Segoe UI";
            ChangedMetadataDiffViewer.FontSize = 12D;
            ChangedMetadataDiffViewer.FontStretch = System.Windows.FontStretch.FromOpenTypeStretch(5);
            ChangedMetadataDiffViewer.FontWeight = 400;
            ChangedMetadataDiffViewer.HeaderBackColor = Color.FromArgb(12, 128, 128, 128);
            ChangedMetadataDiffViewer.HeaderForeColor = Color.FromArgb(128, 128, 128);
            ChangedMetadataDiffViewer.HeaderHeight = 0D;
            ChangedMetadataDiffViewer.IgnoreCase = false;
            ChangedMetadataDiffViewer.IgnoreUnchanged = false;
            ChangedMetadataDiffViewer.IgnoreWhiteSpace = true;
            ChangedMetadataDiffViewer.ImaginaryBackColor = Color.FromArgb(24, 128, 128, 128);
            ChangedMetadataDiffViewer.InlineModeToggleTitle = "_Unified view";
            ChangedMetadataDiffViewer.InsertedBackColor = Color.FromArgb(0, 0, 0);
            ChangedMetadataDiffViewer.InsertedForeColor = Color.FromArgb(0, 0, 0);
            ChangedMetadataDiffViewer.IsFontItalic = false;
            ChangedMetadataDiffViewer.IsSideBySide = true;
            ChangedMetadataDiffViewer.LineNumberForeColor = Color.FromArgb(64, 128, 160);
            ChangedMetadataDiffViewer.LineNumberWidth = 60;
            ChangedMetadataDiffViewer.LinesContext = 1;
            ChangedMetadataDiffViewer.Location = new Point(5, 0);
            ChangedMetadataDiffViewer.Margin = new Padding(3, 4, 3, 4);
            ChangedMetadataDiffViewer.Name = "ChangedMetadataDiffViewer";
            ChangedMetadataDiffViewer.NewText = null;
            ChangedMetadataDiffViewer.NewTextHeader = null;
            ChangedMetadataDiffViewer.OldText = null;
            ChangedMetadataDiffViewer.OldTextHeader = null;
            ChangedMetadataDiffViewer.SideBySideModeToggleTitle = "_Split view";
            ChangedMetadataDiffViewer.Size = new Size(752, 176);
            ChangedMetadataDiffViewer.SplitterBackColor = Color.FromArgb(64, 128, 128, 128);
            ChangedMetadataDiffViewer.SplitterBorderColor = Color.FromArgb(64, 128, 128, 128);
            ChangedMetadataDiffViewer.SplitterBorderWidth = new Padding(0);
            ChangedMetadataDiffViewer.SplitterWidth = 5D;
            ChangedMetadataDiffViewer.TabIndex = 8;
            ChangedMetadataDiffViewer.UnchangedBackColor = Color.FromArgb(0, 0, 0, 0);
            ChangedMetadataDiffViewer.UnchangedForeColor = Color.FromArgb(0, 0, 0);
            // 
            // ChangedMetadataProgressBar
            // 
            ChangedMetadataProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ChangedMetadataProgressBar.Location = new Point(364, 353);
            ChangedMetadataProgressBar.Name = "ChangedMetadataProgressBar";
            ChangedMetadataProgressBar.Size = new Size(388, 29);
            ChangedMetadataProgressBar.TabIndex = 4;
            // 
            // DownloadChangedMetadataButton
            // 
            DownloadChangedMetadataButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DownloadChangedMetadataButton.Location = new Point(9, 355);
            DownloadChangedMetadataButton.Name = "DownloadChangedMetadataButton";
            DownloadChangedMetadataButton.Size = new Size(349, 29);
            DownloadChangedMetadataButton.TabIndex = 3;
            DownloadChangedMetadataButton.Text = "Download Selected Metadata from Dokimion";
            DownloadChangedMetadataButton.UseVisualStyleBackColor = true;
            DownloadChangedMetadataButton.Click += DownloadChangedMetadataButton_Click;
            // 
            // ClearAllChangedMetadataButton
            // 
            ClearAllChangedMetadataButton.Location = new Point(104, 8);
            ClearAllChangedMetadataButton.Name = "ClearAllChangedMetadataButton";
            ClearAllChangedMetadataButton.Size = new Size(94, 29);
            ClearAllChangedMetadataButton.TabIndex = 2;
            ClearAllChangedMetadataButton.Text = "Clear All";
            ClearAllChangedMetadataButton.UseVisualStyleBackColor = true;
            ClearAllChangedMetadataButton.Click += ClearAllChangedMetadataButton_Click;
            // 
            // SelectAllChangedMetadataButton
            // 
            SelectAllChangedMetadataButton.Location = new Point(5, 8);
            SelectAllChangedMetadataButton.Name = "SelectAllChangedMetadataButton";
            SelectAllChangedMetadataButton.Size = new Size(94, 29);
            SelectAllChangedMetadataButton.TabIndex = 1;
            SelectAllChangedMetadataButton.Text = "Select All";
            SelectAllChangedMetadataButton.UseVisualStyleBackColor = true;
            SelectAllChangedMetadataButton.Click += SelectAllChangedMetadataButton_Click;
            // 
            // panelSelectProject
            // 
            panelSelectProject.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSelectProject.Controls.Add(ProjectsListBox);
            panelSelectProject.Controls.Add(label2);
            panelSelectProject.Location = new Point(11, 53);
            panelSelectProject.Name = "panelSelectProject";
            panelSelectProject.Size = new Size(758, 389);
            panelSelectProject.TabIndex = 0;
            panelSelectProject.Visible = false;
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.Location = new Point(16, 59);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(735, 324);
            ProjectsListBox.TabIndex = 32;
            ProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 17);
            label2.Name = "label2";
            label2.Size = new Size(235, 20);
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
            panelSelectRepo.Location = new Point(11, 53);
            panelSelectRepo.Name = "panelSelectRepo";
            panelSelectRepo.Size = new Size(758, 389);
            panelSelectRepo.TabIndex = 0;
            panelSelectRepo.Visible = false;
            // 
            // FolderForAllProjectsCheckBox
            // 
            FolderForAllProjectsCheckBox.AutoSize = true;
            FolderForAllProjectsCheckBox.Location = new Point(142, 104);
            FolderForAllProjectsCheckBox.Name = "FolderForAllProjectsCheckBox";
            FolderForAllProjectsCheckBox.Size = new Size(415, 24);
            FolderForAllProjectsCheckBox.TabIndex = 5;
            FolderForAllProjectsCheckBox.Text = "Use one folder for all projects, each project in a subfolder.";
            FolderForAllProjectsCheckBox.UseVisualStyleBackColor = true;
            // 
            // FolderTextBox
            // 
            FolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FolderTextBox.Location = new Point(42, 136);
            FolderTextBox.Name = "FolderTextBox";
            FolderTextBox.Size = new Size(710, 27);
            FolderTextBox.TabIndex = 4;
            FolderTextBox.TextChanged += FolderTextBox_TextChanged;
            // 
            // BrowseFileSystemButton
            // 
            BrowseFileSystemButton.Location = new Point(42, 101);
            BrowseFileSystemButton.Name = "BrowseFileSystemButton";
            BrowseFileSystemButton.Size = new Size(94, 29);
            BrowseFileSystemButton.TabIndex = 3;
            BrowseFileSystemButton.Text = "Browse...";
            BrowseFileSystemButton.UseVisualStyleBackColor = true;
            BrowseFileSystemButton.Click += BrowseFileSystemButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 59);
            label3.Name = "label3";
            label3.Size = new Size(295, 20);
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
            panelDownloadNewTestCases.Location = new Point(11, 53);
            panelDownloadNewTestCases.Name = "panelDownloadNewTestCases";
            panelDownloadNewTestCases.Size = new Size(758, 389);
            panelDownloadNewTestCases.TabIndex = 0;
            panelDownloadNewTestCases.Visible = false;
            // 
            // RescanButton
            // 
            RescanButton.Location = new Point(253, 8);
            RescanButton.Name = "RescanButton";
            RescanButton.Size = new Size(131, 29);
            RescanButton.TabIndex = 7;
            RescanButton.Text = "Rescan test cases";
            RescanButton.UseVisualStyleBackColor = true;
            RescanButton.Click += RescanButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Location = new Point(126, 8);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(94, 29);
            ClearAllButton.TabIndex = 6;
            ClearAllButton.Text = "Clear All";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(4, 7);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(94, 29);
            SelectAllButton.TabIndex = 5;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // DownloadTestCasesButton
            // 
            DownloadTestCasesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DownloadTestCasesButton.Location = new Point(14, 355);
            DownloadTestCasesButton.Name = "DownloadTestCasesButton";
            DownloadTestCasesButton.Size = new Size(251, 29);
            DownloadTestCasesButton.TabIndex = 3;
            DownloadTestCasesButton.Text = "Download selected test cases";
            DownloadTestCasesButton.UseVisualStyleBackColor = true;
            DownloadTestCasesButton.Click += DownloadTestCasesButton_Click;
            // 
            // NewTestCasesDataGridView
            // 
            NewTestCasesDataGridView.AllowUserToAddRows = false;
            NewTestCasesDataGridView.AllowUserToDeleteRows = false;
            NewTestCasesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NewTestCasesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            NewTestCasesDataGridView.Columns.AddRange(new DataGridViewColumn[] { MetadataSelect, MetadataID, MetadataName, Issue });
            NewTestCasesDataGridView.Location = new Point(2, 43);
            NewTestCasesDataGridView.Name = "NewTestCasesDataGridView";
            NewTestCasesDataGridView.RowHeadersVisible = false;
            NewTestCasesDataGridView.RowHeadersWidth = 51;
            NewTestCasesDataGridView.Size = new Size(750, 307);
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
            Issue.MinimumWidth = 6;
            Issue.Name = "Issue";
            Issue.ReadOnly = true;
            Issue.Width = 200;
            // 
            // MetadataProgressBar
            // 
            MetadataProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MetadataProgressBar.Location = new Point(278, 353);
            MetadataProgressBar.Name = "MetadataProgressBar";
            MetadataProgressBar.Size = new Size(474, 29);
            MetadataProgressBar.Style = ProgressBarStyle.Continuous;
            MetadataProgressBar.TabIndex = 1;
            // 
            // panelHandleDifferences
            // 
            panelHandleDifferences.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelHandleDifferences.Controls.Add(splitContainer2);
            panelHandleDifferences.Controls.Add(HandleDiffRescanTestCasesButton);
            panelHandleDifferences.Controls.Add(HandleDiffProgressBar);
            panelHandleDifferences.Controls.Add(UploadDiffToDokimionButton);
            panelHandleDifferences.Controls.Add(HandleDiffClearAllButton);
            panelHandleDifferences.Controls.Add(HandleDiffSelectAllButton);
            panelHandleDifferences.Location = new Point(11, 53);
            panelHandleDifferences.Name = "panelHandleDifferences";
            panelHandleDifferences.Size = new Size(758, 389);
            panelHandleDifferences.TabIndex = 0;
            panelHandleDifferences.Visible = false;
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer2.Location = new Point(1, 40);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(HandleDiffDataGridView);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(HandleDiffDiffViewer);
            splitContainer2.Size = new Size(757, 305);
            splitContainer2.SplitterDistance = 130;
            splitContainer2.TabIndex = 13;
            // 
            // HandleDiffDataGridView
            // 
            HandleDiffDataGridView.AllowUserToAddRows = false;
            HandleDiffDataGridView.AllowUserToDeleteRows = false;
            HandleDiffDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            HandleDiffDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            HandleDiffDataGridView.ColumnHeadersHeight = 29;
            HandleDiffDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewCheckBoxColumn1, dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            HandleDiffDataGridView.Location = new Point(-1, 0);
            HandleDiffDataGridView.Name = "HandleDiffDataGridView";
            HandleDiffDataGridView.RowHeadersVisible = false;
            HandleDiffDataGridView.RowHeadersWidth = 51;
            HandleDiffDataGridView.Size = new Size(754, 128);
            HandleDiffDataGridView.TabIndex = 12;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCheckBoxColumn1.HeaderText = "Select";
            dataGridViewCheckBoxColumn1.MinimumWidth = 6;
            dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "ID";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Name";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Notes";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 200;
            // 
            // HandleDiffDiffViewer
            // 
            HandleDiffDiffViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            HandleDiffDiffViewer.BorderColor = Color.FromArgb(0, 0, 0, 0);
            HandleDiffDiffViewer.BorderWidth = new Padding(0);
            HandleDiffDiffViewer.ChangeTypeForeColor = Color.FromArgb(128, 128, 128);
            HandleDiffDiffViewer.CollapseUnchangedSectionsToggleTitle = "_Collapse unchanged sections";
            HandleDiffDiffViewer.ContextLinesMenuItemsTitle = "_Lines for context";
            HandleDiffDiffViewer.DeletedBackColor = Color.FromArgb(64, 216, 32, 32);
            HandleDiffDiffViewer.DeletedForeColor = Color.FromArgb(0, 0, 0);
            HandleDiffDiffViewer.FontFamilyNames = "Segoe UI";
            HandleDiffDiffViewer.FontSize = 12D;
            HandleDiffDiffViewer.FontStretch = System.Windows.FontStretch.FromOpenTypeStretch(5);
            HandleDiffDiffViewer.FontWeight = 400;
            HandleDiffDiffViewer.HeaderBackColor = Color.FromArgb(12, 128, 128, 128);
            HandleDiffDiffViewer.HeaderForeColor = Color.FromArgb(128, 128, 128);
            HandleDiffDiffViewer.HeaderHeight = 0D;
            HandleDiffDiffViewer.IgnoreCase = false;
            HandleDiffDiffViewer.IgnoreUnchanged = false;
            HandleDiffDiffViewer.IgnoreWhiteSpace = true;
            HandleDiffDiffViewer.ImaginaryBackColor = Color.FromArgb(24, 128, 128, 128);
            HandleDiffDiffViewer.InlineModeToggleTitle = "_Unified view";
            HandleDiffDiffViewer.InsertedBackColor = Color.FromArgb(0, 0, 0);
            HandleDiffDiffViewer.InsertedForeColor = Color.FromArgb(0, 0, 0);
            HandleDiffDiffViewer.IsFontItalic = false;
            HandleDiffDiffViewer.IsSideBySide = true;
            HandleDiffDiffViewer.LineNumberForeColor = Color.FromArgb(64, 128, 160);
            HandleDiffDiffViewer.LineNumberWidth = 60;
            HandleDiffDiffViewer.LinesContext = 1;
            HandleDiffDiffViewer.Location = new Point(2, -3);
            HandleDiffDiffViewer.Margin = new Padding(3, 4, 3, 4);
            HandleDiffDiffViewer.Name = "HandleDiffDiffViewer";
            HandleDiffDiffViewer.NewText = null;
            HandleDiffDiffViewer.NewTextHeader = null;
            HandleDiffDiffViewer.OldText = null;
            HandleDiffDiffViewer.OldTextHeader = null;
            HandleDiffDiffViewer.SideBySideModeToggleTitle = "_Split view";
            HandleDiffDiffViewer.Size = new Size(752, 176);
            HandleDiffDiffViewer.SplitterBackColor = Color.FromArgb(64, 128, 128, 128);
            HandleDiffDiffViewer.SplitterBorderColor = Color.FromArgb(64, 128, 128, 128);
            HandleDiffDiffViewer.SplitterBorderWidth = new Padding(0);
            HandleDiffDiffViewer.SplitterWidth = 5D;
            HandleDiffDiffViewer.TabIndex = 14;
            HandleDiffDiffViewer.UnchangedBackColor = Color.FromArgb(0, 0, 0, 0);
            HandleDiffDiffViewer.UnchangedForeColor = Color.FromArgb(0, 0, 0);
            // 
            // HandleDiffRescanTestCasesButton
            // 
            HandleDiffRescanTestCasesButton.Location = new Point(206, 7);
            HandleDiffRescanTestCasesButton.Name = "HandleDiffRescanTestCasesButton";
            HandleDiffRescanTestCasesButton.Size = new Size(152, 29);
            HandleDiffRescanTestCasesButton.TabIndex = 12;
            HandleDiffRescanTestCasesButton.Text = "Rescan Test Cases";
            HandleDiffRescanTestCasesButton.UseVisualStyleBackColor = true;
            HandleDiffRescanTestCasesButton.Click += HandleDiffRescanTestCasesButton_Click;
            // 
            // HandleDiffProgressBar
            // 
            HandleDiffProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            HandleDiffProgressBar.Location = new Point(344, 352);
            HandleDiffProgressBar.Name = "HandleDiffProgressBar";
            HandleDiffProgressBar.Size = new Size(410, 29);
            HandleDiffProgressBar.TabIndex = 10;
            // 
            // UploadDiffToDokimionButton
            // 
            UploadDiffToDokimionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            UploadDiffToDokimionButton.Location = new Point(11, 353);
            UploadDiffToDokimionButton.Name = "UploadDiffToDokimionButton";
            UploadDiffToDokimionButton.Size = new Size(327, 29);
            UploadDiffToDokimionButton.TabIndex = 9;
            UploadDiffToDokimionButton.Text = "Upload Selected Test Cases to Dokimion";
            UploadDiffToDokimionButton.UseVisualStyleBackColor = true;
            UploadDiffToDokimionButton.Click += UploadDiffToDokimionButton_Click;
            // 
            // HandleDiffClearAllButton
            // 
            HandleDiffClearAllButton.Location = new Point(106, 7);
            HandleDiffClearAllButton.Name = "HandleDiffClearAllButton";
            HandleDiffClearAllButton.Size = new Size(94, 29);
            HandleDiffClearAllButton.TabIndex = 8;
            HandleDiffClearAllButton.Text = "Clear All";
            HandleDiffClearAllButton.UseVisualStyleBackColor = true;
            HandleDiffClearAllButton.Click += HandleDiffClearAllButton_Click;
            // 
            // HandleDiffSelectAllButton
            // 
            HandleDiffSelectAllButton.Location = new Point(7, 7);
            HandleDiffSelectAllButton.Name = "HandleDiffSelectAllButton";
            HandleDiffSelectAllButton.Size = new Size(94, 29);
            HandleDiffSelectAllButton.TabIndex = 7;
            HandleDiffSelectAllButton.Text = "Select All";
            HandleDiffSelectAllButton.UseVisualStyleBackColor = true;
            HandleDiffSelectAllButton.Click += HandleDiffSelectAllButton_Click;
            // 
            // panelDone
            // 
            panelDone.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDone.Controls.Add(label7);
            panelDone.Location = new Point(11, 53);
            panelDone.Name = "panelDone";
            panelDone.Size = new Size(758, 389);
            panelDone.TabIndex = 0;
            panelDone.Visible = false;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 16F);
            label7.Location = new Point(295, 165);
            label7.Name = "label7";
            label7.Size = new Size(89, 37);
            label7.TabIndex = 0;
            label7.Text = "Done!";
            // 
            // Updater
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 553);
            Controls.Add(panelDownloadChangedMetadata);
            Controls.Add(panelHandleDifferences);
            Controls.Add(panelDownloadNewTestCases);
            Controls.Add(panelDone);
            Controls.Add(panelSelectRepo);
            Controls.Add(panelLogin);
            Controls.Add(panelSelectProject);
            Controls.Add(FeedbackTextBox);
            Controls.Add(StepTextBox);
            Controls.Add(QuitButton);
            Controls.Add(NextButton);
            Controls.Add(PrevButton);
            Name = "Updater";
            Text = "Updater";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelDownloadChangedMetadata.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ChangedMetadataDataGridView).EndInit();
            panelSelectProject.ResumeLayout(false);
            panelSelectProject.PerformLayout();
            panelSelectRepo.ResumeLayout(false);
            panelSelectRepo.PerformLayout();
            panelDownloadNewTestCases.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NewTestCasesDataGridView).EndInit();
            panelHandleDifferences.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)HandleDiffDataGridView).EndInit();
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
        public ProgressBar ChangedMetadataProgressBar;
        private Button DownloadChangedMetadataButton;
        private Button ClearAllChangedMetadataButton;
        private Button SelectAllChangedMetadataButton;
        private DataGridViewCheckBoxColumn MetadataSelect;
        private DataGridViewTextBoxColumn MetadataID;
        private DataGridViewTextBoxColumn MetadataName;
        private DataGridViewTextBoxColumn Issue;
        public ProgressBar HandleDiffProgressBar;
        private Button UploadDiffToDokimionButton;
        private Button HandleDiffClearAllButton;
        private Button HandleDiffSelectAllButton;
        private Button HandleDiffRescanTestCasesButton;
        private SplitContainer splitContainer1;
        public DataGridView ChangedMetadataDataGridView;
        private DataGridViewCheckBoxColumn NewTestCaseSelect;
        private DataGridViewTextBoxColumn NewTestCaseId;
        private DataGridViewTextBoxColumn NewTestCaseName;
        private DataGridViewTextBoxColumn NewTestCaseNotes;
        public DiffPlex.WindowsForms.Controls.DiffViewer ChangedMetadataDiffViewer;
        private SplitContainer splitContainer2;
        public DataGridView HandleDiffDataGridView;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        public DiffPlex.WindowsForms.Controls.DiffViewer HandleDiffDiffViewer;
    }
}
