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
        public Dictionary<string, string> attributeNameForKey = new Dictionary<string, string>();

        public string Name
        {
            get { return name; }
        }
    }

    public class AttributeIdToName
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
        public string ProjectFolder = "";
        public const string SPACE_REPLACER = "_";

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
                Error = "Cannot log out because:\r\n" + e.Message;
                if (resp != null)
                {
                    Error += "\r\n" + resp.ReasonPhrase;
                }
            }
            m_Client.DefaultRequestHeaders.Remove("whoruSessionId");
        }

        public bool Login(string username, string password)
        {
            if (false)
            {
                m_Client.DefaultRequestHeaders.Add("Whoru-Api-Token", "abc");
                return true;
            }
            else
            {
                string url = $"{BaseDokimionApiUrl()}/user/login?login={username}&password={password}";
                HttpResponseMessage? resp = null;
                try
                {
                    resp = m_Client.PostAsync(url, null).Result;
                }
                catch (Exception e)
                {
                    Error = "Cannot log in because:\r\n" + e.Message;
                    if (resp != null)
                    {
                        Error += "\r\n" + resp.ReasonPhrase;
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
                    Error = "Server returned error: " + resp.ReasonPhrase + "\r\n" + json;
                }
                return results;
            }
        }

        public Dictionary<string, string> GetAttributeNamesForProject(string projectId)
        {
            string url = BaseDokimionApiUrl() + "/" + projectId + "/attribute";

            List<AttributeIdToName>? attrList = null;
            Error = "";

            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            if (resp.IsSuccessStatusCode)
            {
                attrList = JsonConvert.DeserializeObject<List<AttributeIdToName>>(json);
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
                    project.attributeNameForKey = GetAttributeNamesForProject(project.id);
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
            MarkdownFile markdownFile = new MarkdownFile();
            string md = markdownFile.GenerateMarkdown(testcase, project);
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

                string filename = $"{testcaseId}_{attachment.id}_{attachment.title}";
                if (false == SaveAttachment(filename, folderPath, fileContent))
                {
                    return false;
                }
            }

            return true;
        }



        public static string ImproveBreaks(string s)
        {
            // This is to add CR/LF after a <br> so that the saved markdown is more readable.
            s = s.Replace("<br>", "<br>\r\n");
            s = s.Replace("<br>\r\n\r\n", "<br>\r\n");
            s = s.Replace("<br>\r\n\n", "<br>\r\n");

            // replace solo \n with \r\n, just to be consistent
            s = s.Replace("\n", "\r\n");
            s = s.Replace("\r\r", "\r");
            return s;
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

        bool AreStringsEqual(string a, string b)
        {
            if (false == a.Contains('\r'))
            {
                a = a.Replace("\n", "\r\n");
            }

            if (false == b.Contains('\r'))
            {
                b = b.Replace("\n", "\r\n");
            }

            return a == b;
        }

        public bool IsTestCaseChanged(TestCaseForUpload testcaseFromDokimion, TestCaseForUpload extracted)
        {
            string fromDokimion;
            if (testcaseFromDokimion.id != extracted.id) return true;
            if (testcaseFromDokimion.name != extracted.name) return true;
            fromDokimion = ImproveBreaks(testcaseFromDokimion.description);
            if (false == AreStringsEqual(fromDokimion, extracted.description)) return true;
            fromDokimion = ImproveBreaks(testcaseFromDokimion.preconditions);
            if (false == AreStringsEqual(fromDokimion, extracted.preconditions)) return true;
            if (testcaseFromDokimion.automated != extracted.automated) return true;
            if (testcaseFromDokimion.broken != extracted.broken) return true;
            if (testcaseFromDokimion.deleted != extracted.deleted) return true;
            if (testcaseFromDokimion.locked != extracted.locked) return true;
            if (testcaseFromDokimion.launchBroken != extracted.launchBroken) return true;

            if (testcaseFromDokimion.steps.Count != extracted.steps.Count) return true;
            // Since there are no differences yet, the steps for both have the same size.
            for (int i = 0; i < extracted.steps.Count; i++)
            {
                fromDokimion = ImproveBreaks(testcaseFromDokimion.steps[i].action);
                if (false == AreStringsEqual(fromDokimion, extracted.steps[i].action)) return true;
                fromDokimion = ImproveBreaks(testcaseFromDokimion.steps[i].expectation);
                if (false == AreStringsEqual(fromDokimion, extracted.steps[i].expectation)) return true;
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
            MarkdownFile markdownFile = new();
            TestCaseForUpload? extracted = markdownFile.MarkdownToObject(markdownDocument, path, project);
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
            return UploadStatus.Updated;

            bool didUpload = false;
            foreach (var attachment in testcase.attachments)
            {
                // Get the content from our disk:
                string filename = $"{testcase.id}_{attachment.id}_{attachment.title}";
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

        private bool UploadAttachments(Project project, TestCaseForUpload tc, Attachment att)
        {
            string uri = BaseDokimionApiUrl() + $"/{project.id}/testcase/{tc.id}/attachment";

            string filePath = Path.Combine(ProjectFolder, $"{tc.id}_{att.id}_{att.title}");
            ByteArrayContent fileData = new(File.ReadAllBytes(filePath));

            StringContent fileName = new(att.title);

            MultipartFormDataContent form = new();
            form.Add(fileData, "file", att.title);
            form.Add(fileName, "fileId");

            HttpClient client = new();
            var response = client.PostAsync(uri, form).Result;
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            return true;
        }

        public bool UploadPlainTextTextCase(Project project, TestCaseForUpload? fromDokimion, TestCaseForUpload? fromGitHub)
        {
            Error = "";
            string url = BaseDokimionApiUrl() + $"/{project.id}/testcase";

            if (fromDokimion == null)
            {
                Error += "Cannot upload to a test case that does not already exist in Dokimion.\r\n";
                Error += "Create the test case in Dokimion before trying to upload it.\r\n";
                return false;
            }

            if (fromGitHub == null)
            {
                Error += "We need a test case in GitHub to upload it.\r\n";
                return false;
            }


            // Copy just the parts we can set into what is already there
            fromDokimion.name = fromGitHub.name;
            fromDokimion.description = fromGitHub.description;
            fromDokimion.attributes = fromGitHub.attributes;
            fromDokimion.steps = fromGitHub.steps;

            string json = JsonConvert.SerializeObject(fromDokimion);
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
                return false;
            }
            if (false == resp.IsSuccessStatusCode)
            {
                Error += resp.StatusCode + "\r\n";
                if (resp.Content != null)
                {
                    Error += resp.Content.ReadAsStringAsync().Result + "\r\n";
                }
                return false;
            }
            return true;
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
                Array.Sort(attr.Value);     // so we can compare what is in the file to what is in Dokimion.
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
