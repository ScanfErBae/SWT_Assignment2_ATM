using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;


namespace ATM.Test.Unit
{
    [TestFixture]
    class FileOutputTest
    {
        private ITransponderReceiver receiver;
        private FileOutput _uut;

        [SetUp]
        public void Setup()
        {
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            // Dependency injection with the real TDR
            _uut = new FileOutput(receiver);
        }

        [Test]
        public void TestPrint()
        {
            string path = "";

            path = (Directory.GetCurrentDirectory() + @"\Output.txt");

            // Setup test data
            File.Delete(path);
            string TestString = "This is a test string";
            _uut.Print(TestString);
            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.IsTrue(File.Exists(path));

        }
    }
}