using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepSelectRepo : StepCode
    {
        public StepSelectRepo(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Select Repo";
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
            Data.Settings.repo = Form.FolderTextBox.Text;
            Data.Settings.oneFolderForAllProjects = Form.FolderForAllProjectsCheckBox.Checked;
            Data.SaveSettings();

            return NextStepCode;
        }

        public override void Activate()
        {
            Form.FolderTextBox.Text = Data.Settings.repo;
            Form.FolderForAllProjectsCheckBox.Checked = Data.Settings.oneFolderForAllProjects;

            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = Form.FolderTextBox.Text.Length > 0;
        }

        public void BrowseFileSystemButton_Click()
        {
            FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Select the root of the GitHub repo clone for this project";
            dlg.ShowNewFolderButton = true;
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                Form.FolderTextBox.Text = dlg.SelectedPath;
                Data.Settings.repo = dlg.SelectedPath;
                Data.SaveSettings();
                Form.NextButton.Enabled = true;
            }
        }

        public void FolderTextBox_TextChanged()
        {
            Form.NextButton.Enabled = Form.FolderTextBox.Text.Length > 0;
        }
    }
}
