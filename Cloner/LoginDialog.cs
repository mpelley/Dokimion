using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloner
{
    public partial class LoginDialog : Form
    {
        public Dokimion? Source;
        public Dokimion? Destination;

        public string SourceUsername
        {
            get => SourceUsernameTextBox.Text;
            set => SourceUsernameTextBox.Text = value;
        }

        public string DestUsername
        {
            get => DestUsernameTextBox.Text;
            set => DestUsernameTextBox.Text = value;
        }

        public bool SourceUseHttps
        {
            get => SourceUseHttpsCheckBox.Checked;
            set => SourceUseHttpsCheckBox.Checked = value;
        }

        public bool DestUseHttps
        {
            get => DestUseHttpsCheckBox.Checked;
            set => DestUseHttpsCheckBox.Checked = value;
        }

        public string SourceAddress
        {
            get => SourceAddressTextBox.Text;
            set => SourceAddressTextBox.Text = value;
        }

        public string DestAddress
        {
            get => DestAddressTextBox.Text;
            set => DestAddressTextBox.Text = value;
        }


        public LoginDialog()
        {
            InitializeComponent();
        }

        private void SourceLoginButton_Click(object sender, EventArgs e)
        {
            Source = null;
            SourceLoginStatusTextBox.Text = string.Empty;
            Dokimion src = new(SourceAddress, SourceUseHttpsCheckBox.Checked);
            if (src.Login(SourceUsernameTextBox.Text, SourcePasswordTextBox.Text))
            {
                SourceLoginStatusTextBox.Text = "Login successful";
                Source = src;
            }
            else
            {
                SourceLoginStatusTextBox.Text = "Cannot login because: \r\n" + src.Error;
            }
        }

        private void DestLoginButton_Click(object sender, EventArgs e)
        {
            Destination = null;
            DestLoginStatusTextBox.Text = string.Empty;
            Dokimion dest = new(DestAddress, DestUseHttpsCheckBox.Checked);
            if (dest.Login(DestUsernameTextBox.Text, DestPasswordTextBox.Text))
            {
                DestLoginStatusTextBox.Text = "Login successful";
                Destination = dest;
            }
            else
            {
                DestLoginStatusTextBox.Text = "Cannot login because: \r\n" + dest.Error;
            }
        }

        private void SourceShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SourcePasswordTextBox.PasswordChar = SourceShowPasswordCheckBox.Checked ? '\0' : '*';
        }

        private void DestShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DestPasswordTextBox.PasswordChar = DestShowPasswordCheckBox.Checked ? '\0' : '*';
        }
    }
}
