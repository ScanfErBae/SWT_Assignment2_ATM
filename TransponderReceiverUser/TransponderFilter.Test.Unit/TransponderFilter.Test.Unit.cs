using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using TransponderReceiverUser;

namespace TransponderFilter.Test.Unit
{
[TestFixture]
    class TransponderFilter
    {
        private ITransponderReceiver _fakeTransponderReceiver;
        private FilterRelevantPlanes _uut;
        private RelevantPlaneEventArgs receivedArgs;
        private int NumberOfRelevantPlanesEvents;
        private int NumberOfNotRelevantPlanesEvents;


        [SetUp]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            // Inject the fake TDR
            _uut = new FilterRelevantPlanes(_fakeTransponderReceiver);
            receivedArgs = null;
            NumberOfRelevantPlanesEvents = 0;
            NumberOfNotRelevantPlanesEvents = 0;

            // Setup a fake event handler
            // Remember the received arg
            // and count the number of events
            _uut.RelevantPlaneEvent +=
                (s, a) => {
                    receivedArgs = a;
                    NumberOfRelevantPlanesEvents++;
                };

            _uut.NotRelevantPlaneEvent +=
                (s, a) => {
                    receivedArgs = a;
                    NumberOfNotRelevantPlanesEvents++;
                };
        }

        [Test]
        public void TestReception()
        {
            // Setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.RelevantPlanes, Is.EqualTo(3));
        }
    }
}
