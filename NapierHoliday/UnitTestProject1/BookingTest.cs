using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessObjects;

namespace UnitTestProject1
{
 /* 
* Author: Ovidiu - Andrei Radulescu
* Testing class for booking
* Last edited: 10.12.2017
*/
    [TestClass]
    public class BookingTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        //tests storing of arrival date
        [TestMethod]
        public void ArrivalTest()
        {
            Booking target = new Booking();
            DateTime arrival = new DateTime(2017,12,20);
            target.Arrival = arrival;
            Assert.AreEqual(arrival, target.Arrival);
        }
        //tests storing of departure date
        [TestMethod]
        public void DepartureTest()
        {
            Booking target = new Booking();
            DateTime departure = new DateTime(2017, 12, 31);
            target.Departure = departure;
            Assert.AreEqual(departure, target.Departure);
        }
        //tests storing of refference number
        [TestMethod]
        public void NumberTest()
        {
            Booking target = new Booking();
            int refNo = 100;
            target.BookingRefference = refNo;
            Assert.AreEqual(refNo, target.BookingRefference);
        }
        //tests storing of the chalet id and the rule
        [TestMethod]
        public void ChaletTest()
        {
            Booking target = new Booking();
            //stores a value in order for assert to work
            //because the first for loop value won't be valid
            target.ChaletID = 10;
            //goes through every value from 0 to 20(a chalet is supposed to be between 1 and 10,)
            for (int chalet= 0; chalet < 20; chalet++)
            try
            {
                target.ChaletID = chalet;
                Assert.AreEqual(chalet, target.ChaletID);
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual(10, target.ChaletID);
            }
        }
        //tests adding a guest and retrieving it from the booking
        [TestMethod]
        public void GuestTest()
        {
            Booking target = new Booking();
            Guest targetGuest = new Guest("Andrei", "045619", 20);
            target.AddGuest(targetGuest);
            Assert.AreEqual(targetGuest, target.GuestList[0]);
        }
        [TestMethod]
        public void CarHireTest()
        {
            Booking target = new Booking();
            DateTime start = new DateTime(2018,01,01);
            DateTime end = new DateTime(2018, 02, 02);
            CarHire targetCar = new CarHire(start,end,"Andrei");
            target.Car = targetCar;
            Assert.AreEqual(targetCar, target.Car);
        }
    }
}
