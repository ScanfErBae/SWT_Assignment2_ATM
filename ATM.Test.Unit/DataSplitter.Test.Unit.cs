using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using ATM;

namespace ATM.Test.Unit
{
    [TestFixture]
    class DataSplitter_Test_Unit
    {
        
        private DataSplitter _uut;
        private List<AirplaneArgs> receivedArgs;
        private int NumberOfEvents;
        private ITransponderReceiver receiver;

        [SetUp]
        public void Setup()
        {
            _uut = new DataSplitter(receiver);
            receivedArgs = null;
            NumberOfEvents = 0;

            // Setup a fake event handler
            // Remember the received arg
            // and count the number of events

            _uut.DataReceivedEvent += (s, a) =>
            {
                receivedArgs = a;
                NumberOfEvents++;
            };
        }

        //[Test]
        //public void TestReception()
        //{
        //    // Setup test data

        //    // Act: Trigger the fake object to execute event invocation

        //    // Assert something here or use an NSubstitute Received
        //}
    }
}



