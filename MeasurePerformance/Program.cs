using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;

namespace MeasurePerformance
{
    public class Step
    {
        public string action;
        public string expectation;
    }

    public class Attachment
    {
        public string id;
        public string title;
        public Int64 createdTime;
        public Int64 dataSize;
    }

    public class TestCase
    {
        public string id;
        public string name;
        public string description;
        public Step[] steps;
        public Attachment[] attachments;
        public string createdBy;
        public bool automated;
        public bool broken;
        public bool deleted;
        public Int64 createdTime;
        public Int64 lastModifiedTime;
        public Dictionary<string, string[]> attributes;
    }

    internal class Program
    {
        public static HttpClient m_Client;

        private static string BaseDokimionApiUrl()
        {
            //return "http://testing.languagetechnology.org/api";
            return "http://192.168.56.101/api";
        }



        static void Main(string[] args)
        {
            Initialize();
            //SetPassword();
            //AddUser();
            //MakeProject();
            //GetProjects();
            GetTestCases("104036963");
        }

        public static void Initialize()
        {
                m_Client = new HttpClient();
                m_Client.DefaultRequestHeaders.Add("Whoru-Api-Token", "abc");
        }

        public static string GetProjects()
        {
            string url = BaseDokimionApiUrl() + "/project";
            string json = m_Client.GetStringAsync(url).Result;

            return json;
        }

        public static string GetUsers()
        {
            string url = BaseDokimionApiUrl() + "/user";
            string json = m_Client.GetStringAsync(url).Result;

            return json;
        }

        public static void AddUser()
        {
            string url = BaseDokimionApiUrl() + "/user/mike";
            string project = "{" +
                "\"firstname\": \"Mike\"," +
                "\"lastname\": \"Pelley\"," +
                "\"email\": \"mikepelley@sbcglobal.net\"," +
                "\"password\": \"mikepass\"," +
                "\"passwordChangeRequired\": false" +
                "}";

            var content = new StringContent(project, Encoding.UTF8, "application/json");

            var resp = m_Client.PostAsync(url, content).Result;

            string reason = resp.Content.ReadAsStringAsync().Result;

        }
        public static void SetPassword()
        {
            string url = BaseDokimionApiUrl() + "/user/admin";
            string project = "{" +
                "\"password\": \"adminpass\"," +
                "\"passwordChangeRequired\": false" +
                "}";

            var content = new StringContent(project, Encoding.UTF8, "application/json");

            var resp = m_Client.PutAsync(url, content).Result;

            string reason = resp.Content.ReadAsStringAsync().Result;

        }

        public static void MakeProject()
        {
            string url = BaseDokimionApiUrl() + "/project";
            string project = "{" +
                "\"id\": \"mike_test\"," +
                "\"description\": \"Mike's test project\"," +
                "\"readWriteUsers\": [\"michael_pelley@sil.org\"]," +
                "\"name\": \"Mike_Test\"" +
                "}";

            var content = new StringContent(project, Encoding.UTF8, "application/json");

            var resp = m_Client.PostAsync(url, content).Result;

            string reason = resp.Content.ReadAsStringAsync().Result;

        }

        public static void MakeAdmin()
        {
            string url = BaseDokimionApiUrl() + "/videotutorial/user/michael_pelley@sil.org";
            string project = "{" +
                "\"description\": \"Mike's test project\"," +
                "\"readWriteUsers\": [\"michael_pelley@sil.org\"]," +
                "\"name\": \"Mike_Test\"," +
                "}";

            JsonContent content = JsonContent.Create(project, typeof(string));

            var resp = m_Client.PostAsync(url, content).Result;
            string reason = resp.Content.ReadAsStringAsync().Result;

        }

        public static List<TestCase> GetTestCases(string project)
        {
            string url = BaseDokimionApiUrl() + "/" + project + "/testcase/count";
            string json = m_Client.GetStringAsync(url).Result;

            List<TestCase> cases = new List<TestCase>();
            int count = int.Parse(json);
            long total = 0;
            long max = 0;
            for (int i = 0; i < count; i++)
            {
                Console.Write($"\r{i}");
                long start = DateTime.Now.Ticks;
                url = BaseDokimionApiUrl() + "/" + project + $"/testcase?limit=1&skip={i}";
                json = m_Client.GetStringAsync(url).Result;
                TestCase[]? testCase = JsonConvert.DeserializeObject<TestCase[]>(json);
                if (testCase != null && testCase.Length == 1)
                {
                    cases.Add(testCase[0]);
                    File.WriteAllText($"{project}_TestCase_{testCase[0].id}.json", json);
                }
                else
                {
                    ;
                }
                long duration = DateTime.Now.Ticks - start;
                total += duration;
                max = Math.Max(max, duration);
            }
            Console.WriteLine($"Average: {total/count/10000} milliseconds");
            Console.WriteLine($"Maximum: {max / 10000} milliseconds");
            return cases;
        }
    }
}