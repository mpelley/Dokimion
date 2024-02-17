using Newtonsoft.Json;
using System.Windows.Forms.VisualStyles;

namespace Cloner
{
    public partial class Form1 : Form
    {
        private Dokimion? SourceDokimion;
        private Dokimion? DestDokimion;
        private Dictionary<string, Project> ProjectMap;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "";
            ProjectsListBox.Items.Clear();
            ProjectMap = new Dictionary<string, Project>();

            LoginDialog dlg = new LoginDialog();

            Settings? settings = GetSettings();
            if (null == settings)
            {
                settings = new Settings();
            }
            else
            {
                dlg.SourceUsername = settings.sourceusername;
                dlg.SourceAddress = settings.sourceaddress;
                dlg.SourceUseHttps = settings.sourceusehttps;

                dlg.DestUsername = settings.destusername;
                dlg.DestAddress = settings.destaddress;
                dlg.DestUseHttps = settings.destusehttps;
            }

            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;

            settings.sourceusername = dlg.SourceUsername;
            settings.sourceaddress = dlg.SourceAddress;
            settings.sourceusehttps = dlg.SourceUseHttps;

            settings.destusername = dlg.DestUsername;
            settings.destaddress = dlg.DestAddress;
            settings.destusehttps = dlg.DestUseHttps;

            try
            {
                File.WriteAllText("Cloner.json", JsonConvert.SerializeObject(settings));
            }
            catch
            {
                ; // Ignore errors
            }

            SourceDokimion = dlg.Source;
            DestDokimion = dlg.Destination;

            ProjectsListBox.Items.Clear();
            ProjectMap.Clear();

            if (SourceDokimion != null && DestDokimion != null)
            {
                List<Project>? projects = SourceDokimion.GetProjects();
                if (projects == null)
                {
                    StatusTextBox.Text = SourceDokimion.Error;
                }
                else
                {
                    foreach (Project project in projects)
                    {
                        ProjectMap.Add(project.name, project);
                        ProjectsListBox.Items.Add(project.name);
                    }
                }

            }
            ApplyFiltersButton.Enabled = false;
        }

        private Settings? GetSettings()
        {
            string settingsJson = "";
            try
            {
                settingsJson = File.ReadAllText("Cloner.json");
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
                    StatusTextBox.Text = "Cannot decode Cloner.json which contains: \n" + settingsJson;
                }
            }
            return settings;
        }

        private void ProjectsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAttributeTree();
            ApplyFiltersButton.Enabled = true;
        }

        private void ShowAttributeTree()
        {
            FilterTreeView.Nodes.Clear();
            Project project = ProjectMap[(string)ProjectsListBox.SelectedItem];
            foreach (var attr in project.attributes)
            {
                var attrNode = FilterTreeView.Nodes.Add(attr.name);
                attrNode.Tag = attr;
                foreach (var value in attr.attrValues)
                {
                    attrNode.Nodes.Add(value.value);
                }
            }

            FilterTreeView.ExpandAll();

            TestcaseDataGridView.Rows.Clear();
        }

        private void ApplyFiltersButton_Click(object sender, EventArgs e)
        {
            Project project = ProjectMap[(string)ProjectsListBox.SelectedItem];
            List<string> attrList = new();
            foreach (TreeNode attrNode in FilterTreeView.Nodes)
            {
                Attribute attr = attrNode.Tag as Attribute;
                string attrId = attr.id;
                foreach (TreeNode valueNode in attrNode.Nodes)
                {
                    if (valueNode.Checked)
                    {
                        attrList.Add(attrId + "=" + valueNode.Text);
                    }
                }
            }
            List<BriefTestCase> testcases = SourceDokimion.GetTestCasesForAttributes(project, attrList.ToArray());

            TestcaseDataGridView.RowHeadersVisible = false;
            TestcaseDataGridView.Columns.Clear();
            TestcaseDataGridView.ColumnCount = 2;
            TestcaseDataGridView.Columns.Insert(0, new DataGridViewCheckBoxColumn());
            TestcaseDataGridView.Columns[0].HeaderCell.Value = "To Clone";
            TestcaseDataGridView.Columns[1].HeaderCell.Value = "ID";
            TestcaseDataGridView.Columns[2].HeaderCell.Value = "Name";
            TestcaseDataGridView.Columns[0].Width = 100;
            TestcaseDataGridView.Columns[1].Width = 50;
            TestcaseDataGridView.Columns[2].Width = 600;
            TestcaseDataGridView.Rows.Clear();

            if (testcases == null)
            {
                StatusTextBox.Text = SourceDokimion.Error;
                DoTheCloneButton.Enabled = false;
                return;
            }

            foreach (BriefTestCase testcase in testcases)
            {
                TestcaseDataGridView.Rows.Add(true, testcase.id, testcase.name);
            }

            DoTheCloneButton.Enabled = true;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FilterTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SetCheckedStateOfChildren(e.Node);
        }

        private void SetCheckedStateOfChildren(TreeNode node)
        {
            bool newState = node.Checked;
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = newState;
                SetCheckedStateOfChildren(child);
            }
        }

        private bool AreAnyChecked(TreeNodeCollection nodes)
        {
            bool found = false;
            foreach (TreeNode node in nodes)
            {
                if (AreAnyChecked(node))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }


        private bool AreAnyChecked(TreeNode node)
        {
            if (node.Checked)
            {
                return true;
            }
            foreach (TreeNode child in node.Nodes)
            {
                if (AreAnyChecked(child))
                {
                    return true;
                }
            }
            return false;
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in TestcaseDataGridView.Rows)
            {
                row.Cells[0].Value = true;
            }
            DoTheCloneButton.Enabled = true;
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in TestcaseDataGridView.Rows)
            {
                row.Cells[0].Value = false;
            }
            DoTheCloneButton.Enabled = false;
        }

        private void DoTheCloneButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Text = "";
            Project sourceProject = ProjectMap[(string)ProjectsListBox.SelectedItem];
            Project destProject = new(sourceProject);

            // Reject the clone if the desired project already exists in the destination server; we won't modify existing projects
            string projectName = NewProjectNameTextBox.Text;
            if (IsProjectInDest(projectName))
            {
                StatusTextBox.Text = $"Cannot clone; project {projectName} already exists in destination";
                return;
            }

            // Create the new project in the destination
            if (false == CreateProject(destProject))
            {
                return;
            }

            // Copy the project's attributes from the source to destination
            if (false == CopyAttributes(sourceProject, destProject))
            {
                return;
            }

            // Get the destination attributes; they will have different ID's than the source
            destProject.attributes = DestDokimion.GetAttributesForProject(destProject.id);

            // Generate a mapping of the source attribute IDs to the destination attribute IDs
            Dictionary<string, string> attrMap = GenerateAttributeMap(sourceProject.attributes, destProject.attributes);
            if (attrMap == null)
            {
                return;
            }

            // Get the test suites from the source
            TestSuite[]? testSuites = SourceDokimion.GetTestSuites(sourceProject);
            if (testSuites == null)
            {
                StatusTextBox.Text = SourceDokimion.Error;
                return;
            }

            // Copy the test suites to the destination server
            foreach (TestSuite testSuite in testSuites)
            {
                foreach (var attr in testSuite.filter.filters)
                {
                    attr.id = attrMap[attr.id];
                }

                if (false == DestDokimion.CreateTestSuite(destProject, testSuite))
                {
                    StatusTextBox.Text = SourceDokimion.Error;
                    return;
                }
            }

            // Determine the maximum test ID that we need to use
            int maxId = 0;
            foreach (DataGridViewRow row in TestcaseDataGridView.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    if (false == int.TryParse((string)row.Cells[1].Value, out int id))
                    {
                        StatusTextBox.Text = $"Cannot parse {(string)row.Cells[1].Value} as an integer.";
                        return;
                    }
                    maxId = Math.Max(maxId, id);
                }
            }

            // Create dummy test cases for each ID up to the maximum, so we can overwrite them later
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = maxId;
            ProgressBar.Step = 1;
            ProgressBar.Value = 0;
            StatusTextBox.Text = $"Creating {maxId} empty test cases";

            TestCaseForUpload emptyTc = new();
            emptyTc.name = "dummy";
            emptyTc.deleted = true;

            for (int i=1; i <= maxId; i++)
            {
                if (false == DestDokimion.CreateTestCase(destProject, emptyTc))
                {
                    StatusTextBox.Text = DestDokimion.Error;
                    return;
                }
                ProgressBar.PerformStep();
                Thread.Sleep(TimeSpan.FromMilliseconds(10));
            }

            // Determine the number of test cases that we will actually update
            int testCaseCount = 0;
            foreach (DataGridViewRow row in TestcaseDataGridView.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    testCaseCount++;
                }
            }

            // Update the test cases we will be using
            ProgressBar.Maximum = testCaseCount;
            ProgressBar.Value = 0;
            StatusTextBox.Text = $"Filling in {testCaseCount} test cases";

            foreach (DataGridViewRow row in TestcaseDataGridView.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    if (false == CopyTestCase(sourceProject, destProject, (string)row.Cells[1].Value, attrMap))
                    {
                        return;
                    }
                }
                ProgressBar.PerformStep();
                Thread.Sleep(TimeSpan.FromMilliseconds(10));
            }

            StatusTextBox.Text = "Cloning has completed";
        }

        private bool IsProjectInDest(string projectName)
        {
            List<Project> destProjects = DestDokimion.GetProjects();
            var matchingProjects = destProjects.Where((p) => p.name == projectName);
            return matchingProjects.Any();
        }

        private bool CreateProject(Project destProject)
        {
            destProject.name = NewProjectNameTextBox.Text;
            string id = NewProjectNameTextBox.Text.ToLower();
            id = id.Replace(" ", "");
            if (id.Length > 10)
            {
                id = id.Substring(0, 10);
            }
            destProject.id = id;
            if (false == DestDokimion.CreateProject(destProject))
            {
                StatusTextBox.Text = "Failed to create Project.\r\n" + DestDokimion.Error;
                return false;
            }
            return true;
        }

        private bool CopyAttributes(Project sourceProject, Project destProject)
        {
            foreach (var srcAttr in sourceProject.attributes)
            {
                Attribute destAttr = new Attribute(srcAttr);
                destAttr.id = null;
                if (false == DestDokimion.CreateAttribute(destProject, destAttr))
                {
                    StatusTextBox.Text = "Failed to copy Attributes.\r\n" + DestDokimion.Error;
                    return false;
                }
            }
            return true;
        }

        private Dictionary<string, string>? GenerateAttributeMap(List<Attribute> srcAttrList, List<Attribute> destAttrList)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach (Attribute srcAttr in srcAttrList)
            {
                List<Attribute> destAttr = destAttrList.Where((a) => a.name == srcAttr.name).ToList();
                if (destAttr.Count != 1)
                {
                    StatusTextBox.Text = $"Can't locate Attribute {srcAttr.name} in destination";
                    return null;
                }
                map.Add(srcAttr.id, destAttr.First().id);
            }
            return map;
        }

        private bool CopyTestCase(Project sourceProject, Project destProject, string id, Dictionary<string, string> attrMap)
        {
            TestCaseForUpload tc = SourceDokimion.GetTestCaseForUpload(sourceProject, id);
            if (tc == null)
            {
                StatusTextBox.Text = SourceDokimion.Error;
                return false;
            }

            Dictionary<string, string[]> newAttr = new();
            foreach (var attr in tc.attributes)
            {
                newAttr.Add(attrMap[attr.Key], attr.Value);
            }
            tc.attributes = newAttr;

            TestCaseForUpload destTestcase = DestDokimion.GetTestCaseForUpload(destProject, id);
            tc.lastModifiedTime = destTestcase.lastModifiedTime;

            if (false == DestDokimion.ModifyTestCase(destProject, tc))
            {
                StatusTextBox.Text = DestDokimion.Error;
                return false;
            }

            return true;
        }

        private void TestcaseDataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            ShouldCloningBeEnabled();
        }

        private void NewProjectNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ShouldCloningBeEnabled();
        }

        private void ShouldCloningBeEnabled()
        {
            DoTheCloneButton.Enabled = (NumberTestCasesChecked() > 0) && (false == string.IsNullOrEmpty(NewProjectNameTextBox.Text));
        }

        private int NumberTestCasesChecked()
        {
            int numChecked = 0;
            foreach (DataGridViewRow row in TestcaseDataGridView.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    numChecked++;
                }
            }

            return numChecked;
        }
    }

}