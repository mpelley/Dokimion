using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Markdig;
using Markdig.Syntax;
using Markdig.Parsers;
using Markdig.Syntax.Inlines;
using Markdig.Helpers;

namespace Dokimion
{
    public class PlainTextFile
    {
        public string Error = "";

        public TestCaseForUpload? GetTestCaseFromFileSystem(string path, Project project)
        {
            TestCaseForUpload? uploaded;

            uploaded = null;
            // Read text from Markdown file.
            string[] plainTextDocument;
            try
            {
                plainTextDocument = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }

            // Extract info from markdownDocument to an instance of TestCaseForUpload.
            uploaded = PlainTextToObject(plainTextDocument, path, project);
            if (uploaded == null)
            {
                Error += $"\r\nCannot convert Plain Text file {path} to a test case object";
                return null;
            }

            return uploaded;
        }


        public TestCaseForUpload? PlainTextToObject(string[] plainTextDoc, string filename, Project project)
        {
            TestCaseForUpload tc = new TestCaseForUpload();
            Error = "";

            int index = 0;
            Console.WriteLine(filename);

            return tc;
        }



        private string AttributeKeyForName(Project project, string name)
        {
            foreach (var att in project.attributes)
            {
                if (att.Value == name)
                    return att.Key;
            }
            return "";
        }


    }
}
