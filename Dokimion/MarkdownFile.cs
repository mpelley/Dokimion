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
    public class MarkdownFile
    {
        public string Error = "";

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


        public TestCaseForUpload? MarkdownToObject(MarkdownDocument markdownDoc, string filename, Project project)
        {
            TestCaseForUpload tc = new TestCaseForUpload();
            Error = "";

            int index = 0;
            Console.WriteLine(filename);
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
                                if (false == GetMarkdownAttachments(markdownDoc, ref index, tc))
                                {
                                    return null;
                                }
                                break;
                            default:
                                // Handle ID and Name...
                                if (text.Substring(0, 2).ToLower() == "id")
                                {
                                    tc.id = text.Substring(2).Trim();
                                    if (tc.id == "2")
                                        ;
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
            string inlineText = "";
            foreach (Inline? inline in container)
            {
                if (inline != null)
                {
                    string? text = GetItemText(inline);
                    if (text == null)
                    {
                        return null;
                    }
                    inlineText += text;
                }
                else
                {
                    ;
                }
            }
            return inlineText;
        }

        string? GetItemText(MarkdownObject item)
        {
            string itemText = "";
            switch (item)
            {
                case LiteralInline literal:
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
                case HtmlInline html:
                    itemText += html.Tag;
                    break;
                case EmphasisInline emphasis:
                    for (int i = 0; i < emphasis.DelimiterCount; i++)
                    {
                        itemText += emphasis.DelimiterChar;
                    }
                    if (emphasis.FirstChild != null)
                    {
                        itemText += GetItemText(emphasis.FirstChild);
                    }
                    for (int i = 0; i < emphasis.DelimiterCount; i++)
                    {
                        itemText += emphasis.DelimiterChar;
                    }
                    break;
                case CodeInline code:
                    for (int i = 0; i < code.DelimiterCount; i++)
                    {
                        itemText += code.Delimiter;
                    }
                    itemText += code.Content;
                    for (int i = 0; i < code.DelimiterCount; i++)
                    {
                        itemText += code.Delimiter;
                    }
                    break;
                default:
                    string errorText = item.ToPositionText();
                    Error += $"Unexpected item type at line {item.Line}: {errorText}.";
                    return null;
            }
            return itemText;
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
                    case HtmlBlock html:
                        if (false == firstBlock)
                        {
                            text += "\r\n\r\n";
                        }
                        foreach (StringLine line in html.Lines)
                        {
                            text += line.Slice.ToString();
                            text += "\r\n";
                        }
                        firstBlock = false;
                        break;
                    case ParagraphBlock paragraph:
                        if (false == firstBlock)
                        {
                            text += "\r\n\r\n";
                        }
                        if (paragraph.Inline != null)
                        {
                            foreach (var inline in paragraph.Inline)
                            {
                                string? inlineText = GetItemText(inline);
                                if (inlineText == null)
                                {
                                    return null;
                                }
                                text += inlineText;
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
                    case FencedCodeBlock code:
                        foreach (StringLine line in code.Lines)
                        {
                            text += line.Slice.ToString();
                            text += "\r\n";
                        }

                        break;
                    default:
                        break;
                }
                index++;
            }
            if (text.Length > 3)
            {
                if (text.Substring(text.Length - 2, 2) == "\r\n")
                {
                    text = text.Substring(0, text.Length - 2);
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
                Error += $"Was expecting an Attribute list at line {block.Line}";
                return null;
            }

            Dictionary<string, string[]> attributes = new Dictionary<string, string[]>();
            Dictionary<string, string>? items = GetMarkdownDictionary(block);
            if (items == null)
            {
                return null;
            }

            foreach (var item in items)
            {
                string[] values = new string[0];
                if (item.Value != "")
                {
                    values = item.Value.Split(",");
                }
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
                string? text = GetItemText(child);
                if (text == null)
                {
                    return null;
                }
                itemText += text;
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
                if (item.Order == 0)
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
            if (action == null)
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


        private bool GetMarkdownAttachments(MarkdownDocument markdownDoc, ref int index, TestCaseForUpload tc)
        {
            tc.attachments = new List<Attachment>();
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
                    Error += $"Expect a Header 2 for Attachment under Attachments at line {block.Line}";
                    return false;
                }

                if (heading.Inline == null)
                {
                    Error += $"Expect a Header 2 for Attachment under Attachments at line {block.Line}";
                    return false;
                }

                string? text = GetInlineText(heading.Inline);
                if ((string.IsNullOrEmpty(text)) || (text.ToLower() != "attachment"))
                {
                    Error += $"Expect a Header 2 for Attachment under Attachments at line {block.Line}";
                    return false;
                }

                index++;
                if (false == GetMarkdownAttachment(markdownDoc[index], tc))
                {
                    return false;
                }
                index++;
            }
            return true;
        }

        private bool GetMarkdownAttachment(Block block, TestCaseForUpload tc)
        {
            if (block is not ListBlock)
            {
                Error += $"Expect only a list in Attachment at line {block.Line}.";
                return false;
            }

            Dictionary<string, string>? items = GetMarkdownDictionary(block);
            if (items == null)
            {
                return false;
            }

            Attachment attachment = new();
            foreach (var item in items)
            {
                switch (item.Key.ToLower())
                {
                    case "id":
                        attachment.id = item.Value;
                        break;
                    case "title":
                        attachment.title = item.Value;
                        break;
                    case "createdby":
                        attachment.createdBy = item.Value;
                        break;
                    case "createdtime":
                        try
                        {
                            attachment.createdTime = long.Parse(item.Value);
                        }
                        catch
                        {
                            Error += $"Cannot parse {item.Value} as an integer for attachment's createdTime";
                        }
                        break;
                    case "datasize":
                        try
                        {
                            attachment.dataSize = long.Parse(item.Value);
                        }
                        catch
                        {
                            Error += $"Cannot parse {item.Value} as an integer for attachment's dataSize";
                        }
                        break;
                    default:
                        break;
                }
            }
            tc.attachments.Add(attachment);
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

        private string AttributeKeyForName(Project project, string name)
        {
            foreach (var att in project.attributeNameForKey)
            {
                if (att.Value == name)
                    return att.Key;
            }
            return "";
        }

        public string GenerateMarkdown(TestCase tc, Project project)
        {
            string md = "";
            md += $"# ID {tc.id}\r\n\r\n";
            md += $"# Name {tc.name}\r\n\r\n";

            if (false == string.IsNullOrEmpty(tc.description))
            {
                md += $"# Description\r\n\r\n";
                md += "```html\r\n";
                md += $"{Dokimion.ImproveBreaks(tc.description)}\r\n";
                md += "```\r\n\r\n";
            }

            if (false == string.IsNullOrEmpty(tc.preconditions))
            {
                md += $"# Preconditions\r\n\r\n";
                md += "```html\r\n";
                md += $"{Dokimion.ImproveBreaks(tc.preconditions)}\r\n";
                md += "```\r\n\r\n";
            }

            if (tc.steps.Count > 0)
            {
                md += $"# Steps\r\n\r\n";
                foreach (Step step in tc.steps)
                {
                    md += $"## Step\r\n\r\n";
                    md += $"### Action\r\n\r\n";
                    if (false == string.IsNullOrEmpty(step.action))
                    {
                        md += "```html\r\n";
                        md += $"{Dokimion.ImproveBreaks(step.action)}\r\n";
                        md += "```\r\n\r\n";
                    }
                    md += $"### Expectation\r\n\r\n";
                    if (false == string.IsNullOrEmpty(step.expectation))
                    {
                        md += "```html\r\n";
                        md += $"{Dokimion.ImproveBreaks(step.expectation)}\r\n";
                        md += "```\r\n\r\n";
                    }
                }
            }

            if (tc.attributes.Count > 0)
            {
                md += $"# Attributes\r\n\r\n";
                md += GenerateAttributesMarkdown(tc, project);
            }

            if (tc.attachments.Count > 0)
            {
                md += $"# Attachments\r\n\r\n";
                md += GenerateAttachmentsMarkdown(tc);
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

        private string GenerateAttachmentsMarkdown(TestCase tc)
        {
            string text = "";
            foreach (Attachment attachment in tc.attachments)
            {
                text += $"## Attachment\r\n\r\n";
                text += $"* id: {attachment.id}\r\n";
                text += $"* title: {attachment.title}\r\n";
                text += $"* createdTime: {attachment.createdTime}\r\n";
                text += $"* createdBy: {attachment.createdBy}\r\n";
                text += $"* dataSize: {attachment.dataSize}\r\n\r\n";
            }
            return text;
        }

        private string GenerateAttributesMarkdown(TestCase tc, Project project)
        {
            // First generate a copy of the test case attributes
            // with a human name instead of the magic number.
            Dictionary<string, string> attrDictWithNames = new();

            foreach (var attr in tc.attributes)
            {
                string? attrName = null;
                try
                {
                    attrName = project.attributeNameForKey[attr.Key];
                }
                catch
                {
                    Error += $"Test case {tc.id} has a garbage attribute.\r\n";
                }
                if (false == string.IsNullOrEmpty(attrName))
                {
                    attrName = attrName.Replace(" ", Dokimion.SPACE_REPLACER);
                    string s = "";
                    bool first = true;
                    foreach (string value in attr.Value)
                    {
                        string trimmedValue = value.Trim();
                        s += first ? $"{trimmedValue}" : $", {trimmedValue}";
                        first = false;
                    }
                    attrDictWithNames.Add(attrName, s);
                }
            }

            // Sort the keys so they always are in the same order in the md file
            // to make comparing easier.
            List<string> keys = attrDictWithNames.Keys.ToList();
            keys.Sort();

            // Generate the markdown text.
            string text = "";
            foreach (var key in keys)
            {
                string value = "";
                try
                {
                    value = attrDictWithNames[key];
                }
                catch
                {
                    Error = $"Cannot get value for {key}";
                }
                text += $"* {key}: {value}\r\n";
            }
            text += $"\r\n";

            return text;
        }

    }
}
