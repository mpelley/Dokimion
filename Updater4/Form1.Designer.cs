namespace Updater4
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
            LoginButton = new Button();
            TestCaseDataGridView = new DataGridView();
            TitleInDokimion = new DataGridViewTextBoxColumn();
            ID = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            Selected = new DataGridViewCheckBoxColumn();
            TitleInFileSystem = new DataGridViewTextBoxColumn();
            DokimionToFsButton = new Button();
            groupBox1 = new GroupBox();
            FolderTextBox = new TextBox();
            BrowseFileSystemButton = new Button();
            FsToDokimionButton = new Button();
            SelectAllButton = new Button();
            ClearAllButton = new Button();
            ShowDiffsButton = new Button();
            QuitButton = new Button();
            StatusTextBox = new TextBox();
            ProjectsListBox = new ListBox();
            GetTestCasesButton = new Button();
            FilterListBox = new ListBox();
            ProgressBar = new ProgressBar();
            label1 = new Label();
            label2 = new Label();
            ServerTextBox = new TextBox();
            groupBox2 = new GroupBox();
            SaveProjectButton = new Button();
            RestoreProjectButton = new Button();
            button1 = new Button();
            DeleteEmptyButton = new Button();
            ((System.ComponentModel.ISupportInitialize)TestCaseDataGridView).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
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
            TestCaseDataGridView.Columns.AddRange(new DataGridViewColumn[] { TitleInDokimion, ID, Status, Selected, TitleInFileSystem });
            TestCaseDataGridView.Location = new Point(25, 300);
            TestCaseDataGridView.Name = "TestCaseDataGridView";
            TestCaseDataGridView.RowHeadersVisible = false;
            TestCaseDataGridView.RowHeadersWidth = 51;
            TestCaseDataGridView.Size = new Size(869, 149);
            TestCaseDataGridView.TabIndex = 20;
            // 
            // TitleInDokimion
            // 
            TitleInDokimion.HeaderText = "Title in Dokimion";
            TitleInDokimion.MinimumWidth = 100;
            TitleInDokimion.Name = "TitleInDokimion";
            TitleInDokimion.ReadOnly = true;
            TitleInDokimion.Width = 300;
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
            // Selected
            // 
            Selected.HeaderText = "Select";
            Selected.MinimumWidth = 20;
            Selected.Name = "Selected";
            Selected.Width = 60;
            // 
            // TitleInFileSystem
            // 
            TitleInFileSystem.HeaderText = "Title in File System";
            TitleInFileSystem.MinimumWidth = 100;
            TitleInFileSystem.Name = "TitleInFileSystem";
            TitleInFileSystem.ReadOnly = true;
            TitleInFileSystem.Width = 300;
            // 
            // DokimionToFsButton
            // 
            DokimionToFsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DokimionToFsButton.Location = new Point(34, 535);
            DokimionToFsButton.Name = "DokimionToFsButton";
            DokimionToFsButton.Size = new Size(152, 53);
            DokimionToFsButton.TabIndex = 21;
            DokimionToFsButton.Text = "Dokimion to File System";
            DokimionToFsButton.UseVisualStyleBackColor = true;
            DokimionToFsButton.Click += DokimionToFsButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(FolderTextBox);
            groupBox1.Controls.Add(BrowseFileSystemButton);
            groupBox1.Location = new Point(481, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(354, 95);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "GitHub Repo";
            // 
            // FolderTextBox
            // 
            FolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FolderTextBox.Location = new Point(18, 58);
            FolderTextBox.Name = "FolderTextBox";
            FolderTextBox.ReadOnly = true;
            FolderTextBox.Size = new Size(330, 27);
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
            // FsToDokimionButton
            // 
            FsToDokimionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            FsToDokimionButton.Location = new Point(382, 535);
            FsToDokimionButton.Name = "FsToDokimionButton";
            FsToDokimionButton.Size = new Size(152, 53);
            FsToDokimionButton.TabIndex = 26;
            FsToDokimionButton.Text = "File System to Dokimion";
            FsToDokimionButton.UseVisualStyleBackColor = true;
            FsToDokimionButton.Click += FsToDokimionButton_Click;
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(25, 249);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(89, 38);
            SelectAllButton.TabIndex = 27;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Location = new Point(120, 249);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(89, 38);
            ClearAllButton.TabIndex = 28;
            ClearAllButton.Text = "Clear All";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // ShowDiffsButton
            // 
            ShowDiffsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ShowDiffsButton.Location = new Point(210, 535);
            ShowDiffsButton.Name = "ShowDiffsButton";
            ShowDiffsButton.Size = new Size(152, 53);
            ShowDiffsButton.TabIndex = 29;
            ShowDiffsButton.Text = "Show Test Case Differences";
            ShowDiffsButton.UseVisualStyleBackColor = true;
            ShowDiffsButton.Click += ShowDiffsButton_Click;
            // 
            // QuitButton
            // 
            QuitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            QuitButton.Location = new Point(742, 535);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(152, 53);
            QuitButton.TabIndex = 30;
            QuitButton.Text = "Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // StatusTextBox
            // 
            StatusTextBox.AcceptsReturn = true;
            StatusTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StatusTextBox.Location = new Point(25, 455);
            StatusTextBox.Multiline = true;
            StatusTextBox.Name = "StatusTextBox";
            StatusTextBox.ReadOnly = true;
            StatusTextBox.ScrollBars = ScrollBars.Both;
            StatusTextBox.Size = new Size(869, 64);
            StatusTextBox.TabIndex = 2;
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.Location = new Point(25, 139);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(348, 104);
            ProjectsListBox.TabIndex = 31;
            ProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // GetTestCasesButton
            // 
            GetTestCasesButton.Location = new Point(399, 131);
            GetTestCasesButton.Name = "GetTestCasesButton";
            GetTestCasesButton.Size = new Size(201, 53);
            GetTestCasesButton.TabIndex = 32;
            GetTestCasesButton.Text = "Compare Test Cases in Project";
            GetTestCasesButton.UseVisualStyleBackColor = true;
            GetTestCasesButton.Click += CompareButton_Click;
            // 
            // FilterListBox
            // 
            FilterListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FilterListBox.FormattingEnabled = true;
            FilterListBox.Location = new Point(627, 139);
            FilterListBox.Name = "FilterListBox";
            FilterListBox.Size = new Size(267, 104);
            FilterListBox.TabIndex = 33;
            FilterListBox.SelectedIndexChanged += FilterListBox_SelectedIndexChanged;
            // 
            // ProgressBar
            // 
            ProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProgressBar.Location = new Point(215, 249);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(679, 45);
            ProgressBar.Step = 1;
            ProgressBar.Style = ProgressBarStyle.Continuous;
            ProgressBar.TabIndex = 34;
            ProgressBar.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 110);
            label1.Name = "label1";
            label1.Size = new Size(154, 20);
            label1.TabIndex = 35;
            label1.Text = "Projects on Dokimion:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(627, 114);
            label2.Name = "label2";
            label2.Size = new Size(110, 20);
            label2.TabIndex = 36;
            label2.Text = "Test Case Filter:";
            // 
            // ServerTextBox
            // 
            ServerTextBox.Location = new Point(13, 55);
            ServerTextBox.Name = "ServerTextBox";
            ServerTextBox.ReadOnly = true;
            ServerTextBox.Size = new Size(401, 27);
            ServerTextBox.TabIndex = 37;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ServerTextBox);
            groupBox2.Controls.Add(LoginButton);
            groupBox2.Location = new Point(21, 15);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(433, 92);
            groupBox2.TabIndex = 38;
            groupBox2.TabStop = false;
            groupBox2.Text = "Dokimion Server";
            // 
            // SaveProjectButton
            // 
            SaveProjectButton.Location = new Point(379, 190);
            SaveProjectButton.Name = "SaveProjectButton";
            SaveProjectButton.Size = new Size(96, 53);
            SaveProjectButton.TabIndex = 39;
            SaveProjectButton.Text = "Save Project Info";
            SaveProjectButton.UseVisualStyleBackColor = true;
            SaveProjectButton.Click += SaveProjectButton_Click;
            // 
            // RestoreProjectButton
            // 
            RestoreProjectButton.Location = new Point(492, 190);
            RestoreProjectButton.Name = "RestoreProjectButton";
            RestoreProjectButton.Size = new Size(129, 53);
            RestoreProjectButton.TabIndex = 40;
            RestoreProjectButton.Text = "Create Project From Info";
            RestoreProjectButton.UseVisualStyleBackColor = true;
            RestoreProjectButton.Click += RestoreProjectButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(198, 105);
            button1.Name = "button1";
            button1.Size = new Size(175, 30);
            button1.TabIndex = 41;
            button1.Text = "Refresh Project List";
            button1.UseVisualStyleBackColor = true;
            button1.Click += RefreshButton_Click;
            // 
            // DeleteEmptyButton
            // 
            DeleteEmptyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DeleteEmptyButton.Location = new Point(554, 535);
            DeleteEmptyButton.Name = "DeleteEmptyButton";
            DeleteEmptyButton.Size = new Size(110, 53);
            DeleteEmptyButton.TabIndex = 42;
            DeleteEmptyButton.Text = "Delete Empty Test Cases";
            DeleteEmptyButton.UseVisualStyleBackColor = true;
            DeleteEmptyButton.Click += DeleteEmptyButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = QuitButton;
            ClientSize = new Size(912, 611);
            Controls.Add(DeleteEmptyButton);
            Controls.Add(button1);
            Controls.Add(RestoreProjectButton);
            Controls.Add(SaveProjectButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ProgressBar);
            Controls.Add(FilterListBox);
            Controls.Add(GetTestCasesButton);
            Controls.Add(ProjectsListBox);
            Controls.Add(StatusTextBox);
            Controls.Add(QuitButton);
            Controls.Add(ShowDiffsButton);
            Controls.Add(ClearAllButton);
            Controls.Add(SelectAllButton);
            Controls.Add(FsToDokimionButton);
            Controls.Add(groupBox1);
            Controls.Add(DokimionToFsButton);
            Controls.Add(TestCaseDataGridView);
            Controls.Add(groupBox2);
            Name = "Form1";
            Text = "Dokimion Test Case Maintenance";
            ((System.ComponentModel.ISupportInitialize)TestCaseDataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button LoginButton;
        private DataGridView TestCaseDataGridView;
        private Button DokimionToFsButton;
        private GroupBox groupBox1;
        private TextBox FolderTextBox;
        private Button BrowseFileSystemButton;
        private Button FsToDokimionButton;
        private Button SelectAllButton;
        private Button ClearAllButton;
        private Button ShowDiffsButton;
        private Button QuitButton;
        private TextBox StatusTextBox;
        private ListBox ProjectsListBox;
        private Button GetTestCasesButton;
        private ListBox FilterListBox;
        private DataGridViewTextBoxColumn TitleInDokimion;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewCheckBoxColumn Selected;
        private DataGridViewTextBoxColumn TitleInFileSystem;
        private ProgressBar ProgressBar;
        private Label label1;
        private Label label2;
        private TextBox ServerTextBox;
        private GroupBox groupBox2;
        private Button SaveProjectButton;
        private Button RestoreProjectButton;
        private Button button1;
        private Button DeleteEmptyButton;
    }
}
