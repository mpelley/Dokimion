namespace Cloner
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
            ProjectsListBox = new ListBox();
            LoginButton = new Button();
            FilterTreeView = new TreeView();
            ApplyFiltersButton = new Button();
            DoTheCloneButton = new Button();
            StatusTextBox = new TextBox();
            label1 = new Label();
            NewProjectNameTextBox = new TextBox();
            QuitButton = new Button();
            TestcaseDataGridView = new DataGridView();
            SelectAllButton = new Button();
            ClearAllButton = new Button();
            ProgressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)TestcaseDataGridView).BeginInit();
            SuspendLayout();
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.ItemHeight = 20;
            ProjectsListBox.Location = new Point(9, 68);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(187, 144);
            ProjectsListBox.TabIndex = 0;
            ProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(17, 20);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(94, 29);
            LoginButton.TabIndex = 1;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // FilterTreeView
            // 
            FilterTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            FilterTreeView.CheckBoxes = true;
            FilterTreeView.Location = new Point(213, 68);
            FilterTreeView.Name = "FilterTreeView";
            FilterTreeView.Size = new Size(240, 148);
            FilterTreeView.TabIndex = 2;
            FilterTreeView.AfterCheck += FilterTreeView_AfterCheck;
            // 
            // ApplyFiltersButton
            // 
            ApplyFiltersButton.Enabled = false;
            ApplyFiltersButton.Location = new Point(475, 20);
            ApplyFiltersButton.Name = "ApplyFiltersButton";
            ApplyFiltersButton.Size = new Size(130, 29);
            ApplyFiltersButton.TabIndex = 5;
            ApplyFiltersButton.Text = "Apply Filters";
            ApplyFiltersButton.UseVisualStyleBackColor = true;
            ApplyFiltersButton.Click += ApplyFiltersButton_Click;
            // 
            // DoTheCloneButton
            // 
            DoTheCloneButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DoTheCloneButton.Enabled = false;
            DoTheCloneButton.Location = new Point(17, 294);
            DoTheCloneButton.Name = "DoTheCloneButton";
            DoTheCloneButton.Size = new Size(145, 29);
            DoTheCloneButton.TabIndex = 7;
            DoTheCloneButton.Text = "Do the Clone";
            DoTheCloneButton.UseVisualStyleBackColor = true;
            DoTheCloneButton.Click += DoTheCloneButton_Click;
            // 
            // StatusTextBox
            // 
            StatusTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StatusTextBox.Location = new Point(16, 341);
            StatusTextBox.MaxLength = 1000000;
            StatusTextBox.Multiline = true;
            StatusTextBox.Name = "StatusTextBox";
            StatusTextBox.ReadOnly = true;
            StatusTextBox.Size = new Size(942, 89);
            StatusTextBox.TabIndex = 8;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(17, 247);
            label1.Name = "label1";
            label1.Size = new Size(136, 20);
            label1.TabIndex = 10;
            label1.Text = "New Project Name:";
            // 
            // NewProjectNameTextBox
            // 
            NewProjectNameTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NewProjectNameTextBox.Location = new Point(159, 240);
            NewProjectNameTextBox.Name = "NewProjectNameTextBox";
            NewProjectNameTextBox.Size = new Size(799, 27);
            NewProjectNameTextBox.TabIndex = 11;
            NewProjectNameTextBox.TextChanged += NewProjectNameTextBox_TextChanged;
            // 
            // QuitButton
            // 
            QuitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            QuitButton.Location = new Point(12, 447);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(94, 29);
            QuitButton.TabIndex = 12;
            QuitButton.Text = "Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // TestcaseDataGridView
            // 
            TestcaseDataGridView.AllowUserToAddRows = false;
            TestcaseDataGridView.AllowUserToDeleteRows = false;
            TestcaseDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TestcaseDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TestcaseDataGridView.Location = new Point(479, 68);
            TestcaseDataGridView.Name = "TestcaseDataGridView";
            TestcaseDataGridView.RowHeadersWidth = 51;
            TestcaseDataGridView.RowTemplate.Height = 29;
            TestcaseDataGridView.Size = new Size(478, 151);
            TestcaseDataGridView.TabIndex = 13;
            TestcaseDataGridView.CellStateChanged += TestcaseDataGridView_CellStateChanged;
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(636, 20);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(94, 29);
            SelectAllButton.TabIndex = 14;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Location = new Point(754, 20);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(106, 29);
            ClearAllButton.TabIndex = 15;
            ClearAllButton.Text = "Clear All";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProgressBar.Location = new Point(186, 294);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(771, 29);
            ProgressBar.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = QuitButton;
            ClientSize = new Size(969, 488);
            Controls.Add(ProgressBar);
            Controls.Add(ClearAllButton);
            Controls.Add(SelectAllButton);
            Controls.Add(TestcaseDataGridView);
            Controls.Add(QuitButton);
            Controls.Add(NewProjectNameTextBox);
            Controls.Add(label1);
            Controls.Add(StatusTextBox);
            Controls.Add(DoTheCloneButton);
            Controls.Add(ApplyFiltersButton);
            Controls.Add(FilterTreeView);
            Controls.Add(LoginButton);
            Controls.Add(ProjectsListBox);
            Name = "Form1";
            Text = "Cloner";
            ((System.ComponentModel.ISupportInitialize)TestcaseDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox ProjectsListBox;
        private Button LoginButton;
        private TreeView FilterTreeView;
        private Button ApplyFiltersButton;
        private Button DoTheCloneButton;
        private TextBox StatusTextBox;
        private Label label1;
        private TextBox NewProjectNameTextBox;
        private Button QuitButton;
        private DataGridView TestcaseDataGridView;
        private Button SelectAllButton;
        private Button ClearAllButton;
        private ProgressBar ProgressBar;
    }
}