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
    [TestFixture]
    class CalculateTest
    {
        private Calculate _uut;
        private int NumberOfEvents;

        [SetUp]
        public void Setup()
        {
            _uut = new Calculate();
            NumberOfEvents = 0;

        }

        [Test]
        public void TestToRadCalculate()
        {
            // Setup test data
            _uut.ToRad(6);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ToRad(6), Is.EqualTo(0.10471975511965978));

        }

        [Test]
        public void TestToDegreeCalculate()
        {
            // Setup test data
            _uut.ToDegrees(0.104719755);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ToDegrees(0.104719755), Is.EqualTo(5.9999999931439998));

        }

        [Test]
        public void TestCalculateBearing()
        {
            // Setup test data
            _uut.CalculateBearing(56, 10, 58, 12);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.CalculateBearing(56, 10, 58, 12), Is.EqualTo(28.567417515726333));

        }

        //[Test]
        //public void TestCalculateVelocity()
        //{
        //    // Setup test data
        //    _uut.ToDegrees(0.104719755);
        //    // Act: Trigger the fake object to execute event invocation

        //    // Assert something here or use an NSubstitute Received
        //    Assert.That(_uut.ToDegrees(0.104719755), Is.EqualTo(5.9999999931439998));

        //}
    }
}