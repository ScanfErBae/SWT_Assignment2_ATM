﻿//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NSubstitute;
//using NUnit.Framework;
//using TransponderReceiver;
//using ATM;

//namespace ATM.Test.Unit
//{
//    class DataSplitterTestUnit
//    {

//        private ITransponderReceiver _fakeTransponderReceiver;
//        private DataSplitter _uut;
//        private AirplaneArgs receivedArgs;
//        private int NumberOfEvents;

//        [SetUp]
//        public void Setup()
//        {
//            //Make a fake Transponder Data Receiver
//            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

//            //Inject the fake
//            _uut = new DataSplitter(_fakeTransponderReceiver);

//            receivedArgs = null;
//            NumberOfEvents = 0;


//            _uut.DataReceivedEvent += (s, a) =>
//            {
//                receivedArgs = a;
//                NumberOfEvents++;
//            };
//        }

//        [Test]
//        public void TestReception_ThreeRelevantPlanes()
//        {
//            // Setup test data
//            List<string> testData = new List<string>();
//            testData.Add("ATR423;39045;12932;14000;20151006213456789");
//            testData.Add("BCD123;10005;85890;12000;20151006213456789");
//            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

//            // Act: Trigger the fake object to execute event invocation
//            _fakeTransponderReceiver.TransponderDataReady
//                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

//            //Assert
//            Assert.That(NumberOfEvents, Is.EqualTo(1));
//        }

//        //[Test]
//        //public void Data_From_Plane_Is_Split()
//        //{
//        //    // Setup test data
//        //    List<string> testPlane = new List<string>();
//        //    testPlane.Add("ATR423;39045;12932;14000;20151006213456789");

//        //    //Act: Trigger the fake object to execute event invocation
//        //    _fakeTransponderReceiver.TransponderDataReady
//        //        += Raise.EventWith(this, new RawTransponderDataEventArgs(testPlane));

//        //    //Assert
//        //    Assert.That(receivedArgs._planes, Is.EqualTo(testPlane));
//        //}


//    }
//}



