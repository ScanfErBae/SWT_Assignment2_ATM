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
using ATM;


namespace ATM.Test.Unit
{
    [TestFixture]
    class SeperationConditionTest
    {
        private SeparationCondition _uut;
        private IOutput _fakeFileOutput;
        private IOutput _fakeConsoleOutput;

        DateTime time = new DateTime(2019, 10, 29, 15, 55, 40, 200);


        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _fakeFileOutput = Substitute.For<FileOutput>();
            _fakeConsoleOutput = Substitute.For<ConsoleOutput>();
            _uut = new SeparationCondition(_fakeFileOutput, _fakeConsoleOutput);
        }

        [Test]
        public void TestComparePlanesFalse()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("ABC1234", 30000, 20000, 2500, time);

            // Act: Trigger the fake object to execute event invocation
            _uut.ComparePlanes(testPlane1, testPlane2);
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ComparePlanes(testPlane1, testPlane2), Is.EqualTo(false));
        }

        [Test]
        public void TestComparePlanesTrue()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("ABC1234", 20200, 20000, 2500, time);

            // Act: Trigger the fake object to execute event invocation
            _uut.ComparePlanes(testPlane1, testPlane2);
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ComparePlanes(testPlane1, testPlane2), Is.EqualTo(true));
        }



    }
}