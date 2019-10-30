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
    class PlaneTest
    {
        private Plane _uut;


        DateTime time = new DateTime(2019, 10, 29, 15, 55, 40, 200);

        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _uut = new Plane();
        }

        [TestCase("ABC1234", 20000, 20000, 2500)]
        public void TestPlaneConstructor(string tag, int x, int y, int z)
        {
            // Setup test data
            Plane assertPlane = new Plane("ABC1234", 20000, 20000, 2500, time);
            _uut.Tag = tag;
            _uut.XCoordinate = x;
            _uut.YCoordinate = y;
            _uut.ZCoordinate = z;
            _uut.CurrentTime = time;
            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut, Is.EqualTo(assertPlane));
        }

        [Test]
        public void TestPlaneCopyConstructor()
        {
            // Setup test data
            Plane assertPlane = new Plane("ABC1234", 20000, 20000, 2500, time);
            _uut = new Plane(assertPlane);
            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut, Is.EqualTo(assertPlane));
        }

        [Test]
        public void TestPlaneEqualOperator()
        {
            // Setup test data
            Plane assertPlane = new Plane("ABC1234", 20000, 20000, 2500, time);
            _uut = assertPlane;
            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut, Is.EqualTo(assertPlane));
        }

        [Test]
        public void TestPlaneEqualToFalse()
        {
            // Setup test data
            int notPlane = 5;
            _uut.Equals(notPlane);
            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.Equals(notPlane), Is.EqualTo(false));
        }


        [TestCase("ABC1234", 20000, 20000, 2500)]
        public void TestPlaneUpdate(string tag, int x, int y, int z)
        {
            // Setup test data

            _uut.Tag = tag;
            _uut.XCoordinate = x;
            _uut.YCoordinate = y;
            _uut.ZCoordinate = z;
            _uut.CurrentTime = time;

            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane assertPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.UpdateData(30000,30000,3000, time2);

            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut, Is.EqualTo(assertPlane));
        }


    }
}