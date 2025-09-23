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
            //Form.FeedbackTextBox.Refresh();

            string repo = Data.GetRepoFolder();
            Form.ChangedMetadataDataGridView.Rows.Clear();
            foreach (string id in Data.TestCases.Keys)
            {
                TestCase tc = Data.TestCases[id];
                string metadataPath = Path.Combine(repo, id + ".JSON");
                if (File.Exists(metadataPath))
                {
                    string fileJson = File.ReadAllText(metadataPath);
                    MetadataFromFiles.Add(id, fileJson);
                    string dokJson = tc.ExtractMetadata().PrettyPrint();
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

            Form.ChangedMetadataDiffViewer.OldTextHeader = "From Repo";
            Form.ChangedMetadataDiffViewer.NewTextHeader = "From Dokimion";

            Form.FeedbackTextBox.Text += "\r\nDone.";
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
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
                string json = metadata.PrettyPrint();
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
                Form.ChangedMetadataDiffViewer.OldText = MetadataFromFiles[id];
                Form.ChangedMetadataDiffViewer.NewText = Data.TestCases[id].ExtractMetadata().PrettyPrint();
            }
        }
    }
}
