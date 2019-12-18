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
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Output.txt";

        // Output filen ender på dit skrivebord!!!

        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _uut = new FileOutput();
        }

        [TestCase("TestTag")]
        public void TestPrint(string tag)
        {
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

        [TestCase("TestTag")]
        public void TestPrintContent(string tag)
        {

            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            File.Delete(path);

            DateTime time = new DateTime(2019, 10, 29, 15, 55, 40, 200);

            Plane testPlane = new Plane
            {
                Tag = tag,
                CurrentTime = time
            };
            testPlane.SeparationCond.Add("ABC1234");
            // Setup test data
            _uut.Print(testPlane);

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fs, Encoding.Default);
            //Arrange

            string str = sr.ReadLine();

            sr.Close();
            fs.Close();

            string expected = "Tuesday, 29 October, 2019 Kl: 15:55:40:200 Plane: TestTag Close to: ABC1234";

            StringAssert.Contains(str, expected);


        }
    }
}