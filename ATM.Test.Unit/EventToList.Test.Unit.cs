using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace ATM.Test.Unit
{
    public class EventToListTest
    {
        private List<Plane> _relevantPlanesList = new List<Plane>();
        private IFilter _fakeFilter;
        private ICalculate _fakeCalculator;
        private ISeparationCondition _fakeSeparationCondition;
        private EventToList _uut;

        DateTime time1 = new DateTime(2019, 10, 29, 15, 55, 40, 200);
        DateTime time2 = new DateTime(2019, 10, 29, 15, 57, 40, 200);


        [SetUp]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeFilter = Substitute.For<IFilter>();
            _fakeCalculator = Substitute.For<ICalculate>();
            _fakeSeparationCondition = Substitute.For<ISeparationCondition>();
            // Inject the fake TDR
            _uut = new EventToList(_fakeFilter, _fakeCalculator, _fakeSeparationCondition);
        }

        [Test]
        public void TestAddPlaneToListIndex0()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);

            _uut.AddPlane(testPlane1);

            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList[0], Is.EqualTo(testPlane1));
        }

        [Test]
        public void TestAddPlaneToListIndex1()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);
            Plane testPlane2 = new Plane("ABC5678", 30000, 30000, 2500, time1);

            _uut.AddPlane(testPlane1);
            _uut.AddPlane(testPlane2);

            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList[1], Is.EqualTo(testPlane2));
        }

        [Test]
        public void TestUpdatePlane()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);
            Plane testPlane2 = new Plane("ABC1234", 20200, 20200, 2500, time1);

            // Act: Trigger the fake object to execute event invocation
            _uut.AddPlane(testPlane1);

            _uut.UpdatePlane(testPlane1, testPlane2);
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList[0], Is.EqualTo(testPlane2));
        }


        [Test]
        public void TestRemovePlaneTestOfPlaneOutOfIndex()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);
            Plane testPlane2 = new Plane("ABC1234", 20200, 20200, 2500, time1);

            // Act: Trigger the fake object to execute event invocation
            _uut.AddPlane(testPlane1);
            _uut.AddPlane(testPlane2);

            _uut._relevantPlanesList[1].Relevant = false;

            _uut.RemoveOldPlanes(_uut._relevantPlanesList);
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList.IndexOf(testPlane2), Is.EqualTo(-1));
        }

        [Test]
        public void TestRemovePlanePlaneStillInIndex()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);
            Plane testPlane2 = new Plane("ABC1234", 20200, 20200, 2500, time1);

            // Act: Trigger the fake object to execute event invocation
            _uut.AddPlane(testPlane1);
            _uut.AddPlane(testPlane2);

            _uut._relevantPlanesList[1].Relevant = false;

            _uut.RemoveOldPlanes(_uut._relevantPlanesList);
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList.IndexOf(testPlane1), Is.EqualTo(0));
        }

        [Test]
        public void EventToList_OnePlaneGetsRegistered_TriggersEventAndPlaneGetsAddedToList()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);

            List<Plane> planeList = new List<Plane>();
            planeList.Add(testPlane1);

            _fakeFilter.RelevantAirplanesReceivedEvent
                += Raise.EventWith(this, new RelevantAirplaneArgs { _relevantPlanes = planeList });
            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList[0], Is.EqualTo(testPlane1));
        }

        [Test]
        public void EventToList_OnePlaneGetsRegistered_PlanesGetsRegisteredAgainAndGetsUpdatedInList()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);

            List<Plane> planeList = new List<Plane>();
            planeList.Add(testPlane1);

            _fakeFilter.RelevantAirplanesReceivedEvent
                += Raise.EventWith(this, new RelevantAirplaneArgs { _relevantPlanes = planeList });

            planeList.Clear();
            testPlane1.XCoordinate = 20300;
            planeList.Add(testPlane1);

            _fakeFilter.RelevantAirplanesReceivedEvent
                += Raise.EventWith(this, new RelevantAirplaneArgs { _relevantPlanes = planeList });

            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList[0], Is.EqualTo(testPlane1));
        }

        [Test]
        public void EventToList_TwoPlaneGetsRegisteredAndAfterwordsOnlyOnePlane_SecondPlaneIsNotInList()
        {
            // Setup test data
            Plane testPlane1 = new Plane("ABC1234", 20000, 20000, 2500, time1);
            Plane testPlane2 = new Plane("DEF5678", 25000, 25000, 2900, time1);

            List<Plane> planeList = new List<Plane>();
            planeList.Add(testPlane1);
            planeList.Add(testPlane2);
            _fakeFilter.RelevantAirplanesReceivedEvent
                += Raise.EventWith(this, new RelevantAirplaneArgs { _relevantPlanes = planeList });

            planeList.Remove(testPlane2);
            _fakeFilter.RelevantAirplanesReceivedEvent
                += Raise.EventWith(this, new RelevantAirplaneArgs { _relevantPlanes = planeList });

            // Act: Trigger the fake object to execute event invocation
            // Assert something here or use an NSubstitute Received
            Assert.That(_uut._relevantPlanesList.IndexOf(testPlane2), Is.EqualTo(-1));
        }
    }
}