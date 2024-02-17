using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Cloner
{
    internal class Settings
    {
        public string sourceusername = "";
        public string destusername = "";
        public bool sourceusehttps;
        public bool destusehttps;
        public string sourceaddress = "";
        public string destaddress = "";
    }

    public class Project
    {
        public string description = "";
        public string[] readWriteUsers = { "" };
        public string id = "";
        public string name = "";
        public bool deleted;
        public List<Attribute> attributes = new List<Attribute>();

        public Project()
        {
        }

        public Project(Project p)
        {
            description = p.description;
            readWriteUsers = (string[])p.readWriteUsers.Clone();
            id = p.id;
            name = p.name;
            deleted = p.deleted;
            attributes = new(p.attributes);
        }

    }

    internal class User
    {
        public string id = "";
    }

    public class AttrValue
    {
        public string value;

        public AttrValue()
        {
        }

        public AttrValue(AttrValue attr)
        {
            value = attr.value;
        }
    }

    public class Attribute
    {
        public string id = "";
        public string name = "";
        public List<AttrValue> attrValues;

        public Attribute()
        {
        }

        public Attribute(Attribute attr)
        {
            id = attr.id;
            name = attr.name;
            attrValues = new List<AttrValue>();
            foreach (AttrValue attrValue in attr.attrValues)
            {
                attrValues.Add(attrValue);
            }
        }
    }

    public class Step
    {
        public string action = "";
        public string expectation = "";
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

    public class BriefTestCase
    {
        public string id = "";
        public string name = "";
    }

    public class BriefTestCaseResponse
    {
        public BriefTestCase[] testCases;
    }

    public class TestSuiteAttribute
    {
        public string name = "";
        public string id = "";
        public List<AttrValue> attrValues = new List<AttrValue>();
    }

    public class TestSuiteFilter
    {
        public List<TestSuiteAttribute> filters = new List<TestSuiteAttribute>();
    }

    public class TestSuite
    {
        public string name;
        public TestSuiteFilter filter;
    }


}