using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    /* 
     * Author: Ovidiu - Andrei Radulescu
     * Booking class containing details of a booking such as arrival, departure
     * Last edited: 10.12.2017
     */
    [Serializable]
    public class Booking
    {
        //properties of the booking class
        private int bookingRefference;
        private DateTime arrival;
        private DateTime departure;
        private int chaletID;
        private List<Guest> guestList = new List<Guest>();
        private bool breakfast = false;
        private bool evening = false;
        private bool hire = false;
        private CarHire car = new CarHire();

        //empty constructor(required for serialization)
        public Booking() { }
        //gets and sets the booking refference property
        public int BookingRefference
        { get { return bookingRefference; }
            set { bookingRefference = value; }
        }

        //gets and sets the arrival date property
        public DateTime Arrival
        {
            get { return arrival; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("No arrival date entered", "No date");
                }
                arrival = value;
            }
        }
        //gets and sets the departure date property
        public DateTime Departure
        {
            get { return departure; }
            set
            {
                //exception 
                if (value == null)
                {
                    throw new ArgumentException("No departure date entered", "No date");
                }
                departure = value;
            }
        }

        //gets and sets the chalet property
        public int ChaletID
        {
            get { return chaletID; }
            set
            {
                if(value >10 || value <=0)
                {
                    throw new ArgumentException("Chateau number must be between 1 and 10");
                }
                chaletID = value;
            }
        }
        //gets and sets the lists of guests property
        public List<Guest> GuestList
        {
            get { return guestList; }
            set { guestList = value; }
        }
        //gets and sets the car hire property(the actual objects that has the details)
        public CarHire Car
        {
            get { return car; }
            set { car = value; }
        }
        //gets and sets the breakfast property
        public bool Breakfast
        {
            get { return breakfast; }
            set { breakfast = value; }
        }
        //gets and sets the evening dinner property
        public bool Evening
        {
            get { return evening; }
            set { evening = value; }
        }
        //gets and sets the car hire property(if it exists or not) property
        public bool Hire
        {
            get { return hire; }
            set { hire = value; }
        }
        //adds a guest to the list
        public void AddGuest(Guest guest)
        {
            guestList.Add(guest);
        }
    }
}
