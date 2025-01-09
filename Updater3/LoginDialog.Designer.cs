namespace Updater3
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
            SourceAddressTextBox = new TextBox();
            SourceUseHttpsCheckBox = new CheckBox();
            SourceShowPasswordCheckBox = new CheckBox();
            label2 = new Label();
            label1 = new Label();
            SourceLoginStatusTextBox = new TextBox();
            SourceLoginButton = new Button();
            SourcePasswordTextBox = new TextBox();
            SourceUsernameTextBox = new TextBox();
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
            // SourceAddressTextBox
            // 
            SourceAddressTextBox.Location = new Point(225, 12);
            SourceAddressTextBox.Name = "SourceAddressTextBox";
            SourceAddressTextBox.Size = new Size(464, 27);
            SourceAddressTextBox.TabIndex = 12;
            // 
            // SourceUseHttpsCheckBox
            // 
            SourceUseHttpsCheckBox.AutoSize = true;
            SourceUseHttpsCheckBox.Location = new Point(12, 13);
            SourceUseHttpsCheckBox.Name = "SourceUseHttpsCheckBox";
            SourceUseHttpsCheckBox.Size = new Size(92, 24);
            SourceUseHttpsCheckBox.TabIndex = 11;
            SourceUseHttpsCheckBox.Text = "Use https";
            SourceUseHttpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // SourceShowPasswordCheckBox
            // 
            SourceShowPasswordCheckBox.AutoSize = true;
            SourceShowPasswordCheckBox.Location = new Point(624, 48);
            SourceShowPasswordCheckBox.Name = "SourceShowPasswordCheckBox";
            SourceShowPasswordCheckBox.Size = new Size(67, 24);
            SourceShowPasswordCheckBox.TabIndex = 19;
            SourceShowPasswordCheckBox.Text = "Show";
            SourceShowPasswordCheckBox.UseVisualStyleBackColor = true;
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
            // SourceLoginStatusTextBox
            // 
            SourceLoginStatusTextBox.Location = new Point(134, 80);
            SourceLoginStatusTextBox.Multiline = true;
            SourceLoginStatusTextBox.Name = "SourceLoginStatusTextBox";
            SourceLoginStatusTextBox.ReadOnly = true;
            SourceLoginStatusTextBox.Size = new Size(546, 60);
            SourceLoginStatusTextBox.TabIndex = 13;
            SourceLoginStatusTextBox.TabStop = false;
            // 
            // SourceLoginButton
            // 
            SourceLoginButton.Location = new Point(17, 91);
            SourceLoginButton.Name = "SourceLoginButton";
            SourceLoginButton.Size = new Size(91, 39);
            SourceLoginButton.TabIndex = 18;
            SourceLoginButton.Text = "Login";
            SourceLoginButton.UseVisualStyleBackColor = true;
            // 
            // SourcePasswordTextBox
            // 
            SourcePasswordTextBox.Location = new Point(399, 47);
            SourcePasswordTextBox.Name = "SourcePasswordTextBox";
            SourcePasswordTextBox.PasswordChar = '*';
            SourcePasswordTextBox.Size = new Size(209, 27);
            SourcePasswordTextBox.TabIndex = 16;
            // 
            // SourceUsernameTextBox
            // 
            SourceUsernameTextBox.Location = new Point(88, 45);
            SourceUsernameTextBox.Name = "SourceUsernameTextBox";
            SourceUsernameTextBox.Size = new Size(225, 27);
            SourceUsernameTextBox.TabIndex = 14;
            // 
            // LoginDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 153);
            Controls.Add(label5);
            Controls.Add(SourceAddressTextBox);
            Controls.Add(SourceUseHttpsCheckBox);
            Controls.Add(SourceShowPasswordCheckBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SourceLoginStatusTextBox);
            Controls.Add(SourceLoginButton);
            Controls.Add(SourcePasswordTextBox);
            Controls.Add(SourceUsernameTextBox);
            Name = "LoginDialog";
            Text = "LoginDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private TextBox SourceAddressTextBox;
        private CheckBox SourceUseHttpsCheckBox;
        private CheckBox SourceShowPasswordCheckBox;
        private Label label2;
        private Label label1;
        private TextBox SourceLoginStatusTextBox;
        private Button SourceLoginButton;
        private TextBox SourcePasswordTextBox;
        private TextBox SourceUsernameTextBox;
    }
}