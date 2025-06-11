namespace GitHubToDokimion
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

            DokimionProjectsListBox.DisplayMember = "Name";

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

        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "Getting Projects from Dokimion.";
            StatusTextBox.Refresh();
            GetProjectsFromDokimion();
            StatusTextBox.Text += "\r\nDone.";
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

        private void GetTestCasesButton_Click(object sender, EventArgs e)
        {
            TestCaseDataGridView.Rows.Clear();
            m_TestCases = new Dictionary<int, DocumentPair>();

            FilterListBox.SelectedIndex = 0;
            StatusTextBox.Text = "Busy loading test cases from Dokimion.";
            StatusTextBox.Refresh();

            Project? project = (Project?)DokimionProjectsListBox.SelectedItem;
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
                    files = dirInfo.EnumerateFiles("*.md");
                }
                catch (Exception ex)
                {
                    StatusTextBox.Text += $"\r\nCannot get test cases from file system for folder {folderPath} because:\r\n" + ex.Message;
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
            MarkdownFile markdownFile = new();
            TestCaseForUpload? testcaseFromFile = markdownFile.GetTestCaseFromFileSystem(path, project);
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
            if (FolderForAllProjectsCheckBox.Checked)
            {
                folder = Path.Combine(folder, project.Name);
            }
            TestCase? fullTestCase = m_Dokimion.GetTestCaseAsObject(id.ToString(), project);
            string filePath = Path.Combine(folder, id + ".md");
            PlainTextFile plainTextFile = new();

            TestCaseForUpload? testcaseFromFile = plainTextFile.GetTestCaseFromFileSystem(filePath, project);
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

            string serverMarkdown = "";
            if (fullTestCase != null)
            {
                serverMarkdown = m_Dokimion.GenerateMarkdown(fullTestCase, project);
            }
            string fileSystemMarkdown = "";
            try
            {
                fileSystemMarkdown = File.ReadAllText(filePath);
            }
            catch { }
            DocumentPair dp = new DocumentPair
            {
                TestCase = testcase,
                ServerDocument = serverMarkdown,
                FileSystemDocument = fileSystemMarkdown
            };
            m_TestCases[id] = dp;
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

        }
    }

    public class DocumentPair
    {
        public string TestCase = "";
        public string ServerDocument = "";
        public string FileSystemDocument = "";
    }

}
