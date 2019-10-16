using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace ECSWithEvent.Test.Unit
{
    [TestFixture]
    public class ECSTest
    {
        // member variables to hold uut and fakes
        private ITempSensor _fakeTempSensor;
        private IHeater _fakeHeater;
        private ECS _uut;
        private IWindow _fakeWindow;

        [SetUp]
        public void Setup()
        {
            // Create the fake stubs and mocks
            _fakeHeater = Substitute.For<IHeater>();
            _fakeTempSensor = Substitute.For<ITempSensor>();
            _fakeWindow = Substitute.For<IWindow>();
            // Inject them into the uut via the constructor
            _uut = new ECS(_fakeTempSensor, _fakeHeater, _fakeWindow, 25, 28);
        }

        #region Event Reception Tests

        [TestCase(-5)]
        [TestCase(-0)]
        [TestCase(10)]
        [TestCase(30)]
        public void EventHandling_TempChangedEventRaised_CorrectValueReceived(int newTemp)
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = newTemp });

            // This asserts that uut has connected to the event
            // And handles value correctly
            Assert.That(_uut.CurrentTemperature, Is.EqualTo(newTemp));
        }

        #endregion

        #region Threshold tests

        [Test]
        public void Thresholds_ValidUpperTemperatureThresholdSet_NoExceptionsThrown()
        {
            // Check that it doesn't throw
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.UpperTemperatureThreshold = 27; }, Throws.Nothing);
        }

        [Test]
        public void Thresholds_ValidLowerTemperatureThresholdSet_NoExceptionsThrown()
        {
            // Check that it doesn't throw 
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.LowerTemperatureThreshold = 26; }, Throws.Nothing);
        }

        [Test]
        public void Thresholds_UpperSetToLower_NoExceptionsThrown()
        {
            // Check that it doesn't throw when they are equal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.UpperTemperatureThreshold = _uut.LowerTemperatureThreshold; }, Throws.Nothing);
        }

        [Test]
        public void Thresholds_LowerSetToUpper_NoExceptionsThrown()
        {
            // Check that it doesn't throw when they are equal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.LowerTemperatureThreshold = _uut.UpperTemperatureThreshold; }, Throws.Nothing);
        }


        [Test]
        public void Thresholds_InvalidUpperTemperatureThresholdSet_ArgumentExceptionThrown()
        {
            // Check that it throws when upper is illegal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.UpperTemperatureThreshold = 24; }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Thresholds_InvalidLowerTemperatureThresholdSet_ArgumentExceptionThrown()
        {
            // Check that it throws when lower is illegal
            // First parameter is a lambda expression, implicitly acting
            Assert.That(() => { _uut.LowerTemperatureThreshold = 29; }, Throws.TypeOf<ArgumentException>());
        }

        #endregion

        #region Regulation tests

        #region T < Tlow

        [Test]
        public void Regulate_TempIsLow_HeaterIsTurnedOn()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 20 });

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOn();
        }


        [Test]
        public void Regulate_TempIsLow_WindowIsClosed()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 20 });

            // Assert on the mock - was the window called correctly
            _fakeWindow.Received(1).Close();
        }

        #endregion

        #region T == Tlow

        [Test]
        public void Regulate_TempIsAtLowerThreshold_HeaterIsTurnedOff()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 25 });

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOff();
        }

        [Test]
        public void Regulate_TempIsAtLowerThreshold_WindowIsClosed()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 25 });

            // Assert on the mock - was the window called correctly
            _fakeWindow.Received(1).Close();
        }

        #endregion

        #region Tlow < T < Thigh

        [Test]
        public void Regulate_TempIsBetweenLowerAndUpperThresholds_HeaterIsTurnedOff()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 27 });

            // Assert on the mock - was the heater called correctly
            _fakeHeater.DidNotReceive().TurnOn();
        }

        [Test]
        public void Regulate_TempIsBetweenLowerAndUpperThresholds_WindowIsClosed()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 27 });

            // Assert on the mock - was the window called correctly
            _fakeWindow.Received(1).Close();
        }

        #endregion

        #region T == Thigh

        [Test]
        public void Regulate_TempIsAtUpperThreshold_HeaterIsTurnedOff()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 27 });

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(0).TurnOn();
        }

        [Test]
        public void Regulate_TempIsAtUpperThreshold_WindowIsClosed()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 27 });

            // Assert on the mock - was the window called correctly
            _fakeWindow.Received(1).Close();
        }

        #endregion

        #region T > Thigh

        [Test]
        public void Regulate_TempIsAboveUpperThreshold_HeaterIsTurnedOff()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 27 });

            // Assert on the mock - was the heater called correctly
            _fakeHeater.Received(1).TurnOff();
        }

        [Test]
        public void Regulate_TempIsAboveUpperThreshold_WindowIsOpened()
        {
            // Raise event in fake
            _fakeTempSensor.TempChangedEvent += Raise.EventWith<TempChangedEventArgs>(
                this,
                new TempChangedEventArgs() { Temp = 29 });

            // Assert on the mock - was the window called correctly
            _fakeWindow.Received(1).Open();
        }

        #endregion

        #endregion

        #region SelfTest Tests

        [TestCase(true, true, true, true)]
        [TestCase(false, true, true, false)]
        [TestCase(false, false, true, false)]
        [TestCase(false, true, false, false)]
        [TestCase(false, false, false, false)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, false)]
        [TestCase(true, true, false, false)]
        public void RunSelftest_AllSubtestsPass_ReturnsTrue(
            bool TempTest,
            bool HeaterTest,
            bool WindowTest,
            bool ExpectedResult)
        {
            _fakeTempSensor.RunSelfTest().Returns(TempTest);
            _fakeHeater.RunSelfTest().Returns(HeaterTest);
            _fakeWindow.RunSelfTest().Returns(WindowTest);

            Assert.That(_uut.RunSelfTest(), Is.EqualTo(ExpectedResult));
        }


        #endregion
    }
}
