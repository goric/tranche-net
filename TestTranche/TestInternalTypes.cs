using System;
using System.IO;
using System.Reflection;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ILGen;
using LexicalAnalysis;
using SemanticAnalysis;
using SyntaxAnalysis;
using tc;

namespace TestTranche
{
    [TestClass]
    public class TestInternalTypes
    {
        private const string FILE_LOCATION = @"..\..\..\samplePrograms\sample1.tn";
        private static readonly Assembly _assembly;

        static TestInternalTypes()
        {
            CompileAndBuildExecutable();
            _assembly = Assembly.LoadFile(Environment.CurrentDirectory + "\\sample1-test.exe");
        }

        [TestMethod]
        public void TestDealTypeCreated()
        {
            var types = _assembly.GetTypes();
            var dealType = types.FirstOrDefault(t => t.Name == "Deal");

            Assert.IsNotNull(dealType);
            
            var members = dealType.GetMembers();
            
            Assert.IsTrue(members.Any());
            Assert.IsTrue(members.Select(p => p.Name).Contains("Name"));
            Assert.IsTrue(members.Select(p => p.Name).Contains("CutoffDate"));
        }

        [TestMethod]
        public void TestSettingsTypeCreated()
        {
            var types = _assembly.GetTypes();
            var settingsType = types.FirstOrDefault(t => t.Name == "Settings");

            Assert.IsNotNull(settingsType);

            var members = settingsType.GetMembers();

            Assert.IsTrue(members.Any());
            Assert.IsTrue(members.Select(p => p.Name).Contains("OutputFile"));
        }

        [TestMethod]
        public void TestSecurityTypesCreated()
        {
            var types = _assembly.GetTypes();
            var securitiesType = types.FirstOrDefault(t => t.Name == "Securities");
            var bondType = types.FirstOrDefault(t => t.Name == "Bond");

            Assert.IsNotNull(securitiesType);
            Assert.IsNotNull(bondType);

            var members = securitiesType.GetMembers();

            Assert.IsTrue(members.Any());
            Assert.IsTrue(members.SingleOrDefault(t => t.Name == "Bonds") != null);

            members = bondType.GetMembers();
            Assert.IsTrue(members.Any());

            var names = members.Select(t => t.Name).ToList();
            Assert.IsTrue(names.Contains("ID"));
            Assert.IsTrue(names.Contains("CUSIP"));
            Assert.IsTrue(names.Contains("Class"));
            Assert.IsTrue(names.Contains("OriginalBalance"));
            Assert.IsTrue(names.Contains("CurrentBalance"));
        }

        [TestMethod]
        public void TestCollateralTypesCreated()
        {
            var types = _assembly.GetTypes();
            var collateralType = types.FirstOrDefault(t => t.Name == "Collateral");
            var collateralItemType = types.FirstOrDefault(t => t.Name == "CollateralItem");

            Assert.IsNotNull(collateralType);
            Assert.IsNotNull(collateralItemType);

            var members = collateralType.GetMembers();

            Assert.IsTrue(members.Any());
            Assert.IsTrue(members.SingleOrDefault(t => t.Name == "CollateralItems") != null);

            members = collateralItemType.GetMembers();
            Assert.IsTrue(members.Any());

            var names = members.Select(t => t.Name).ToList();
            Assert.IsTrue(names.Contains("ID"));
            Assert.IsTrue(names.Contains("PropertyName"));
            Assert.IsTrue(names.Contains("OriginalBalance"));
            Assert.IsTrue(names.Contains("CurrentBalance"));
            Assert.IsTrue(names.Contains("InterestRate"));
            Assert.IsTrue(names.Contains("Maturity"));
            Assert.IsTrue(names.Contains("PropertyType"));
            Assert.IsTrue(names.Contains("City"));
            Assert.IsTrue(names.Contains("State"));
        }

        [TestMethod]
        public void TestRuleTypesCreated()
        {
            var types = _assembly.GetTypes();
            var rulesType = types.FirstOrDefault(t => t.Name == "CreditPaymentRules");

            Assert.IsNotNull(rulesType);

            var members = rulesType.GetMembers();

            Assert.IsTrue(members.Any());
            Assert.IsTrue(members.SingleOrDefault(t => t.Name == "InterestRules") != null);
            Assert.IsTrue(members.SingleOrDefault(t => t.Name == "PrincipalRules") != null);
        }

        [TestMethod]
        public void TestSimulationTypeHasInternalTypeProperties()
        {
            var types = _assembly.GetTypes();
            var simulationType = types.FirstOrDefault(t => t.Name == "Simulation");

            Assert.IsNotNull(simulationType);

            var members = simulationType.GetMembers();

            Assert.IsTrue(members.Any());

            var names = members.Select(t => t.Name).ToList();
            Assert.IsTrue(names.Contains("Settings"));
            Assert.IsTrue(names.Contains("Deal"));
            Assert.IsTrue(names.Contains("Collateral"));
            Assert.IsTrue(names.Contains("Securities"));
            Assert.IsTrue(names.Contains("CreditPaymentRules"));
        }

        private static void CompileAndBuildExecutable()
        {
            var scan = new Scanner();
            scan.SetSource(File.ReadAllText(FILE_LOCATION), 0);

            var parser = new Parser(scan);
            parser.Parse();

            var root = parser.SyntaxTreeRoot;
            var mgr = new ScopeManager();

            var first = new FirstPass(root, mgr);
            var second = new SecondPass(root, mgr);
            first.Run();
            second.Run();

            const string asmName = "sample1-test";
            var cg = new CodeGenerator(asmName);
            cg.Generate(root);
            cg.WriteAssembly();
        }
    }
}
