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

        public TestCaseForUpload? GetTestCaseFromFileSystem(string path, Project project, int testCaseId)
        {
            TestCaseForUpload? uploaded = null;

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
            uploaded = PlainTextToObject(plainTextDocument, path, project, testCaseId);
            if (uploaded == null)
            {
                Error += $"\r\nCannot convert Plain Text file {path} to a test case object";
                return null;
            }

            return uploaded;
        }


        public TestCaseForUpload? PlainTextToObject(string[] plainTextDoc, string filename, Project project, int testCaseId)
        {
            TestCaseForUpload tc = new TestCaseForUpload();
            tc.id = testCaseId.ToString();
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
                        tc.name = MakeHtml(line.Substring(titleText.Length).Trim());
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
                            Error += $"For test case {tc.id}:\r\n";
                            Error += $"Invalid attribute format {item}\n";
                            return;
                        }

                        AttributeForUpload? attribute = Dokimion.GetAttributeForName(project, parts[0].Trim());
                        if (attribute == null)
                        {
                            Error += $"For test case {tc.id}:\r\n";
                            Error += $"Invalid attribute name: {parts[0]}.\r\n";
                            return;
                        }

                        string[] values = parts[1].Split(",");
                        for (int i=0; i<values.Length; i++)
                        {
                            values[i] = values[i].Trim();
                        }
                        Array.Sort(values);     // so we can compare what is in the file to what is in Dokimion.
                        foreach (string value in values)
                        {
                            bool found = false;
                            if (attribute.attrValues != null)
                            {
                                foreach (var attValue in attribute.attrValues)
                                {
                                    if (attValue.value == value)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }
                            if (false == found)
                            {
                                Error += $"For test case {tc.id}:\r\n";
                                Error += $"Attribute value {value} does not exist in project {project.name} for attribute {parts[0]}.\r\n";
                                return;
                            }
                        }

                        attributes.Add(attribute.id, values);
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

                string html = MakeHtml(line);

                result += html;
            }

            return result;
        }

        private string MakeHtml(string line)
        {
            string html = line;
            html = html.Replace("\"", "&quot;");
            html = html.Replace("'", "&apos;");
            html = html.Replace("&", "&amp;");
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
            return html;
        }

        public static string RemoveHtml(string html)
        {
            string clean = html.Replace("<br>\r\n", "\r\n");
            clean = clean.Replace("&quot;", "\"");
            clean = clean.Replace("&apos;", "'");
            clean = clean.Replace("&amp;", "&");
            clean = clean.Replace("&lt;", "<");
            clean = clean.Replace("&gt;", ">");
            return clean;
        }


        public string GeneratePlainText(TestCaseForUpload tc, Project project)
        {
            string pt = "";
            pt += $"Title: {tc.name}\r\n";
            pt += "Description:\r\n";
            pt += tc.description + "\r\n";

            pt += "Attributes:\r\n";
            List<string> names = new List<string>();
            foreach(var testCaseAttribute in tc.attributes)
            {
                var projectAttribute = Dokimion.GetAttributeForId(project, testCaseAttribute.Key);
                names.Add(projectAttribute.name);
            }
            names.Sort();

            foreach( var name in names)
            {
                var projectAttribute = Dokimion.GetAttributeForName(project, name);
                var testCaseAttribute = tc.attributes[projectAttribute.id];
                if (testCaseAttribute != null)
                {
                    pt += $"{name}: {string.Join(",", testCaseAttribute)}<br>\r\n";
                }
            }
            //foreach(var att in tc.attributes)
            //{
            //    var attribute = Dokimion.GetAttributeForId(project, att.Key);
            //    if (attribute != null)
            //    {
            //        pt += $"{attribute.name}: {string.Join(",", att.Value)}<br>\r\n";
            //    }
            //}

            pt += "Steps:\r\n";
            if (tc.steps.Count > 0)
            {
                pt += tc.steps[0].action;
            }

            return pt;
        }

    }
}
