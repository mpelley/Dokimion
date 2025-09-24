using Dokimion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepHandleDifferences : StepCode
    {
        public StepHandleDifferences(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Handle Differences";
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
            Form.FeedbackTextBox.Text = "Comparing Test Cases";
            Form.HandleDiffDataGridView.Rows.Clear();
            string repo = Data.GetRepoFolder();
            IEnumerable<string> files = Directory.EnumerateFiles(repo, "*.json");
            foreach (string jsonFileName in files)
            {
                string id = Path.GetFileNameWithoutExtension(jsonFileName);
                if (Data.TestCases.ContainsKey(id))
                {
                    string stepFileName = Path.Combine(repo, id + ".txt");
                    string fileStep;
                    if (false == File.Exists(stepFileName))
                    {
                        stepFileName = Path.Combine(repo, id + ".html");
                    }
                    if (false == File.Exists(stepFileName))
                    {
                        continue;
                    }
                    try
                    {
                        fileStep = File.ReadAllText(stepFileName);
                    }
                    catch (Exception ex)
                    {
                        Form.FeedbackTextBox.Text += $"Cannot read file {stepFileName} because \r\n{ex.Message}";
                        return;
                    }
                    TestCase tc = Data.TestCases[id];
                    string dokStep = "";
                    try
                    {
                        dokStep = tc.steps[0].action;
                    }
                    catch
                    {
                        // Leave string as empty if we can't get the action
                    }
                    if (fileStep != dokStep)
                    {
                        Form.HandleDiffDataGridView.Rows.Add(false, id, tc.name, "Different");
                    }
                }
                else
                {
                    Form.HandleDiffDataGridView.Rows.Add(false, id, "", "Missing in Dokimion");
                }
            }

            Form.FeedbackTextBox.Text += "Done.";
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
        }
    }
}
