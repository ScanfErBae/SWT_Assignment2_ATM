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
    class ConsoleOutputTest
    {
        private ConsoleOutput _uut;

        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _uut = new ConsoleOutput();
        }


        [Test]
        public void TestPrintOfTag()
        {
            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.planeTag = testPlane.Tag;

            Assert.That(_uut.planeTag = testPlane.Tag, Is.EqualTo(testPlane.Tag));


        }

        //[TestCase("Tag", 20000, 20000, 2500)]
        //public void TestPrint(string tag, int x, int y, int z)
        //{
        //    DateTime time = new DateTime(2019, 10, 29, 15, 55, 40, 200);
        //    Plane testPlane = new Plane(tag,x,y,z, time);
        //    string expectedString = "Tag"
        //    // Setup test data
        //    _uut.Print(testPlane);
        //    // Act: Trigger the fake object to execute event invocation
        //    // Assert something here or use an NSubstitute Received
        //    Assert.AreEqual(testPlane, Console.Out);

        //}
    }
}