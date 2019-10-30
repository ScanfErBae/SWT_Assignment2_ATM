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
    public class StubOutput : IOutput
    {
        public int numberOfCalls { get; set; }

        public StubOutput()
        {
            numberOfCalls = 0;
        }
        public void Print(Plane plane)
        {
            numberOfCalls++;
        }
    }

    [TestFixture]
    class SeperationConditionTest
    {
        private SeparationCondition _uut;
        private StubOutput _stubFileOutput;
        private StubOutput _stubConsoleOutput;

        DateTime time = new DateTime(2019, 10, 29, 15, 55, 40, 200);


        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _stubFileOutput = new StubOutput();
            _stubConsoleOutput = new StubOutput();
            _uut = new SeparationCondition(_stubFileOutput, _stubConsoleOutput);
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

        [Test]
        public void TestSeperationCond_TwoPlanesCloesToEachother_Plane2IsInPlane1sSepcondlist()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20200, 20000, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((testPlane1.SeparationCond[0]), Is.EqualTo(testPlane2.Tag));
        }

        [Test]
        public void TestSeperationCond_2PlanesCloesToEachother_Plane1IsInPlane2sSepcondlist()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20200, 20000, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((testPlane2.SeparationCond[0]), Is.EqualTo(testPlane1.Tag));
        }

        [Test]
        public void TestSeperationCond_TwoPlanesNotCloseToEachother_Plane2sSeperationlistIsEmpty()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20400, 20900, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((testPlane2.SeparationCond), Is.Empty);
        }

        [Test]
        public void TestSeperationCond_TwoPlanesNotCloseToEachother_Plane1sSeperationlistIsEmpty()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20400, 20900, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((testPlane1.SeparationCond), Is.Empty);
        }

        [Test]
        public void TestSeperationCond_TwoPlanesCloseToEachotherAndThenNotClose_Plane1sSeperationlistIsEmpty()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20200, 20000, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            _uut.Separation(Planelist);

            testPlane2.XCoordinate = 20500;
            Planelist.Clear();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((testPlane1.SeparationCond), Is.Empty);
        }

        [Test]
        public void TestSeperationCond_TwoPlanesCloseToEachotherAndThenNotClose_Plane2sSeperationlistIsEmpty()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20200, 20000, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            _uut.Separation(Planelist);

            testPlane2.XCoordinate = 20500;
            Planelist.Clear();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((testPlane2.SeparationCond), Is.Empty);
        }

        [Test]
        public void TestSeperation_TwoPlanesCloesToEachotherAndAreStillClose_ButOnleTwoPlanesGetsWrittenToFile()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20200, 20000, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            _uut.Separation(Planelist);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((_stubFileOutput.numberOfCalls), Is.EqualTo(2));
        }

        [Test]
        public void TestSeperation_TwoPlanesCloesToEachotherAndAreStillClose_AndForePlanesGetsWrittenToConsole()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time);
            Plane testPlane2 = new Plane("DEF5678", 20200, 20000, 2500, time);

            List<Plane> Planelist = new List<Plane>();
            Planelist.Add(testPlane1);
            Planelist.Add(testPlane2);

            _uut.Separation(Planelist);

            // Act: Trigger the fake object to execute event invocation
            _uut.Separation(Planelist);

            // Assert something here or use an NSubstitute Received
            Assert.That((_stubConsoleOutput.numberOfCalls), Is.EqualTo(4));
        }
    }
}