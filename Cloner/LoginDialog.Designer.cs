namespace Cloner
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
            SourceUsernameTextBox = new TextBox();
            SourcePasswordTextBox = new TextBox();
            SourceLoginButton = new Button();
            SourceLoginStatusTextBox = new TextBox();
            groupBox1 = new GroupBox();
            label5 = new Label();
            SourceAddressTextBox = new TextBox();
            SourceUseHttpsCheckBox = new CheckBox();
            SourceShowPasswordCheckBox = new CheckBox();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            DestAddressTextBox = new TextBox();
            label6 = new Label();
            DestUseHttpsCheckBox = new CheckBox();
            DestShowPasswordCheckBox = new CheckBox();
            label3 = new Label();
            label4 = new Label();
            DestLoginStatusTextBox = new TextBox();
            DestLoginButton = new Button();
            DestPasswordTextBox = new TextBox();
            DestUsernameTextBox = new TextBox();
            CloseButton = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // SourceUsernameTextBox
            // 
            SourceUsernameTextBox.Location = new Point(90, 57);
            SourceUsernameTextBox.Name = "SourceUsernameTextBox";
            SourceUsernameTextBox.Size = new Size(225, 27);
            SourceUsernameTextBox.TabIndex = 3;
            // 
            // SourcePasswordTextBox
            // 
            SourcePasswordTextBox.Location = new Point(401, 59);
            SourcePasswordTextBox.Name = "SourcePasswordTextBox";
            SourcePasswordTextBox.PasswordChar = '*';
            SourcePasswordTextBox.Size = new Size(209, 27);
            SourcePasswordTextBox.TabIndex = 4;
            // 
            // SourceLoginButton
            // 
            SourceLoginButton.Location = new Point(19, 103);
            SourceLoginButton.Name = "SourceLoginButton";
            SourceLoginButton.Size = new Size(91, 39);
            SourceLoginButton.TabIndex = 5;
            SourceLoginButton.Text = "Login";
            SourceLoginButton.UseVisualStyleBackColor = true;
            SourceLoginButton.Click += SourceLoginButton_Click;
            // 
            // SourceLoginStatusTextBox
            // 
            SourceLoginStatusTextBox.Location = new Point(136, 92);
            SourceLoginStatusTextBox.Multiline = true;
            SourceLoginStatusTextBox.Name = "SourceLoginStatusTextBox";
            SourceLoginStatusTextBox.ReadOnly = true;
            SourceLoginStatusTextBox.Size = new Size(546, 60);
            SourceLoginStatusTextBox.TabIndex = 3;
            SourceLoginStatusTextBox.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(SourceAddressTextBox);
            groupBox1.Controls.Add(SourceUseHttpsCheckBox);
            groupBox1.Controls.Add(SourceShowPasswordCheckBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(SourceLoginStatusTextBox);
            groupBox1.Controls.Add(SourceLoginButton);
            groupBox1.Controls.Add(SourcePasswordTextBox);
            groupBox1.Controls.Add(SourceUsernameTextBox);
            groupBox1.Location = new Point(22, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(710, 158);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Source Server";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(111, 27);
            label5.Name = "label5";
            label5.Size = new Size(110, 20);
            label5.TabIndex = 10;
            label5.Text = "Server Address:";
            // 
            // SourceAddressTextBox
            // 
            SourceAddressTextBox.Location = new Point(227, 24);
            SourceAddressTextBox.Name = "SourceAddressTextBox";
            SourceAddressTextBox.Size = new Size(464, 27);
            SourceAddressTextBox.TabIndex = 2;
            // 
            // SourceUseHttpsCheckBox
            // 
            SourceUseHttpsCheckBox.AutoSize = true;
            SourceUseHttpsCheckBox.Location = new Point(14, 25);
            SourceUseHttpsCheckBox.Name = "SourceUseHttpsCheckBox";
            SourceUseHttpsCheckBox.Size = new Size(92, 24);
            SourceUseHttpsCheckBox.TabIndex = 1;
            SourceUseHttpsCheckBox.Text = "Use https";
            SourceUseHttpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // SourceShowPasswordCheckBox
            // 
            SourceShowPasswordCheckBox.AutoSize = true;
            SourceShowPasswordCheckBox.Location = new Point(626, 60);
            SourceShowPasswordCheckBox.Name = "SourceShowPasswordCheckBox";
            SourceShowPasswordCheckBox.Size = new Size(67, 24);
            SourceShowPasswordCheckBox.TabIndex = 6;
            SourceShowPasswordCheckBox.Text = "Show";
            SourceShowPasswordCheckBox.UseVisualStyleBackColor = true;
            SourceShowPasswordCheckBox.CheckedChanged += SourceShowPasswordCheckBox_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(324, 59);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 5;
            label2.Text = "Password:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 62);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 4;
            label1.Text = "User name:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(DestAddressTextBox);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(DestUseHttpsCheckBox);
            groupBox2.Controls.Add(DestShowPasswordCheckBox);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(DestLoginStatusTextBox);
            groupBox2.Controls.Add(DestLoginButton);
            groupBox2.Controls.Add(DestPasswordTextBox);
            groupBox2.Controls.Add(DestUsernameTextBox);
            groupBox2.Location = new Point(22, 183);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(710, 151);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Destination Server";
            // 
            // DestAddressTextBox
            // 
            DestAddressTextBox.Location = new Point(227, 23);
            DestAddressTextBox.Name = "DestAddressTextBox";
            DestAddressTextBox.Size = new Size(466, 27);
            DestAddressTextBox.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(111, 26);
            label6.Name = "label6";
            label6.Size = new Size(110, 20);
            label6.TabIndex = 8;
            label6.Text = "Server Address:";
            // 
            // DestUseHttpsCheckBox
            // 
            DestUseHttpsCheckBox.AutoSize = true;
            DestUseHttpsCheckBox.Location = new Point(14, 25);
            DestUseHttpsCheckBox.Name = "DestUseHttpsCheckBox";
            DestUseHttpsCheckBox.Size = new Size(92, 24);
            DestUseHttpsCheckBox.TabIndex = 6;
            DestUseHttpsCheckBox.Text = "Use https";
            DestUseHttpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DestShowPasswordCheckBox
            // 
            DestShowPasswordCheckBox.AutoSize = true;
            DestShowPasswordCheckBox.Location = new Point(622, 56);
            DestShowPasswordCheckBox.Name = "DestShowPasswordCheckBox";
            DestShowPasswordCheckBox.Size = new Size(67, 24);
            DestShowPasswordCheckBox.TabIndex = 6;
            DestShowPasswordCheckBox.Text = "Show";
            DestShowPasswordCheckBox.UseVisualStyleBackColor = true;
            DestShowPasswordCheckBox.CheckedChanged += DestShowPasswordCheckBox_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(320, 55);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.TabIndex = 5;
            label3.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 58);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 4;
            label4.Text = "User name";
            // 
            // DestLoginStatusTextBox
            // 
            DestLoginStatusTextBox.Location = new Point(136, 88);
            DestLoginStatusTextBox.Multiline = true;
            DestLoginStatusTextBox.Name = "DestLoginStatusTextBox";
            DestLoginStatusTextBox.ReadOnly = true;
            DestLoginStatusTextBox.Size = new Size(525, 57);
            DestLoginStatusTextBox.TabIndex = 3;
            // 
            // DestLoginButton
            // 
            DestLoginButton.Location = new Point(19, 97);
            DestLoginButton.Name = "DestLoginButton";
            DestLoginButton.Size = new Size(91, 39);
            DestLoginButton.TabIndex = 10;
            DestLoginButton.Text = "Login";
            DestLoginButton.UseVisualStyleBackColor = true;
            DestLoginButton.Click += DestLoginButton_Click;
            // 
            // DestPasswordTextBox
            // 
            DestPasswordTextBox.Location = new Point(397, 55);
            DestPasswordTextBox.Name = "DestPasswordTextBox";
            DestPasswordTextBox.PasswordChar = '*';
            DestPasswordTextBox.Size = new Size(209, 27);
            DestPasswordTextBox.TabIndex = 9;
            // 
            // DestUsernameTextBox
            // 
            DestUsernameTextBox.Location = new Point(86, 53);
            DestUsernameTextBox.Name = "DestUsernameTextBox";
            DestUsernameTextBox.Size = new Size(225, 27);
            DestUsernameTextBox.TabIndex = 8;
            // 
            // CloseButton
            // 
            CloseButton.DialogResult = DialogResult.OK;
            CloseButton.Location = new Point(22, 356);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(169, 48);
            CloseButton.TabIndex = 11;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            // 
            // LoginDialog
            // 
            AcceptButton = CloseButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(752, 416);
            Controls.Add(CloseButton);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "LoginDialog";
            Text = "Login To Servers";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox SourceUsernameTextBox;
        private TextBox SourcePasswordTextBox;
        private Button SourceLoginButton;
        private TextBox SourceLoginStatusTextBox;
        private GroupBox groupBox1;
        private CheckBox SourceShowPasswordCheckBox;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private CheckBox DestShowPasswordCheckBox;
        private Label label3;
        private Label label4;
        private TextBox DestLoginStatusTextBox;
        private Button DestLoginButton;
        private TextBox DestPasswordTextBox;
        private TextBox DestUsernameTextBox;
        private Button CloseButton;
        private CheckBox DestUseHttpsCheckBox;
        private CheckBox SourceUseHttpsCheckBox;
        private Label label5;
        private TextBox SourceAddressTextBox;
        private TextBox DestAddressTextBox;
        private Label label6;
    }
}