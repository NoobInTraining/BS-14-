using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class LibraryTest
    {
        [TestMethod]
        public void TestHTMLParser()
        {
            string html = "<CompilactedHTML><div class=\"first\"><div class=\"seconds\"><table><tablebody><div class=\"third\" length=\"42\" height=\"42\">You should only read this text!</div></table>my proterties</table></div></div></CompilactedHTML>";
            var s = BS14Library.HtmlParsing.GetElementValue(html, "div", "class=\"first\"");
            Assert.AreEqual("<div class=\"seconds\"><table><tablebody><div class=\"third\" length=\"42\" height=\"42\">You should only read this text!</div></table>my proterties</table></div>", s);
        }
    }
}
