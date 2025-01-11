using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dokimion
{
    public partial class LoginDialog : Form
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        public string Username
        {
            get => UserNameTextBox.Text;
            set => UserNameTextBox.Text = value;
        }

        public string Password
        {
            get => PasswordTextBox.Text;
            set => PasswordTextBox.Text = value;
        }

        public string ServerUrl
        {
            get => UrlTextBox.Text;
            set { UrlTextBox.Text = value; }
        }

        public bool UseHttps
        {
            get => UseHttpsCheckBox.Checked;
            set => UseHttpsCheckBox.Checked = value;
        }

        private void ShowPasswordCheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.PasswordChar = ShowPasswordCheckBox.Checked ? '\0' : '*';
        }
    }
}
