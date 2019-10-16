using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace ECSWithEvent.Test.Unit
{
    [TestFixture]
    class TempSensorTest
    {
        private TempSensor _uut;
        private TempChangedEventArgs receivedArgs;
        private int NumberOfEvents;

        [SetUp]
        public void Setup()
        {
            _uut = new TempSensor();
            receivedArgs = null;
            NumberOfEvents = 0;

            // Setup a fake event handler
            // Remember the received arg
            // and count the number of events
            _uut.TempChangedEvent +=
                (s, a) => {
                    receivedArgs = a;
                    NumberOfEvents++; 
                };
        }

        [Test]
        public void ctor_SetTemp_EventTriggered()
        {
            _uut.SetTemp(0);
            Assert.That(receivedArgs, Is.Not.Null);
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(30)]
        public void ctor_SetTemp_CorrectData(int newTemp)
        {
            _uut.SetTemp(newTemp);
            Assert.That(receivedArgs.Temp, Is.EqualTo(newTemp));
        }

        [Test]
        public void SetTemp_SameValueAgain_OneEventTriggered()
        {
            _uut.SetTemp(10);
            _uut.SetTemp(10);

            Assert.That(NumberOfEvents, Is.EqualTo(1));
        }

        [Test]
        public void SetTemp_ValueChanges_TwoEventsTriggered()
        {
            _uut.SetTemp(10);
            _uut.SetTemp(15);

            Assert.That(NumberOfEvents, Is.EqualTo(2));
        }
        [Test]
        public void SetTemp_ValueChanges_CorrectNewValue()
        {
            _uut.SetTemp(10);
            _uut.SetTemp(15);

            Assert.That(receivedArgs.Temp, Is.EqualTo(15));
        }

    }
}
