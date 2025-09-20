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
            panelSendNewTestCases = new Panel();
            label5 = new Label();
            panelSelectProject = new Panel();
            ProjectsListBox = new ListBox();
            label2 = new Label();
            panelSelectRepo = new Panel();
            FolderForAllProjectsCheckBox = new CheckBox();
            FolderTextBox = new TextBox();
            BrowseFileSystemButton = new Button();
            label3 = new Label();
            panelDownloadMetadata = new Panel();
            RescanButton = new Button();
            ClearAllButton = new Button();
            SelectAllButton = new Button();
            MetadataDiffViewer = new DiffPlex.WindowsForms.Controls.DiffViewer();
            DownloadMetadataButton = new Button();
            MetadataDataGridView = new DataGridView();
            MetadataSelect = new DataGridViewCheckBoxColumn();
            MetadataID = new DataGridViewTextBoxColumn();
            MetadataName = new DataGridViewTextBoxColumn();
            MetadataIssue = new DataGridViewTextBoxColumn();
            MetadataProgressBar = new ProgressBar();
            panelHandleDifferences = new Panel();
            label6 = new Label();
            panelDone = new Panel();
            label7 = new Label();
            panelLogin.SuspendLayout();
            panelSendNewTestCases.SuspendLayout();
            panelSelectProject.SuspendLayout();
            panelSelectRepo.SuspendLayout();
            panelDownloadMetadata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MetadataDataGridView).BeginInit();
            panelHandleDifferences.SuspendLayout();
            panelDone.SuspendLayout();
            SuspendLayout();
            // 
            // PrevButton
            // 
            PrevButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PrevButton.Location = new Point(12, 512);
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
            QuitButton.Location = new Point(676, 512);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(94, 29);
            QuitButton.TabIndex = 2;
            QuitButton.Text = "Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // StepTextBox
            // 
            StepTextBox.Location = new Point(12, 12);
            StepTextBox.Name = "StepTextBox";
            StepTextBox.ReadOnly = true;
            StepTextBox.Size = new Size(337, 27);
            StepTextBox.TabIndex = 3;
            // 
            // StepProgressBar
            // 
            StepProgressBar.Location = new Point(390, 12);
            StepProgressBar.Name = "StepProgressBar";
            StepProgressBar.Size = new Size(380, 29);
            StepProgressBar.TabIndex = 4;
            // 
            // FeedbackTextBox
            // 
            FeedbackTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FeedbackTextBox.Location = new Point(12, 449);
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
            panelLogin.Location = new Point(12, 54);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(758, 389);
            panelLogin.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(141, 138);
            label8.Name = "label8";
            label8.Size = new Size(110, 20);
            label8.TabIndex = 29;
            label8.Text = "Server Address:";
            // 
            // UrlTextBox
            // 
            UrlTextBox.Location = new Point(257, 135);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.Size = new Size(464, 27);
            UrlTextBox.TabIndex = 22;
            // 
            // UseHttpsCheckBox
            // 
            UseHttpsCheckBox.AutoSize = true;
            UseHttpsCheckBox.Location = new Point(44, 136);
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
            label9.Location = new Point(354, 170);
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
            LoginButton.Location = new Point(49, 214);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(91, 39);
            LoginButton.TabIndex = 27;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(431, 170);
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
            // panelSendNewTestCases
            // 
            panelSendNewTestCases.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSendNewTestCases.Controls.Add(label5);
            panelSendNewTestCases.Location = new Point(12, 54);
            panelSendNewTestCases.Name = "panelSendNewTestCases";
            panelSendNewTestCases.Size = new Size(758, 389);
            panelSendNewTestCases.TabIndex = 0;
            panelSendNewTestCases.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 17);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 0;
            label5.Text = "label5";
            // 
            // panelSelectProject
            // 
            panelSelectProject.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSelectProject.Controls.Add(ProjectsListBox);
            panelSelectProject.Controls.Add(label2);
            panelSelectProject.Location = new Point(12, 54);
            panelSelectProject.Name = "panelSelectProject";
            panelSelectProject.Size = new Size(758, 389);
            panelSelectProject.TabIndex = 0;
            panelSelectProject.Visible = false;
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.Location = new Point(16, 58);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(736, 324);
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
            panelSelectRepo.Location = new Point(12, 54);
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
            label3.Location = new Point(35, 58);
            label3.Name = "label3";
            label3.Size = new Size(295, 20);
            label3.TabIndex = 0;
            label3.Text = "Select the folder holding the repo or repos.";
            // 
            // panelDownloadMetadata
            // 
            panelDownloadMetadata.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDownloadMetadata.Controls.Add(RescanButton);
            panelDownloadMetadata.Controls.Add(ClearAllButton);
            panelDownloadMetadata.Controls.Add(SelectAllButton);
            panelDownloadMetadata.Controls.Add(MetadataDiffViewer);
            panelDownloadMetadata.Controls.Add(DownloadMetadataButton);
            panelDownloadMetadata.Controls.Add(MetadataDataGridView);
            panelDownloadMetadata.Controls.Add(MetadataProgressBar);
            panelDownloadMetadata.Location = new Point(12, 54);
            panelDownloadMetadata.Name = "panelDownloadMetadata";
            panelDownloadMetadata.Size = new Size(758, 389);
            panelDownloadMetadata.TabIndex = 0;
            panelDownloadMetadata.Visible = false;
            // 
            // RescanButton
            // 
            RescanButton.Location = new Point(253, 22);
            RescanButton.Name = "RescanButton";
            RescanButton.Size = new Size(131, 29);
            RescanButton.TabIndex = 7;
            RescanButton.Text = "Rescan test cases";
            RescanButton.UseVisualStyleBackColor = true;
            RescanButton.Click += RescanButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Location = new Point(135, 20);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(94, 29);
            ClearAllButton.TabIndex = 6;
            ClearAllButton.Text = "Clear All";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(16, 21);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(94, 29);
            SelectAllButton.TabIndex = 5;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // MetadataDiffViewer
            // 
            MetadataDiffViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MetadataDiffViewer.BorderColor = Color.FromArgb(0, 0, 0, 0);
            MetadataDiffViewer.BorderWidth = new Padding(0);
            MetadataDiffViewer.ChangeTypeForeColor = Color.FromArgb(128, 128, 128);
            MetadataDiffViewer.CollapseUnchangedSectionsToggleTitle = "_Collapse unchanged sections";
            MetadataDiffViewer.ContextLinesMenuItemsTitle = "_Lines for context";
            MetadataDiffViewer.DeletedBackColor = Color.FromArgb(64, 216, 32, 32);
            MetadataDiffViewer.DeletedForeColor = Color.FromArgb(0, 0, 0);
            MetadataDiffViewer.FontFamilyNames = "Segoe UI";
            MetadataDiffViewer.FontSize = 12D;
            MetadataDiffViewer.FontStretch = System.Windows.FontStretch.FromOpenTypeStretch(5);
            MetadataDiffViewer.FontWeight = 400;
            MetadataDiffViewer.HeaderBackColor = Color.FromArgb(12, 128, 128, 128);
            MetadataDiffViewer.HeaderForeColor = Color.FromArgb(128, 128, 128);
            MetadataDiffViewer.HeaderHeight = 20D;
            MetadataDiffViewer.IgnoreCase = false;
            MetadataDiffViewer.IgnoreUnchanged = false;
            MetadataDiffViewer.IgnoreWhiteSpace = true;
            MetadataDiffViewer.ImaginaryBackColor = Color.FromArgb(24, 128, 128, 128);
            MetadataDiffViewer.InlineModeToggleTitle = "_Unified view";
            MetadataDiffViewer.InsertedBackColor = Color.FromArgb(0, 0, 0);
            MetadataDiffViewer.InsertedForeColor = Color.FromArgb(0, 0, 0);
            MetadataDiffViewer.IsFontItalic = false;
            MetadataDiffViewer.IsSideBySide = true;
            MetadataDiffViewer.LineNumberForeColor = Color.FromArgb(64, 128, 160);
            MetadataDiffViewer.LineNumberWidth = 60;
            MetadataDiffViewer.LinesContext = 1;
            MetadataDiffViewer.Location = new Point(16, 193);
            MetadataDiffViewer.Name = "MetadataDiffViewer";
            MetadataDiffViewer.NewText = null;
            MetadataDiffViewer.NewTextHeader = "Dokimion";
            MetadataDiffViewer.OldText = null;
            MetadataDiffViewer.OldTextHeader = "File System";
            MetadataDiffViewer.SideBySideModeToggleTitle = "_Split view";
            MetadataDiffViewer.Size = new Size(736, 154);
            MetadataDiffViewer.SplitterBackColor = Color.FromArgb(64, 128, 128, 128);
            MetadataDiffViewer.SplitterBorderColor = Color.FromArgb(64, 128, 128, 128);
            MetadataDiffViewer.SplitterBorderWidth = new Padding(0);
            MetadataDiffViewer.SplitterWidth = 5D;
            MetadataDiffViewer.TabIndex = 4;
            MetadataDiffViewer.UnchangedBackColor = Color.FromArgb(64, 128, 128, 128);
            MetadataDiffViewer.UnchangedForeColor = Color.FromArgb(0, 0, 0);
            // 
            // DownloadMetadataButton
            // 
            DownloadMetadataButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DownloadMetadataButton.Location = new Point(14, 354);
            DownloadMetadataButton.Name = "DownloadMetadataButton";
            DownloadMetadataButton.Size = new Size(252, 29);
            DownloadMetadataButton.TabIndex = 3;
            DownloadMetadataButton.Text = "Download selected metadata files";
            DownloadMetadataButton.UseVisualStyleBackColor = true;
            DownloadMetadataButton.Click += DownloadMetadataButton_Click;
            // 
            // MetadataDataGridView
            // 
            MetadataDataGridView.AllowUserToAddRows = false;
            MetadataDataGridView.AllowUserToDeleteRows = false;
            MetadataDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            MetadataDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MetadataDataGridView.Columns.AddRange(new DataGridViewColumn[] { MetadataSelect, MetadataID, MetadataName, MetadataIssue });
            MetadataDataGridView.Location = new Point(13, 58);
            MetadataDataGridView.Name = "MetadataDataGridView";
            MetadataDataGridView.RowHeadersWidth = 51;
            MetadataDataGridView.Size = new Size(739, 129);
            MetadataDataGridView.TabIndex = 2;
            MetadataDataGridView.RowHeaderMouseClick += MetadataDataGridView_RowHeaderMouseClick;
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
            // MetadataIssue
            // 
            MetadataIssue.HeaderText = "Issue";
            MetadataIssue.MinimumWidth = 6;
            MetadataIssue.Name = "MetadataIssue";
            MetadataIssue.ReadOnly = true;
            MetadataIssue.Width = 125;
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
            panelHandleDifferences.Controls.Add(label6);
            panelHandleDifferences.Location = new Point(12, 54);
            panelHandleDifferences.Name = "panelHandleDifferences";
            panelHandleDifferences.Size = new Size(758, 389);
            panelHandleDifferences.TabIndex = 0;
            panelHandleDifferences.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 17);
            label6.Name = "label6";
            label6.Size = new Size(50, 20);
            label6.TabIndex = 0;
            label6.Text = "label6";
            // 
            // panelDone
            // 
            panelDone.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDone.Controls.Add(label7);
            panelDone.Location = new Point(12, 54);
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
            Controls.Add(panelDownloadMetadata);
            Controls.Add(panelSelectRepo);
            Controls.Add(panelLogin);
            Controls.Add(panelSelectProject);
            Controls.Add(panelSendNewTestCases);
            Controls.Add(panelHandleDifferences);
            Controls.Add(panelDone);
            Controls.Add(FeedbackTextBox);
            Controls.Add(StepProgressBar);
            Controls.Add(StepTextBox);
            Controls.Add(QuitButton);
            Controls.Add(NextButton);
            Controls.Add(PrevButton);
            Name = "Updater";
            Text = "Updater";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelSendNewTestCases.ResumeLayout(false);
            panelSendNewTestCases.PerformLayout();
            panelSelectProject.ResumeLayout(false);
            panelSelectProject.PerformLayout();
            panelSelectRepo.ResumeLayout(false);
            panelSelectRepo.PerformLayout();
            panelDownloadMetadata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MetadataDataGridView).EndInit();
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
        private StepDownloadMetadata StepDownloadMetadata;
        private StepSendNewTestCases StepSendNewTestCases;
        private StepHandleDifferences StepHandleDifferences;
        private StepDone StepDone;

        private StepCode ActiveStepCode;

        private Panel panelLogin;

        private Panel panelSelectProject;

        private Panel panelSelectRepo;
        private Label label3;

        private Panel panelDownloadMetadata;

        private Panel panelSendNewTestCases;
        private Label label5;

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
        public DataGridView MetadataDataGridView;
        private Button DownloadMetadataButton;
        public DiffPlex.WindowsForms.Controls.DiffViewer MetadataDiffViewer;
        private Button ClearAllButton;
        private Button SelectAllButton;
        private Button RescanButton;
        private DataGridViewCheckBoxColumn MetadataSelect;
        private DataGridViewTextBoxColumn MetadataID;
        private DataGridViewTextBoxColumn MetadataName;
        private DataGridViewTextBoxColumn MetadataIssue;
    }
}
