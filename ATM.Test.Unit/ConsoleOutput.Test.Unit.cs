using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private List<string> SeperationList;

        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _uut = new ConsoleOutput();
            SeperationList = new List<string>();

        }


        [Test]
        public void TestPrintOfTag()
        {
            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.Print(testPlane);

            Assert.That(_uut.planeTag, Is.EqualTo("Flight " +testPlane.Tag +" \t"));
        }

        [Test]
        public void TestPrintOfPositionX()
        {
            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.Print(testPlane);

            Assert.That(_uut.planePositionX, Is.EqualTo("Position: (" + testPlane.XCoordinate+", "));
        }

        [Test]
        public void TestPrintOfPositionY()
        {
            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.Print(testPlane);

            Assert.That(_uut.planePositionY, Is.EqualTo(testPlane.YCoordinate+") \t "));
        }

        [Test]
        public void TestPrintOfAltitude()
        {
            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.Print(testPlane);

            Assert.That(_uut.planeAltitude, Is.EqualTo("Altitude: "+testPlane.ZCoordinate + "   \t"));
        }

        [Test]
        public void TestPrintOfVelocity()
        {
            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.Print(testPlane);

            Assert.That(_uut.planeVelocity, Is.EqualTo("Velocity: " + string.Format("{0:0.00}", testPlane.Velocity) + " m/s \t"));
        }

        [Test]
        public void TestPrintOfBearing()
        {
            DateTime time2 = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time2);

            _uut.Print(testPlane);

            Assert.That(_uut.planeBearing, Is.EqualTo("Bearing: " + string.Format("{0:0.00}", testPlane.Bearing) + " degrees \n"));
        }

        [Test]
        public void TestPrintOfSeperationCondition()
        {
            DateTime time = new DateTime(2019, 10, 30, 16, 55, 40, 200);
            Plane testPlane = new Plane("ABC1234", 30000, 30000, 3000, time);
            testPlane.SeparationCond.Add("BBB1234");
            testPlane.CurrentTime = time;
            _uut.Print(testPlane);

            Assert.That(_uut.planeCondInfo, Is.EqualTo("SEPARATION CONDITION ACTIVE ON: Flight " + testPlane.Tag +" in connection with " + testPlane.SeparationCond[0] + ", at "+testPlane.CurrentTime+"\n"));
        }


    }
}