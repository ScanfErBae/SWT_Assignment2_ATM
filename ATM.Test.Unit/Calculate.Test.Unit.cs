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

        [SetUp]
        public void Setup()
        {
            _uut = new Calculate();
        }

        [TestCase(6, 0.10471975511965978)]
        [TestCase(0, 0)]
        [TestCase(-6, -0.10471975511965978)]
        public void TestToRadCalculate(double a, double b)
        {
            // Setup test data
            _uut.ToRad(a);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ToRad(a), Is.EqualTo(b));

        }

        [TestCase(0.104719755, 5.9999999931439998)]
        [TestCase(-0.104719755, -5.9999999931439998)]
        [TestCase(0, 0)]
        public void TestToDegreeCalculate(double a, double b)
        {
            // Setup test data
            _uut.ToDegrees(a);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ToDegrees(a), Is.EqualTo(b));

        }

        [TestCase(25, 352.39448782705813)]
        [TestCase(-25, -352.39448782705813)]
        [TestCase(0, 0)]
        public void TestToBearingCalculate(double a, double b)
        {
            // Setup test data
            _uut.ToBearing(a);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ToBearing(a), Is.EqualTo(b));

        }

        [TestCase(56, 10, 58, 12, 28.567417515726333)]
        [TestCase(-56, -10, -58, -12, 208.56741751572633)]
        [TestCase(0,0,0,0,0)]
        public void TestCalculateBearing(int a, int b, int c, int d, double e)
        {
            // Setup test data
            _uut.CalculateBearing(a,b,c,d);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.CalculateBearing(a, b, c, d), Is.EqualTo(e));

        }

        //[TestCase(75, 75, 2019, 10, 27, 15, 20, 20, 50, 80, 80, 2019, 10, 27, 15, 22, 20, 50, 0)]
        //public void TestCalculateVelocity(int x1, int y1, int year1, int month1, int day1, int hour1, int min1, int sec1, int ms1, int x2, int y2, int year2, int month2, int day2, int hour2, int min2, int sec2, int ms2, double result)
        //{
        //    // Setup test data
        //    _uut.CalculateVelocity(x1, y1, year1, month1, day1, hour1, min1, sec1, ms1, x2, y2, year2, month2, day2,
        //        hour2, min2, sec2, ms2);
        //    // Act: Trigger the fake object to execute event invocation

        //    // Assert something here or use an NSubstitute Received
        //    Assert.That(_uut.CalculateVelocity(x1, y1, year1, month1, day1, hour1, min1, sec1, ms1, x2, y2, year2, month2, day2,
        //        hour2, min2, sec2, ms2), Is.EqualTo(result));

        //}
    }
}