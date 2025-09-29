using Dokimion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class StepHandleDifferences : StepCode
    {
        Dictionary<string, string> TestCaseActionsFromFiles;

        public StepHandleDifferences(Panel panel, Data data, Updater form) : base(panel, data, form)
        {
            StepName = "Handle Differences";
            TestCaseActionsFromFiles = new();
        }

        public override void Init()
		{
		}

        public override StepCode? Prev()
        {
            Form.HandleDiffDiffViewer.OldText = "";
            Form.HandleDiffDiffViewer.NewText = "";
            return PrevStepCode;
        }

        public override StepCode? Next()
        {
            Form.HandleDiffDiffViewer.OldText = "";
            Form.HandleDiffDiffViewer.NewText = "";
            return NextStepCode;
        }

        public override void Activate()
        {
            Form.FeedbackTextBox.Text = "Comparing Test Cases";
            Form.HandleDiffDiffViewer.OldTextHeader = "From Dokimion";
            Form.HandleDiffDiffViewer.NewTextHeader = "From Repo";
            Form.HandleDiffDiffViewer.OldText = "";
            Form.HandleDiffDiffViewer.NewText = "";

            bool ok = CompareAllTestCases();
            if (!ok)
            {
                return;
            }

            Form.FeedbackTextBox.Text += "\r\nDone.";
            Form.PrevButton.Enabled = true;
            Form.NextButton.Enabled = true;
        }

        private bool CompareAllTestCases()
        {
            string repo = Data.GetRepoFolder();
            Form.HandleDiffDataGridView.Rows.Clear();
            TestCaseActionsFromFiles.Clear();
            IEnumerable<string> files = Directory.EnumerateFiles(repo, "*.json");
            foreach (string jsonFileName in files)
            {
                string id = Path.GetFileNameWithoutExtension(jsonFileName);
                if (Data.TestCases.ContainsKey(id))
                {
                    string stepFileName = Path.Combine(repo, id + ".txt");
                    string fileStep = "";
                    if (File.Exists(stepFileName))
                    {
                        string[] textLines;
                        try
                        {
                            textLines = File.ReadAllLines(stepFileName);
                        }
                        catch (Exception ex)
                        {
                            Form.FeedbackTextBox.Text += $"\r\nCannot read file {stepFileName} because \r\n{ex.Message}";
                            return false;
                        }

                        for (int line = 0; line < textLines.Length; line++)
                        {
                            fileStep += textLines[line] + "<br>\r\n";
                        }
                    }
                    else
                    {
                        stepFileName = Path.Combine(repo, id + ".html");
                        if (File.Exists(stepFileName))
                        {
                            try
                            {
                                fileStep = File.ReadAllText(stepFileName);
                            }
                            catch (Exception ex)
                            {
                                Form.FeedbackTextBox.Text += $"\r\nCannot read file {stepFileName} because \r\n{ex.Message}";
                                return false;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    TestCaseActionsFromFiles.Add(id, fileStep);

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
                        Form.HandleDiffDataGridView.Rows.Add(false, id, tc.name, "File is different from Dokimion");
                    }
                }
                else
                {
                    Form.HandleDiffDataGridView.Rows.Add(false, id, "", "Missing in Dokimion");
                }
            }

            return true;
        }

        public void HandleDiffSelectAllButton_Click()
        {
            var rows = Form.HandleDiffDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                selectCell.Value = true;
            }
        }

        public void HandleDiffClearAllButton_Click()
        {
            var rows = Form.HandleDiffDataGridView.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                selectCell.Value = false;
            }
        }

        public void UploadDiffToDokimionButton_Click()
        {
            int numSelected = 0;
            var rows = Form.HandleDiffDataGridView.Rows;
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
                Form.FeedbackTextBox.Text = "Select some test cases to upload and try again.";
                return;
            }

            Form.FeedbackTextBox.Text = $"Uploading {numSelected} test cases from files to Dokimion.";
            Form.FeedbackTextBox.Refresh();

            Form.HandleDiffProgressBar.Minimum = 0;
            Form.HandleDiffProgressBar.Maximum = numSelected;
            Form.HandleDiffProgressBar.Value = 0;
            Form.HandleDiffProgressBar.Step = 1;

            string repo = Data.GetRepoFolder();

            int testcasesChanged = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                if ((bool)selectCell.Value == false)
                {
                    continue;
                }
                string id = (string)rows[i].Cells[1].Value;
                string path = Path.Combine(repo, id + ".JSON");
                string json;
                try
                {
                    json = File.ReadAllText(path);
                }
                catch (Exception ex)
                {
                    Form.FeedbackTextBox.Text = $"Cannot upload test case {id} to Dokimion because:";
                    Form.FeedbackTextBox.Text += $"\r\n{ex.Message}.";
                    return;
                }

                // A convoluted means to populate a TestCaseForUpload from the human-readable metadata file...
                HumanMetadata? hmd = JsonConvert.DeserializeObject<HumanMetadata>(json);
                if (hmd == null)
                {
                    Form.FeedbackTextBox.Text = $"Cannot deserialize {path}.";
                    return;
                }
                Metadata md = new(hmd);
                json = JsonConvert.SerializeObject(md);
                TestCaseForUpload? testCase = JsonConvert.DeserializeObject<TestCaseForUpload>(json);
                if (testCase == null)
                {
                    Form.FeedbackTextBox.Text = $"Cannot de-reserialized {path}.";
                    return;
                }

                string action = "";
                try
                {
                    action = TestCaseActionsFromFiles[id];
                }
                catch (Exception ex)
                {
                    Form.FeedbackTextBox.Text = $"Cannot get pre-loaded action from file for ID {id} because: \r\n{ex.Message}.";
                    return;
                }
                testCase.steps = new();
                Step step = new Step();
                step.action = action;
                testCase.steps.Add(step);
                bool abort = false;
                UploadStatus status = Data.Dokimion.UploadTestCaseObjectToProject(repo, testCase, Data.Project);
                switch (status)
                {
                    case UploadStatus.Updated:
                        testcasesChanged++;
                        // If it is a new test case, add it to the Data.TestCases list
                        // so it is there next time we do the comparison of test cases.
                        TestCase? newTestCase = Data.Dokimion.GetTestCaseAsObject(testCase.id, Data.Project);
                        if (newTestCase != null)
                        {
                            if (Data.TestCases.ContainsKey(testCase.id))
                            {
                                Data.TestCases[testCase.id] = newTestCase;
                            }
                            else
                            {
                                Data.TestCases.Add(testCase.id, newTestCase);
                            }
                        }
                        break;
                    case UploadStatus.Error:
                        Form.FeedbackTextBox.Text = Data.Dokimion.Error;
                        break;
                    case UploadStatus.NotChanged:
                        break;
                    case UploadStatus.NoChange:
                        break;
                    case UploadStatus.Aborted:
                        abort = true;
                        break;
                }
                if (abort)
                {
                    break;
                }

                Form.HandleDiffProgressBar.PerformStep();
                Form.HandleDiffProgressBar.Refresh();
            }
            Form.HandleDiffDiffViewer.OldText = "";
            Form.HandleDiffDiffViewer.NewText = "";
            CompareAllTestCases();
            Form.FeedbackTextBox.Text += $"\r\n{testcasesChanged} test cases were uploaded to Dokimion.";
        }

        public void HandleDiffDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string? id = Form.HandleDiffDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (id == null)
            {
                Form.FeedbackTextBox.Text = $"ID is missing for row {e.RowIndex}";
            }
            else
            {
                try
                {
                    Form.HandleDiffDiffViewer.OldText = Data.TestCases[id].steps[0].action;
                }
                catch
                {
                    Form.HandleDiffDiffViewer.OldText = "";
                }
                if (TestCaseActionsFromFiles.ContainsKey(id))
                {
                    Form.HandleDiffDiffViewer.NewText = TestCaseActionsFromFiles[id];
                }
                else
                {
                    Form.HandleDiffDiffViewer.NewText = "";
                }
            }
        }

        public void HandleDiffRescanTestCasesButton_Click(object sender, EventArgs e)
        {
            CompareAllTestCases();
        }

    }
}
