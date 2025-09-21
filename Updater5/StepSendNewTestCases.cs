using Dokimion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepSendNewTestCases : StepCode
    {
        public StepSendNewTestCases(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Send New Test Cases";
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
            Form.FeedbackTextBox.Text = "Looking for new test cases.";
            Form.FeedbackTextBox.Refresh();

            List<string> newTestCases = new();
            string repo = Data.GetRepoFolder();
            foreach (string path in Directory.EnumerateFiles(repo, "*.txt"))
            {
                string id = Path.GetFileNameWithoutExtension(path);
                if (false == Data.TestCases.ContainsKey(id))
                {
                    newTestCases.Add(id);
                }
            }

            Form.NewTestCasesDataGridView.Rows.Clear();
            foreach (string id in newTestCases)
            {
                string metadataPath = Path.Combine(repo, id + ".JSON");
                if (File.Exists(metadataPath))
                {
                    string json = File.ReadAllText(metadataPath);
                    Metadata? md = JsonConvert.DeserializeObject<Metadata>(json);
                    if (md == null)
                    {
                        int index = Form.NewTestCasesDataGridView.Rows.Add([false, id, "<unknown>", $"Cannot decode {metadataPath}"]);
                        Form.NewTestCasesDataGridView.Rows[index].Cells[0].ReadOnly = true;
                    }
                    else if (string.IsNullOrEmpty(md.id) || string.IsNullOrEmpty(md.name))
                    {
                        int index = Form.NewTestCasesDataGridView.Rows.Add([false, id, md.name, $"{metadataPath} appears to be incomplete"]);
                        Form.NewTestCasesDataGridView.Rows[index].Cells[0].ReadOnly = true;
                    }
                    else
                    {
                        int index = Form.NewTestCasesDataGridView.Rows.Add([false, id, md.name, ""]);
                        Form.NewTestCasesDataGridView.Rows[index].Cells[0].ReadOnly = false;
                    }
                }
                else
                {
                    int index = Form.NewTestCasesDataGridView.Rows.Add([false, id, "<unknown>", $"{metadataPath} is missing!"]);
                    Form.NewTestCasesDataGridView.Rows[index].Cells[0].ReadOnly = true;
                }
            }

            Form.FeedbackTextBox.Text = "\r\nDone.";
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
        }

        public void SelectAllNewTestCasesButton_Click()
        {
            var rows = Form.NewTestCasesDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if (false == selectCell.ReadOnly)
                {
                    selectCell.Value = true;
                }
            }
        }

        public void ClearAllNewTestCasesButton_Click()
        {
            var rows = Form.NewTestCasesDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if (false == selectCell.ReadOnly)
                {
                    selectCell.Value = false;
                }
            }
        }

        public void UploadNewTestCasesButton_Click()
        {
            var rows = Form.NewTestCasesDataGridView.Rows;
            int numSelectedRows = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if ((bool)selectCell.Value)
                {
                    numSelectedRows++;
                }
            }
            Form.FeedbackTextBox.Text = $"We will upload {numSelectedRows} test cases to Dokimion.";
            Form.FeedbackTextBox.Refresh();

            string repo = Data.GetRepoFolder();
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if (false == (bool)selectCell.Value)
                {
                    continue;
                }
                string id = (string)rows[i].Cells[1].Value;
                string metadataPath = Path.Combine(repo, id + ".JSON");
                string json = File.ReadAllText(metadataPath);
                TestCase? tc = JsonConvert.DeserializeObject<TestCase>(json);
                if (tc == null)
                {
                    Form.FeedbackTextBox.Text = $"Can't decode {metadataPath}";
                    return;
                }
                string stepPath = Path.Combine(repo, id + ".txt");
                string stepText = File.ReadAllText(stepPath);
                Step step = new();
                step.action = stepText;
                tc.steps.Add(step);
                ;
            }
            Form.FeedbackTextBox.Text += "\r\nDone.";

        }
    }
}
