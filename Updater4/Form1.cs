
namespace Updater4
{
    public partial class Form1 : Form
    {
        Dokimion.Dokimion m_Dokimion;

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
            FilterListBox.Items.AddRange(new string[] { "None", "Dokimion Newer", "File System Newer", "Dokimion missing", "File System missing", "Unknown" });
            FilterListBox.SelectedIndex = 0;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
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

            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;

            settings.username = dlg.Username;
            settings.server = dlg.ServerUrl;
            settings.useHttps = dlg.UseHttps;
            SaveSettings(settings);

            m_Dokimion = new Dokimion.Dokimion(dlg.ServerUrl, dlg.UseHttps);
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
            }

        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CompareButton_Click(object sender, EventArgs e)
        {
            TestCaseDataGridView.Rows.Clear();
            FilterListBox.SelectedIndex = 0;
            StatusTextBox.Text = "Busy loading test cases from Dokimion.";
            Thread.Sleep(TimeSpan.FromMilliseconds(100));

            Project? project = (Project?)ProjectsListBox.SelectedItem;
            if (project == null)
            {
                StatusTextBox.Text = "Please select one of the projects to get the test cases for";
                return;
            }

            GetDokimionTestCases(project);
            GetFileSystemTestCases(project);
            CompareTestCases(project);
            StatusTextBox.Text = "Done";
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

        private void GetFileSystemTestCases(Project project)
        {
            if (project != null)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(FolderTextBox.Text);
                foreach (var file in dirInfo.EnumerateFiles())
                {
                    string path = file.FullName;
                    TestCaseForUpload? testcaseFromFile = m_Dokimion.GetTestCaseFromFileSystem(path, project);
                    if (testcaseFromFile == null)
                    {
                        StatusTextBox.Text = m_Dokimion.Error;
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
            }
        }

        private void CompareTestCases(Project project)
        {
            foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
            {
                CompareTestCase(project, row);
            }
        }

        private void CompareTestCase(Project project, DataGridViewRow row)
        {
            if (string.IsNullOrEmpty((string)row.Cells[DOKIMION_TITLE_COLUMN].Value))
            {
                row.Cells[STATUS_COLUMN].Value = "�";
            }
            else if (string.IsNullOrEmpty((string)row.Cells[FILE_SYSTEM_TITLE_COLUMN].Value))
            {
                row.Cells[STATUS_COLUMN].Value = "�";
            }

            int id = (int)row.Cells[ID_COLUMN].Value;
            string folder = FolderTextBox.Text;
            TestCase? fullTestCase = m_Dokimion.GetTestCaseAsObject(id.ToString(), project);
            string filePath = Path.Combine(folder, id + ".xml");
            TestCaseForUpload? testcaseFromFile = m_Dokimion.GetTestCaseFromFileSystem(filePath, project);
            if (fullTestCase == null)
            {
                row.Cells[DOKIMION_TITLE_COLUMN].Value = "";
                row.Cells[STATUS_COLUMN].Value = "�";
            }
            else if (testcaseFromFile == null)
            {
                row.Cells[FILE_SYSTEM_TITLE_COLUMN].Value = "";
                row.Cells[STATUS_COLUMN].Value = "�";
            }
            else
            {
                row.Cells[DOKIMION_TITLE_COLUMN].Value = fullTestCase.name;
                row.Cells[FILE_SYSTEM_TITLE_COLUMN].Value = testcaseFromFile.name;
                if (fullTestCase.lastModifiedTime == testcaseFromFile.lastModifiedTime)
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
                StatusTextBox.Text = "Downloading test cases to file system.";
                Thread.Sleep(TimeSpan.FromMilliseconds(100));

                foreach (DataGridViewRow row in TestCaseDataGridView.Rows)
                {
                    if (row.Visible && (bool)row.Cells[SELECT_COLUMN].Value == true)
                    {
                        string? id = row.Cells[ID_COLUMN].Value.ToString();
                        if (id != null)
                        {
                            if (false == m_Dokimion.DownloadTestcase(id, project, FolderTextBox.Text))
                            {
                                StatusTextBox.Text = m_Dokimion.Error;
                                return;
                            }
                            CompareTestCase(project, row);
                            row.Cells[SELECT_COLUMN].Value = false;
                        }
                    }
                }
                StatusTextBox.Text = "Done.";
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
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                int testcasesChanged = 0;

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
                        StatusTextBox.Text = "Aborted.";
                        return;
                    }
                }

                StatusTextBox.Text = "Done.";
            }
        }

        private void FilterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? filter = (string?)FilterListBox.SelectedItem;
            for (int i = 0; i < TestCaseDataGridView.Rows.Count; i++)
            {
                if (filter == "None")
                {
                    TestCaseDataGridView.Rows[i].Visible = true;
                }
                else
                {
                    switch (TestCaseDataGridView.Rows[i].Cells[STATUS_COLUMN].Value)
                    {
                        case "�":
                            TestCaseDataGridView.Rows[i].Visible = (filter == "File System missing");
                            break;
                        case "�":
                            TestCaseDataGridView.Rows[i].Visible = (filter == "Dokimion missing");
                            break;
                        case "=":
                            TestCaseDataGridView.Rows[i].Visible = false;
                            break;
                        case ">":
                            TestCaseDataGridView.Rows[i].Visible = (filter == "Dokimion Newer");
                            break;
                        case "<":
                            TestCaseDataGridView.Rows[i].Visible = (filter == "File System Newer");
                            break;
                        case "<>":
                            TestCaseDataGridView.Rows[i].Visible = (filter == "Unknown");
                            break;
                        default:
                            TestCaseDataGridView.Rows[i].Visible = true;
                            break;
                    }
                }
            }
        }

    }
}
