using MimeMapping;

using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Serilog;

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

        [JsonIgnoreAttribute]
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

        public Metadata ExtractMetadata()
        {
            string json = JsonConvert.SerializeObject(this);
            Metadata? metadata = JsonConvert.DeserializeObject<Metadata>(json);
            if (metadata == null)
            {
                throw new Exception($"Cannot extract metadata from test case {this.id}");
            }
            return metadata;
        }
    }

    public class Metadata : TestCaseShort
    {
        // This is TestCase minus the steps member
        public string description = "";
        public string preconditions = "";
        public Int64 lastModifiedTime;
        public string createdBy = "";
        public Int64 createdTime;
        public string lastModifiedBy = "";

        public string PrettyPrint()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }
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
            // Read text from XML file.
            string xmlText = "";
            try
            {
                xmlText = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }

            // Create XmlDocument.
            XmlDocument? xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xmlText);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }

            // Extract info from XmlDocument to an instance of TestCaseForUpload.
            uploaded = XmlToObject(xmlDoc, path, project);
            if (uploaded == null)
            {
                Error += $"\r\nCannot convert XML file {path} to a test case object";
                return null;
            }

            return uploaded;
        }

        private TestCaseForUpload? XmlToObject(XmlDocument xmlDoc, string filename, Project project)
        {
            TestCaseForUpload tc = new TestCaseForUpload();
            XmlNode? overall = xmlDoc.FirstChild;
            if (overall == null)
            {
                Error = $"Base node of {filename} does not exist.";
                return null;
            }
            if (overall.Name != "TestCase")
            {
                Error = $"Base node of {filename} is not \"TestCase\".";
                return null;
            }

            XmlNode? idNode = FindNodeNamed(overall, "id");
            if (idNode == null)
            {
                Error = $"id node in {filename} is missing.";
                return null;
            }
            tc.id = idNode.InnerText;

            XmlNode? nameNode = FindNodeNamed(overall, "name");
            if (nameNode == null)
            {
                Error = $"name node in {filename} is missing.";
                return null;
            }
            tc.name = nameNode.InnerText;

            XmlNode? descNode = FindNodeNamed(overall, "description");
            if (descNode == null)
            {
                Error = $"description node in {filename} is missing.";
                return null;
            }
            tc.description = CleanText(descNode.InnerText);

            XmlNode? preconditionsNode = FindNodeNamed(overall, "preconditions");
            if (preconditionsNode == null)
            {
                tc.preconditions = "";
            }
            else
            {
                tc.preconditions = CleanText(preconditionsNode.InnerText);
            }

            XmlNode? autoNode = FindNodeNamed(overall, "automated");
            if (autoNode == null)
            {
                Error = $"automated node in {filename} is missing.";
                return null;
            }
            tc.automated = autoNode.InnerText.ToLower() == "true";

            XmlNode? brokenNode = FindNodeNamed(overall, "broken");
            if (brokenNode == null)
            {
                return null;
            }
            tc.broken = brokenNode.InnerText.ToLower() == "true";

            XmlNode? deletedNode = FindNodeNamed(overall, "deleted");
            if (deletedNode == null)
            {
                Error = $"broken node in {filename} is missing.";
                return null;
            }
            tc.deleted = deletedNode.InnerText.ToLower() == "true";

            XmlNode? lockedNode = FindNodeNamed(overall, "locked");
            if (lockedNode == null)
            {
                Error = $"locked node in {filename} is missing.";
                return null;
            }
            tc.locked = lockedNode.InnerText.ToLower() == "true";

            XmlNode? launchBrokenNode = FindNodeNamed(overall, "launchBroken");
            if (launchBrokenNode == null)
            {
                Error = $"launchbroken node in {filename} is missing.";
                return null;
            }
            tc.launchBroken = launchBrokenNode.InnerText.ToLower() == "true";

            XmlNode? modifiedNode = FindNodeNamed(overall, "lastModifiedTime");
            if (modifiedNode == null)
            {
                Error = $"lastModifiedTime node in {filename} is missing.";
                return null;
            }
            Int64 time = Int64.Parse(modifiedNode.InnerText);
            tc.lastModifiedTime = time;

            XmlNode? stepsNode = FindNodeNamed(overall, "steps");
            if (stepsNode == null)
            {
                Error = $"steps node in {filename} is missing.";
                return null;
            }
            tc.steps = new List<Step>();
            foreach (XmlNode stepNode in stepsNode.ChildNodes)
            {
                if (stepNode.Name != "step")
                {
                    Error = $"Expected child of <steps> to be <step>, not <{stepNode.Name}>.";
                    return null;
                }
                Step step = new Step();
                XmlNode? actionNode = FindNodeNamed(stepNode, "action");
                if (actionNode == null)
                {
                    return null;
                }
                step.action = CleanText(actionNode.InnerText);
                XmlNode? expNode = FindNodeNamed(stepNode, "expectation");
                if (expNode == null)
                {
                    return null;
                }
                step.expectation = CleanText(expNode.InnerText);
                tc.steps.Add(step);
            }

            XmlNode? attributesNode = FindNodeNamed(overall, "attributes");
            if (attributesNode == null)
            {
                Error = $"attributes node in {filename} is missing.";
                return null;
            }
            tc.attributes = new Dictionary<string, string[]>();
            foreach (XmlNode attrNode in attributesNode.ChildNodes)
            {
                string key = AttributeKeyForName(project, attrNode.Name);
                // keys might be missing if the project isn't correctly set up
                if (string.IsNullOrEmpty(key))
                {
                    Log.Error($"Missing Attribute {attrNode.Name} in project");
                }
                else if (tc.attributes.ContainsKey(key))
                {
                    Log.Error($"Duplicate Attribute {key} in test case {tc.name}.");
                }
                else
                {
                    string[] values = attrNode.InnerText.Split(new char[] { ',' });
                    for (int i = 0; i < values.Length; i++)
                    {
                        string value = values[i].Replace("\"", "");
                        value = value.Trim();
                        values[i] = value;
                    }
                    tc.attributes.Add(key, values);
                }
            }

            XmlNode? attachmentsNode = FindNodeNamed(overall, "attachments");
            if (attachmentsNode == null)
            {
                Error = $"attachments node in {filename} is missing.";
                return null;
            }
            tc.attachments = new List<Attachment>();
            foreach (XmlNode attachNode in attachmentsNode.ChildNodes)
            {
                Attachment attach = new Attachment();
                XmlNode? attachIdNode = FindNodeNamed(attachNode, "id");
                if (attachIdNode == null)
                {
                    Error = $"attachment id node in {filename} is missing.";
                    return null;
                }
                attach.id = attachIdNode.InnerText;
                XmlNode? attachTitleNode = FindNodeNamed(attachNode, "title");
                if (attachTitleNode == null)
                {
                    Error = $"attachment title node in {filename} is missing.";
                    return null;
                }
                attach.title = attachTitleNode.InnerText;
                XmlNode? attachCtimeNode = FindNodeNamed(attachNode, "createdTime");
                if (attachCtimeNode == null)
                {
                    Error = $"attachment createdTime node in {filename} is missing.";
                    return null;
                }
                attach.createdTime = Int64.Parse(attachCtimeNode.InnerText);
                XmlNode? attachCbyNode = FindNodeNamed(attachNode, "createdBy");
                if (attachCbyNode == null)
                {
                    Error = $"attachment createdBy node in {filename} is missing.";
                    return null;
                }
                attach.createdBy = attachCbyNode.InnerText;
                XmlNode? attachSizeNode = FindNodeNamed(attachNode, "dataSize");
                if (attachSizeNode == null)
                {
                    Error = $"attachment dataSize node in {filename} is missing.";
                    return null;
                }
                attach.dataSize = Int64.Parse(attachSizeNode.InnerText);
                tc.attachments.Add(attach);
            }

            return tc;
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

        private XmlNode? FindNodeNamed(XmlNode node, string name)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == name)
                {
                    return child;
                }
            }
            Error = $"Node named {node.Name} does not have a child named {name}";
            return null;
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

            // Generate the XML format for this testcaseFromDokimion
            string xml = GenerateXml(testcase, project);
            if (string.IsNullOrEmpty(xml))
            {
                return false;
            }

            // Save the xml of the test case:
            if (false == SaveTestCase(testcaseId, folderPath, xml))
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

        public string GenerateXml(TestCase tc, Project project)
        {
            // Create our top-level node
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement testcase = xmlDoc.CreateElement("TestCase");
            xmlDoc.AppendChild(testcase);

            // Create all the members
            XmlElement id = xmlDoc.CreateElement("id");
            id.InnerText = tc.id;
            testcase.AppendChild(id);

            XmlElement name = xmlDoc.CreateElement("name");
            name.InnerText = tc.name;
            testcase.AppendChild(name);

            XmlElement description = xmlDoc.CreateElement("description");
            description.InnerText = tc.description.Replace("<br>", "<br>\r\n");
            testcase.AppendChild(description);

            XmlElement preconditions = xmlDoc.CreateElement("preconditions");
            if (tc.preconditions == null)
            {
                preconditions.InnerText = "";
            }
            else
            {
                preconditions.InnerText = tc.preconditions.Replace("<br>", "<br>\r\n");
            }
            testcase.AppendChild(preconditions);

            XmlElement automated = xmlDoc.CreateElement("automated");
            automated.InnerText = tc.automated.ToString();
            testcase.AppendChild(automated);

            XmlElement broken = xmlDoc.CreateElement("broken");
            broken.InnerText = tc.broken.ToString();
            testcase.AppendChild(broken);

            XmlElement deleted = xmlDoc.CreateElement("deleted");
            deleted.InnerText = tc.deleted.ToString();
            testcase.AppendChild(deleted);

            XmlElement locked = xmlDoc.CreateElement("locked");
            locked.InnerText = tc.locked.ToString();
            testcase.AppendChild(locked);

            XmlElement launchBroken = xmlDoc.CreateElement("launchBroken");
            launchBroken.InnerText = tc.launchBroken.ToString();
            testcase.AppendChild(launchBroken);

            XmlElement createdTime = xmlDoc.CreateElement("createdTime");
            createdTime.InnerText = tc.createdTime.ToString();
            testcase.AppendChild(createdTime);

            XmlElement lastModifiedTime = xmlDoc.CreateElement("lastModifiedTime");
            lastModifiedTime.InnerText = tc.lastModifiedTime.ToString();
            testcase.AppendChild(lastModifiedTime);

            DateTime timestamp = DateTime.Parse("January 1, 1970");
            timestamp += TimeSpan.FromMilliseconds(tc.lastModifiedTime);
            string humanReadableTime = timestamp.ToString("R");
            XmlElement readableModifiedTime = xmlDoc.CreateElement("readableModifiedTime");
            readableModifiedTime.InnerText = humanReadableTime;
            testcase.AppendChild(readableModifiedTime);

            XmlElement lastModifiedBy = xmlDoc.CreateElement("lastModifiedBy");
            lastModifiedBy.InnerText = tc.lastModifiedBy;
            testcase.AppendChild(lastModifiedBy);

            XmlElement steps = xmlDoc.CreateElement("steps");
            testcase.AppendChild(steps);
            foreach (var step in tc.steps)
            {
                XmlElement stepNode = xmlDoc.CreateElement("step");
                steps.AppendChild(stepNode);
                XmlElement actionNode = xmlDoc.CreateElement("action");
                string action = step.action;
                action = action.Replace("<br>", "<br>\r\n");
                actionNode.InnerText = action;
                stepNode.AppendChild(actionNode);
                XmlElement expectation = xmlDoc.CreateElement("expectation");
                string exp = step.expectation;
                exp = exp.Replace("<br>", "<br>\r\n");
                expectation.InnerText = exp;
                stepNode.AppendChild(expectation);
            }

            XmlElement attachments = xmlDoc.CreateElement("attachments");
            testcase.AppendChild(attachments);
            foreach (var att in tc.attachments)
            {
                XmlElement attNode = xmlDoc.CreateElement("attachment");
                attachments.AppendChild(attNode);
                XmlElement attId = xmlDoc.CreateElement("id");
                attId.InnerText = att.id;
                attNode.AppendChild(attId);
                XmlElement title = xmlDoc.CreateElement("title");
                title.InnerText = att.title;
                attNode.AppendChild(title);
                XmlElement created = xmlDoc.CreateElement("createdTime");
                created.InnerText = att.createdTime.ToString();
                attNode.AppendChild(created);
                XmlElement createdBy = xmlDoc.CreateElement("createdBy");
                createdBy.InnerText = att.createdBy;
                attNode.AppendChild(createdBy);
                XmlElement dataSize = xmlDoc.CreateElement("dataSize");
                dataSize.InnerText = att.dataSize.ToString();
                attNode.AppendChild(dataSize);
            }

            XmlElement attributes = xmlDoc.CreateElement("attributes");
            testcase.AppendChild(attributes);
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
                    XmlElement attNode = xmlDoc.CreateElement(attrName);
                    string s = "";
                    bool first = true;
                    foreach (string value in attr.Value)
                    {
                        string trimmedValue = value.Trim();
                        s += first ? $"\"{trimmedValue}\"" : $", \"{trimmedValue}\"";
                        first = false;
                    }
                    attNode.InnerText = s;
                    attributes.AppendChild(attNode);
                }
            }

            // Generate a string to return from the Xml Document
            MemoryStream stream = new MemoryStream();
            xmlDoc.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = stream.ToArray();
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(bytes, 0, bytes.Length);
        }

        private bool SaveTestCase(string testcaseId, string folderPath, string xml)
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
            string path = Path.Combine(folderPath, testcaseId + ".xml");
            try
            {
                File.WriteAllText(path, xml);
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
            // Assume folder exists from saving the XML file.
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
                if (testcaseFromDokimion.steps[i].action != extracted.steps[i].action) return true;
                if (testcaseFromDokimion.steps[i].expectation != extracted.steps[i].expectation) return true;
            }
            if (testcaseFromDokimion.attributes.Count != extracted.attributes.Count) return true;
            // We know they have the same number of key/value pairs
            foreach (string key in extracted.attributes.Keys)
            {
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
            string filename = Path.Combine(folderPath, testcaseId + ".xml");
            UploadStatus resp = UploadXmlFileToProjectIfDifferent(filename, project, out TestCaseForUpload? uploaded);
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

        public UploadStatus UploadXmlFileToProjectIfDifferent(string path, Project project, out TestCaseForUpload? uploaded)
        {
            uploaded = null;
            // Read text from XML file.
            string xmlText = "";
            try
            {
                xmlText = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return UploadStatus.Error;
            }

            // Create XmlDocument.
            XmlDocument? xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xmlText);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return UploadStatus.Error;
            }

            // Extract info from XmlDocument to an instance of TestCase.
            TestCaseForUpload? extracted = XmlToObject(xmlDoc, path, project);
            if (extracted == null)
            {
                Error = $"Cannot convert XML file {path} to a test case object";
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
