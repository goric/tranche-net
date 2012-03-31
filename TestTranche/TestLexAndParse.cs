using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LexicalAnalysis;
using SyntaxAnalysis;

namespace TestTranche
{
    [TestClass]
    public class TestLexAndParse
    {
        [TestMethod]
        public void TestCanParseHelloWorld()
        {
            const string source = @"..\..\..\SamplePrograms\helloWorld.tn";

            try
            {
                var ret = Parse(source);
                Assert.AreEqual(true, ret);
            }
            catch(Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void TestCanParseDiscountExample()
        {
            const string source = @"..\..\..\SamplePrograms\DiscountExample.tn";

            try
            {
                var ret = Parse(source);
                Assert.AreEqual(true, ret);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void TestCanParseCollatExample()
        {
            const string source = @"..\..\..\SamplePrograms\CollatExample.tn";

            try
            {
                var ret = Parse(source);
                Assert.AreEqual(true, ret);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void TestCanParseAmortExample()
        {
            const string source = @"..\..\..\SamplePrograms\AmortExample.tn";

            try
            {
                var ret = Parse(source);
                Assert.AreEqual(true, ret);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void TestCanParseBasicExample()
        {
            const string source = @"..\..\..\SamplePrograms\BasicExample.tn";

            try
            {
                var ret = Parse(source);
                Assert.AreEqual(true, ret);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        private bool Parse(string source)
        {
            var scan = new Scanner();
            scan.SetSource(File.ReadAllText(source), 0);

            var parser = new Parser(scan);
            return parser.Parse();
        }
    }
}
