using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Tests
{
    [TestClass()]
    public class TestsHelper
    {
        public TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public static void ConfigureServices(TestContext testContext)
        {
            Lib.IOHelper.AppPath = Directory.GetCurrentDirectory().Replace(@"Tests.ProductQuery\bin\Debug", @"Api\bin\publish");
            Environment.CurrentDirectory = Lib.IOHelper.AppPath;
            string[] args = new string[] { };

            Api.RunArgs.Url = args.Length >= 1 ? args[0] : string.Empty;
            Api.RunArgs.Environment = args.Length >= 2 ? args[1] : string.Empty;
            Api.Program.BuildWebHost(args);
        }
    }
}
