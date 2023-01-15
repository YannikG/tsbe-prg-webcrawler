using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Utilities;

namespace YannikG.TSBE.Webcrawler.Core.Tests.Utilities
{
    public class StringExtensionTests
    {
        [TestCase("firstLine\\n afterNewline", "firstLine afterNewline")]
        [TestCase("firstLine\\r afterNewline", "firstLine afterNewline")]
        [TestCase("firstLine\\n\\r afterNewline", "firstLine afterNewline")]
        [TestCase(null, null)]
        [TestCase("no newLine", "no newLine")]
        public void Test_RemoveNewLine(string input, string expected)
        {
            Assert.That(input.RemoveNewLine(), Is.EqualTo(expected));
        }
    }
}