﻿using Newtonsoft.Json;
using System.Text;
using System.Xml;

namespace Updater
{
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

    public class Step
    {
        public string action = "";
        public string expectation = "";
    }

    public class Attachment
    {
        public string id = "";
        public string title = "";
        public Int64 createdTime;
        public Int64 dataSize;
    }

    public class TestCase
    {
        public string id = "";
        public string name = "";
        public string description = "";
        public string preconditions = "";
        public List<Step> steps = new List<Step>();
        public List<Attachment> attachments = new List<Attachment>();
        public string createdBy = "";
        public bool automated;
        public bool broken;
        public bool deleted;
        public bool launchBroken;
        public bool locked;
        public Int64 createdTime;
        public Int64 lastModifiedTime;
        public string lastModifiedBy = "";
        public Dictionary<string, string[]> attributes = new Dictionary<string, string[]>() ;
    }

    public class TestCaseForUpload
    {
        public string id = "";
        public string name = "";
        public string description = "";
        public string preconditions = "";
        public List<Step> steps = new List<Step>();
        public bool automated;
        public bool broken;
        public bool deleted;
        public bool launchBroken;
        public bool locked;
        public Int64 lastModifiedTime;
        public Dictionary<string, string[]> attributes = new Dictionary<string, string[]>();
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
        public Int64 createdTime;
        public Int64 lastModifiedTime;
        public Dictionary<string, string[]> attributes = new Dictionary<string, string[]>();

        public string Name
        {
            get { return name; }
        }

    }

    internal class User
    {
        public string id = "";
    }

    public class TestCaseTreeResponse
    {
        public List<TestCaseShort> testCases = new List<TestCaseShort>();
    }

    public class Attribute
    {
        public string id = "";
        public string name = "";
    }


    internal class Dokimion
    {
        private HttpClient m_Client = new HttpClient();
        private string m_Url = "";
        private string m_UserId = "";
        public string Error = "";
        private const string SPACE_REPLACER = "_";

        private string BaseDokimionApiUrl()
        {
            return $"http://{m_Url}/api";
        }

        private Dokimion()
        {
            m_Client = new HttpClient();
        }

        public Dokimion(string server) : this()
        {
            m_Url = server;
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
                    m_UserId = user.id;
                    m_Client.DefaultRequestHeaders.Add("whoruSessionId", m_UserId);
                    results = true;
                }
                else
                {
                    Error = "Cannot decode " + json;
                }
            }
            else
            {
                Error = "Server returned error to login request: " + resp.ReasonPhrase + ":\r\n" + json;
            }
            return results;
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

        private TestCase? GetTestCaseAsObject(string url)
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
                for (int val=0; val < attr.Value.Count(); val++)
                {
                    attr.Value[val] = attr.Value[val].Trim();
                }
            }
            return testcase;
        }

        public bool DownloadTestcase(string testcaseId, Project project, string folderPath)
        {
            string? xml = GetTestCaseAsXml(testcaseId, project);
            if (string.IsNullOrEmpty(xml))
            {
                return false;
            }

            return SaveTestCase(testcaseId, folderPath, xml);
        }

        private string? GetTestCaseAsXml(string testcaseId, Project project)
        {
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase/" + testcaseId;

            // Get the test case from Dokimion
            TestCase? testcase = GetTestCaseAsObject(url);
            if (testcase == null)
            {
                return null;
            }

            // Generate the XML format for this testcaseFromDokimion
            string xml = GenerateXml(testcase, project);
            return xml;
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

        private string GenerateXml(TestCase tc, Project project)
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

        public bool UploadFileToProject(string filename, Project project, out bool changed)
        {
            changed = false;

            // Read text from XML file.
            string xmlText = "";
            try
            {
                xmlText = File.ReadAllText(filename);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
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
                return false;
            }

            // Extract info from XmlDocument to an instance of TestCase.
            TestCaseForUpload? extracted = XmlToObject(xmlDoc, filename, project);
            if (extracted == null)
            {
                return false;
            }

            // Download the TestCase from Dokimion
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase/" + extracted.id;
            TestCase? testcaseFromDokimion = GetTestCaseAsObject(url);
            if (testcaseFromDokimion == null)
            {
                return false;
            }

            // Compare selected contents of the tc TestCase to the TestCase read from Dokimion.
            changed = IsTestCaseChanged(testcaseFromDokimion, extracted);

            // If different, write tc TestCase to server.
            if (changed)
            {
                DialogResult answer = UploadTestCase(extracted, project);
                switch (answer)
                {
                    case DialogResult.TryAgain:
                        extracted.lastModifiedTime = testcaseFromDokimion.lastModifiedTime;
                        answer = UploadTestCase(extracted, project);
                        if (answer != DialogResult.OK)
                        {
                            return false;
                        }
                        break;
                    case DialogResult.Cancel:
                        return false;
                    case DialogResult.OK:
                        break;
                    case DialogResult.Continue:
                        changed = false;
                        break;
                }
                // If we successfully uploaded the test case ...
                if (answer == DialogResult.OK)
                {
                    // ... download the test case again so that we get the latest timestamp
                    FileInfo? fi = new FileInfo(filename);
                    string? folderPath = fi.DirectoryName;
                    if (folderPath == null)
                    {
                        Error = "Cannot get folder name from " + filename;
                        return false;
                    }
                    if (false == DownloadTestcase(extracted.id, project, folderPath))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private DialogResult UploadTestCase(TestCaseForUpload tc, Project project)
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
                    var answer = MessageBox.Show("The testcase on the server has changed since the file in the repo was created.\r\n" +
                        "Do you wish to upload anyway, overwriting changes on the server? (Try Again)\r\n" + 
                        "Or skip uploading this file and continue with other files? (Continue)\r\n" +
                        "Or quit uploading files? (Cancel)", 
                        "Conflict Detected", MessageBoxButtons.CancelTryContinue);
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
                return null;
            }
            tc.id = idNode.InnerText;

            XmlNode? nameNode = FindNodeNamed(overall, "name");
            if (nameNode == null)
            {
                return null;
            }
            tc.name = nameNode.InnerText;

            XmlNode? descNode = FindNodeNamed(overall, "description");
            if (descNode == null)
            {
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
                return null;
            }
            tc.deleted = deletedNode.InnerText.ToLower() == "true";

            XmlNode? lockedNode = FindNodeNamed(overall, "locked");
            if (lockedNode == null)
            {
                return null;
            }
            tc.locked = lockedNode.InnerText.ToLower() == "true";

            XmlNode? launchBrokenNode = FindNodeNamed(overall, "launchBroken");
            if (launchBrokenNode == null)
            {
                return null;
            }
            tc.launchBroken = launchBrokenNode.InnerText.ToLower() == "true";

            XmlNode? modifiedNode = FindNodeNamed(overall, "lastModifiedTime");
            if (modifiedNode == null)
            {
                return null;
            }
            Int64 time = Int64.Parse(modifiedNode.InnerText);
            tc.lastModifiedTime = time;

            XmlNode? stepsNode = FindNodeNamed(overall, "steps");
            if (stepsNode == null)
            {
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
                return null;
            }
            tc.attributes = new Dictionary<string, string[]>();
            foreach (XmlNode attrNode in attributesNode.ChildNodes)
            {
                string key = AttributeKeyForName(project, attrNode.Name);
                string[] values = attrNode.InnerText.Split(new char[] { ',' });
                for (int i = 0; i < values.Length; i++)
                {
                    string value = values[i].Replace("\"", "");
                    value = value.Trim();
                    values[i] = value;
                }
                tc.attributes.Add(key, values);
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

        private bool IsTestCaseChanged(TestCase testcaseFromDokimion, TestCaseForUpload extracted)
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
                    string attrvalue = testcaseFromDokimion.attributes[key][i];
                    attrvalue = attrvalue.Trim();
                    if (attrvalue != extracted.attributes[key][i]) return true;
                }
            }
            return false;
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

        private string AttributeKeyForName(Project project, string name)
        {
            foreach(var att in project.attributes)
            {
                if (att.Value == name)
                    return att.Key;
            }
            return "";
        }
    }
}
