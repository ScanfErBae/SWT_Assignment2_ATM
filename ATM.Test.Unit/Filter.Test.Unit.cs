using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    class FilterTest
    {
        private IDataSplitter _fakeIDataSplitter;
        private Filter _uut;
        private RelevantAirplaneArgs receivedArgs;
        private int NumberOfEvents;

        [SetUp]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeIDataSplitter = Substitute.For<IDataSplitter>();
            // Inject the fake TDR
            _uut = new Filter(_fakeIDataSplitter);

            receivedArgs = null;
            NumberOfEvents = 0;

            _uut.RelevantAirplanesReceivedEvent +=
                (s, a) => {
                    receivedArgs = a;
                    NumberOfEvents++;
                };
        }

        [Test]
        public void TestReception_ThreeRelevantPlanes_ThreePlanesInTriggeredEvent()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 39045, 12932, 14000, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));
            testData.Add(new Plane("BCD123", 10005, 85890, 12000, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));
            testData.Add(new Plane("XYZ987", 25059, 75654, 4000, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs{_planes = testData});

            // Assert something here or use an NSubstitute Received
            Assert.That(receivedArgs._relevantPlanes.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestReception_CoordinatesAreOnEdgeButInsideAreaLower_EventTriggeredOnce()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 10000, 10000, 500, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(1));
        }

        [Test]
        public void TestReception_CoordinatesAreOnEdgeButInsideAreaUpper_EventTriggeredOnce()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 90000, 90000, 20000, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(1));
        }


        [Test]
        public void TestReception_XCoordinateBelowTenThousand_NoEventTriggered()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 9999, 12932, 14000, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(0));
        }


        [Test]
        public void TestReception_YCoordinateBelowTenThousand_NoEventTriggered()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 54222, 9999, 14000, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(0));
        }

        [Test]
        public void TestReception_ZCoordinateBelowTenThousand_NoEventTriggered()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 54222, 87662, 499, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(0));
        }

        [Test]
        public void TestReception_XCoordinateAboveTenThousand_NoEventTriggered()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 90001, 87662, 6531, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(0));
        }

        [Test]
        public void TestReception_YCoordinateAboveTenThousand_NoEventTriggered()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 12547, 90001, 6531, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(0));
        }

        [Test]
        public void TestReception_ZCoordinateAboveTenThousand_NoEventTriggered()
        {
            // Setup test data
            List<Plane> testData = new List<Plane>();
            testData.Add(new Plane("ATR423", 12547, 24875, 20001, new DateTime(2015, 10, 6, 21, 34, 56).AddMilliseconds(789)));

            // Act: Trigger the fake object to execute event invocation
            _fakeIDataSplitter.DataReceivedEvent
                += Raise.EventWith(this, new AirplaneArgs { _planes = testData });

            // Assert something here or use an NSubstitute Received
            Assert.That(NumberOfEvents, Is.EqualTo(0));
        }
    }
}
