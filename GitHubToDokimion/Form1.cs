namespace GitHubToDokimion
{
    public partial class Form1 : Form
    {
        Dokimion.Dokimion m_Dokimion;
        Dictionary<int, DocumentPair> m_Documents;
        Dictionary<int, TestCasePair> m_TestCases;
        Project m_Project;
        Settings m_Settings;

        public const int DOKIMION_TITLE_COLUMN = 0;
        public const int ID_COLUMN = 1;
        public const int STATUS_COLUMN = 2;
        public const int FILE_SYSTEM_TITLE_COLUMN = 3;
        public const int FILE_SYSTEM_FILENAME_COLUMN = 4;



        public Form1()
        {
            InitializeComponent();

            DokimionProjectsListBox.DisplayMember = "Name";

            TitleInDokimion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TitleInFileSystem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Status.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            TestCaseDataGridView.Rows.Clear();

            FilterListBox.Items.Clear();
            FilterListBox.Items.AddRange(new string[] { "No Filtering", "Not Equal", "Different", "Dokimion missing", "File System missing" });
            FilterListBox.SelectedIndex = 0;

            ProgressBar.Visible = false;

            FsToDokimionButton.Enabled = false;

            m_Dokimion = new Dokimion.Dokimion("", false);
            m_Documents = new Dictionary<int, DocumentPair>();
            m_TestCases = new Dictionary<int, TestCasePair>();
            m_Project = new();

            m_Settings = GetSettings();
            if (null == m_Settings)
            {
                m_Settings = new Settings();
            }
            FolderForAllProjectsCheckBox.Checked = m_Settings.useOneFolderForAllProjects;
            FolderTextBox.Text = m_Settings.repo;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "";
            StatusTextBox.Refresh();
            LoginDialog dlg = new();

            dlg.Username = m_Settings.username;
            dlg.ServerUrl = m_Settings.server;
            dlg.UseHttps = m_Settings.useHttps;

            ServerTextBox.Text = "";

            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;

            m_Settings.username = dlg.Username;
            m_Settings.server = dlg.ServerUrl;
            m_Settings.useHttps = dlg.UseHttps;
            SaveSettings();

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

        private Settings GetSettings()
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
            Settings? settings = null;
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
            if (settings == null)
            {
                settings = new Settings();
            }
            return settings;
        }

        private void SaveSettings()
        {
            try
            {
                File.WriteAllText("Updater.json", JsonConvert.SerializeObject(m_Settings));
            }
            catch
            {
                ; // Ignore errors
            }
        }

        private void GetProjectsFromDokimion()
        {
            DokimionProjectsListBox.Items.Clear();
            List<Project>? projects = m_Dokimion.GetProjects();
            if (projects == null)
            {
                StatusTextBox.Text = m_Dokimion.Error;
            }
            else
            {
                foreach (Project project in projects)
                {
                    DokimionProjectsListBox.Items.Add(project);
                }
            }
        }


        private void FsToDokimionButton_Click(object sender, EventArgs e)
        {
            var cells = TestCaseDataGridView.SelectedCells;
            if (cells.Count == 0)
            {
                StatusTextBox.Text = "Select a cell in the grid for the test case to send.";
                return;
            }
            var cell = cells[0];
            var row = TestCaseDataGridView.Rows[cell.RowIndex];
            int id = (int)row.Cells[ID_COLUMN].Value;

            StatusTextBox.Text = $"Sending test case {id} to Dokimion.\r\n";
            bool result = m_Dokimion.UploadPlainTextTextCase(m_Project, m_TestCases[id].fromDokimion, m_TestCases[id].fromGitHub);
            if (result)
            {
                StatusTextBox.Text += "Done.\r\n";
            }
            else
            {
                StatusTextBox.Text += m_Dokimion.Error;
            }
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshProjectListButton_Click(object sender, EventArgs e)
        {
            ClearCompares();
            StatusTextBox.Text = "Getting Projects from Dokimion.";
            StatusTextBox.Refresh();
            GetProjectsFromDokimion();
            StatusTextBox.Text += "\r\nDone.";
        }

        private void ClearCompares()
        {
            TestCaseDataGridView.Rows.Clear();
            FsToDokimionButton.Enabled = false;
            diffViewer1.OldText = null;
            diffViewer1.NewText = null;
            diffViewer1.Refresh();
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
                m_Settings.repo = dlg.SelectedPath;
                SaveSettings();
                ClearCompares();
            }
        }

        private void CompareTestCasesButton_Click(object sender, EventArgs e)
        {
            ClearCompares();
            m_Documents = new Dictionary<int, DocumentPair>();

            FilterListBox.SelectedIndex = 0;
            StatusTextBox.Text = "Busy loading test cases from Dokimion.";
            StatusTextBox.Refresh();

            Project? project = (Project?)DokimionProjectsListBox.SelectedItem;
            if (project == null)
            {
                StatusTextBox.Text = "Please select one of the projects to get the test cases for.";
                return;
            }
            m_Project = project;

            StatusTextBox.Text = "";
            GetDokimionTestCases(project);
            if (GetFileSystemTestCases(project))
            {
                CompareTestCases(project);
            }
        }

        private void GetDokimionTestCases(Project project)
        {
            StatusTextBox.Text = "Getting list of test cases from Dokimion.\r\n";
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
                    TestCaseDataGridView.Rows[index].Cells[DOKIMION_TITLE_COLUMN].Value = PlainTextFile.RemoveHtml(shortTestCase.name);
                    TestCaseDataGridView.Rows[index].Cells[STATUS_COLUMN].Value = "";
                }
            }
            StatusTextBox.Text += "Done.\r\n";
        }

        private bool GetFileSystemTestCases(Project project)
        {
            if (project != null)
            {
                string folderPath = FolderTextBox.Text;
                if (FolderForAllProjectsCheckBox.Checked)
                {
                    folderPath = Path.Combine(folderPath, project.Name);
                }
                DirectoryInfo? dirInfo = new DirectoryInfo(folderPath);
                if (dirInfo == null)
                {
                    StatusTextBox.Text += $"\r\nCannot get test cases from file system for folder {folderPath}.";
                    return false;
                }

                StatusTextBox.Text += "Getting test cases from file system.";
                ProgressBar.Minimum = 0;
                IEnumerable<FileInfo> files;
                try
                {
                    files = dirInfo.EnumerateFiles("*.txt");
                }
                catch (Exception ex)
                {
                    StatusTextBox.Text += $"\r\nCannot get test cases from file system for folder {folderPath} because:\r\n" + ex.Message;
                    return false;
                }

                if ((files == null) || (files.Count() == 0))
                {
                    StatusTextBox.Text += $"\r\nNo *.txt files found in folder {folderPath}.";
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
                    GetFileSystemTestCase(project, file);
                    ProgressBar.PerformStep();
                }
                ProgressBar.Visible = false;
                StatusTextBox.Text += "\r\nDone";
            }
            return true;
        }

        private void GetFileSystemTestCase(Project project, FileInfo file)
        {
            const string pattern = @"DOK(\d+)(_|\d|\w)*\.txt";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(file.Name);
            if (false == match.Success)
            {
                StatusTextBox.Text += $"\r\n{file.Name} does not match desired file name pattern.";
                return;
            }
            if (match.Groups[1].Captures.Count < 1)
            {
                StatusTextBox.Text += $"\r\n{file.Name} does not match desired file name pattern.";
                return;
            }
            string idString = match.Groups[1].Captures[0].Value;
            int testCaseId = int.Parse(idString);

            PlainTextFile plainTextFile = new();

            TestCaseForUpload? testcaseFromFile = plainTextFile.GetTestCaseFromFileSystem(file.FullName, project);
            if (testcaseFromFile == null)
            {
                StatusTextBox.Text += "\r\n" + plainTextFile.Error;
            }
            else
            {
                if (false == string.IsNullOrEmpty(plainTextFile.Error))
                {
                    StatusTextBox.Text += "\r\n" + plainTextFile.Error;
                }
                testcaseFromFile.id = testCaseId.ToString();
                int index = IndexForId(testCaseId);
                if (index < 0)
                {
                    index = TestCaseDataGridView.Rows.Add();
                    TestCaseDataGridView.Rows[index].Visible = true;
                    TestCaseDataGridView.Rows[index].Cells[ID_COLUMN].Value = testCaseId;
                }
                TestCaseDataGridView.Rows[index].Cells[FILE_SYSTEM_TITLE_COLUMN].Value = PlainTextFile.RemoveHtml(testcaseFromFile.name);
                TestCaseDataGridView.Rows[index].Cells[STATUS_COLUMN].Value = "";
                TestCaseDataGridView.Rows[index].Cells[FILE_SYSTEM_FILENAME_COLUMN].Value = file.FullName;
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
            m_Documents = new();
            m_TestCases = new();

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
            if (FolderForAllProjectsCheckBox.Checked)
            {
                folder = Path.Combine(folder, project.Name);
            }

            DocumentPair documentPair = new DocumentPair();
            PlainTextFile plainTextFile = new();
            TestCase? fullTestCase = m_Dokimion.GetTestCaseAsObject(id.ToString(), project);
            if (fullTestCase != null)
            {
                documentPair.ServerDocument = plainTextFile.GeneratePlainText(fullTestCase, project);
            }

            string filePath = (string)row.Cells[FILE_SYSTEM_FILENAME_COLUMN].Value;
            TestCaseForUpload? testcaseFromFile = plainTextFile.GetTestCaseFromFileSystem(filePath, project);
            if (testcaseFromFile != null)
            {
                documentPair.FileSystemDocument = plainTextFile.GeneratePlainText(testcaseFromFile, project);
            }

            m_Documents.Add(id, documentPair);

            TestCasePair testCasePair = new();
            testCasePair.fromDokimion = fullTestCase;
            testCasePair.fromGitHub = testcaseFromFile;
            m_TestCases.Add(id, testCasePair);

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
                // Use the items from Dokimion for those that are not in the file
                testcaseFromFile.id = fullTestCase.id;
                testcaseFromFile.lastModifiedTime = fullTestCase.lastModifiedTime;
                testcaseFromFile.automated = fullTestCase.automated;
                testcaseFromFile.broken = fullTestCase.broken;
                testcaseFromFile.deleted = fullTestCase.deleted;
                testcaseFromFile.locked = fullTestCase.locked;
                testcaseFromFile.launchBroken = fullTestCase.launchBroken;
                testcaseFromFile.attachments = fullTestCase.attachments;

                row.Cells[DOKIMION_TITLE_COLUMN].Value = PlainTextFile.RemoveHtml(fullTestCase.name);
                row.Cells[FILE_SYSTEM_TITLE_COLUMN].Value = PlainTextFile.RemoveHtml(testcaseFromFile.name);
                testcase = $"{id}: {fullTestCase.name}";
                if (m_Dokimion.IsTestCaseChanged(fullTestCase, testcaseFromFile))
                {
                    row.Cells[STATUS_COLUMN].Value = "<>";
                }
                else
                {
                    row.Cells[STATUS_COLUMN].Value = "=";
                }
            }
            m_Documents[id].TestCase = testcase;
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

        private void ProjectsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Documents = new Dictionary<int, DocumentPair>();
            ClearCompares();
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
                        case "<>":
                            TestCaseDataGridView.Rows[i].Visible = ((filter == "Different") || (filter == "Not Equal"));
                            break;
                        default:
                            TestCaseDataGridView.Rows[i].Visible = true;
                            break;
                    }
                }
            }
        }

        private void TestCaseDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = e.RowIndex;
            HandleDataGridClick(row);
        }

        private void TestCaseDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            HandleDataGridClick(row);
        }

        private void HandleDataGridClick(int row)
        {
            if (row < 0)
            {
                return;
            }
            int id = (int)TestCaseDataGridView.Rows[row].Cells[ID_COLUMN].Value;

            TestCaseNameTextBox.Text = PlainTextFile.RemoveHtml(m_Documents[id].TestCase);

            diffViewer1.OldText = PlainTextFile.RemoveHtml(m_Documents[id].ServerDocument);
            diffViewer1.NewText = PlainTextFile.RemoveHtml(m_Documents[id].FileSystemDocument);
            diffViewer1.Refresh();

            FsToDokimionButton.Text = $"Send Test Case {id} to Dokimion";
            FsToDokimionButton.Enabled = true;
        }

        private void FolderForAllProjectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_Settings.useOneFolderForAllProjects = FolderForAllProjectsCheckBox.Checked;
            SaveSettings();
        }

    }

    public class DocumentPair
    {
        public string TestCase = "";
        public string ServerDocument = "";
        public string FileSystemDocument = "";
    }

    public class TestCasePair
    {
        public TestCaseForUpload? fromDokimion;
        public TestCaseForUpload? fromGitHub;
    }

}
