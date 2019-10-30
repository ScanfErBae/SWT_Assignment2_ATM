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


namespace ATM.Test.Unit
{
    [TestFixture]
    class PlaneTest
    {
        private Plane _uut;
        
        DateTime time = new DateTime(2019, 10, 29, 15, 55, 40, 200);


        [SetUp]
        public void Setup()
        {
            // Dependency injection with the real TDR
            _uut = new Plane();

        }

        //[TestCase("ABC1234", 20000, 20000, 2500)]
        //public void TestPlaneConstructor(string tag, int x, int y, int z)
        //{
        //    // Setup test data
        //    Plane assertPlane = new Plane("ABC1234", 20000, 20000, 2500, time);
        //    _uut.Tag = tag;
        //    _uut.XCoordinate = x;
        //    _uut.YCoordinate = y;
        //    _uut.ZCoordinate = z;
        //    _uut.CurrentTime = time;
        //    // Act: Trigger the fake object to execute event invocation
        //    // Assert something here or use an NSubstitute Received
        //    Assert.That(_uut), Is.EqualTo(assertPlane);


        //}
    }
}