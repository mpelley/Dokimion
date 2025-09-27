using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dokimion;
using Newtonsoft.Json;

namespace Updater5
{
    public class StepDownloadNewTestCases : StepCode
    {
        public StepDownloadNewTestCases(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Download new test cases";
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
            Project? project = Data.Project;
            if (project == null)
            {
                Form.FeedbackTextBox.Text = "Select a Project first";
                Form.PrevButton.Enabled = true;
                return;
            }
            GetDokimionTestCases(project);
            CompareTestCases(project);
            Form.FeedbackTextBox.Text += $"\r\nDone.";

            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
        }

        private void GetDokimionTestCases(Project project)
        {
            Form.FeedbackTextBox.Text = "Getting list of test cases from Dokimion";
            Form.FeedbackTextBox.Refresh();
            List<TestCaseShort>? testcases = Data.Dokimion.GetTestCaseSummariesForProject(project.id);

            if (testcases == null)
            {
                Form.FeedbackTextBox.Text = Data.Dokimion.Error;
            }
            else
            {
                Form.FeedbackTextBox.Text += $"\r\nGetting {testcases.Count} test cases from Dokimion.";
                Form.FeedbackTextBox.Refresh();
                Form.MetadataProgressBar.Minimum = 0;
                Form.MetadataProgressBar.Maximum = testcases.Count;
                Form.MetadataProgressBar.Value = 0;
                Form.MetadataProgressBar.Step = 1;
                Data.TestCases.Clear();
                foreach (TestCaseShort shortTestCase in testcases)
                {
                    TestCase? testCase = Data.Dokimion.GetTestCaseAsObject(shortTestCase.id, project);
                    if (testCase != null)
                    {
                        Data.TestCases.Add(shortTestCase.id, testCase);
                    }
                    Form.MetadataProgressBar.PerformStep();
                }

            }
        }

        private void CompareTestCases(Project project)
        {
            Form.FeedbackTextBox.Text += $"\r\nVerifying that {Data.TestCases.Count} test cases exist in {Data.GetRepoFolder()}.";
            Form.FeedbackTextBox.Refresh();

            Form.MetadataProgressBar.Minimum = 0;
            Form.MetadataProgressBar.Maximum = Data.TestCases.Count;
            Form.MetadataProgressBar.Value = 0;
            Form.MetadataProgressBar.Step = 1;

            Form.NewTestCasesDataGridView.Rows.Clear();
            string repo = Data.GetRepoFolder();
            foreach (var tc in Data.TestCases.Values)
            {
                string id = tc.id;
                if (false == File.Exists(Path.Combine(repo, id + ".JSON")))
                {
                    if (File.Exists(Path.Combine(repo, id + ".txt")))
                    {
                        Form.NewTestCasesDataGridView.Rows.Add([false, id, tc.name, "Text step file already exists!"]);
                    }
                    else if (File.Exists(Path.Combine(repo, id + ".html")))
                    {
                        Form.NewTestCasesDataGridView.Rows.Add([false, id, tc.name, "HTML step file already exists!"]);
                    }
                    else
                    {
                        Form.NewTestCasesDataGridView.Rows.Add([false, id, tc.name, ""]);
                    }
                }
            }
        }

        public void DownloadTestCasesButton_Click()
        {
            int numSelected = 0;
            var rows = Form.NewTestCasesDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if ((bool)selectCell.Value == true)
                {
                    numSelected++;
                }
            }

            if (numSelected == 0)
            {
                Form.FeedbackTextBox.Text = "Select some test cases to download and try again.";
                return;
            }

            Form.FeedbackTextBox.Text = $"Downloading {numSelected} test cases to files in repo.";
            Form.FeedbackTextBox.Refresh();

            Form.MetadataProgressBar.Minimum = 0;
            Form.MetadataProgressBar.Maximum = numSelected;
            Form.MetadataProgressBar.Value = 0;
            Form.MetadataProgressBar.Step = 1;

            string repo = Data.GetRepoFolder();

            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if ((bool)selectCell.Value == false)
                {
                    continue;
                }
                string id = (string)rows[i].Cells[1].Value;
                TestCase tc = Data.TestCases[id];
                if (tc != null)
                {
                    Metadata md = tc.ExtractMetadata();
                    string json = md.PrettyPrint();
                    string path = Path.Combine(repo, id + ".JSON");
                    try
                    {
                        File.WriteAllText(path, json);
                    }
                    catch (Exception ex)
                    {
                        Form.FeedbackTextBox.Text += $"\r\nError {ex.Message}";
                        return;
                    }

                    if ((File.Exists(Path.Combine(repo, id + ".txt"))) || (File.Exists(Path.Combine(repo, id + ".html"))))
                    {
                        Form.FeedbackTextBox.Text = $"\r\nNot saving step data for {id} because it already exists.";
                    }
                    else
                    {
                        string action = "";
                        path = Path.Combine(repo, id + ".html");
                        try
                        {
                            action = tc.steps[0].action;
                        }
                        catch
                        {
                            path = Path.Combine(repo, id + ".txt");
                        }

                        try
                        {
                            File.WriteAllText(path, action);
                        }
                        catch (Exception ex)
                        {
                            Form.FeedbackTextBox.Text += $"\r\nError writing step data for id {id} {ex.Message}";
                            return;
                        }
                    }

                }
                Form.MetadataProgressBar.PerformStep();
            }

            // Do this to update the grid:
            CompareTestCases(Data.Project);

        }

        public void SelectAllButton_Click()
        {
            var rows = Form.NewTestCasesDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                selectCell.Value = true;
            }
        }

        public void ClearAllButton_Click()
        {
            var rows = Form.NewTestCasesDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                selectCell.Value = false;
            }
        }

        public void RescanButton_Click()
        {
            Activate();
        }


    }
}
