using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Updater5
{
    public class StepLogin : StepCode
    {
        public StepLogin(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Log into Dokimion";
        }

        public override void Init()
		{
		}

        public override StepCode? Prev()
        {
            return PrevStepCode;
        }

        public override StepCode? Next()
        {
            return NextStepCode;
        }

        public override void Activate()
        {
            Form.PrevButton.Enabled = false;
            Form.NextButton.Enabled = false;
            Data.LoggedIn = false;

            Form.FeedbackTextBox.Text = "";
            Form.FeedbackTextBox.Refresh();

            string? error = Data.GetSettings();
            if (false == String.IsNullOrEmpty(error))
            {
                Form.FeedbackTextBox.Text = error;
            }
            else
            {
                Form.UserNameTextBox.Text = Data.Settings.username;
                Form.UrlTextBox.Text = Data.Settings.server;
                Form.UseHttpsCheckBox.Checked = Data.Settings.useHttps;
            }
            Form.Text = $"Updater version {System.Windows.Forms.Application.ProductVersion}";

        }


        public void LoginButton_Clicked()
        {
            Form.NextButton.Enabled = false;
            Data.LoggedIn = false;

            Form.FeedbackTextBox.Text = "Logging into Dokimion.";
            Form.FeedbackTextBox.Refresh();

            Data.Dokimion = new Dokimion.Dokimion(Form.UrlTextBox.Text, Form.UseHttpsCheckBox.Checked);
            if (false == Data.Dokimion.Login(Form.UserNameTextBox.Text, Form.PasswordTextBox.Text))
            {
                Form.FeedbackTextBox.Text = Data.Dokimion.Error;
                Form.FeedbackTextBox.Text += "\r\nSettings not saved.";
            }
            else
            {
                Form.Text = "Updater:  " + Data.Dokimion.ServerUrl;
                Form.FeedbackTextBox.Text = "Logged in!";
                Form.NextButton.Enabled = true;
                Data.LoggedIn = true;

                Data.Settings.username = Form.UserNameTextBox.Text;
                Data.Settings.server = Form.UrlTextBox.Text;
                Data.Settings.useHttps = Form.UseHttpsCheckBox.Checked;
                Data.SaveSettings();
            }

        }

        public void ShowPasswordCheckBox_CheckedChanged()
        {
            Form.PasswordTextBox.PasswordChar = Form.ShowPasswordCheckBox.Checked ? '\0' : '*';
        }

    }
}
