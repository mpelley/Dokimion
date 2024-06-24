namespace Updater
{
    public partial class DokimionSettings : Form
    {
        public DokimionSettings()
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }
    }
}
