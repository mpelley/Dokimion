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
            CompareButton = new Button();
            FilterListBox = new ListBox();
            ProgressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)TestCaseDataGridView).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(25, 12);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(218, 37);
            LoginButton.TabIndex = 17;
            LoginButton.Text = "Log into Dokimion Server";
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
            TestCaseDataGridView.Location = new Point(25, 223);
            TestCaseDataGridView.Name = "TestCaseDataGridView";
            TestCaseDataGridView.RowHeadersVisible = false;
            TestCaseDataGridView.RowHeadersWidth = 51;
            TestCaseDataGridView.Size = new Size(875, 221);
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
            DokimionToFsButton.Location = new Point(34, 531);
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
            groupBox1.Location = new Point(263, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(637, 85);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "GitHub Repo";
            // 
            // FolderTextBox
            // 
            FolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FolderTextBox.Location = new Point(14, 53);
            FolderTextBox.Name = "FolderTextBox";
            FolderTextBox.Size = new Size(617, 27);
            FolderTextBox.TabIndex = 1;
            // 
            // BrowseFileSystemButton
            // 
            BrowseFileSystemButton.Location = new Point(10, 19);
            BrowseFileSystemButton.Name = "BrowseFileSystemButton";
            BrowseFileSystemButton.Size = new Size(94, 29);
            BrowseFileSystemButton.TabIndex = 0;
            BrowseFileSystemButton.Text = "Browse";
            BrowseFileSystemButton.UseVisualStyleBackColor = true;
            BrowseFileSystemButton.Click += BrowseFileSystemButton_Click;
            // 
            // FsToDokimionButton
            // 
            FsToDokimionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            FsToDokimionButton.Location = new Point(382, 531);
            FsToDokimionButton.Name = "FsToDokimionButton";
            FsToDokimionButton.Size = new Size(152, 53);
            FsToDokimionButton.TabIndex = 26;
            FsToDokimionButton.Text = "File System to Dokimion";
            FsToDokimionButton.UseVisualStyleBackColor = true;
            FsToDokimionButton.Click += FsToDokimionButton_Click;
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(410, 103);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(89, 40);
            SelectAllButton.TabIndex = 27;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Location = new Point(505, 103);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(89, 40);
            ClearAllButton.TabIndex = 28;
            ClearAllButton.Text = "Clear All";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // ShowDiffsButton
            // 
            ShowDiffsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ShowDiffsButton.Location = new Point(210, 531);
            ShowDiffsButton.Name = "ShowDiffsButton";
            ShowDiffsButton.Size = new Size(152, 53);
            ShowDiffsButton.TabIndex = 29;
            ShowDiffsButton.Text = "Show Differences";
            ShowDiffsButton.UseVisualStyleBackColor = true;
            ShowDiffsButton.Click += ShowDiffsButton_Click;
            // 
            // QuitButton
            // 
            QuitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            QuitButton.Location = new Point(747, 531);
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
            StatusTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            StatusTextBox.Location = new Point(25, 451);
            StatusTextBox.Multiline = true;
            StatusTextBox.Name = "StatusTextBox";
            StatusTextBox.ReadOnly = true;
            StatusTextBox.ScrollBars = ScrollBars.Both;
            StatusTextBox.Size = new Size(875, 64);
            StatusTextBox.TabIndex = 2;
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.Location = new Point(25, 103);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(271, 104);
            ProjectsListBox.TabIndex = 31;
            // 
            // CompareButton
            // 
            CompareButton.Location = new Point(316, 103);
            CompareButton.Name = "CompareButton";
            CompareButton.Size = new Size(89, 40);
            CompareButton.TabIndex = 32;
            CompareButton.Text = "Compare";
            CompareButton.UseVisualStyleBackColor = true;
            CompareButton.Click += CompareButton_Click;
            // 
            // FilterListBox
            // 
            FilterListBox.FormattingEnabled = true;
            FilterListBox.Location = new Point(627, 103);
            FilterListBox.Name = "FilterListBox";
            FilterListBox.Size = new Size(237, 104);
            FilterListBox.TabIndex = 33;
            FilterListBox.SelectedIndexChanged += FilterListBox_SelectedIndexChanged;
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(316, 162);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(278, 45);
            ProgressBar.Step = 1;
            ProgressBar.Style = ProgressBarStyle.Continuous;
            ProgressBar.TabIndex = 34;
            ProgressBar.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = QuitButton;
            ClientSize = new Size(912, 607);
            Controls.Add(ProgressBar);
            Controls.Add(FilterListBox);
            Controls.Add(CompareButton);
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
            Controls.Add(LoginButton);
            Name = "Form1";
            Text = "Updater4";
            ((System.ComponentModel.ISupportInitialize)TestCaseDataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private Button CompareButton;
        private ListBox FilterListBox;
        private DataGridViewTextBoxColumn TitleInDokimion;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewCheckBoxColumn Selected;
        private DataGridViewTextBoxColumn TitleInFileSystem;
        private ProgressBar ProgressBar;
    }
}
