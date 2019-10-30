using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

/* Der skal testes i alle 4 kvadranter af de 360 grader (spørg jesper)
 * Der skal testes med 0
 * Der skal testes med - tal
 */


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

        [TestCase(0, 0)]
        [TestCase(45, 0.78539816339744828)]
        [TestCase(90, 1.5707963267948966)]
        [TestCase(135, 2.3561944901923448)]
        [TestCase(180, 3.1415926535897931)]
        [TestCase(225, 3.9269908169872414)]
        [TestCase(270, 4.7123889803846897)]
        [TestCase(315, 5.497787143782138)]
        [TestCase(360, 6.2831853071795862)]
        [TestCase(361, 0)]
        [TestCase(-6, 0)]
        public void TestToRadCalculate(double a, double b)
        {
            // Setup test data
            _uut.ToRad(a);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ToRad(a), Is.EqualTo(b));

        }

        [TestCase(0.1, 5.7295779513082321)]
        [TestCase(1, 57.295779513082323)]
        [TestCase(1.5, 85.943669269623484)]
        [TestCase(2, 114.59155902616465)]
        [TestCase(2.5, 143.23944878270581)]
        [TestCase(3, 171.88733853924697)]
        [TestCase(3.5, 200.53522829578813)]
        [TestCase(4, 229.18311805232929)]
        [TestCase(4.5, 257.83100780887048)]
        [TestCase(6.5, 12.422566835035127)]
        [TestCase(-1, 0)]
        [TestCase(0, 0)]
        public void TestToDegreeCalculate(double a, double b)
        {
            // Setup test data
            _uut.ToDegrees(a);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.ToDegrees(a), Is.EqualTo(b));

        }

        [TestCase(12500, 13000, 12500, 14000, 90)]
        [TestCase(12500, 13000, 14500, 10500, 308.65980825409008)]
        [TestCase(10000, 10000, 10050, 10005, 5.7105931374996421)]
        [TestCase(10000, 10000, 10005, 10050, 84.289406862500371)]
        [TestCase(10000, 10000, 10050, 10050, 45)]
        [TestCase(10000, 10000, 10000, 10050, 90)]
        [TestCase(20000, 20000, 19950, 20050, 135)]
        [TestCase(20000, 20000, 19950, 20000, 180)]
        [TestCase(20000, 20000, 19950, 19950, 225)]
        [TestCase(20000, 20000, 20000, 19950, 270)]
        [TestCase(20000, 20000, 20050, 19950, 315)]
        [TestCase(14642, 63625, 26252, 43315, 299.75399467041603)]
        public void TestCalculateBearing(int lat1, int lon1, int lat2, int lon2, double res)
        {
            Plane oldPlane = new Plane
            {
                XCoordinate = lon1,
                YCoordinate = lat1
            };

            Plane newPlane = new Plane
            {
                XCoordinate = lon2, YCoordinate = lat2
            };

            // Setup test data
            _uut.CalculateBearing(oldPlane, newPlane);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.CalculateBearing(oldPlane, newPlane), Is.EqualTo(res));
        }

        [TestCase(20000, 20000, 2019, 10, 27, 15, 0, 0, 0, 20050, 20050, 2019, 10, 27, 15, 2, 0, 0, 0.58925565098878963)]
        [TestCase(10000, 10000, 2019, 10, 27, 15, 0, 0, 0, 10100, 10100, 2019, 10, 27, 15, 2, 0, 0, 1.1785113019775793)]
        [TestCase(20000, 20000, 2019, 10, 27, 15, 0, 0, 0, 20500, 20500, 2019, 10, 27, 15, 2, 0, 0, 5.8925565098878963)]
        [TestCase(20000, 20000, 2019, 10, 27, 15, 0, 0, 0, 20500, 20500, 2019, 10, 27, 15, 1, 0, 0, 11.785113019775793)]
        [TestCase(20000, 20000, 2019, 10, 27, 15, 0, 0, 0, 20000, 20500, 2019, 10, 27, 15, 2, 0, 0, 4.166666666666667)]
        [TestCase(20000, 20000, 2019, 10, 27, 15, 0, 0, 0, 30000, 30000, 2019, 10, 27, 15, 2, 0, 0, 117.85113019775791)]
        [TestCase(10000, 10000, 2019, 10, 27, 15, 0, 0, 0, 90000, 90000, 2019, 10, 27, 15, 5, 0, 0, 377.12361663282536)]
        public void TestCalculateVelocity(int x1, int y1, int year1, int month1, int day1, int hour1, int min1, int sec1, int ms1, int x2, int y2, int year2, int month2, int day2, int hour2, int min2, int sec2, int ms2, double result)
        {
            Plane oldPlane = new Plane
            {
                XCoordinate = x1,
                YCoordinate = y1,
                CurrentTime = new DateTime(year1, month1, day1, hour1, min1, sec1).AddMilliseconds(ms1)
            };

            Plane newPlane = new Plane
            {
                XCoordinate = x2,
                YCoordinate = y2,
                CurrentTime = new DateTime(year2, month2, day2, hour2, min2, sec2).AddMilliseconds(ms2)
            };
            // Setup test data
            _uut.CalculateVelocity(oldPlane, newPlane);
            // Act: Trigger the fake object to execute event invocation

            // Assert something here or use an NSubstitute Received
            Assert.That(_uut.CalculateVelocity(oldPlane, newPlane), Is.EqualTo(result));
        }
    }
}