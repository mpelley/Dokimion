namespace Updater4
{
    partial class ShowDiffs
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
            diffViewer1 = new DiffViewer();
            TestCaseTextBox = new TextBox();
            PreviousButton = new Button();
            NextButton = new Button();
            CloseButton = new Button();
            SuspendLayout();
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
            diffViewer1.Location = new Point(5, 65);
            diffViewer1.Margin = new Padding(3, 4, 3, 4);
            diffViewer1.Name = "diffViewer1";
            diffViewer1.NewText = null;
            diffViewer1.NewTextHeader = null;
            diffViewer1.OldText = null;
            diffViewer1.OldTextHeader = null;
            diffViewer1.SideBySideModeToggleTitle = "_Split view";
            diffViewer1.Size = new Size(905, 533);
            diffViewer1.SplitterBackColor = Color.FromArgb(64, 128, 128, 128);
            diffViewer1.SplitterBorderColor = Color.FromArgb(64, 128, 128, 128);
            diffViewer1.SplitterBorderWidth = new Padding(0);
            diffViewer1.SplitterWidth = 5D;
            diffViewer1.TabIndex = 0;
            diffViewer1.UnchangedBackColor = Color.FromArgb(12, 128, 128, 128);
            diffViewer1.UnchangedForeColor = Color.FromArgb(0, 0, 0);
            // 
            // TestCaseTextBox
            // 
            TestCaseTextBox.Location = new Point(97, 16);
            TestCaseTextBox.Margin = new Padding(3, 4, 3, 4);
            TestCaseTextBox.Name = "TestCaseTextBox";
            TestCaseTextBox.ReadOnly = true;
            TestCaseTextBox.Size = new Size(265, 27);
            TestCaseTextBox.TabIndex = 1;
            // 
            // PreviousButton
            // 
            PreviousButton.Location = new Point(5, 15);
            PreviousButton.Margin = new Padding(3, 4, 3, 4);
            PreviousButton.Name = "PreviousButton";
            PreviousButton.Size = new Size(86, 31);
            PreviousButton.TabIndex = 2;
            PreviousButton.Text = "Previous";
            PreviousButton.UseVisualStyleBackColor = true;
            PreviousButton.Click += PreviousButton_Click;
            // 
            // NextButton
            // 
            NextButton.Location = new Point(369, 15);
            NextButton.Margin = new Padding(3, 4, 3, 4);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(86, 31);
            NextButton.TabIndex = 3;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            NextButton.Click += NextButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.DialogResult = DialogResult.OK;
            CloseButton.Location = new Point(824, 15);
            CloseButton.Margin = new Padding(3, 4, 3, 4);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(86, 31);
            CloseButton.TabIndex = 4;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // ShowDiffs
            // 
            AcceptButton = CloseButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(CloseButton);
            Controls.Add(NextButton);
            Controls.Add(PreviousButton);
            Controls.Add(TestCaseTextBox);
            Controls.Add(diffViewer1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ShowDiffs";
            Text = "Show Differences";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DiffViewer diffViewer1;
        private TextBox TestCaseTextBox;
        private Button PreviousButton;
        private Button NextButton;
        private Button CloseButton;
    }
}