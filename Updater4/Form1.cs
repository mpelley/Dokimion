
namespace Updater4
{
    public partial class Form1 : Form
    {
        Dokimion.Dokimion m_Dokimion;

        Dictionary<int, DocumentPair> m_TestCases;

        public const int DOKIMION_TITLE_COLUMN = 0;
        public const int ID_COLUMN = 1;
        public const int STATUS_COLUMN = 2;
        public const int SELECT_COLUMN = 3;
        public const int FILE_SYSTEM_TITLE_COLUMN = 4;

        public Form1()
        {
            InitializeComponent();

            m_Dokimion = new Dokimion.Dokimion("", false);

            ProjectsListBox.DisplayMember = "Name";

            TitleInDokimion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TitleInFileSystem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Status.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Selected.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            TestCaseDataGridView.Rows.Clear();

            FilterListBox.Items.Clear();
            FilterListBox.Items.AddRange(new string[] { "No Filtering", "Not Equal", "Dokimion Newer", "File System Newer", "Dokimion missing", "File System missing", "Unknown" });
            FilterListBox.SelectedIndex = 0;

            ProgressBar.Visible = false;

            m_TestCases = new Dictionary<int, DocumentPair>();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "";
            StatusTextBox.Refresh();
            LoginDialog dlg = new();

            Settings? settings = GetSettings();
            if (null == settings)
            {
                settings = new Settings();
            }
            else
            {
                dlg.Username = settings.username;
                dlg.ServerUrl = settings.server;
                dlg.UseHttps = settings.useHttps;
                FolderTextBox.Text = settings.repo;
            }
            ServerTextBox.Text = "";

            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;

            settings.username = dlg.Username;
            settings.server = dlg.ServerUrl;
            settings.useHttps = dlg.UseHttps;
            SaveSettings(settings);

            StatusTextBox.Text = "Logging into Dokimion.";
            StatusTextBox.Refresh();
            m_Dokimion = new Dokimion.Dokimion(dlg.ServerUrl, dlg.UseHttps);
            if (false == m_Dokimion.Login(dlg.Username, dlg.Password))
            {
                StatusTextBox.Text = m_Dokimion.Error;
            }
            else
            {
                ServerTextBox.Text = m_Dokimion.ServerUrl;
                StatusTextBox.Text += "\r\nGetting Projects from Dokimion.";
                StatusTextBox.Refresh();
                GetProjectsFromDokimion();
                StatusTextBox.Text += "\r\nDone.";
            }
        }

        private void GetProjectsFromDokimion()
        {
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

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "Getting Projects from Dokimion.";
            StatusTextBox.Refresh();
            GetProjectsFromDokimion();
            StatusTextBox.Text += "\r\nDone.";
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
                    StatusTextBox.Text = "Cannot decode Updater.json which contains: \r\n" + settingsJson;
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


        private void BrowseFileSystemButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Select the root of the GitHub repo clone for this project";
            dlg.ShowNewFolderButton = true;
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                FolderTextBox.Text = dlg.SelectedPath;
                Settings? settings = GetSettings();
                if (settings != null)
                {
                    settings.repo = dlg.SelectedPath;
                    SaveSettings(settings);
                }
                TestCaseDataGridView.Rows.Clear();
            }
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CompareButton_Click(object sender, EventArgs e)
        {
            TestCaseDataGridView.Rows.Clear();
            m_TestCases = new Dictionary<int, DocumentPair>();

            FilterListBox.SelectedIndex = 0;
            StatusTextBox.Text = "Busy loading test cases from Dokimion.";
            StatusTextBox.Refresh();

            Project? project = (Project?)ProjectsListBox.SelectedItem;
            if (project == null)
            {
                StatusTextBox.Text = "Please select one of the projects to get the test cases for.";
                return;
            }

            StatusTextBox.Text = "";
            GetDokimionTestCases(project);
            if (GetFileSystemTestCases(project))
            {
                CompareTestCases(project);
            }
        }


        private int IndexForId(int id)
        {
            foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
            {
                if ((int)row.Cells[ID_COLUMN].Value == id)
                {
                    return row.Index;
                }
            }
            return -1;
        }


        private void GetDokimionTestCases(Project project)
        {
            string projectId = project.id;
            List<TestCaseShort>? testcases = m_Dokimion.GetTestCaseSummariesForProject(projectId);

            if (testcases == null)
            {
                StatusTextBox.Text = m_Dokimion.Error;
            }
            else
            {
                foreach (TestCaseShort shortTestCase in testcases)
                {
                    int testCaseId = int.Parse(shortTestCase.id);
                    int index = IndexForId(testCaseId);
                    if (index < 0)
                    {
                        index = TestCaseDataGridView.Rows.Add();
                        TestCaseDataGridView.Rows[index].Visible = true;
                        TestCaseDataGridView.Rows[index].Cells[ID_COLUMN].Value = testCaseId;
                    }
                    TestCaseDataGridView.Rows[index].Cells[DOKIMION_TITLE_COLUMN].Value = shortTestCase.name;
                    TestCaseDataGridView.Rows[index].Cells[STATUS_COLUMN].Value = "";
                    TestCaseDataGridView.Rows[index].Cells[SELECT_COLUMN].Value = false;
                }
            }
        }

        private bool GetFileSystemTestCases(Project project)
        {
            if (project != null)
            {
                DirectoryInfo? dirInfo = new DirectoryInfo(FolderTextBox.Text);
                if (dirInfo == null)
                {
                    StatusTextBox.Text += $"\r\nCannot get test cases from file system for folder {FolderTextBox.Text}.";
                    return false;
                }

                StatusTextBox.Text += "Getting  test cases from file system.";
                ProgressBar.Minimum = 0;
                IEnumerable<FileInfo> files;
                try
                {
                    files = dirInfo.EnumerateFiles("*.xml");
                }
                catch (Exception ex)
                {
                    StatusTextBox.Text += $"\r\nCannot get test cases from file system for folder {FolderTextBox.Text} because:\r\n" + ex.Message;
                    return false;
                }
                ProgressBar.Maximum = files.Count();
                ProgressBar.Step = 1;
                ProgressBar.Value = 0;
                ProgressBar.Visible = true;
                ProgressBar.Refresh();

                foreach (var file in files)
                {
                    string path = file.FullName;
                    GetFileSystemTestCase(project, path);
                    ProgressBar.PerformStep();
                }
                ProgressBar.Visible = false;
                StatusTextBox.Text += "\r\nDone";
            }
            return true;
        }

        private void GetFileSystemTestCase(Project project, string path)
        {
            TestCaseForUpload? testcaseFromFile = m_Dokimion.GetTestCaseFromFileSystem(path, project);
            if (testcaseFromFile == null)
            {
                StatusTextBox.Text += "\r\n" + m_Dokimion.Error;
            }
            else
            {
                int testCaseId = int.Parse(testcaseFromFile.id);
                int index = IndexForId(testCaseId);
                if (index < 0)
                {
                    index = TestCaseDataGridView.Rows.Add();
                    TestCaseDataGridView.Rows[index].Visible = true;
                    TestCaseDataGridView.Rows[index].Cells[ID_COLUMN].Value = testCaseId;
                }
                TestCaseDataGridView.Rows[index].Cells[FILE_SYSTEM_TITLE_COLUMN].Value = testcaseFromFile.name;
                TestCaseDataGridView.Rows[index].Cells[STATUS_COLUMN].Value = "";
                TestCaseDataGridView.Rows[index].Cells[SELECT_COLUMN].Value = false;
            }
        }

        private void CompareTestCases(Project project)
        {
            StatusTextBox.Text += "\r\nComparing Test Cases";
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = TestCaseDataGridView.Rows.Count;
            ProgressBar.Step = 1;
            ProgressBar.Value = 0;
            ProgressBar.Visible = true;
            ProgressBar.Refresh();

            foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
            {
                CompareTestCase(project, row);
                ProgressBar.PerformStep();
            }
            ProgressBar.Visible = false;
            StatusTextBox.Text += "\r\nDone";
        }

        private void CompareTestCase(Project project, DataGridViewRow row)
        {
            string testcase = "";
            int id = (int)row.Cells[ID_COLUMN].Value;
            string folder = FolderTextBox.Text;
            TestCase? fullTestCase = m_Dokimion.GetTestCaseAsObject(id.ToString(), project);
            string filePath = Path.Combine(folder, id + ".xml");
            TestCaseForUpload? testcaseFromFile = m_Dokimion.GetTestCaseFromFileSystem(filePath, project);
            if (fullTestCase == null && testcaseFromFile == null)
            {
                StatusTextBox.Text += $"Cannot get test case from either Dokimion or file system for id {id}.";
                return;
            }

            if (fullTestCase == null && testcaseFromFile != null)
            {
                row.Cells[DOKIMION_TITLE_COLUMN].Value = "";
                row.Cells[STATUS_COLUMN].Value = "«";
                testcase = $"{id}: {testcaseFromFile.name}";
            }
            else if (testcaseFromFile == null && fullTestCase != null)
            {
                row.Cells[FILE_SYSTEM_TITLE_COLUMN].Value = "";
                row.Cells[STATUS_COLUMN].Value = "»";
                testcase = $"{id}: {fullTestCase.name}";
            }
            else if (testcaseFromFile != null && fullTestCase != null)
            {
                row.Cells[DOKIMION_TITLE_COLUMN].Value = fullTestCase.name;
                row.Cells[FILE_SYSTEM_TITLE_COLUMN].Value = testcaseFromFile.name;
                testcase = $"{id}: {fullTestCase.name}";
                if (fullTestCase.lastModifiedTime == 0 || testcaseFromFile.lastModifiedTime == 0)
                {
                    row.Cells[STATUS_COLUMN].Value = "<>";
                }
                else if (fullTestCase.lastModifiedTime == testcaseFromFile.lastModifiedTime)
                {
                    if (m_Dokimion.IsTestCaseChanged(fullTestCase, testcaseFromFile))
                    {
                        row.Cells[STATUS_COLUMN].Value = "<>";
                    }
                    else
                    {
                        row.Cells[STATUS_COLUMN].Value = "=";
                    }
                }
                else if (fullTestCase.lastModifiedTime > testcaseFromFile.lastModifiedTime)
                {
                    row.Cells[STATUS_COLUMN].Value = ">";
                }
                else
                {
                    row.Cells[STATUS_COLUMN].Value = "<";
                }
            }

            string serverXml = "";
            if (fullTestCase != null)
            {
                serverXml = m_Dokimion.GenerateXml(fullTestCase, project);
            }
            string fileSystemXml = "";
            try
            {
                fileSystemXml = File.ReadAllText(filePath);
            }
            catch { }
            DocumentPair dp = new DocumentPair
            {
                TestCase = testcase,
                ServerDocument = serverXml,
                FileSystemDocument = fileSystemXml
            };
            m_TestCases[id] = dp;


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
            for (int i = 0; i < TestCaseDataGridView.Rows.Count; i++)
            {
                if (TestCaseDataGridView.Rows[i].Visible)
                {
                    TestCaseDataGridView.Rows[i].Cells[SELECT_COLUMN].Value = state;
                }
                else
                {
                    TestCaseDataGridView.Rows[i].Cells[SELECT_COLUMN].Value = false;

                }
            }
        }

        private void DokimionToFsButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FolderTextBox.Text))
            {
                StatusTextBox.Text = "Select a folder for saving the test cases.";
                return;
            }

            Project? project = (Project?)ProjectsListBox.SelectedItem;

            if (project == null)
            {
                StatusTextBox.Text = "We need a project to be selected.";
            }
            else
            {
                int casesToDownload = 0;
                foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
                {
                    if (row.Visible && (bool)row.Cells[SELECT_COLUMN].Value == true)
                    {
                        casesToDownload++;
                    }
                }

                StatusTextBox.Text = "Downloading test cases to file system... ";
                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = casesToDownload;
                ProgressBar.Step = 1;
                ProgressBar.Value = 0;
                ProgressBar.Visible = true;
                ProgressBar.Refresh();


                foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
                {
                    if (row.Visible && (bool)row.Cells[SELECT_COLUMN].Value == true)
                    {
                        string? id = row.Cells[ID_COLUMN].Value.ToString();
                        if (id != null)
                        {
                            if (false == m_Dokimion.DownloadTestcase(id, project, FolderTextBox.Text))
                            {
                                StatusTextBox.Text += "\r\n" + m_Dokimion.Error;
                                return;
                            }

                            string filePath = Path.Combine(FolderTextBox.Text, id + ".xml");
                            GetFileSystemTestCase(project, filePath);
                            CompareTestCase(project, row);
                            row.Cells[SELECT_COLUMN].Value = false;
                        }
                        ProgressBar.PerformStep();
                    }
                }
                ProgressBar.Visible = false;
                StatusTextBox.Text += "\r\nDone.";
            }
        }

        private void FsToDokimionButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FolderTextBox.Text))
            {
                StatusTextBox.Text = "Select a folder for saving the test cases.";
                return;
            }

            Project? project = (Project?)ProjectsListBox.SelectedItem;

            if (project == null)
            {
                StatusTextBox.Text = "We need a project to be selected.";
            }
            else
            {
                StatusTextBox.Text = "Uploading test cases to Dokimion.";
                StatusTextBox.Refresh();
                int testcasesChanged = 0;

                ProgressBar.Maximum = TestCaseDataGridView.Rows.Count;
                ProgressBar.Step = 1;
                ProgressBar.Value = 0;
                ProgressBar.Visible = true;
                ProgressBar.Refresh();

                foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
                {
                    bool abort = false;
                    if (row.Visible && (bool)row.Cells[SELECT_COLUMN].Value == true)
                    {
                        string? id = row.Cells[ID_COLUMN].Value.ToString();
                        if (id != null)
                        {
                            switch (m_Dokimion.UploadTestCaseToProject(FolderTextBox.Text, id, project))
                            {
                                case UploadStatus.Updated:
                                    testcasesChanged++;
                                    break;
                                case UploadStatus.Error:
                                    StatusTextBox.Text = m_Dokimion.Error;
                                    break;
                                case UploadStatus.NotChanged:
                                    break;
                                case UploadStatus.NoChange:
                                    break;
                                case UploadStatus.Aborted:
                                    abort = true;
                                    break;
                            }
                            CompareTestCase(project, row);
                            row.Cells[SELECT_COLUMN].Value = false;
                        }
                    }
                    if (abort)
                    {
                        StatusTextBox.Text += "Aborted.";
                        return;
                    }
                    ProgressBar.PerformStep();
                }

                ProgressBar.Visible = false;
                StatusTextBox.Text += $"\r\n{testcasesChanged} test cases were sent to Dokimion";
                StatusTextBox.Text += "\r\nDone.";
            }
        }

        private void SaveProjectButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "";
            string folder = FolderTextBox.Text;
            if (string.IsNullOrEmpty(folder))
            {
                StatusTextBox.Text = "Select a folder for saving the project.";
                return;
            }

            Project? project = (Project?)ProjectsListBox.SelectedItem;
            if (project == null)
            {
                StatusTextBox.Text = "We need a project to be selected.";
                return;
            }

            string filePath = Path.Combine(folder, $"{project.id}.json");
            string? json = m_Dokimion.GetProject(project.id);
            if (json == null)
            {
                StatusTextBox.Text = $"Cannot get project {project.Name} from Dokimion.";
                return;
            }
            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                StatusTextBox.Text = $"Could not save project {project.Name} because:\r\n{ex.Message}";
            }

            filePath = Path.Combine(folder, $"{project.id}_attributes.json");
            json = m_Dokimion.GetProjectAttributes(project.id);
            if (json == null)
            {
                StatusTextBox.Text = $"Cannot get project attributes for {project.Name} from Dokimion.";
                return;
            }
            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                StatusTextBox.Text = $"Could not save project attributes for {project.Name} because:\r\n{ex.Message}";
            }

            StatusTextBox.Text = $"Project {project.Name} saved.";
        }

        private void RestoreProjectButton_Click(object sender, EventArgs e)
        {

            string folder = FolderTextBox.Text;
            if (string.IsNullOrEmpty(folder))
            {
                StatusTextBox.Text = "Select a folder to create project from.";
                return;
            }

            StatusTextBox.Text = $"Restoring project from {folder}.";
            StatusTextBox.Refresh();

            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            IEnumerable<FileInfo> files;
            try
            {
                files = dirInfo.EnumerateFiles("*.json");
            }
            catch (Exception ex)
            {
                StatusTextBox.Text += $"\r\nCannot get test cases from file system for folder {FolderTextBox.Text} because:\r\n" + ex.Message;
                return;
            }
            if (files.Count() != 2)
            {
                StatusTextBox.Text += $"\r\nExpect two .JSON files in folder {FolderTextBox.Text}";
                return;
            }
            string projectFile = "";
            string attributesFile = "";
            if (files.First().Name.Contains("_attributes"))
            {
                attributesFile = files.First().FullName;
            }
            else
            {
                projectFile = files.First().FullName;
            }
            if (files.Last().Name.Contains("_attributes"))
            {
                attributesFile = files.Last().FullName;
            }
            else
            {
                projectFile = files.Last().FullName;
            }
            if (projectFile == "" || attributesFile == "")
            {
                StatusTextBox.Text += $"\r\nExpect a JSON file with _attributes in its name in folder {FolderTextBox.Text}";
                return;
            }

            var project = m_Dokimion.GetProjectFromJson(projectFile);
            if (project == null)
            {
                StatusTextBox.Text += $"\r\nCannot decode file {projectFile}";
                return;
            }

            string? dokimionProject = m_Dokimion.GetProject(project.id);
            bool doUpdate = false;
            if (false == string.IsNullOrEmpty(dokimionProject))
            {
                var answer = MessageBox.Show($"Project {project.name} already exists on Dokimion server at id {project.id}.  Do you wish to upload attributes?", "Project Exists", MessageBoxButtons.YesNo);
                if (answer == DialogResult.No)
                {
                    StatusTextBox.Text += $"\r\nCreation Aborted.";
                    return;
                }
                doUpdate = true;
            }

            bool success = m_Dokimion.CreateProjectFromFiles(projectFile, attributesFile, doUpdate);
            if (false == success)
            {
                StatusTextBox.Text += $"Cannot restore project from {projectFile} because\r\n{m_Dokimion.Error}";
                return;
            }

            StatusTextBox.Text += "\r\nRefreshing projects from Dokimion";
            StatusTextBox.Refresh();
            GetProjectsFromDokimion();

            StatusTextBox.Text += "\r\nDone.";
        }

        private void FilterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? filter = (string?)FilterListBox.SelectedItem;
            for (int i = 0; i < TestCaseDataGridView.Rows.Count; i++)
            {
                if (filter == "No Filtering")
                {
                    TestCaseDataGridView.Rows[i].Visible = true;
                }
                else
                {
                    switch (TestCaseDataGridView.Rows[i].Cells[STATUS_COLUMN].Value)
                    {
                        case "»":
                            TestCaseDataGridView.Rows[i].Visible = ((filter == "File System missing") || (filter == "Not Equal"));
                            break;
                        case "«":
                            TestCaseDataGridView.Rows[i].Visible = ((filter == "Dokimion missing") || (filter == "Not Equal"));
                            break;
                        case "=":
                            TestCaseDataGridView.Rows[i].Visible = false;
                            break;
                        case ">":
                            TestCaseDataGridView.Rows[i].Visible = ((filter == "Dokimion Newer") || (filter == "Not Equal"));
                            break;
                        case "<":
                            TestCaseDataGridView.Rows[i].Visible = ((filter == "File System Newer") || (filter == "Not Equal"));
                            break;
                        case "<>":
                            TestCaseDataGridView.Rows[i].Visible = ((filter == "Unknown") || (filter == "Not Equal"));
                            break;
                        default:
                            TestCaseDataGridView.Rows[i].Visible = true;
                            break;
                    }
                }
            }
        }

        private void ShowDiffsButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "";
            ShowDiffs dlg = new();

            foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
            {
                if (row.Visible && (bool)row.Cells[SELECT_COLUMN].Value == true)
                {
                    int id = (int)row.Cells[ID_COLUMN].Value;
                    if (m_TestCases.ContainsKey(id))
                    {
                        dlg.DocumentPairs.Add(m_TestCases[id]);
                    }
                }
            }
            if (dlg.DocumentPairs.Count > 0)
            {
                dlg.ShowDialog();
            }
            else
            {
                StatusTextBox.Text = "Please select some test cases to show the differences.";
            }
        }

        private void DeleteEmptyButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "Deleting empty test cases.";
            Project? project = (Project?)ProjectsListBox.SelectedItem;
            if (project == null)
            {
                StatusTextBox.Text += "\r\nWe need a project to be selected.";
                return;
            }

            string projectId = project.id;
            List<TestCaseShort>? testcases = m_Dokimion.GetTestCaseSummariesForProject(projectId);

            if (testcases == null)
            {
                StatusTextBox.Text += "\r\n" + m_Dokimion.Error;
            }
            else
            {
                ProgressBar.Maximum = testcases.Count();
                ProgressBar.Step = 1;
                ProgressBar.Value = 0;
                ProgressBar.Visible = true;
                ProgressBar.Refresh();
                int deleted = 0;
                foreach (TestCaseShort shortTestCase in testcases)
                {
                    if (shortTestCase.name == "<<empty>>")
                    {
                        if (false == m_Dokimion.DeleteTestCase(projectId, shortTestCase.id))
                        {
                            StatusTextBox.Text += "\r\n" + m_Dokimion.Error;
                        }
                        deleted++;
                    }
                    ProgressBar.PerformStep();
                }
                if (deleted == 0)
                {
                    StatusTextBox.Text += "\r\nNo test cases deleted.";
                }
                else
                {
                    StatusTextBox.Text += $"\r\n{deleted} Empty test cases were deleted.";
                }
            }
            ProgressBar.Visible = false;
        }

        private void ProjectsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestCaseDataGridView.Rows.Clear();
        }
    }
}
