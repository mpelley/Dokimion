using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater4
{
    public partial class ShowDiffs : Form
    {
        public List<DocumentPair> DocumentPairs = new ();
        private int index = 0;


        public ShowDiffs()
        {
            InitializeComponent();

            //diffViewer1.Margin = new Padding(0);
            //diffViewer1.Dock = DockStyle.Fill;
            diffViewer1.OldTextHeader = "Server";
            diffViewer1.NewTextHeader = "File System";
        }

        new public DialogResult ShowDialog()
        {
            index = 0;
            ShowTestCase();
            return base.ShowDialog();
        }

        private void ShowTestCase()
        {
            if (DocumentPairs.Count == 0)
            {
                TestCaseTextBox.Text = "No test cases selected";
            }
            else
            {
                TestCaseTextBox.Text = DocumentPairs[index].TestCase;
            }
            diffViewer1.OldText = DocumentPairs[index].ServerDocument;
            diffViewer1.NewText = DocumentPairs[index].FileSystemDocument;
            diffViewer1.Refresh();
        }

        private void PreviousButton_Click(object? sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                index = DocumentPairs.Count - 1;
            }
            ShowTestCase();
        }

        private void NextButton_Click(object? sender, EventArgs e)
        {
            if (index < DocumentPairs.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            ShowTestCase();
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            // Let dialog close
        }
    }

    public class DocumentPair
    {
        public string TestCase = "";
        public string ServerDocument = "";
        public string FileSystemDocument = "";
    }

}
