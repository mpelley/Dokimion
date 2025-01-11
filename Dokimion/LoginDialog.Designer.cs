namespace Dokimion
{
    partial class LoginDialog
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
            label5 = new Label();
            UrlTextBox = new TextBox();
            UseHttpsCheckBox = new CheckBox();
            ShowPasswordCheckBox = new CheckBox();
            label2 = new Label();
            label1 = new Label();
            LoginButton = new Button();
            PasswordTextBox = new TextBox();
            UserNameTextBox = new TextBox();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(109, 15);
            label5.Name = "label5";
            label5.Size = new Size(110, 20);
            label5.TabIndex = 20;
            label5.Text = "Server Address:";
            // 
            // UrlTextBox
            // 
            UrlTextBox.Location = new Point(225, 12);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.Size = new Size(464, 27);
            UrlTextBox.TabIndex = 12;
            // 
            // UseHttpsCheckBox
            // 
            UseHttpsCheckBox.AutoSize = true;
            UseHttpsCheckBox.Location = new Point(12, 13);
            UseHttpsCheckBox.Name = "UseHttpsCheckBox";
            UseHttpsCheckBox.Size = new Size(92, 24);
            UseHttpsCheckBox.TabIndex = 11;
            UseHttpsCheckBox.Text = "Use https";
            UseHttpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowPasswordCheckBox
            // 
            ShowPasswordCheckBox.AutoSize = true;
            ShowPasswordCheckBox.Location = new Point(624, 48);
            ShowPasswordCheckBox.Name = "ShowPasswordCheckBox";
            ShowPasswordCheckBox.Size = new Size(67, 24);
            ShowPasswordCheckBox.TabIndex = 19;
            ShowPasswordCheckBox.Text = "Show";
            ShowPasswordCheckBox.UseVisualStyleBackColor = true;
            ShowPasswordCheckBox.CheckedChanged += ShowPasswordCheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(322, 47);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 17;
            label2.Text = "Password:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 50);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 15;
            label1.Text = "User name:";
            // 
            // LoginButton
            // 
            LoginButton.DialogResult = DialogResult.OK;
            LoginButton.Location = new Point(17, 91);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(91, 39);
            LoginButton.TabIndex = 18;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(399, 47);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(209, 27);
            PasswordTextBox.TabIndex = 16;
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Location = new Point(88, 45);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(225, 27);
            UserNameTextBox.TabIndex = 14;
            // 
            // LoginDialog
            // 
            AcceptButton = LoginButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 153);
            Controls.Add(label5);
            Controls.Add(UrlTextBox);
            Controls.Add(UseHttpsCheckBox);
            Controls.Add(ShowPasswordCheckBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LoginButton);
            Controls.Add(PasswordTextBox);
            Controls.Add(UserNameTextBox);
            Name = "LoginDialog";
            Text = "LoginDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private TextBox UrlTextBox;
        private CheckBox UseHttpsCheckBox;
        private CheckBox ShowPasswordCheckBox;
        private Label label2;
        private Label label1;
        private Button LoginButton;
        private TextBox PasswordTextBox;
        private TextBox UserNameTextBox;
    }
}