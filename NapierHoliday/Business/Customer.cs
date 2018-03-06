using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessObjects
{
    /* 
   * Author: Ovidiu - Andrei Radulescu
   * Customer class containing details of a customer, such as the name and address and their bookings
   * Last edited: 10.12.2017
   */
    [Serializable]
    public class Customer
    {
        //properties of customer
        private string name;
        private string address;
        private int customerNo;
        private List<int> custBookings = new List<int>();
       
        //empty constructor
        public Customer()
        {

        }
        //constructor that takes in the name, address and the customer number properties
        public Customer(int no, string n, string a)
        {
            this.Name = n;
            this.Address = a;
            this.CustomerNo = no;
        }

        //gets and sets the customer number
        public int CustomerNo
        { get { return customerNo; }
          set { customerNo = value; }
        }

        //gets and sets the name
        public string Name
        {
            get { return name; }
            set
            {
                //a try catch exception in case the entered name is empty or contains characters that are not letters
                if (value.Equals("") || !(value.All(c => Char.IsLetter(c) || c == ' ' || c == '-')))
                {
                    throw new ArgumentException("Name is incorrect");
                }
                name = value;
            }
        }
        //adds a booking's refference number to the customer's list of bookings
        public void AddBooking(int reff)
        {
            custBookings.Add(reff);
        }

        //gets and sets the address
        public string Address
        {
            get { return address; }
            set
            {
                //a try catch exception in case the entered address is empty or contains characters that are not appropiate
                if (value.Equals("") || !(value.All(c => Char.IsLetter(c) || Char.IsDigit(c) || c == ' ' || c == '/'))  )
                {
                    throw new ArgumentException("Address is incorrect");
                }
                address = value;
            }
        }
        //gets and sets the list of Bookings
        public List<int> Bookings
        {
            get { return custBookings; }
            set { custBookings = value; }
        }
    }
}
