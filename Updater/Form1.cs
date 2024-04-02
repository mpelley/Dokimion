using Newtonsoft.Json;
using Serilog;

namespace Updater
{
    public partial class Form1 : Form
    {
        private Dokimion m_Dokimion;

        public Form1()
        {
            InitializeComponent();
            TestCaseListBox.CheckOnClick = true;
            TestCaseListBox.DisplayMember = "Display";
            ProjectsListBox.DisplayMember = "Name";
            m_Dokimion = new Dokimion("");

        }

        private void DokimionSettingsButton_Click(object sender, EventArgs e)
        {
            DokimionSettings dlg = new DokimionSettings();

            Settings? settings = GetSettings();
            if (null == settings)
            {
                settings = new Settings();
            }
            else
            {
                dlg.Username = settings.username;
                dlg.ServerUrl = settings.server;
                RepoPathTextBox.Text = settings.repo;
            }

            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;

            settings.username = dlg.Username;
            settings.server = dlg.ServerUrl;
            try
            {
                File.WriteAllText("Updater.json", JsonConvert.SerializeObject(settings));
            }
            catch
            {
                ; // Ignore errors
            }

            m_Dokimion = new Dokimion(dlg.ServerUrl);
            if (false == m_Dokimion.Login(dlg.Username, dlg.Password))
            {
                StatusTextBox.Text = m_Dokimion.Error;
            }
            else
            {
                StatusTextBox.Text = "";
                ProjectsListBox.Items.Clear();
                List<Project>? projects = m_Dokimion.GetProjects();
                if (projects == null)
                {
                    StatusTextBox.Text = m_Dokimion.Error;
                }
                else
                {
                    foreach (Project project in projects)
                    {
                        ProjectsListBox.Items.Add(project);
                    }
                }
            }
        }

        private Settings? GetSettings()
        {
            string settingsJson = "";
            try
            {
                settingsJson = File.ReadAllText("Updater.json");
            }
            catch
            {
                // Ignore errors
            }
            Settings? settings = new Settings();
            if (false == string.IsNullOrEmpty(settingsJson))
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
                }
                catch
                {
                    StatusTextBox.Text = "Cannot decode Updater.json which contains: \n" + settingsJson;
                }
            }
            return settings;
        }

        private void SaveSettings(Settings settings)
        {
            try
            {
                File.WriteAllText("Updater.json", JsonConvert.SerializeObject(settings));
            }
            catch
            {
                ; // Ignore errors
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GetTestCasesButton_Click(object sender, EventArgs e)
        {
            TestCaseListBox.Items.Clear();
            Project? item = (Project?)ProjectsListBox.SelectedItem;
            if (item == null)
            {
                StatusTextBox.Text = "Please select one of the projects to get the test cases for";
                return;
            }
            string project = item.id;
            List<TestCaseShort>? testcases = m_Dokimion.GetTestCaseSummariesForProject(project);
            if (testcases == null)
            {
                StatusTextBox.Text = m_Dokimion.Error;
            }
            else
            {
                foreach (TestCaseShort testCase in testcases)
                {
                    TestCaseListBox.Items.Add(testCase);
                }
                SetAllTestCases(true);
            }
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            SetAllTestCases(true);
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            SetAllTestCases(false);
        }

        private void SetAllTestCases(bool state)
        {
            for (int i = 0; i < TestCaseListBox.Items.Count; i++)
            {
                TestCaseListBox.SetItemChecked(i, state);
            }
        }

        private void BrowseGitHubButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Select the root of the GitHub repo clone";
            dlg.ShowNewFolderButton = true;
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                RepoPathTextBox.Text = dlg.SelectedPath;
                Settings? settings = GetSettings();
                if (settings != null)
                {
                    settings.repo = dlg.SelectedPath;
                    SaveSettings(settings);
                }
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (TestCaseListBox.CheckedItems.Count == 0)
            {
                StatusTextBox.Text = "Please select one or more test cases to download.";
                return;
            }
            if (string.IsNullOrEmpty(RepoPathTextBox.Text))
            {
                StatusTextBox.Text = "Please enter a path to the local repo clone.";
                return;
            }
            Project? project = (Project?)ProjectsListBox.SelectedItem;
            if (project == null)
            {
                StatusTextBox.Text = "Please select one of the projects to get the test cases for";
                return;
            }

            StatusTextBox.Text = "";
            m_Dokimion.Error = "";
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Step = 1;
            ProgressBar.Maximum = TestCaseListBox.CheckedItems.Count;
            foreach (TestCaseShort item in TestCaseListBox.CheckedItems)
            {
                string folderPath = Path.Combine(RepoPathTextBox.Text, project.Name);
                if (false == m_Dokimion.DownloadTestcase(item.id, project, folderPath))
                {
                    StatusTextBox.Text = m_Dokimion.Error;
                    return;
                }
                ProgressBar.PerformStep();
            }

            ProgressBar.Value = 0;
            StatusTextBox.Text = (m_Dokimion.Error == "") ?
                $"{TestCaseListBox.CheckedItems.Count} test cases saved" :
                m_Dokimion.Error;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (TestCaseListBox.CheckedItems.Count == 0)
            {
                StatusTextBox.Text = "Please select one or more test cases to upload.";
                return;
            }
            if (string.IsNullOrEmpty(RepoPathTextBox.Text))
            {
                StatusTextBox.Text = "Please enter a path to the local repo clone.";
                return;
            }
            Project? project = (Project?)ProjectsListBox.SelectedItem;
            if (project == null)
            {
                StatusTextBox.Text = "Please select one of the projects to get the test cases for";
                return;
            }

            StatusTextBox.Text = "";
            m_Dokimion.Error = "";
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Step = 1;
            ProgressBar.Maximum = TestCaseListBox.CheckedItems.Count;
            int testcasesChanged = 0;

            StatusTextBox.Text = $"Evaluating {TestCaseListBox.CheckedItems.Count} test cases";

            string folderPath = Path.Combine(RepoPathTextBox.Text, project.Name);

            foreach (TestCaseShort item in TestCaseListBox.CheckedItems)
            {
                bool abort = false;
                switch  (m_Dokimion.UploadFileToProject(folderPath, item.id, project, out bool changed))
                {
                    case UploadStatus.Updated:
                        Log.Information($"Test case {item.id}, \"{item.name}\" written to Project \"{project.name}\".");
                        testcasesChanged++;
                        break;
                    case UploadStatus.Error:
                        StatusTextBox.Text = m_Dokimion.Error;
                        Log.Error($"Error uploading Test case {item.id}, \"{item.name}\" to Project \"{project.name}\": {m_Dokimion.Error}.");
                        break;
                    case UploadStatus.NotChanged:
                        Log.Information($"Test case {item.id}, \"{item.name}\" in Project \"{project.name}\" was newer on server and not updated.");
                        break;
                    case UploadStatus.NoChange:
                        Log.Information($"Test case {item.id}, \"{item.name}\" in Project \"{project.name}\" did not need to be updated.");
                        break;
                    case UploadStatus.Aborted:
                        Log.Information("Upload of test cases aborted by user.");
                        abort = true; 
                        break;
                }

                ProgressBar.PerformStep();

                if (abort)
                {
                    break;
                }
            }

            ProgressBar.Value = 0;
            switch (testcasesChanged)
            {
                case 0:
                    StatusTextBox.Text = $"No test cases were updated.";
                    break;
                case 1:
                    StatusTextBox.Text = $"One test case was updated.";
                    break;
                default:
                    StatusTextBox.Text = $"{testcasesChanged} test cases were updated.";
                    break;
            }

        }

        private void ProjectsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestCaseListBox.Items.Clear();
        }
    }
}