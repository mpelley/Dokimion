using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dokimion;

namespace Updater5
{
    public class StepDownloadMetadata : StepCode
    {
        Dictionary<string, string> FileJsons;

        public StepDownloadMetadata(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Download Metadata";
            FileJsons = new();
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
            string projectId = project.id;
            Form.FeedbackTextBox.Text = "Getting list of test cases from Dokimion";
            Form.FeedbackTextBox.Refresh();
            List<TestCaseShort>? testcases = Data.Dokimion.GetTestCaseSummariesForProject(projectId);

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
            Form.FeedbackTextBox.Text += $"\r\nComparing {Data.TestCases.Count} test case metadata to files in repo.";
            Form.FeedbackTextBox.Refresh();

            Form.MetadataProgressBar.Minimum = 0;
            Form.MetadataProgressBar.Maximum = Data.TestCases.Count;
            Form.MetadataProgressBar.Value = 0;
            Form.MetadataProgressBar.Step = 1;

            Form.MetadataDataGridView.Rows.Clear();
            FileJsons = new();
            string repo = GetRepoFolder();
            foreach (var tc in Data.TestCases.Values)
            {
                string id = tc.id;
                string path = Path.Combine(repo, id + ".JSON");
                string fileJson = "";
                try
                {
                    fileJson = File.ReadAllText(path);
                }
                catch
                {
                    Form.MetadataDataGridView.Rows.Add([false, id, tc.name, "Missing"]);
                    continue;
                }
                FileJsons.Add(id, fileJson);
                Metadata md = tc.ExtractMetadata();
                string dokJson = md.PrettyPrint();
                if (dokJson != fileJson)
                {
                    Form.MetadataDataGridView.Rows.Add([false, id, tc.name, "Different"]);
                }
            }
        }


        public void DownloadMetadataButton_Click()
        {
            int numSelected = 0;
            var rows = Form.MetadataDataGridView.Rows;
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

            Form.FeedbackTextBox.Text = $"Downloading {numSelected} test case metadata to files in repo.";
            Form.FeedbackTextBox.Refresh();

            Form.MetadataProgressBar.Minimum = 0;
            Form.MetadataProgressBar.Maximum = numSelected;
            Form.MetadataProgressBar.Value = 0;
            Form.MetadataProgressBar.Step = 1;

            string repo = GetRepoFolder();

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
                    string path = Path.Combine(repo, tc.id + ".JSON");
                    try
                    {
                        File.WriteAllText(path, json);
                    }
                    catch (Exception ex)
                    {
                        Form.FeedbackTextBox.Text += $"\r\nError {ex.Message}";
                        return;
                    }
                }
                Form.MetadataProgressBar.PerformStep();
            }

            // Do this to update the grid:
            CompareTestCases(Data.Project);

        }

        private string GetRepoFolder()
        {
            string repo = Data.Settings.repo;
            if (Data.Settings.oneFolderForAllProjects)
            {
                repo = Path.Join(repo, Data.Project.Name);
            }
            if (false == Directory.Exists(repo))
            {
                Directory.CreateDirectory(repo);
            }

            return repo;
        }
        public void SelectAllButton_Click()
        {
            var rows = Form.MetadataDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                selectCell.Value = true;
            }
        }

        public void ClearAllButton_Click()
        {
            var rows = Form.MetadataDataGridView.Rows;
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

        public void MetadataDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var rows = Form.MetadataDataGridView.Rows;
            var row = rows[e.RowIndex];
            string id = (string)row.Cells[1].Value;
            TestCase tc = Data.TestCases[id];
            if (tc != null)
            {
                Metadata md = tc.ExtractMetadata();
                string dokJson = md.PrettyPrint();
                string fileJson;
                if (FileJsons.ContainsKey(id))
                {
                    fileJson = this.FileJsons[id];
                }
                else
                {
                    fileJson = "<File not in file system>";
                }
                Form.MetadataDiffViewer.OldText = fileJson;
                Form.MetadataDiffViewer.NewText = dokJson;
                Form.MetadataDiffViewer.Refresh();
            }
        }

    }
}
