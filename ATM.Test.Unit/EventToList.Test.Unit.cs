//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NSubstitute;

//namespace ATM.Test.Unit
//{
//    public class EventToListTest
//    {
//        private List<Plane> _relevantPlanesList = new List<Plane>();
//        private IFilter _fakeFilter;
//        private ICalculate _fakeCalculator;
//        private ISeparationCondition _fakeSeparationCondition;
//        private EventToList _uut;

//        [SetUp]
//        public void Setup()
//        {
//            // Make a fake Transponder Data Receiver
//            _fakeFilter = Substitute.For<IFilter>();
//            _fakeCalculator = Substitute.For<ICalculate>();
//            _fakeSeparationCondition = Substitute.For<ISeparationCondition>();
//            // Inject the fake TDR
//            _uut = new EventToList(_fakeFilter, _fakeCalculator, _fakeSeparationCondition);
//        }
//    }
//}
