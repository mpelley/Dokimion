using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;

namespace Cloner
{
    public class Dokimion
    {
        private HttpClient m_Client = new HttpClient();
        private string m_Url = "";
        private string m_UserId = "";
        private bool m_UseHttps = false;
        private const string SPACE_REPLACER = "_";

        public string Error = "";


        private string BaseDokimionApiUrl()
        {
            string protocol = m_UseHttps ? "https" : "http";
            return $"{protocol}://{m_Url}/api";
        }

        private Dokimion()
        {
            m_Client = new HttpClient();
        }

        public Dokimion(string server, bool useHttps) : this()
        {
            m_Url = server;
            m_UseHttps = useHttps;
        }

        static public string CleanString(string str)
        {
            str = str.Replace("\r\n", "<br>");
            str = str.Replace("\r", "<br>");
            str = str.Replace("\n", "<br>");
            return str;
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

        public List<Attribute> GetAttributesForProject(string projectId)
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
                    return new List<Attribute>();
                }
            }
            else
            {
                Error = "Server returned error: " + resp.ReasonPhrase + "\r\n" + json;
                return new List<Attribute>();
            }

            return attrList;
        }

        public string GetAttributeIdForName(Project project, string name)
        {
            string id = "";
            foreach (var attr in project.attributes)
            {
                if (name == attr.name)
                {
                    id = attr.id;
                    break;
                }
            }
            return id;
        }

        /// <summary>
        /// Return a list of test cases from the supplied project that have the supplied attributes.
        /// Different attribute names are ANDed together by Dokimion.
        /// Different attribute values with the same attribute name are ORed together by Dokimion.
        /// The filtering is done by Dokimion the same way as it is for the Dokimion UI.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="attributes">An array of strings.  Each string is of the form "AttributeID=AttributeValue" with only one value.</param>
        /// <returns></returns>
        public List<BriefTestCase>? GetTestCasesForAttributes(Project project, string[] attributes)
        {
            List<BriefTestCase> testCaseList = new();
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase/tree";
            bool first = true;
            foreach (var attr in attributes)
            {
                url += first ? "?" : "&";
                first = false;
                url += "attributes." + attr;
            }

            var resp = m_Client.GetAsync(url).Result;
            string json = resp.Content.ReadAsStringAsync().Result;

            BriefTestCaseResponse? testcaseResponse = null;
            if (resp.IsSuccessStatusCode)
            {
                try
                {
                    testcaseResponse = JsonConvert.DeserializeObject<BriefTestCaseResponse>(json);
                }
                catch
                {
                    ;
                }
                if (testcaseResponse == null)
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

            foreach (var testcase in testcaseResponse.testCases)
            {
                testCaseList.Add(testcase);
            }

            return testCaseList;
        }

        public BriefTestCase? GetBriefTestCaseForId(Project project, string id)
        {
            BriefTestCase tc = null;
            string json = GetTestCaseForId(project, id);
            if (json == null)
            {
                return null;
            }

            try
            {
                tc = JsonConvert.DeserializeObject<BriefTestCase>(json);
            }
            catch
            {
                Error = "Cannot decode: \r\n" + json;
                return null;
            }

            return tc;
        }

        public TestCaseForUpload? GetTestCaseForUpload(Project project, string id)
        {
            TestCaseForUpload tc = null;
            string json = GetTestCaseForId(project, id);
            if (json == null)
            {
                return null;
            }

            try
            {
                tc = JsonConvert.DeserializeObject<TestCaseForUpload>(json);
            }
            catch
            {
                Error = "Cannot decode: \r\n" + json;
                return null;
            }
            return tc;
        }

        public string? GetTestCaseForId(Project project, string id)
        {
            string url = BaseDokimionApiUrl() + "/" + project.id + "/testcase/" + id;
            return GetContent(url, "Testcase", id);
        }

        public TestSuite[]? GetTestSuites(Project project)
        {
            TestSuite[]? suites = null;
            string url = BaseDokimionApiUrl() + $"/{project.id}/testsuite";
            string? content = GetContent(url, "Test Suite", "");
            if (content != null)
            {
                try
                {
                    suites = JsonConvert.DeserializeObject<TestSuite[]>(content);
                }
                catch
                {
                    Error = "Cannot decode: \r\n" + content;
                    suites = null;
                }
            }

            return suites;
        }

        private string? GetContent(string url, string elementType, string elementName)
        {
            string? content = null;

            try
            {
                var resp = m_Client.GetAsync(url).Result;
                content = resp.Content.ReadAsStringAsync().Result;
                if (false == resp.IsSuccessStatusCode)
                {
                    Error = $"Server returned {resp.ReasonPhrase} when getting {elementType} {elementName}.\r\n";
                    Error += content;
                    return null;
                }
            }
            catch (Exception ex)
            {
                Error = $"Exception thrown while getting {elementType} {elementName}\r\n" + ex.Message;
                if (ex.InnerException != null)
                {
                    Error += ex.InnerException.Message;
                }
            }

            return content;
        }

        public bool CreateProject(Project project)
        {
            string url = BaseDokimionApiUrl() + "/project";
            project.description = CleanString(project.description);
            string json = JsonConvert.SerializeObject(project);
            return PostContent(url, json, "Project", project.name);
        }

        public bool CreateAttribute(Project project, Attribute attr)
        {
            string url = BaseDokimionApiUrl() + $"/{project.id}/attribute";
            string json = JsonConvert.SerializeObject(attr);
            return PostContent(url, json, "Attribute", attr.name);
        }

        public bool CreateTestCase(Project project, TestCaseForUpload tc)
        {
            string url = BaseDokimionApiUrl() + $"/{project.id}/testcase";
            string json = JsonConvert.SerializeObject(tc);
            return PostContent(url, json, "Test case", tc.name);
        }

        public bool ModifyTestCase(Project project, TestCaseForUpload tc)
        {
            string url = BaseDokimionApiUrl() + $"/{project.id}/testcase";
            string json = JsonConvert.SerializeObject(tc);
            return PostContent(url, json, "Test case", tc.id);
        }

        public bool CreateTestSuite(Project project, TestSuite suite)
        {
            string url = BaseDokimionApiUrl() + $"/{project.id}/testsuite";
            string json = JsonConvert.SerializeObject(suite);
            return PostContent(url, json, "Test Suite", suite.name);
        }

        private bool PostContent(string url, string json, string elementType, string elementName)
        {
            bool success = true;
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var resp = m_Client.PostAsync(url, content).Result;
                if (false == resp.IsSuccessStatusCode)
                {
                    string problem = resp.Content.ReadAsStringAsync().Result;
                    Error = $"Server returned {resp.ReasonPhrase} when creating {elementType} {elementName}\r\n";
                    Error += problem;
                    success = false;
                }
            }
            catch (Exception ex)
            {
                Error = $"Exception thrown while creating {elementType} {elementName}\r\n" + ex.Message;
                if (ex.InnerException != null)
                {
                    Error += ex.InnerException.Message;
                }
                success = false;
            }
            return success;
        }

        private bool PutContent(string url, string json, string elementType, string elementName)
        {
            bool success = true;
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var resp = m_Client.PutAsync(url, content).Result;
                if (false == resp.IsSuccessStatusCode)
                {
                    string problem = resp.Content.ReadAsStringAsync().Result;
                    Error = $"Server returned {resp.ReasonPhrase} when modifying {elementType} {elementName}\r\n";
                    Error += problem;
                    success = false;
                }
            }
            catch (Exception ex)
            {
                Error = $"Exception thrown while modifying {elementType} {elementName}\r\n" + ex.Message;
                if (ex.InnerException != null)
                {
                    Error += ex.InnerException.Message;
                }
                success = false;
            }
            return success;
        }

    }
}
