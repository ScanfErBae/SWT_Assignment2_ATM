using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
        private FileOutput _uut;

        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _uut = new FileOutput();
        }

        [TestCase("TestTag")]
        public void TestPrint(string tag)
        {
            string path = "";
            path = (Directory.GetCurrentDirectory() + @"\Output.txt");
            //string path = "";
            //path = @"C:\Users\Frederik\Documents\Uni\SWT\SWT_Assignment2_ATM\ATM\Output.txt";
            DateTime time = new DateTime(2019,10,29, 15, 55, 40, 200);

            Plane testPlane = new Plane
            {
                Tag = tag,
                CurrentTime = time
            };
            testPlane.SeparationCond.Add("ABC1234");
            // Setup test data
            File.Delete(path);
            _uut.Print(testPlane);
            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.IsTrue(File.Exists(path));

        }
    }
}