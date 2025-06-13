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
        private enum Parts
        {
            None,
            Description,
            Attributes,
            Steps
        }

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
            string titleText = "Title:";
            Parts part = Parts.Description;
            List<string> content = new ();
            int index = 0;

            Console.WriteLine(filename);
            foreach (string line in plainTextDoc)
            {
                index++;
                if (index == 1)
                {
                    if (line.StartsWith(titleText))
                    {
                        tc.name = line.Substring(titleText.Length).Trim();
                        if (tc.name == "")
                        {
                            Error += "Title should not be empty";
                        }
                    }
                    else
                    {
                        Error += "First line of file should start with \"Title:\", not " + line + "\n";
                    }
                    continue;
                }

                switch (line)
                {
                    case "Description:":
                        SaveTextInTestCase(project, ref tc, content, part);
                        content = new();
                        part = Parts.Description;
                        break;
                    case "Attributes:":
                        SaveTextInTestCase(project, ref tc, content, part);
                        content = new();
                        part = Parts.Attributes;
                        break;
                    case "Steps:":
                        SaveTextInTestCase(project, ref tc, content, part);
                        content = new();
                        part = Parts.Steps;
                        break;
                    default:
                        content.Add(line);
                        break;
                }
            }

            // Save the last content in the file
            if (content.Count != 0)
            {
                SaveTextInTestCase(project, ref tc, content, part);
            }

            // Sanity checks
            if (string.IsNullOrEmpty(tc.description))
            {
                Error += "Description must be provided in the file.\n";
            }
            if (tc.attributes.Count == 0)
            {
                Error += "Attributes must be provided in the file.\n";
            }
            if (tc.steps.Count == 0)
            {
                Error += "Steps must be provided in the file.\n";
            }

            return tc;
        }

        private void SaveTextInTestCase(Project project, ref TestCaseForUpload tc, List<string> content, Parts part)
        {
            string html = MakeHtml(content);
            switch (part)
            {
                case (Parts.Description):
                    tc.description = html;
                    break;
                case (Parts.Attributes):
                    Dictionary<string, string[]> attributes = new Dictionary<string, string[]>();
                    foreach(string item in content)
                    {
                        if (string.IsNullOrEmpty(item))
                            continue;
                        string[] parts = item.Split(':');
                        if (parts.Length != 2)
                        {
                            Error += $"Invalid attribute format {item}\n";
                            return;
                        }
                        string key = AttributeKeyForName(project, parts[0].Trim());
                        if (key == "")
                        {
                            Error += $"Invalid attribute name: {parts[0]}.\r\n";
                            return;
                        }
                        string[] values = parts[1].Split(",");
                        for (int i=0; i<values.Length; i++)
                        {
                            values[i] = values[i].Trim();
                        }
                        Array.Sort(values);     // so we can compare to what is in Dokimion.
                        attributes.Add(key, values);
                    }
                    tc.attributes = attributes;
                    break;
                case (Parts.Steps):
                    Step step = new Step();
                    step.action = html;
                    List<Step> steps = new ();
                    steps.Add(step);
                    tc.steps = steps;
                    break;
                default:
                    break;
            }
        }

        private string MakeHtml(List<string> content)
        {
            bool first = true;
            string result = "";
            foreach (string line in content)
            {
                if (first)
                {
                    first = false;
                }
                else if ((line == "") && (line == content.Last()))
                {
                }
                else
                {
                    result += "<br>\r\n";
                }

                string html = line;
                html = html.Replace("\"", "&quot;");
                html = html.Replace("'", "&apos;");
                html = html.Replace("&", "&amp;");
                html = html.Replace("<", "&lt;");
                html = html.Replace(">", "&gt;");

                result += html;
            }

            return result;
        }


        private string AttributeKeyForName(Project project, string name)
        {
            name = name.Replace(" ", Dokimion.SPACE_REPLACER);
            foreach (var att in project.attributes)
            {
                if (att.Value == name)
                    return att.Key;
            }
            return "";
        }

        public string GeneratePlainText(TestCaseForUpload tc, Project project)
        {
            string pt = "";
            pt += $"Title: {tc.name}\r\n";
            pt += "Description:\r\n";
            pt += tc.description + "\r\n";
            pt += "Attributes:\r\n";
            foreach(var att in tc.attributes)
            {
                string name = project.attributes[att.Key];
                pt += $"{name}: {string.Join(",", att.Value)}<br>\r\n";
            }
            pt += "Steps:\r\n";
            if (tc.steps.Count > 0)
            {
                pt += tc.steps[0].action;
            }

            return pt;
        }

    }
}
