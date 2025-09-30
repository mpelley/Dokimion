using Dokimion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepSelectProject : StepCode
    {
        public StepSelectProject(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Select Project";
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
            Project? project = Form.ProjectsListBox.SelectedItem as Project;
            if (project != null)
            {
                Data.Project = project;
                return NextStepCode;
            }
            return null;
        }

        public override void Activate()
        {
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = false;

            Form.FeedbackTextBox.Text = "Getting Projects from Dokimion.";
            Form.FeedbackTextBox.Refresh();

            GetProjectsFromDokimion();

            Form.FeedbackTextBox.Text += "\r\nDone.";
        }

        private void GetProjectsFromDokimion()
        {
            Form.ProjectsListBox.Items.Clear();
            Data.Projects = Data.Dokimion.GetProjects();
            if (Data.Projects == null)
            {
                Form.FeedbackTextBox.Text = Data.Dokimion.Error;
            }
            else
            {
                foreach (var project in Data.Projects)
                {
                    Form.ProjectsListBox.Items.Add(project);
                }
            }
        }

        public void ProjectsListBox_SelectedIndexChanged()
        {
            Form.NextButton.Enabled = true;
        }
    }
}
