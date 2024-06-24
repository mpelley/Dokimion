namespace Updater
{
    partial class UploadTests
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
            NewTestCaseListBox = new CheckedListBox();
            textBox1 = new TextBox();
            UploadCheckedTestCasesButton = new Button();
            QuitButton = new Button();
            CheckAllButton = new Button();
            ClearAllButton = new Button();
            SuspendLayout();
            // 
            // NewTestCaseListBox
            // 
            NewTestCaseListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NewTestCaseListBox.FormattingEnabled = true;
            NewTestCaseListBox.Location = new Point(26, 79);
            NewTestCaseListBox.Name = "NewTestCaseListBox";
            NewTestCaseListBox.Size = new Size(264, 130);
            NewTestCaseListBox.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(27, 9);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(263, 64);
            textBox1.TabIndex = 1;
            textBox1.Text = "The following test cases are in the repository, but not in Dokimion.  Check the ones that should be uploaded to Dokimion.";
            // 
            // UploadCheckedTestCasesButton
            // 
            UploadCheckedTestCasesButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UploadCheckedTestCasesButton.Location = new Point(310, 78);
            UploadCheckedTestCasesButton.Name = "UploadCheckedTestCasesButton";
            UploadCheckedTestCasesButton.Size = new Size(185, 23);
            UploadCheckedTestCasesButton.TabIndex = 2;
            UploadCheckedTestCasesButton.Text = "Upload checked test cases";
            UploadCheckedTestCasesButton.UseVisualStyleBackColor = true;
            // 
            // QuitButton
            // 
            QuitButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            QuitButton.Location = new Point(310, 126);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(185, 23);
            QuitButton.TabIndex = 3;
            QuitButton.Text = "Quit without uploading";
            QuitButton.UseVisualStyleBackColor = true;
            // 
            // CheckAllButton
            // 
            CheckAllButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CheckAllButton.Location = new Point(54, 244);
            CheckAllButton.Name = "CheckAllButton";
            CheckAllButton.Size = new Size(75, 23);
            CheckAllButton.TabIndex = 4;
            CheckAllButton.Text = "Check all";
            CheckAllButton.UseVisualStyleBackColor = true;
            CheckAllButton.Click += CheckAllButton_Click;
            // 
            // ClearAllButton
            // 
            ClearAllButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ClearAllButton.Location = new Point(169, 244);
            ClearAllButton.Name = "ClearAllButton";
            ClearAllButton.Size = new Size(75, 23);
            ClearAllButton.TabIndex = 5;
            ClearAllButton.Text = "Clear all";
            ClearAllButton.UseVisualStyleBackColor = true;
            ClearAllButton.Click += ClearAllButton_Click;
            // 
            // UploadTests
            // 
            AcceptButton = UploadCheckedTestCasesButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = QuitButton;
            ClientSize = new Size(507, 302);
            Controls.Add(ClearAllButton);
            Controls.Add(CheckAllButton);
            Controls.Add(QuitButton);
            Controls.Add(UploadCheckedTestCasesButton);
            Controls.Add(textBox1);
            Controls.Add(NewTestCaseListBox);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UploadTests";
            Text = "UploadTests";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox NewTestCaseListBox;
        private TextBox textBox1;
        private Button UploadCheckedTestCasesButton;
        private Button QuitButton;
        private Button CheckAllButton;
        private Button ClearAllButton;
    }
}