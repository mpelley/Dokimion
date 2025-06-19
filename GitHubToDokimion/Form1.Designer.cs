namespace GitHubToDokimion
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            RefreshProjectListButton = new Button();
            label1 = new Label();
            groupBox2 = new GroupBox();
            DokimionProjectsListBox = new ListBox();
            ServerTextBox = new TextBox();
            LoginButton = new Button();
            groupBox1 = new GroupBox();
            FolderForAllProjectsCheckBox = new CheckBox();
            FolderTextBox = new TextBox();
            BrowseFileSystemButton = new Button();
            CompareTestCasesButton = new Button();
            ProgressBar = new ProgressBar();
            StatusTextBox = new TextBox();
            FilterListBox = new ListBox();
            FsToDokimionButton = new Button();
            QuitButton = new Button();
            splitContainer1 = new SplitContainer();
            TestCaseDataGridView = new DataGridView();
            TestCaseNameTextBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            diffViewer1 = new DiffPlex.WindowsForms.Controls.DiffViewer();
            TitleInFileSystem = new DataGridViewTextBoxColumn();
            ID = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            TitleInDokimion = new DataGridViewTextBoxColumn();
            FileName = new DataGridViewTextBoxColumn();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TestCaseDataGridView).BeginInit();
            SuspendLayout();
            // 
            // RefreshProjectListButton
            // 
            RefreshProjectListButton.Location = new Point(189, 101);
            RefreshProjectListButton.Name = "RefreshProjectListButton";
            RefreshProjectListButton.Size = new Size(175, 29);
            RefreshProjectListButton.TabIndex = 44;
            RefreshProjectListButton.Text = "Refresh Project List";
            RefreshProjectListButton.UseVisualStyleBackColor = true;
            RefreshProjectListButton.Click += RefreshProjectListButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 107);
            label1.Name = "label1";
            label1.Size = new Size(154, 20);
            label1.TabIndex = 42;
            label1.Text = "Projects on Dokimion:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(DokimionProjectsListBox);
            groupBox2.Controls.Add(ServerTextBox);
            groupBox2.Controls.Add(LoginButton);
            groupBox2.Location = new Point(11, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(433, 173);
            groupBox2.TabIndex = 43;
            groupBox2.TabStop = false;
            groupBox2.Text = "Dokimion Server";
            // 
            // DokimionProjectsListBox
            // 
            DokimionProjectsListBox.FormattingEnabled = true;
            DokimionProjectsListBox.Location = new Point(16, 129);
            DokimionProjectsListBox.Name = "DokimionProjectsListBox";
            DokimionProjectsListBox.Size = new Size(411, 24);
            DokimionProjectsListBox.TabIndex = 38;
            DokimionProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // ServerTextBox
            // 
            ServerTextBox.Location = new Point(13, 55);
            ServerTextBox.Name = "ServerTextBox";
            ServerTextBox.ReadOnly = true;
            ServerTextBox.Size = new Size(414, 27);
            ServerTextBox.TabIndex = 37;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(13, 20);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(218, 29);
            LoginButton.TabIndex = 17;
            LoginButton.Text = "Log into Dokimion Server...";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FolderForAllProjectsCheckBox);
            groupBox1.Controls.Add(FolderTextBox);
            groupBox1.Controls.Add(BrowseFileSystemButton);
            groupBox1.Location = new Point(11, 192);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(435, 95);
            groupBox1.TabIndex = 46;
            groupBox1.TabStop = false;
            groupBox1.Text = "GitHub Repo";
            // 
            // FolderForAllProjectsCheckBox
            // 
            FolderForAllProjectsCheckBox.AutoSize = true;
            FolderForAllProjectsCheckBox.Location = new Point(118, 27);
            FolderForAllProjectsCheckBox.Name = "FolderForAllProjectsCheckBox";
            FolderForAllProjectsCheckBox.Size = new Size(205, 24);
            FolderForAllProjectsCheckBox.TabIndex = 2;
            FolderForAllProjectsCheckBox.Text = "One Folder for All Projects";
            FolderForAllProjectsCheckBox.UseVisualStyleBackColor = true;
            FolderForAllProjectsCheckBox.CheckedChanged += FolderForAllProjectsCheckBox_CheckedChanged;
            // 
            // FolderTextBox
            // 
            FolderTextBox.Location = new Point(18, 59);
            FolderTextBox.Name = "FolderTextBox";
            FolderTextBox.ReadOnly = true;
            FolderTextBox.Size = new Size(409, 27);
            FolderTextBox.TabIndex = 1;
            // 
            // BrowseFileSystemButton
            // 
            BrowseFileSystemButton.Location = new Point(18, 23);
            BrowseFileSystemButton.Name = "BrowseFileSystemButton";
            BrowseFileSystemButton.Size = new Size(94, 29);
            BrowseFileSystemButton.TabIndex = 0;
            BrowseFileSystemButton.Text = "Browse...";
            BrowseFileSystemButton.UseVisualStyleBackColor = true;
            BrowseFileSystemButton.Click += BrowseFileSystemButton_Click;
            // 
            // CompareTestCasesButton
            // 
            CompareTestCasesButton.Location = new Point(30, 293);
            CompareTestCasesButton.Name = "CompareTestCasesButton";
            CompareTestCasesButton.Size = new Size(261, 32);
            CompareTestCasesButton.TabIndex = 33;
            CompareTestCasesButton.Text = "Compare Test Cases in Project";
            CompareTestCasesButton.UseVisualStyleBackColor = true;
            CompareTestCasesButton.Click += CompareTestCasesButton_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(11, 331);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(433, 45);
            ProgressBar.Step = 1;
            ProgressBar.Style = ProgressBarStyle.Continuous;
            ProgressBar.TabIndex = 48;
            ProgressBar.Visible = false;
            // 
            // StatusTextBox
            // 
            StatusTextBox.AcceptsReturn = true;
            StatusTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            StatusTextBox.Location = new Point(11, 389);
            StatusTextBox.Multiline = true;
            StatusTextBox.Name = "StatusTextBox";
            StatusTextBox.ReadOnly = true;
            StatusTextBox.ScrollBars = ScrollBars.Both;
            StatusTextBox.Size = new Size(433, 217);
            StatusTextBox.TabIndex = 47;
            // 
            // FilterListBox
            // 
            FilterListBox.FormattingEnabled = true;
            FilterListBox.Location = new Point(463, 17);
            FilterListBox.Name = "FilterListBox";
            FilterListBox.Size = new Size(207, 44);
            FilterListBox.TabIndex = 50;
            FilterListBox.SelectedIndexChanged += FilterListBox_SelectedIndexChanged;
            // 
            // FsToDokimionButton
            // 
            FsToDokimionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            FsToDokimionButton.Location = new Point(463, 555);
            FsToDokimionButton.Name = "FsToDokimionButton";
            FsToDokimionButton.Size = new Size(224, 53);
            FsToDokimionButton.TabIndex = 52;
            FsToDokimionButton.Text = "Send Test Case to Dokimion";
            FsToDokimionButton.UseVisualStyleBackColor = true;
            FsToDokimionButton.Click += FsToDokimionButton_Click;
            // 
            // QuitButton
            // 
            QuitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            QuitButton.Location = new Point(1129, 555);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(152, 53);
            QuitButton.TabIndex = 53;
            QuitButton.Text = "Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(459, 67);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(TestCaseDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(TestCaseNameTextBox);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(diffViewer1);
            splitContainer1.Size = new Size(829, 485);
            splitContainer1.SplitterDistance = 177;
            splitContainer1.TabIndex = 54;
            // 
            // TestCaseDataGridView
            // 
            TestCaseDataGridView.AllowUserToAddRows = false;
            TestCaseDataGridView.AllowUserToDeleteRows = false;
            TestCaseDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            TestCaseDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            TestCaseDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TestCaseDataGridView.Columns.AddRange(new DataGridViewColumn[] { TitleInFileSystem, ID, Status, TitleInDokimion, FileName });
            TestCaseDataGridView.Location = new Point(6, 11);
            TestCaseDataGridView.MultiSelect = false;
            TestCaseDataGridView.Name = "TestCaseDataGridView";
            TestCaseDataGridView.RowHeadersWidth = 51;
            TestCaseDataGridView.Size = new Size(818, 163);
            TestCaseDataGridView.TabIndex = 50;
            TestCaseDataGridView.CellClick += TestCaseDataGridView_CellClick;
            TestCaseDataGridView.RowHeaderMouseClick += TestCaseDataGridView_RowHeaderMouseClick;
            // 
            // TestCaseNameTextBox
            // 
            TestCaseNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TestCaseNameTextBox.Location = new Point(253, 4);
            TestCaseNameTextBox.Name = "TestCaseNameTextBox";
            TestCaseNameTextBox.ReadOnly = true;
            TestCaseNameTextBox.Size = new Size(359, 27);
            TestCaseNameTextBox.TabIndex = 55;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(747, 7);
            label3.Name = "label3";
            label3.Size = new Size(74, 20);
            label3.TabIndex = 54;
            label3.Text = "Dokimion";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 7);
            label2.Name = "label2";
            label2.Size = new Size(56, 20);
            label2.TabIndex = 53;
            label2.Text = "GitHub";
            // 
            // diffViewer1
            // 
            diffViewer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            diffViewer1.BorderColor = Color.FromArgb(0, 0, 0, 0);
            diffViewer1.BorderWidth = new Padding(0);
            diffViewer1.ChangeTypeForeColor = Color.FromArgb(128, 128, 128);
            diffViewer1.CollapseUnchangedSectionsToggleTitle = "_Collapse unchanged sections";
            diffViewer1.ContextLinesMenuItemsTitle = "_Lines for context";
            diffViewer1.DeletedBackColor = Color.FromArgb(64, 216, 32, 32);
            diffViewer1.DeletedForeColor = Color.FromArgb(0, 0, 0);
            diffViewer1.FontFamilyNames = "Segoe UI";
            diffViewer1.FontSize = 12D;
            diffViewer1.FontStretch = System.Windows.FontStretch.FromOpenTypeStretch(5);
            diffViewer1.FontWeight = 400;
            diffViewer1.HeaderBackColor = Color.FromArgb(12, 128, 128, 128);
            diffViewer1.HeaderForeColor = Color.FromArgb(128, 128, 128);
            diffViewer1.HeaderHeight = 0D;
            diffViewer1.IgnoreCase = false;
            diffViewer1.IgnoreUnchanged = false;
            diffViewer1.IgnoreWhiteSpace = true;
            diffViewer1.ImaginaryBackColor = Color.FromArgb(24, 128, 128, 128);
            diffViewer1.InlineModeToggleTitle = "_Unified view";
            diffViewer1.InsertedBackColor = Color.FromArgb(0, 0, 0);
            diffViewer1.InsertedForeColor = Color.FromArgb(0, 0, 0);
            diffViewer1.IsFontItalic = false;
            diffViewer1.IsSideBySide = true;
            diffViewer1.LineNumberForeColor = Color.FromArgb(64, 128, 160);
            diffViewer1.LineNumberWidth = 60;
            diffViewer1.LinesContext = 1;
            diffViewer1.Location = new Point(6, 35);
            diffViewer1.Margin = new Padding(3, 4, 3, 4);
            diffViewer1.Name = "diffViewer1";
            diffViewer1.NewText = null;
            diffViewer1.NewTextHeader = null;
            diffViewer1.OldText = null;
            diffViewer1.OldTextHeader = null;
            diffViewer1.SideBySideModeToggleTitle = "_Split view";
            diffViewer1.Size = new Size(818, 268);
            diffViewer1.SplitterBackColor = Color.FromArgb(64, 128, 128, 128);
            diffViewer1.SplitterBorderColor = Color.FromArgb(64, 128, 128, 128);
            diffViewer1.SplitterBorderWidth = new Padding(0);
            diffViewer1.SplitterWidth = 5D;
            diffViewer1.TabIndex = 52;
            diffViewer1.UnchangedBackColor = Color.FromArgb(12, 128, 128, 128);
            diffViewer1.UnchangedForeColor = Color.FromArgb(0, 0, 0);
            // 
            // TitleInFileSystem
            // 
            TitleInFileSystem.HeaderText = "Title in File System";
            TitleInFileSystem.MinimumWidth = 100;
            TitleInFileSystem.Name = "TitleInFileSystem";
            TitleInFileSystem.ReadOnly = true;
            TitleInFileSystem.Width = 300;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.MinimumWidth = 40;
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 60;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.MinimumWidth = 10;
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.Width = 60;
            // 
            // TitleInDokimion
            // 
            TitleInDokimion.HeaderText = "Title in Dokimion";
            TitleInDokimion.MinimumWidth = 100;
            TitleInDokimion.Name = "TitleInDokimion";
            TitleInDokimion.ReadOnly = true;
            TitleInDokimion.Width = 300;
            // 
            // FileName
            // 
            FileName.HeaderText = "FileName";
            FileName.MinimumWidth = 6;
            FileName.Name = "FileName";
            FileName.Visible = false;
            FileName.Width = 125;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = QuitButton;
            ClientSize = new Size(1293, 619);
            Controls.Add(QuitButton);
            Controls.Add(FsToDokimionButton);
            Controls.Add(FilterListBox);
            Controls.Add(ProgressBar);
            Controls.Add(StatusTextBox);
            Controls.Add(CompareTestCasesButton);
            Controls.Add(groupBox1);
            Controls.Add(RefreshProjectListButton);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "GitHub To Dokimion";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TestCaseDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button RefreshProjectListButton;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox ServerTextBox;
        private Button LoginButton;
        private GroupBox groupBox1;
        private CheckBox FolderForAllProjectsCheckBox;
        private TextBox FolderTextBox;
        private Button BrowseFileSystemButton;
        private Button CompareTestCasesButton;
        private ProgressBar ProgressBar;
        private TextBox StatusTextBox;
        private ListBox FilterListBox;
        private Button FsToDokimionButton;
        private Button QuitButton;
        private ListBox DokimionProjectsListBox;
        private SplitContainer splitContainer1;
        private DataGridView TestCaseDataGridView;
        private DiffPlex.WindowsForms.Controls.DiffViewer diffViewer1;
        private Label label3;
        private Label label2;
        private TextBox TestCaseNameTextBox;
        private DataGridViewTextBoxColumn TitleInFileSystem;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn TitleInDokimion;
        private DataGridViewTextBoxColumn FileName;
    }
}
