using BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data
{
    /* 
 * Author: Ovidiu - Andrei Radulescu
 * Data Holder class containing all the application data(customers,bookings,guests etc) 
 * and methods for their manipulation and saving/retrieving it
 * This class uses the Singleton and Facade design patterns
 * Last edited: 10.12.2017
 */
    public class DataHolderFacadeSingleton
    {
        //all the properties of the class
            //holds the only instance of the class
        private static DataHolderFacadeSingleton instance;
            //tracks the current customer that is making a booking
        private static int currentCustomer;
            //tracks the current existing booking that is being edited
        private static int currentBooking = 0;
        //the lists of all customers,bookings and guests
        private static List<Customer> customers = new List<Customer>();
        private static List<Booking> bookings = new List<Booking>();

        //the private empty constructor
        private DataHolderFacadeSingleton() { }

        //gets the only instance of the class
        public static DataHolderFacadeSingleton Instance
        {
            get
            {
                //if the class hasn't been yet initialised, the constructor is called
                if (instance == null)
                    instance = new DataHolderFacadeSingleton();
                return instance;
            }
        }

        //gets and sets the current customer
        public int CurrentCustomer
        {
            get { return currentCustomer; }
            set { currentCustomer = value; }
        }
        //gets and sets the current booking
        public int CurrentBooking
        {
            get { return currentBooking; }
            set { currentBooking = value; }
        }
        //adds a customer to the list
        public void AddCustomer(Customer c)
        {
            customers.Add(c);
        }
        //adds a booking to the list
        public void AddBooking(Booking b)
        {
            bookings.Add(b);
        }

        //gets the customer list
        public List<Customer> Customers
        {
            get { return customers; }
        }
        //gets the bookings list
        public List<Booking> Bookings
        {
            get { return bookings; }
        }
        //gets a specific customer with the corresponding number from the list
        public Customer GetCustomer(int number)
        {
            foreach (Customer cu in customers)
            {
                if (cu.CustomerNo == number)
                {
                    return cu;
                }
            }
            return null;
        }
        //gets a specific booking with the corresponding number from the list
        public Booking GetBooking(int number)
        {
            foreach(Booking book in bookings)
            {
                if(book.BookingRefference == number)
                {
                    return book;
                }
            }
            return null;
        }
        //saves all the data to the corresponding xml files
        public void SaveData(string type)
        {
            switch(type)
            {
               
                    //saves the bookings
                case "booking":
                    {
                        XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Booking>));
                        //creates(or overwrites) the booking xml file
                        TextWriter WriteFileStream = new StreamWriter(@"../../../Data/Bookings.xml");
                        //writes the list of bookings to the file
                        SerializerObj.Serialize(WriteFileStream, bookings);
                        //closes the file
                        WriteFileStream.Close();
                        break;
                    }
                    //saves the customers
                case "customer":
                    {
                        XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Customer>));
                        //creates(or overwrites) the customer xml file
                        TextWriter WriteFileStream = new StreamWriter(@"../../../Data/Customers.xml");
                        //writes the list of customers to the file
                        SerializerObj.Serialize(WriteFileStream, customers);
                        //closes the file
                        WriteFileStream.Close();
                        break;
                    }
                
            }
        }
        
        //retrieves the data from the corresponding files
        public void GetData(string type)
        {
            switch(type)
            {
                //gets the customer list
                case "customer":
                    {
                        //checks if the file exists
                        if (File.Exists(@"../../../Data /Customers.xml"))
                        {
                            XmlSerializer serializerObj = new XmlSerializer(typeof(List<Customer>));
                            FileStream readFileStream = new FileStream(@"../../../Data /Customers.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                            //gets the list of customers from the file
                            customers = (List<Customer>)serializerObj.Deserialize(readFileStream);
                            readFileStream.Close();
                        }
                           
                        break;
                    }
                    //gets the booking list
                case "booking":
                    {
                        //checks if the file exists
                        if (File.Exists(@"../../../Data /Bookings.xml"))
                        {
                            XmlSerializer serializerObj = new XmlSerializer(typeof(List<Booking>));
                            FileStream readFileStream = new FileStream(@"../../../Data /Bookings.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                            //gets the list of bookings from the file
                            bookings = (List<Booking>)serializerObj.Deserialize(readFileStream);
                            readFileStream.Close();
                        }

                        break;
                    }
            }
        }
        //method for checking if a date interval clashes with already made bookings
        public bool DateClash(DateTime arr, DateTime dep, int chateau)
        {
            bool dateOverlap = false;
            //goes through every booking in the list
            foreach(Booking tempBooking in bookings)
            {
                //if a booking in the same chateu is found
                if(tempBooking.ChaletID == chateau)
                {
                    //if it's the same booking with a date interval included in the old one, then no need to check
                    if (arr >= tempBooking.Arrival && dep <= tempBooking.Departure && tempBooking.BookingRefference == currentBooking)
                        break;
                    //if it's a different booking check if the dates clash
                    //in order for them not to clash the max and min functions need to return the dates of the same range
                    dateOverlap = (Max(arr, tempBooking.Arrival) < Min(dep, tempBooking.Departure));
                    //if an overlap has been found, then you can't make the booking
                    if (dateOverlap)
                        break;                }
            }
            return dateOverlap;
        }
        //compares two dates and returns the smaller one
        private DateTime Min(DateTime departure1, DateTime departure2)
        {
            if (departure1 < departure2)
                return departure1;
            else return departure2;
        }

        //compares two dates and returns the later one
        private DateTime Max(DateTime arrival1, DateTime arrival2)
        {
            if (arrival1 > arrival2)
                return arrival1;
            else return arrival2;
        }

    }
}
