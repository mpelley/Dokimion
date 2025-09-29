namespace Updater5
{
    public partial class Updater : Form
    {
        public Updater()
        {
            InitializeComponent();

            Data = new();
            StepLogin = new(panelLogin, Data, this);
            StepSelectProject = new(panelSelectProject, Data, this);
            StepSelectRepo = new(panelSelectRepo, Data, this);
            StepDownloadNewTestCases = new(panelDownloadNewTestCases, Data, this);
            StepHandleDifferences = new(panelHandleDifferences, Data, this);
            StepDownloadChangedMetadata = new(panelDownloadChangedMetadata, Data, this);
            StepDone = new(panelDone, Data, this);

            StepLogin.PrevStepCode = null;
            StepLogin.NextStepCode = StepSelectProject;
            StepSelectProject.PrevStepCode = StepLogin;
            StepSelectProject.NextStepCode = StepSelectRepo;
            StepSelectRepo.PrevStepCode = StepSelectProject;
            StepSelectRepo.NextStepCode = StepDownloadNewTestCases;
            StepDownloadNewTestCases.PrevStepCode = StepSelectRepo;
            StepDownloadNewTestCases.NextStepCode = StepHandleDifferences;
            StepHandleDifferences.PrevStepCode = StepDownloadNewTestCases;
            StepHandleDifferences.NextStepCode = StepDownloadChangedMetadata;
            StepDownloadChangedMetadata.PrevStepCode = StepHandleDifferences;
            StepDownloadChangedMetadata.NextStepCode = StepDone;
            StepDone.PrevStepCode = StepDownloadChangedMetadata;
            StepDone.NextStepCode = null;

            ProjectsListBox.DisplayMember = "Name";

            ActiveStepCode = StepLogin;
            ActiveStepCode.Panel.Visible = true;
            StepTextBox.Text = ActiveStepCode.StepName;
            FeedbackTextBox.Text = "";
            ActiveStepCode.Activate();

        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            StepCode? newPanelCode = ActiveStepCode.Prev();
            SwapSteps(newPanelCode);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            StepCode? newPanelCode = ActiveStepCode.Next();
            SwapSteps(newPanelCode);
        }

        private bool SwapSteps(StepCode? newStepCode)
        {
            bool swapped = false;
            if (newStepCode != null)
            {
                ActiveStepCode.Panel.Visible = false;
                ActiveStepCode = newStepCode;
                ActiveStepCode.Panel.Visible = true;
                ActiveStepCode.Panel.Refresh();
                ActiveStepCode.Activate();
                StepTextBox.Text = ActiveStepCode.StepName;
                swapped = true;
            }
            return swapped;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            StepLogin.LoginButton_Clicked();
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            StepLogin.ShowPasswordCheckBox_CheckedChanged();
        }

        private void ProjectsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StepSelectProject.ProjectsListBox_SelectedIndexChanged();
        }

        private void BrowseFileSystemButton_Click(object sender, EventArgs e)
        {
            StepSelectRepo.BrowseFileSystemButton_Click();
        }

        private void FolderTextBox_TextChanged(object sender, EventArgs e)
        {
            StepSelectRepo.FolderTextBox_TextChanged();
        }

        private void DownloadTestCasesButton_Click(object sender, EventArgs e)
        {
            StepDownloadNewTestCases.DownloadTestCasesButton_Click();
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            StepDownloadNewTestCases.SelectAllButton_Click();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            StepDownloadNewTestCases.ClearAllButton_Click();
        }

        private void RescanButton_Click(object sender, EventArgs e)
        {
            StepDownloadNewTestCases.RescanButton_Click();
        }

        private void SelectAllChangedMetadataButton_Click(object sender, EventArgs e)
        {
            StepDownloadChangedMetadata.SelectAllChangedMetadataButton_Click();
        }

        private void ClearAllChangedMetadataButton_Click(object sender, EventArgs e)
        {
            StepDownloadChangedMetadata.ClearAllChangedMetadataButton_Click();
        }

        private void DownloadChangedMetadataButton_Click(object sender, EventArgs e)
        {
            StepDownloadChangedMetadata.DownloadMetadataButton_Click();
        }

        private void ChangedMetadataDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            StepDownloadChangedMetadata.ChangedMetadataDataGridView_RowHeaderMouseClick(sender, e);
        }

        private void HandleDiffSelectAllButton_Click(object sender, EventArgs e)
        {
            StepHandleDifferences.HandleDiffSelectAllButton_Click();
        }

        private void HandleDiffClearAllButton_Click(object sender, EventArgs e)
        {
            StepHandleDifferences.HandleDiffClearAllButton_Click();
        }

        private void UploadDiffToDokimionButton_Click(object sender, EventArgs e)
        {
            StepHandleDifferences.UploadDiffToDokimionButton_Click();
        }

        private void HandleDiffDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StepHandleDifferences.HandleDiffDataGridView_CellClick(sender, e);
        }

        private void HandleDiffRescanTestCasesButton_Click(object sender, EventArgs e)
        {
            StepHandleDifferences.HandleDiffRescanTestCasesButton_Click(sender, e);
        }
    }
}
