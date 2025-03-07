using MimeMapping;

using System.Text;
using Newtonsoft.Json;
using Serilog;
using Markdig;
using Markdig.Syntax;
using Markdig.Parsers;
using Markdig.Syntax.Inlines;
using Markdig.Helpers;

// See https://github.com/greatbit/quack/wiki for the API specifications

namespace Dokimion
{
    /// <summary>
    /// This class is the part of the response to the login request we are interested in
    /// </summary>
    internal class User
    {
        public string id = "";
    }

    public enum UploadStatus
    {
        Updated,
        NoChange,
        NotChanged,
        Error,
        Aborted
    }

    public class AttributeValue
    {
        public string? value = null;
    }

    public class AttributeForUpload
    {
        public string? id = null;
        public string? name = null;
        public string? type = null;
        public AttributeValue[]? attrValues;
    }

    public class ProjectForUpload
    {
        public string description = "";
        public string[] readWriteUsers = { "" };
        public string id = "";
        public string name = "";
    }

    public class Project
    {
        public string description = "";
        public string[] readWriteUsers = { "" };
        public string id = "";
        public string name = "";
        public bool deleted;
        public Dictionary<string, string> attributes = new Dictionary<string, string>();

        public string Name
        {
            get { return name; }
        }
    }

    public class Attribute
    {
        public string id = "";
        public string name = "";
    }

    public class Attachment
    {
        public string id = "";
        public string title = "";
        public string createdBy = "";
        public Int64 createdTime;
        public Int64 dataSize;
    }

    public class Step
    {
        public string action = "";
        public string expectation = "";
    }


    public class TestCaseShort
    {
        public string id = "";
        public string name = "";
        public bool automated;
        public bool broken;
        public bool deleted;
        public bool launchBroken;
        public bool locked;
        // These times are returned in the response, but have a value of zero.
        //public Int64 createdTime;
        //public Int64 lastModifiedTime;
        public Dictionary<string, string[]> attributes = new Dictionary<string, string[]>();
        public List<Attachment> attachments = new List<Attachment>();

        public string Display
        {
            get { return $"ID: {id}, {name}"; }
        }

    }

    public class TestCaseTreeResponse
    {
        public List<TestCaseShort> testCases = new List<TestCaseShort>();
    }

    public class TestCaseForUpload : TestCaseShort
    {
        public string description = "";
        public string preconditions = "";
        public List<Step> steps = new List<Step>();
        public Int64 lastModifiedTime;
    }

    public class TestCase : TestCaseForUpload
    {
        public string createdBy = "";
        public Int64 createdTime;
        public string lastModifiedBy = "";
    }


    /// <summary>
    /// This class is responsible for interacting with the Dokimion server.
    /// </summary>
    public class Dokimion
    {
        public HttpClient m_Client;
        public string ServerUrl = "";
        public bool UseHttps;
        public string Error = "";
        private const string SPACE_REPLACER = "_";

        // Default constructor is private so callers use the constructor providing the URL.
        private Dokimion()
        {
            m_Client = new HttpClient();
        }

        public Dokimion(string url, bool useHttps)
        {
            m_Client = new HttpClient();
            ServerUrl = url;
            UseHttps = useHttps;
        }

        private string BaseDokimionApiUrl()
        {
            if (UseHttps)
            {
                return $"https://{ServerUrl}/api";
            }
            return $"http://{ServerUrl}/api";
        }

        public void Logout()
        {
            string url = $"{BaseDokimionApiUrl()}/user/logout";
            HttpResponseMessage? resp = null;
            try
            {
                resp = m_Client.DeleteAsync(url).Result;
            }
            catch (Exception e)
            {
                Error = "Cannot log out because:\n" + e.Message;
                if (resp != null)
                {
                    Error += "\n" + resp.ReasonPhrase;
                }
            }
            m_Client.DefaultRequestHeaders.Remove("whoruSessionId");
        }

        public bool Login(string username, string password)
        {
            string url = $"{BaseDokimionApiUrl()}/user/login?login={username}&password={password}";
            HttpResponseMessage? resp = null;
            try
            {
                resp = m_Client.PostAsync(url, null).Result;
            }
            catch (Exception e)
            {
                Error = "Cannot log in because:\n" + e.Message;
                if (resp != null)
                {
                    Error += "\n" + resp.ReasonPhrase;
                }
                return false;
            }
            bool results = false;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (resp.IsSuccessStatusCode)
            {
                if (string.IsNullOrEmpty(json))
                {
                    Error = "Empty response to Login request";
                    return false;
                }
                User? user = JsonConvert.DeserializeObject<User>(json);
                if (user != null)
                {
                    m_Client.DefaultRequestHeaders.Add("whoruSessionId", user.id);
                    results = true;
                }
                else
                {
                    Error = "Cannot decode " + json;
                }
            }
            else
            {
                Error = "Server returned error: " + resp.ReasonPhrase + "\n" + json;
            }
            return results;
        }

        public Dictionary<string, string> GetAttributesForProject(string projectId)
        {
            string url = BaseDokimionApiUrl() + "/" + projectId + "/attribute";

            List<Attribute>? attrList = null;
            Error = "";

            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (resp.IsSuccessStatusCode)
            {
                attrList = JsonConvert.DeserializeObject<List<Attribute>>(json);
                if (attrList == null)
                {
                    Error = "Cannot decode: \r\n" + json;
                    return new Dictionary<string, string>();
                }
            }
            else
            {
                Error = "Server returned error: " + resp.ReasonPhrase + "\r\n" + json;
                return new Dictionary<string, string>();
            }

            Dictionary<string, string> attrDict = new Dictionary<string, string>();
            foreach (var attr in attrList)
            {
                string name = attr.name.Replace(" ", SPACE_REPLACER);
                attrDict.Add(attr.id, name);
            }

            return attrDict;
        }

        public List<Project>? GetProjects()
        {
            string url = BaseDokimionApiUrl() + "/project";
            List<Project>? projectList = null;
            Error = "";

            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (resp.IsSuccessStatusCode)
            {
                projectList = JsonConvert.DeserializeObject<List<Project>>(json);
                if (projectList == null)
                {
                    Error = "Cannot decode: \r\n" + json;
                }
            }
            else
            {
                Error = "Server returned error: " + resp.ReasonPhrase + "\r\n" + json;
            }

            if (projectList != null)
            {
                foreach (Project project in projectList)
                {
                    project.attributes = GetAttributesForProject(project.id);
                }
            }

            return projectList;
        }


        public List<TestCaseShort>? GetTestCaseSummariesForProject(string projectId)
        {
            string url = BaseDokimionApiUrl() + "/" + projectId + "/testcase/tree";
            TestCaseTreeResponse? testcaseTreeResponse = null;
            Error = "";

            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (resp.IsSuccessStatusCode)
            {
                testcaseTreeResponse = JsonConvert.DeserializeObject<TestCaseTreeResponse>(json);
                if (testcaseTreeResponse == null)
                {
                    Error = "Cannot decode: \r\n" + json;
                    return null;
                }
            }
            else
            {
                Error = "Server returned error: " + resp.ReasonPhrase + "\r\n" + json;
                return null;
            }
            return testcaseTreeResponse.testCases;
        }

        public string? GetProject(string projectId)
        {
            string url = BaseDokimionApiUrl() + "/project/" + projectId;
            Error = "";

            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (false == resp.IsSuccessStatusCode)
            {
                Error = "Server returned error: " + resp.ReasonPhrase + "\r\n" + json;
                return null;
            }
            return json;
        }

        public string? GetProjectAttributes(string projectId)
        {
            string url = BaseDokimionApiUrl() + "/" + projectId + "/attribute";
            Error = "";

            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (false == resp.IsSuccessStatusCode)
            {
                Error = "Server returned error: " + resp.ReasonPhrase + "\r\n" + json;
                return null;
            }
            return json;
        }

        public ProjectForUpload? GetProjectFromJson(string path)
        {
            string projectJson;
            ProjectForUpload? project;
            try
            {
                projectJson = File.ReadAllText(path);
                project = JsonConvert.DeserializeObject<ProjectForUpload>(projectJson);
            }
            catch (Exception ex)
            {
                Error = $"Cannot read project file {path} because:\r\n{ex.Message}";
                return null;
            }
            if (project == null)
            {
                Error = $"Cannot deserialize JSON file {path}";
                return null;
            }

            return project;
        }

        public bool CreateProjectFromFiles(string projectFilePath, string attributesFilePath, bool doUpdate)
        {

            ProjectForUpload? project = GetProjectFromJson(projectFilePath);
            if (project == null)
            {
                return false;
            }

            string attributesJson;
            AttributeForUpload[]? attributes;
            try
            {
                attributesJson = File.ReadAllText(attributesFilePath);
                attributes = JsonConvert.DeserializeObject<AttributeForUpload[]>(attributesJson);
            }
            catch (Exception ex)
            {
                Error = $"Cannot read attributes file {attributesFilePath} because:\r\n{ex.Message}";
                return false;
            }
            if (attributes == null)
            {
                Error = $"Cannot deserialize JSON file {attributesFilePath}";
                return false;
            }

            if (false == doUpdate)
            {
                string json = JsonConvert.SerializeObject(project);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = BaseDokimionApiUrl() + "/project";
                HttpResponseMessage resp = m_Client.PostAsync(url, content).Result;
                if (false == resp.IsSuccessStatusCode)
                {
                    string respContent = resp.Content.ReadAsStringAsync().Result;
                    Error = $"Error {resp.ReasonPhrase} when trying to create project.\r\n{respContent}";
                    return false;
                }
            }

            foreach(var attr in attributes)
            {
                attr.id = null;
                attr.type = "TESTCASE";
                string json = JsonConvert.SerializeObject(attr);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = BaseDokimionApiUrl() + "/" + project.id + "/attribute";
                HttpResponseMessage resp = m_Client.PostAsync(url, content).Result;
                if (false == resp.IsSuccessStatusCode)
                {
                    string respContent = resp.Content.ReadAsStringAsync().Result;
                    Error = $"Error {resp.ReasonPhrase} when trying to create project.\r\n{respContent}";
                    return false;
                }
            }

            return true;
        }

        public TestCaseForUpload? GetTestCaseFromFileSystem(string path, Project project)
        {
            TestCaseForUpload? uploaded;

            uploaded = null;
            // Read text from Markdown file.
            string markdownText = "";
            try
            {
                markdownText = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }

            // Parse the file into MarkdownDocument
            MarkdownDocument? markdownDocument = null;
            try
            {
                markdownDocument = Markdig.Markdown.Parse(markdownText);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }

            // Extract info from markdownDocument to an instance of TestCaseForUpload.
            uploaded = MarkdownToObject(markdownDocument, path, project);
            if (uploaded == null)
            {
                Error += $"\r\nCannot convert Markdown file {path} to a test case object";
                return null;
            }

            return uploaded;
        }


        private TestCaseForUpload? MarkdownToObject(MarkdownDocument markdownDoc, string filename, Project project)
        {
            TestCaseForUpload tc = new TestCaseForUpload();
            Error = "";

            int index = 0;
            while (index < markdownDoc.Count)
            {
                Block block = markdownDoc[index];
                index++;
                switch (block)
                {
                    case HeadingBlock:
                        var heading = (HeadingBlock)block;
                        if (heading.Inline == null)
                        {
                            Error += $"Heading is missing text at line {block.Line}";
                            return null;
                        }
                        string? text = GetInlineText(heading.Inline);
                        if (string.IsNullOrEmpty(text))
                        {
                            Error += $"Heading is missing text at line {block.Line}";
                            return null;
                        }
                        switch (text.ToLower())
                        {
                            case "description":
                                string? description = GetMarkdownText(markdownDoc, ref index);
                                if (false == string.IsNullOrEmpty(description))
                                {
                                    tc.description = description;
                                }
                                break;
                            case "preconditions":
                                string? preconditions = GetMarkdownText(markdownDoc, ref index);
                                if (false == string.IsNullOrEmpty(preconditions))
                                {
                                    tc.preconditions = preconditions;
                                }
                                break;
                            case "steps":
                                if (false == GetMarkdownSteps(markdownDoc, ref index, tc))
                                {
                                    return null;
                                }
                                break;
                            case "metadata":
                                if (index < markdownDoc.Count)
                                {
                                    if (false == GetMarkdownMetaData(markdownDoc[index], tc))
                                    {
                                        return null;
                                    }
                                    index++;
                                }
                                break;
                            case "attributes":
                                if (index < markdownDoc.Count)
                                {
                                    var attributes = GetMarkdownAttributes(markdownDoc[index], project);
                                    if (attributes != null)
                                    {
                                        tc.attributes = attributes;
                                        index++;
                                    }
                                }
                                break;
                            case "attachments":
                                break;
                            default:
                                // Handle ID and Name...
                                if (text.Substring(0, 2).ToLower() == "id")
                                {
                                    tc.id = text.Substring(2).Trim();
                                }
                                else if (text.Substring(0, 4).ToLower() == "name")
                                {
                                    tc.name = text.Substring(4).Trim();
                                }
                                else
                                {
                                    Error += $"Don't know what to do with Header {text} at line {heading.Line}.";
                                    return null;
                                }
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            return tc;
        }

        string? GetInlineText(ContainerInline container)
        {
            string text = "";
            foreach (LiteralInline? inline in container)
            {
                if (inline != null)
                {
                    StringSlice content = inline.Content;
                    text += content.Text.Substring(content.Start, content.Length);
                }
            }
            return text;
        }

        private string? GetMarkdownText(MarkdownDocument markdownDoc, ref int index)
        {
            string text = "";
            bool firstBlock = true;
            while (index < markdownDoc.Count)
            {
                Block? block = markdownDoc[index];
                switch (block)
                {
                    case HeadingBlock:
                        if (text.Length > 3)
                        {
                            if (text.Substring(text.Length - 2, 2) == "\r\n")
                            {
                                text = text.Substring(0, text.Length - 2);
                            }
                        }
                        return text;
                    case HtmlBlock:
                        if (false == firstBlock)
                        {
                            text += "\r\n\r\n";
                        }
                        HtmlBlock html = (HtmlBlock)block;
                        foreach (StringLine line in html.Lines)
                        {
                            text += line.Slice.ToString();
                            text += "\r\n";
                        }
                        firstBlock = false;
                        break;
                    case ParagraphBlock:
                        if (false == firstBlock)
                        {
                            text += "\r\n\r\n";
                        }
                        ParagraphBlock paragraph = (ParagraphBlock)block;
                        if (paragraph.Inline != null)
                        {
                            foreach (var inline in paragraph.Inline)
                            {
                                switch (inline)
                                {
                                    case LiteralInline:
                                        LiteralInline literal = (LiteralInline)inline;
                                        StringSlice slice = literal.Content;
                                        text += slice.ToString();
                                        break;
                                    case LineBreakInline:
                                        LineBreakInline lineBreak = (LineBreakInline)inline;
                                        text += "\r\n";
                                        break;
                                    case HtmlInline:
                                        text += ((HtmlInline)inline).Tag;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        firstBlock = false;
                        break;
                    case ListBlock:
                        string? listText = GetMarkdownListBlockAsText(block);
                        if (string.IsNullOrEmpty(listText))
                        {
                            return null;
                        }
                        text += listText;
                        break;
                    default:
                        break;
                }
                index++;
            }
            if (text.Length > 3)
            {
                if (text.Substring(text.Length-2, 2) == "\r\n")
                {
                    text = text.Substring(0, text.Length-2);
                }
            }
            return text;
        }


        private bool GetMarkdownMetaData(Block block, TestCaseForUpload tc)
        {
            if (block is not ListBlock)
            {
                Error += $"Expect only a list in Metadata at line {block.Line}.";
                return false;
            }

            Dictionary<string, string>? items = GetMarkdownDictionary(block);
            if (items == null)
            {
                return false;
            }

            foreach (var item in items)
            {
                switch (item.Key.ToLower())
                {
                    case "locked":
                        tc.locked = item.Value.ToLower().Contains("true");
                        break;
                    case "broken":
                        tc.broken = item.Value.ToLower().Contains("true");
                        break;
                    case "automated":
                        tc.automated = item.Value.ToLower().Contains("true");
                        break;
                    case "deleted":
                        tc.deleted = item.Value.ToLower().Contains("true");
                        break;
                    case "launchbroken":
                        tc.launchBroken = item.Value.ToLower().Contains("true");
                        break;
                    case "lastmodifiedtime":
                        tc.lastModifiedTime = long.Parse(item.Value);
                        break;
                    default:
                        break;
                }
            }
            return true;
        }


        private Dictionary<string, string[]>? GetMarkdownAttributes(Block block, Project project)
        {
            if (block is not ListBlock)
            {
                Error += $"Was expecting a list at line {block.Line}";
                return null;
            }

            Dictionary<string, string[]> attributes = new Dictionary<string, string[]>();
            Dictionary<string, string>? items = GetMarkdownDictionary(block);
            if (items == null)
            {
                return null;
            }

            foreach(var item in items)
            {
                string[] values = item.Value.Split(",");
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }

                // Look up magic number for attribute name:
                string key = AttributeKeyForName(project, item.Key);
                // keys might be missing if the project isn't correctly set up
                if (string.IsNullOrEmpty(key))
                {
                    Error += $"Missing Attribute {item.Key} in project";
                    return null;
                }
                else if (attributes.ContainsKey(key))
                {
                    Error += $"Duplicate Attribute {key} at {block.Line}.";
                    return null;
                }

                attributes.Add(key, values);
            }

            return attributes;
        }


        private Dictionary<string, string>? GetMarkdownDictionary(Block block)
        {
            if (block is not ListBlock)
            {
                Error += $"Was expecting a list at line {block.Line}";
                return null;
            }

            Dictionary<string, string> items = new Dictionary<string, string>();

            foreach (ListItemBlock item in (ListBlock)block)
            {
                string? text = GetListItemText(item);
                if (text == null)
                {
                    return null;
                }
                string[] pieces = text.Split(":");
                if (pieces.Length == 2)
                {
                    pieces[1] = pieces[1].Trim();
                    items.Add(pieces[0], pieces[1]);
                }
                else
                {
                    Error += $"Don't know what to do with {text} at line {item.Line}. Was expecting name: value";
                    return null;
                }
            }

            return items;
        }


        private string? GetListItemText(ListItemBlock item)
        {
            string itemText = "";
            foreach (var child in item.Descendants())
            {
                switch (child)
                {
                    case LiteralInline:
                        LiteralInline literal = (LiteralInline)child;
                        StringSlice slice = literal.Content;
                        string text = slice.ToString();
                        itemText += text;
                        break;
                    case LineBreakInline:
                        itemText += "\r\n";
                        break;
                    case ParagraphBlock:
                        // Ignore.  Its text will be in a descendent LiteralInline
                        break;
                    default:
                        Error += $"Unexpected child type in list at line {child.Line}.";
                        return null;
                }
            }
            return itemText;
        }

        private string? GetMarkdownListBlockAsText(Block block)
        {
            if (block is not ListBlock)
            {
                Error += $"Was expecting a list at {block.Line}";
                return null;
            }

            int listItemNumber = 1;
            string listText = "";
            foreach (ListItemBlock item in (ListBlock)block)
            {
                listText += "\r\n";
                if (item.Order== 0)
                {
                    listText += $"* ";
                }
                else
                {
                    listText += $"{listItemNumber}. ";
                    listItemNumber++;
                }
                string? text = GetListItemText(item);
                if (text == null)
                {
                    return null;
                }
                listText += text;
            }

            return listText;
        }


        private bool GetMarkdownSteps(MarkdownDocument markdownDoc, ref int index, TestCaseForUpload tc)
        {
            tc.steps = new List<Step>();
            while (index < markdownDoc.Count)
            {
                Block block = markdownDoc[index];
                if (block is not HeadingBlock)
                {
                    Error += $"Expect a Header at line {block.Line}";
                    return false;
                }

                HeadingBlock heading = (HeadingBlock)block;
                if (heading.Level == 1)
                {
                    return true;
                }

                if (heading.Level != 2)
                {
                    Error += $"Expect a Header 2 for Step under Steps at line {block.Line}";
                    return false;
                }

                if (heading.Inline == null)
                {
                    Error += $"Expect a Header 2 for Step under Steps at line {block.Line}";
                    return false;
                }

                string? text = GetInlineText(heading.Inline);
                if ((string.IsNullOrEmpty(text)) || (text.ToLower() != "step"))
                {
                    Error += $"Expect a Header 2 for Step under Steps at line {block.Line}";
                    return false;
                }

                index++;
                if (false == GetMarkdownStep(markdownDoc, ref index, tc))
                {
                    return false;
                }
            }
            return true;
        }


        private bool GetMarkdownStep(MarkdownDocument markdownDoc, ref int index, TestCaseForUpload tc)
        {
            string? action = GetMarkdownAction(markdownDoc, ref index, tc);
            if (string.IsNullOrEmpty(action))
            {
                return false;
            }

            string? expectation = GetMarkdownExpectation(markdownDoc, ref index, tc);
            if (expectation == null)
            {
                return false;
            }

            Step step = new();
            step.action = action;
            step.expectation = expectation;
            tc.steps.Add(step);

            return true;
        }


        private string? GetMarkdownAction(MarkdownDocument markdownDoc, ref int index, TestCaseForUpload tc)
        {
            return GetMarkdownStepDetails(markdownDoc, ref index, tc, "Action");
        }


        private string? GetMarkdownExpectation(MarkdownDocument markdownDoc, ref int index, TestCaseForUpload tc)
        {
            return GetMarkdownStepDetails(markdownDoc, ref index, tc, "Expectation");
        }


        private string? GetMarkdownStepDetails(MarkdownDocument markdownDoc, ref int index, TestCaseForUpload tc, string detail)
        {
            if (index >= markdownDoc.Count)
            {
                Error += $"Reached end of document before getting {detail} for Step.";
                return null;
            }
            Block block = markdownDoc[index];
            index++;
            if (block is not HeadingBlock)
            {
                Error += $"Expected Header 3 for {detail} at line {block.Line}";
                return null;
            }
            HeadingBlock heading = (HeadingBlock)block;

            if (heading.Level != 3)
            {
                Error += $"Expect a Header 3 for {detail} under Step at line {block.Line}";
                return null;
            }

            if (heading.Inline == null)
            {
                Error += $"Expect a Header 3 for {detail} under Step at line {block.Line}";
                return null;
            }

            string? headerName = GetInlineText(heading.Inline);
            if ((string.IsNullOrEmpty(headerName)) || (headerName.ToLower() != detail.ToLower()))
            {
                Error += $"Expect a Header 2 for {detail} under Step at line {block.Line}";
                return null;
            }

            string? text = GetMarkdownText(markdownDoc, ref index);

            return text;
        }


        private string CleanText(string t)
        {
            string newT = t.Replace("<br>\r\n", "<br>");
            newT = newT.Replace("<br>\n", "<br>");
            while (t != newT)
            {
                t = newT;
                newT = CleanText(t);
            };
            return newT;
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

        public string GetTestcasesString(string projectId)
        {
            string url = BaseDokimionApiUrl() + $"/{projectId}/testcase";
            string json = "";
            try
            {
                json = m_Client.GetStringAsync(url).Result;
            }
            catch (Exception ex)
            {
                Error = "Cannot get test cases from Dokimion because:\n" + ex.Message;
            }
            return json;
        }

        public TestCase? GetTestCaseAsObject(string testcaseId, Project project)
        {
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase/" + testcaseId;
            return GetTestCaseAsObject(url);
        }

        public bool DownloadTestcase(string testcaseId, Project project, string folderPath)
        {
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase/" + testcaseId;

            // Get the test case from Dokimion
            TestCase? testcase = GetTestCaseAsObject(url);
            if (testcase == null)
            {
                return false;
            }

            // Generate the Markdown format for this testcase From Dokimion
            string md = GenerateMarkdown(testcase, project);
            if (string.IsNullOrEmpty(md))
            {
                return false;
            }

            // Save the Markdown of the test case:
            if (false == SaveTestCase(testcaseId, folderPath, md))
            {
                return false;
            }

            // Save any attachments:
            foreach (var attachment in testcase.attachments)
            {
                byte[]? fileContent = GetAttachment(project.id, testcase.id, attachment.id);
                if (fileContent == null)
                {
                    return false;
                }

                string filename = $"{testcaseId}_{attachment.title}";
                if (false == SaveAttachment(filename, folderPath, fileContent))
                {
                    return false;
                }
            }

            return true;
        }


        public string GenerateMarkdown(TestCase tc, Project project)
        {
            string md = "";
            md += $"# ID {tc.id}\r\n\r\n";
            md += $"# Name {tc.name}\r\n\r\n";
            md += $"# Description\r\n\r\n";
            if (false == string.IsNullOrEmpty(tc.description))
            {
                md += $"{tc.description}\r\n\r\n";
            }
            md += $"# Preconditions\r\n\r\n";
            if (false == string.IsNullOrEmpty(tc.preconditions))
            {
                md += $"{tc.preconditions}\r\n\r\n";
            }

            md += $"# Steps\r\n\r\n";
            foreach(Step step in tc.steps)
            {
                md += $"## Step\r\n\r\n";
                md += $"### Action\r\n\r\n";
                md += $"{step.action}\r\n\r\n";
                md += $"### Expectation\r\n\r\n";
                if (false == string.IsNullOrEmpty(step.expectation))
                {
                    md += $"{step.expectation}\r\n\r\n";
                }
            }

            md += $"# Attributes\r\n\r\n";
            foreach (var attr in tc.attributes)
            {
                string? attrName = null;
                try
                {
                    attrName = project.attributes[attr.Key];
                }
                catch
                {
                    Error += $"Test case {tc.id} has a garbage attribute.\r\n";
                }
                if (false == string.IsNullOrEmpty(attrName))
                {
                    attrName = attrName.Replace(" ", SPACE_REPLACER);
                    string s = "";
                    bool first = true;
                    foreach (string value in attr.Value)
                    {
                        string trimmedValue = value.Trim();
                        s += first ? $"{trimmedValue}" : $", {trimmedValue}";
                        first = false;
                    }
                    md += $"* {attrName}: {s}\r\n";
                }
            }
            if (tc.attachments.Count > 0)
            {
                md += $"\r\n";
            }

            md += $"# Metadata\r\n\r\n";
            md += $"* automated: {tc.automated}\r\n";
            md += $"* broken: {tc.broken}\r\n";
            md += $"* createdBy: {tc.createdBy}\r\n";
            md += $"* createdTime: {tc.createdTime}\r\n";
            md += $"* deleted: {tc.deleted}\r\n";
            md += $"* lastModifiedBy: {tc.lastModifiedBy}\r\n";
            md += $"* lastModifiedTime: {tc.lastModifiedTime}\r\n";
            md += $"* launchBroken: {tc.launchBroken}\r\n";
            md += $"* locked: {tc.locked}\r\n";

            return md;
        }


        private bool SaveTestCase(string testcaseId, string folderPath, string markdown)
        {
            // Create the folder for this project if it does not exist
            if (false == Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch (Exception e)
                {
                    Error = $"Error creating folder {folderPath} because {e.Message}";
                    if (e.InnerException != null)
                    {
                        Error += "\r\n" + e.InnerException.ToString();
                    }
                    return false;
                }
            }

            // Save the file in the repo folder
            string path = Path.Combine(folderPath, testcaseId + ".md");
            try
            {
                File.WriteAllText(path, markdown);
            }
            catch (Exception e)
            {
                Error = $"Error writing file {path} because {e.Message}";
                if (e.InnerException != null)
                {
                    Error += "\r\n" + e.InnerException.ToString();
                }
                return false;
            }
            return true;
        }

        private byte[]? GetAttachment(string projectId, string testcaseId, string attachmentId)
        {
            string url = BaseDokimionApiUrl() + "/" + projectId + "/testcase/" + testcaseId + "/attachment/" + attachmentId;
            var resp = m_Client.GetAsync(url).Result;
            if (false == resp.IsSuccessStatusCode)
            {
                Error = $"Cannot read attachment for project {projectId}, test case {testcaseId}, attachment {attachmentId} because {resp.ReasonPhrase}";
                if (resp.Content != null)
                {
                    Error += "\r\n" + resp.Content.ReadAsStringAsync().Result;
                }
                Log.Error($"For GET {url} got this error:");
                Log.Error(Error);

                // There is a bug in Dokimion where it has unfetchable attachments, so return an empty array instead of erroring.
                return new byte[] { };
            }

            return resp.Content.ReadAsByteArrayAsync().Result;
        }

        private bool SaveAttachment(string title, string folderPath, byte[] fileContent)
        {
            // Assume folder exists from saving the Markdown file.
            // Save the file in the repo folder
            string path = Path.Combine(folderPath, title);
            try
            {
                File.WriteAllBytes(path, fileContent);
            }
            catch (Exception e)
            {
                Error = $"Error writing file {path} because {e.Message}";
                if (e.InnerException != null)
                {
                    Error += "\r\n" + e.InnerException.ToString();
                }
                return false;
            }
            return true;
        }

        public bool IsTestCaseChanged(TestCaseForUpload testcaseFromDokimion, TestCaseForUpload extracted)
        {
            if (testcaseFromDokimion.id != extracted.id) return true;
            if (testcaseFromDokimion.name != extracted.name) return true;
            if (testcaseFromDokimion.description != extracted.description) return true;
            if (testcaseFromDokimion.preconditions != extracted.preconditions) return true;
            if (testcaseFromDokimion.automated != extracted.automated) return true;
            if (testcaseFromDokimion.broken != extracted.broken) return true;
            if (testcaseFromDokimion.deleted != extracted.deleted) return true;
            if (testcaseFromDokimion.locked != extracted.locked) return true;
            if (testcaseFromDokimion.launchBroken != extracted.launchBroken) return true;
            if (testcaseFromDokimion.steps.Count != extracted.steps.Count) return true;
            // Since there are no differences yet, the steps for both have the same size.
            for (int i = 0; i < extracted.steps.Count; i++)
            {
                string dok = testcaseFromDokimion.steps[i].action;
                if (false == dok.Contains('\r'))
                {
                    dok = dok.Replace("\n", "\r\n");
                }
                string file = extracted.steps[i].action;
                if (dok != file) return true;

                dok = testcaseFromDokimion.steps[i].expectation;
                if (false == dok.Contains('\r'))
                {
                    dok = dok.Replace("\n", "\r\n");
                }
                file = extracted.steps[i].expectation;
                if (dok != file) return true;
            }
            if (testcaseFromDokimion.attributes.Count != extracted.attributes.Count) return true;
            // We know they have the same number of key/value pairs
            foreach (string key in extracted.attributes.Keys)
            {
                if ((false == testcaseFromDokimion.attributes.ContainsKey(key))
                    || (false == extracted.attributes.ContainsKey(key))) return true;
                if (testcaseFromDokimion.attributes[key].Length != extracted.attributes[key].Length) return true;
                // We know the values have the same number of strings in them.
                for (int i = 0; i < extracted.attributes[key].Length; i++)
                {
                    string attributeValue = testcaseFromDokimion.attributes[key][i];
                    attributeValue = attributeValue.Trim();
                    if (attributeValue != extracted.attributes[key][i]) return true;
                }
            }

            if (testcaseFromDokimion.attachments.Count != extracted.attachments.Count) return true;
            // We know they have the same number of items
            // TODO: Handle the case where they are not in the same order
            for (int i = 0; i < testcaseFromDokimion.attachments.Count; i++)
            {
                if (testcaseFromDokimion.attachments[i].id != extracted.attachments[i].id) return true;
                if (testcaseFromDokimion.attachments[i].title != extracted.attachments[i].title) return true;
                if (testcaseFromDokimion.attachments[i].createdBy != extracted.attachments[i].createdBy) return true;
                if (testcaseFromDokimion.attachments[i].createdTime != extracted.attachments[i].createdTime) return true;
                if (testcaseFromDokimion.attachments[i].dataSize != extracted.attachments[i].dataSize) return true;
            }
            return false;
        }

        public UploadStatus UploadTestCaseToProject(string folderPath, string testcaseId, Project project)
        {
            string filename = Path.Combine(folderPath, testcaseId + ".md");
            UploadStatus resp = UploadMarkdownFileToProjectIfDifferent(filename, project, out TestCaseForUpload? uploaded);
            if (uploaded == null)
            {
                return UploadStatus.Error;
            }
            bool updated = resp == UploadStatus.Updated;

            UploadStatus attResp = UploadAttachments(folderPath, uploaded, project);
            switch (attResp)
            {
                case UploadStatus.Error:
                    return UploadStatus.Error;
                case UploadStatus.NoChange:
                    attResp = resp;
                    break;
                case UploadStatus.Updated:
                    updated = true;
                    break;
                default:
                    return UploadStatus.Error;
            }
            if (updated)
            {
                if (false == DownloadTestcase(testcaseId, project, folderPath))
                {
                    // DownloadTestcase sets Error
                    return UploadStatus.Error;
                }
            }
            return attResp;
        }

        public UploadStatus UploadMarkdownFileToProjectIfDifferent(string path, Project project, out TestCaseForUpload? uploaded)
        {
            uploaded = null;
            // Read text from Markdown file.
            string markdownText = "";
            try
            {
                markdownText = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return UploadStatus.Error;
            }

            // Parse the file into MarkdownDocument
            MarkdownDocument? markdownDocument = null;
            try
            {
                markdownDocument = Markdown.Parse(markdownText);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return UploadStatus.Error;
            }

            // Extract info from MarkdownDocument to an instance of TestCase.
            TestCaseForUpload? extracted = MarkdownToObject(markdownDocument, path, project);
            if (extracted == null)
            {
                Error += $"Cannot convert Markdown file {path} to a test case object";
                return UploadStatus.Error;
            }
            uploaded = extracted;

            // Download the TestCase from Dokimion
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase/" + extracted.id;
            TestCase? testcaseFromDokimion = GetTestCaseAsObject(url);
            bool changed = true;
            bool overwrite = false;
            long lastModifiedTimeFromDokimion = 0;
            int extractedId = int.Parse(extracted.id);
            int testCaseId = 0;
            if (testcaseFromDokimion == null)
            {
                // We are uploading a missing test case, so create test cases up to that number so we can then replace it
                TestCaseForUpload tc = new TestCaseForUpload();
                tc.name = "<<empty>>";
                string json = JsonConvert.SerializeObject(tc);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                url = BaseDokimionApiUrl() + "/" + project.id + "/testcase";
                TestCase? testcase;
                do
                {
                    try
                    {
                        var resp = m_Client.PostAsync(url, content).Result;
                        if (false == resp.IsSuccessStatusCode)
                        {
                            Error = $"Error {resp.ReasonPhrase} when trying to create an empty test case.";
                            return UploadStatus.Error;
                        }
                        string responseContent = resp.Content.ReadAsStringAsync().Result;
                        // Deserialize response into our TestCase object
                        testcase = JsonConvert.DeserializeObject<TestCase>(responseContent);
                        if (testcase == null)
                        {
                            Error = "Cannot decode: \r\n" + responseContent;
                            return UploadStatus.Error;
                        }
                        testCaseId = int.Parse(testcase.id);
                        lastModifiedTimeFromDokimion = testcase.lastModifiedTime;
                        extracted.lastModifiedTime = lastModifiedTimeFromDokimion;
                    }
                    catch (Exception ex)
                    {
                        Error = $"Exception thrown when trying to create an empty test case:\n{ex.Message}";
                        return UploadStatus.Error;
                    }
                } while (testCaseId < extractedId);
            }
            else
            {
                lastModifiedTimeFromDokimion = testcaseFromDokimion.lastModifiedTime;
                // Compare selected contents of the tc TestCase to the TestCase read from Dokimion.
                changed = IsTestCaseChanged(testcaseFromDokimion, extracted);
                // Automatically overwrite our dummy test cases and deleted test cases:
                if ((testcaseFromDokimion.name == "<<empty>>") || (testcaseFromDokimion.deleted == true))
                {
                    overwrite = true;
                }
            }

            // If different, write tc TestCase to server.
            if (changed)
            {
                return UploadTestCaseToProjectWithRetries(path, project, extracted, lastModifiedTimeFromDokimion, overwrite);
            }
            return UploadStatus.NoChange;
        }

        UploadStatus UploadAttachments(string folderPath, TestCaseForUpload testcase, Project project)
        {
            bool didUpload = false;
            foreach (var attachment in testcase.attachments)
            {
                // Get the content from our disk:
                string filename = $"{testcase.id}_{attachment.title}";
                string filePath = Path.Combine(folderPath, filename);
                Byte[] diskContent;
                try
                {
                    diskContent = File.ReadAllBytes(filePath);
                }
                catch (Exception ex)
                {
                    Error = $"Cannot read file {filePath} because: " + ex.Message;
                    return UploadStatus.Error;
                }

                // See if an attachment with that ID is already on the server
                bool needToUpload = true;
                bool needToRemove = false;
                byte[]? serverContent = GetAttachment(project.id, testcase.id, attachment.id);
                if (serverContent != null)
                {
                    // If the file is on the server, check if it has the same content
                    if (diskContent.Length == serverContent.Length)
                    {
                        if (diskContent.SequenceEqual(serverContent))
                        {
                            needToUpload = false;
                        }
                        else
                        {
                            needToRemove = true;
                        }
                    }

                }

                if (needToRemove)
                {
                    string url = $"{BaseDokimionApiUrl()}/{project.id}/testcase/{testcase.id}/attachment/{attachment.id}";
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
                    HttpResponseMessage? response = null;
                    try
                    {
                        response = m_Client.SendAsync(request).Result;
                    }
                    catch (Exception ex)
                    {
                        Error = "Cannot delete existing attachment because: " + ex.Message;
                        return UploadStatus.Error;
                    }
                    if (false == response.IsSuccessStatusCode)
                    {
                        Error = "Deletion of attachment failed because: " + response.StatusCode;
                        return UploadStatus.Error;
                    }
                }

                if (needToUpload)
                {
                    string url = $"{BaseDokimionApiUrl()}/{project.id}/testcase/{testcase.id}/attachment";
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    MultipartFormDataContent formData = new MultipartFormDataContent();

                    ByteArrayContent fileContent = new ByteArrayContent(diskContent);
                    string mimeType = MimeMapping.MimeUtility.GetMimeMapping(filename);
                    fileContent.Headers.Add("ContentType", mimeType);
                    formData.Add(fileContent, "file");

                    StringContent fileIdContent = new StringContent(filename);
                    formData.Add(fileIdContent, "fileId");

                    StringContent initialPreview = new StringContent("[]");
                    formData.Add(fileIdContent, "initialPreview");

                    StringContent initialPreviewConfig = new StringContent("[]");
                    formData.Add(fileIdContent, "initialPreviewConfig");

                    StringContent initialPreviewThumbTags = new StringContent("[]");
                    formData.Add(fileIdContent, "initialPreviewThumbTags");

                    request.Content = formData;
                    HttpResponseMessage? response = null;
                    try
                    {
                        response = m_Client.SendAsync(request).Result;
                    }
                    catch (Exception ex)
                    {
                        Error = "Cannot upload attachment because: " + ex.Message;
                        return UploadStatus.Error;
                    }
                    if (false == response.IsSuccessStatusCode)
                    {
                        Error = "Upload of attachment failed because: " + response.StatusCode;
                        return UploadStatus.Error;
                    }
                    didUpload = true;
                }

            }

            return didUpload ? UploadStatus.Updated : UploadStatus.NoChange;
        }

        private UploadStatus UploadTestCaseToProjectWithRetries(string filename, Project project, TestCaseForUpload extracted, long lastModifiedTimeFromDokimion, bool overwrite)
        {
            DialogResult answer = UploadTestCaseObject(extracted, project, overwrite);
            switch (answer)
            {
                case DialogResult.OK:
                    // "OK" is processed further down in this function
                    // to eliminate duplicate code
                    break;

                case DialogResult.TryAgain:
                    // Use the time from what is currently in Dokimion so it will be accepted:
                    extracted.lastModifiedTime = lastModifiedTimeFromDokimion;
                    answer = UploadTestCaseObject(extracted, project, false);
                    // Processed retry response further down in this function
                    break;

                case DialogResult.Cancel:
                    Error = "Uploads aborted by user.";
                    return UploadStatus.Aborted;

                case DialogResult.Continue:
                    return UploadStatus.NotChanged;
            }

            // Process OK or response of retry.
            switch (answer)
            {
                case DialogResult.OK:
                    // Download the test case again so that we get the latest timestamp
                    FileInfo? fi = new FileInfo(filename);
                    string? folderPath = fi.DirectoryName;
                    if (folderPath == null)
                    {
                        Error = "Cannot get folder name from " + filename;
                        return UploadStatus.Error;
                    }
                    return UploadStatus.Updated;
                case DialogResult.TryAgain:
                    // The retry didn't work, so return an error
                    return UploadStatus.Error;
                case DialogResult.Cancel:
                    Error = "Uploads aborted by user.";
                    return UploadStatus.Aborted;
                case DialogResult.Continue:
                    return UploadStatus.NotChanged;
            }

            return UploadStatus.NotChanged;
        }

        private DialogResult UploadTestCaseObject(TestCaseForUpload tc, Project project, bool overwrite)
        {
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase";
            string json = JsonConvert.SerializeObject(tc);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage? resp = null;
            try
            {
                resp = m_Client.PutAsync(url, content).Result;
            }
            catch (Exception ex)
            {
                Error = "Could not upload the test case because:\r\n";
                Error += ex.Message;
                if (ex.InnerException != null)
                {
                    Error += "\r\n" + ex.InnerException.Message;
                }
                if (resp != null)
                {
                    Error += "\r\n" + resp.ReasonPhrase;
                    if (resp.Content != null)
                    {
                        Error += "\r\n" + resp.Content.ReadAsStringAsync().Result;
                    }
                }
                return DialogResult.Cancel;
            }
            if (false == resp.IsSuccessStatusCode)
            {
                string error = resp.Content.ReadAsStringAsync().Result;
                if (error.Contains("Entity has been changed previously"))
                {
                    if (overwrite)
                    {
                        return DialogResult.TryAgain;
                    }
                    Error = $"Testcase {tc.id} on server is newer than our testcase.";
                    var answer = MessageBox.Show($"Testcase {tc.id} on the server has changed since the file in the repo was created.\r\n" +
                            "Do you wish to upload anyway, overwriting changes on the server? (Try Again)\r\n" +
                            "Or skip uploading this file and continue with other files? (Continue)\r\n" +
                            "Or quit uploading files? (Cancel)",
                        "Conflict Detected",
                        MessageBoxButtons.CancelTryContinue);
                    return answer;
                }
                Error = "Server returned error when uploading test case:\r\n" + resp.ReasonPhrase;
                if (resp.Content != null)
                {
                    Error += "\r\n" + error;
                }
                return DialogResult.Cancel;
            }

            return DialogResult.OK;
        }


        public TestCase? GetTestCaseAsObject(string url)
        {
            // Get the test case from Dokimion
            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (false == resp.IsSuccessStatusCode)
            {
                Error = $"Cannot get test case from Dokimion because: {resp.StatusCode} {resp.ReasonPhrase} {json}";
                return null;
            }

            // Deserialize response into our TestCase object
            TestCase? testcase = JsonConvert.DeserializeObject<TestCase>(json);
            if (testcase == null)
            {
                Error = "Cannot decode: \r\n" + json;
                return null;
            }
            // Trim whitespace from start and end of attribute values since Dokimion gives some.
            foreach (var attr in testcase.attributes)
            {
                for (int val = 0; val < attr.Value.Count(); val++)
                {
                    attr.Value[val] = attr.Value[val].Trim();
                }
            }
            // Trim whitespace from other items, just in case:
            testcase.id = testcase.id.Trim();
            testcase.name = testcase.name.Trim();
            testcase.description = testcase.description.Trim();
            testcase.preconditions = testcase.preconditions.Trim();
            return testcase;
        }

        public bool DeleteTestCase(string projectId, string testcaseId)
        {
            string url = BaseDokimionApiUrl() + "/" + projectId + "/testcase/" + testcaseId;
            HttpResponseMessage? resp = null;
            try
            {
                resp = m_Client.DeleteAsync(url).Result;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
            if (false == resp.IsSuccessStatusCode)
            {
                string error = resp.Content.ReadAsStringAsync().Result;
                Error = error;
                return false;
            }

            return true;
        }


        }

}
