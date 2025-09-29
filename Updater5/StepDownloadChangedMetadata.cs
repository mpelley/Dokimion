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
            return PrevStepCode;
        }

        public override StepCode? Next()
        {
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
                    string dokJson = hmd.PrettyPrint();

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
                string json = hmd.PrettyPrint();
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

            CompareMetadata();
            Form.FeedbackTextBox.Text += "\r\nDone.";

        }

        public void ChangedMetadataDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
                    Form.ChangedMetadataDiffViewer.NewText = hmd.PrettyPrint();
                }
                catch
                {
                    Form.ChangedMetadataDiffViewer.NewText = "";
                }
            }
        }
    }
}
