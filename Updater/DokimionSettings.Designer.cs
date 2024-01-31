namespace Updater
{
    partial class DokimionSettings
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
            UserNameTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            PasswordTextBox = new TextBox();
            label3 = new Label();
            UrlTextBox = new TextBox();
            LoginButton = new Button();
            CancelButton = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            UserNameTextBox.Location = new Point(14, 44);
            UserNameTextBox.Margin = new Padding(3, 4, 3, 4);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(389, 27);
            UserNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 15);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 1;
            label1.Text = "User name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 92);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PasswordTextBox.Location = new Point(14, 121);
            PasswordTextBox.Margin = new Padding(3, 4, 3, 4);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(389, 27);
            PasswordTextBox.TabIndex = 2;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(14, 177);
            label3.Name = "label3";
            label3.Size = new Size(208, 20);
            label3.TabIndex = 5;
            label3.Text = "IP Address of Dokimion server";
            // 
            // UrlTextBox
            // 
            UrlTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            UrlTextBox.Location = new Point(14, 207);
            UrlTextBox.Margin = new Padding(3, 4, 3, 4);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.Size = new Size(389, 27);
            UrlTextBox.TabIndex = 4;
            // 
            // LoginButton
            // 
            LoginButton.DialogResult = DialogResult.OK;
            LoginButton.Location = new Point(14, 264);
            LoginButton.Margin = new Padding(3, 4, 3, 4);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(122, 31);
            LoginButton.TabIndex = 6;
            LoginButton.Text = "Log in";
            LoginButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CancelButton.DialogResult = DialogResult.Cancel;
            CancelButton.Location = new Point(318, 264);
            CancelButton.Margin = new Padding(3, 4, 3, 4);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(86, 31);
            CancelButton.TabIndex = 7;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(185, 91);
            checkBox1.Margin = new Padding(3, 4, 3, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(67, 24);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Show";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // DokimionSettings
            // 
            AcceptButton = LoginButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 309);
            Controls.Add(checkBox1);
            Controls.Add(CancelButton);
            Controls.Add(LoginButton);
            Controls.Add(label3);
            Controls.Add(UrlTextBox);
            Controls.Add(label2);
            Controls.Add(PasswordTextBox);
            Controls.Add(label1);
            Controls.Add(UserNameTextBox);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DokimionSettings";
            Text = "DokimionSettings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox UserNameTextBox;
        private Label label1;
        private Label label2;
        private TextBox PasswordTextBox;
        private Label label3;
        private TextBox UrlTextBox;
        private Button LoginButton;
        private new Button CancelButton;
        private CheckBox checkBox1;
    }
}