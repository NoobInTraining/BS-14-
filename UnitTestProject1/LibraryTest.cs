using System;
using BS14Library;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class LibraryTest
    {
        string html = "<CompilactedHTML><div class=\"first\"><div class=\"seconds\"><table><tablebody><div class=\"third\" length=\"3-42\" height=\"3-42\">"+
                       "You should only read this text!</div><div class=\"fourth\" length=\"4\" height=\"4\">This is the second text to read!</div></table>my proterties</table></div></div></CompilactedHTML>";    

        [TestMethod]
        [TestCategory("HTML Parser")]
        public void TestHTMLParserSimple()
        {
            string html = "<CompilactedHTML><someValue>Value</someValue></CompilactedHTML>";
            var s = BS14Library.HtmlParsing.GetElementValue(html, "someValue");
            Assert.AreEqual("Value", s);
        }

        [TestMethod]
        [TestCategory("HTML Parser")]
        public void TestHTMLParserSimple2()
        {
            string html = "<CompilactedHTML><someValue>Value</someValue></CompilactedHTML>";
            var s = BS14Library.HtmlParsing.GetElementValue(html, "CompilactedHTML");
            Assert.AreEqual("<someValue>Value</someValue>", s);
        }

        [TestMethod]
        [TestCategory("HTML Parser")]
        public void HtmlParseOptions()
        {
            string expected = "This is the second text to read!";
            var s = BS14Library.HtmlParsing.GetElementValue(html, "div", HtmlParsingOptions.UnsureAttributeValues, "class", "length", "height");
        }

        [TestMethod]
        [Ignore]
        public void debugMethod()
        {
            string html = "<CompilactedHTML><div class=\"first\"><div class=\"seconds\"><table><tablebody><div class=\"third\" length=\"3-42\" height=\"3-42\">You should only read this text!</div><div class=\"fourth\" length=\"4\" height=\"4\">This is the second text to read!</div></table>my proterties</table></div></div></CompilactedHTML>";
            string toAdd = "[\\w\\d\\s.:|<>\\-_,;#'*+~`´?ß\\\\})(/{&%$\"!°^]+";
            string regex = "<div ((class=\"" + toAdd + " *)|(height=\"" + toAdd + "\" *)|(length=\"" + toAdd + "\" *)){3}>";
            var matches = Regex.Matches(html, regex);
            

            Debug.WriteLine("----------------------------------------------");
            Debug.WriteLine("");
            Debug.WriteLine("");

            int i = 1;
            foreach (Match item in matches)
            {
                Debug.WriteLine("\t" + i + ": " + item.Value);
                
                i++;
            }

            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("----------------------------------------------");

            System.Diagnostics.Debugger.Break();
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("HtmlParsingOptions")]
        public void UnsureAttributes()
        {
            var t = HtmlParsing.GetElementValue(html, "div", HtmlParsingOptions.UnsureAttributeValues, "class", "length", "height");
            bool b = false, c = false;
            b = (t[0].Element == "div") && (t[0].Attributes["class"] == "third") && (t[0].Attributes["length"] == "3-42") && (t[0].Attributes["height"] == "3-42");
            c = (t[1].Element == "div") && (t[1].Attributes["class"] == "fourth") && (t[1].Attributes["length"] == "4") && (t[1].Attributes["height"] == "4");
            Assert.IsTrue(b && c);
        }

        [TestMethod]
        [TestCategory("HtmlParsingOptions")]
        public void GetAllQuestionsFromFile()
        {
            string html = System.IO.File.ReadAllText(@"C:\Users\miles.sasportas\Desktop\testHtmlQuestions.htm");
            var t = HtmlParsing.GetElementValue(html, "div", HtmlParsingOptions.UnsureAttributeValues, "id", "class");
            Assert.AreEqual(16, t.Length);
        }

        [TestMethod]
        [TestCategory("HtmlParsingOptions")]
        [Ignore]
        public void UnsureAttributesWithFileAsHtml()
        {
            string html = System.IO.File.ReadAllText(@"C:\Users\miles.sasportas\Desktop\TestHTML.htm");
            var t = HtmlParsing.GetElementValue(html, "form", HtmlParsingOptions.UnsureAttributeValues, "id", "class", "action", "method", "autocomplete");
            Assert.IsFalse(false);
        }
    }
}
