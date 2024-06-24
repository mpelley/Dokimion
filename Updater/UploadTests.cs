using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{
    public partial class UploadTests : Form
    {
        public UploadTests()
        {
            InitializeComponent();
        }

        private void CheckAllButton_Click(object sender, EventArgs e)
        {
            SetAllTestCases(true);
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            SetAllTestCases(false);
        }

        private void SetAllTestCases(bool state)
        {
            for (int i = 0; i < NewTestCaseListBox.Items.Count; i++)
            {
                NewTestCaseListBox.SetItemChecked(i, state);
            }
        }

        public void SetFileList(List<FileInfo> files)
        {
            NewTestCaseListBox.Items.Clear();
            foreach (FileInfo file in files)
            {
                NewTestCaseListBox.Items.Add(file.Name);
            }
            SetAllTestCases(true);
        }
    }
}
