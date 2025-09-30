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
    public class StepDownloadChangedMetadata : StepCode
    {
        Dictionary<string, string> MetadataFromFiles;

        public StepDownloadChangedMetadata(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Download changed metadata";
            MetadataFromFiles = new Dictionary<string, string>();
        }

        public override void Init()
		{
		}

        public override StepCode? Prev()
        {
            Form.ChangedMetadataDiffViewer.OldText = "";
            Form.ChangedMetadataDiffViewer.NewText = "";
            return PrevStepCode;
        }

        public override StepCode? Next()
        {
            Form.ChangedMetadataDiffViewer.OldText = "";
            Form.ChangedMetadataDiffViewer.NewText = "";
            return NextStepCode;
        }

        public override void Activate()
        {
            Form.FeedbackTextBox.Text = "Checking for changed metadata files.";

            CompareMetadata();

            Form.ChangedMetadataDiffViewer.OldTextHeader = "From Repo";
            Form.ChangedMetadataDiffViewer.NewTextHeader = "From Dokimion";
            Form.ChangedMetadataDiffViewer.OldText = "";
            Form.ChangedMetadataDiffViewer.NewText = "";

            Form.FeedbackTextBox.Text += "\r\nDone.";
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
        }

        private void CompareMetadata()
        {
            string repo = Data.GetRepoFolder();
            MetadataFromFiles.Clear();
            Form.ChangedMetadataDataGridView.Rows.Clear();
            foreach (string id in Data.TestCases.Keys)
            {
                TestCase tc = Data.TestCases[id];
                string metadataPath = Path.Combine(repo, id + ".JSON");
                if (File.Exists(metadataPath))
                {
                    string fileJson = File.ReadAllText(metadataPath);
                    MetadataFromFiles.Add(id, fileJson);

                    Metadata md = tc.ExtractMetadata();
                    HumanMetadata hmd = new(md);
                    string dokJson = hmd.PrettyPrint(Data.Project.attributes);

                    if (fileJson != dokJson)
                    {
                        Form.ChangedMetadataDataGridView.Rows.Add([false, id, tc.name, $"Different"]);
                    }
                }
                else
                {
                    Form.ChangedMetadataDataGridView.Rows.Add([false, id, tc.name, "Missing"]);
                }
            }
        }

        public void SelectAllChangedMetadataButton_Click()
        {
            var rows = Form.ChangedMetadataDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if (false == selectCell.ReadOnly)
                {
                    selectCell.Value = true;
                }
            }
        }

        public void ClearAllChangedMetadataButton_Click()
        {
            var rows = Form.ChangedMetadataDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if (false == selectCell.ReadOnly)
                {
                    selectCell.Value = false;
                }
            }
        }

        public void DownloadMetadataButton_Click()
        {
            var rows = Form.ChangedMetadataDataGridView.Rows;
            int numSelectedRows = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if ((bool)selectCell.Value)
                {
                    numSelectedRows++;
                }
            }
            Form.FeedbackTextBox.Text = $"We will download {numSelectedRows} metadata files from Dokimion.";
            //Form.FeedbackTextBox.Refresh();

            string repo = Data.GetRepoFolder();
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if (false == (bool)selectCell.Value)
                {
                    continue;
                }
                string id = (string)rows[i].Cells[1].Value;
                string path = Path.Combine(repo, id + ".JSON");
                TestCase testCase = Data.TestCases[id];
                Metadata metadata = testCase.ExtractMetadata();
                HumanMetadata hmd = new(metadata);
                string json = hmd.PrettyPrint(Data.Project.attributes);
                try
                {
                    File.WriteAllText(path, json);
                }
                catch (Exception ex)
                {
                    Form.FeedbackTextBox.Text += $"\r\nCannot write {path} because {ex.Message}";
                    return;
                }
            }

            Form.ChangedMetadataDiffViewer.OldText = "";
            Form.ChangedMetadataDiffViewer.NewText = "";
            CompareMetadata();
            Form.FeedbackTextBox.Text += "\r\nDone.";

        }

        public void ChangedMetadataDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            string? id = Form.ChangedMetadataDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (id == null)
            {
                Form.FeedbackTextBox.Text = $"ID is missing for row {e.RowIndex}";
            }
            else
            {
                if (MetadataFromFiles.ContainsKey(id))
                {
                    Form.ChangedMetadataDiffViewer.OldText = MetadataFromFiles[id];
                }
                else
                {
                    Form.ChangedMetadataDiffViewer.OldText = "";
                }
                try
                {
                    Metadata md = Data.TestCases[id].ExtractMetadata();
                    HumanMetadata hmd = new(md);
                    Form.ChangedMetadataDiffViewer.NewText = hmd.PrettyPrint(Data.Project.attributes);
                }
                catch
                {
                    Form.ChangedMetadataDiffViewer.NewText = "";
                }
            }
        }


        public void GetDokimionTestCases()
        {
            Form.FeedbackTextBox.Text = "Getting list of test cases from Dokimion";
            Form.FeedbackTextBox.Refresh();
            List<TestCaseShort>? testcases = Data.Dokimion.GetTestCaseSummariesForProject(Data.Project.id);

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
                    TestCase? testCase = Data.Dokimion.GetTestCaseAsObject(shortTestCase.id, Data.Project);
                    if (testCase != null)
                    {
                        Data.TestCases.Add(shortTestCase.id, testCase);
                    }
                    Form.MetadataProgressBar.PerformStep();
                }

            }
        }


        public void DownloadMetadataRescanButton_Click()
        {
            GetDokimionTestCases();
            CompareMetadata();
        }

    }
}
