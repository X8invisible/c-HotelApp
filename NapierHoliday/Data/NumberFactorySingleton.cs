using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
/* 
* Author: Ovidiu - Andrei Radulescu
* Number Factory class that sets refference numbers for customers and bookings
* This class uses the Singleton and Factory design patterns
* Last edited: 10.12.2017
*/
    public class NumberFactorySingleton
    {
        //properties of the class
            //keeps the only instance of the class
        private static NumberFactorySingleton instance;
        private static int customerNo = 1;
        private static int bookingRef = 1;

        //empty private constructor
        private NumberFactorySingleton() { }

        //gets the only instance of the class
        public static NumberFactorySingleton Instance
        {
            get
            {
                //if the class hasn't been yet initialised, the constructor is called
                if (instance == null)
                    instance = new NumberFactorySingleton();
                return instance;
            }
        }

        //gets a customer number, and then increases the count
        public int CustomerNo
        {
            get { return customerNo++; }
        }
        //gets the booking number, and then increases the count
        public int BookingRef
        {
            get { return bookingRef++; }
        }

        //reverses a customer number increase, in case of an user error
        public void CustomerGoBack()
        {
            customerNo--;
        }

        //gets the number from the last booking made
        //used when the application starts and the data is retrieved from files
        public void UpdateNumbers(string type)
        {
            //needs the data holder class to get the lists
            DataHolderFacadeSingleton inst = DataHolderFacadeSingleton.Instance;
            switch (type)
            {
                //gets the customer number
                case "customer":
                    {
                        List<Customer> temp = inst.Customers;
                        //if the list wasn't empty, get the last customer's number
                        if (temp.Count != 0)
                        {
                            Customer tempCu = temp.Last();
                            customerNo = tempCu.CustomerNo;
                            //increases the number count, as it's already been used by the last customer
                            customerNo++;
                        }
                        break;
                    }
                 //gets the booking number
                case "booking":
                    {
                        List<Booking> temp = inst.Bookings;
                        //if the list wasn't empty, get the last booking's number
                        if (temp.Count != 0)
                        {
                            Booking tempBk = temp.Last();
                            bookingRef = tempBk.BookingRefference;
                            //increases the count
                            bookingRef++;
                        }
                        break;
                    }
            }
        }
    }
}
