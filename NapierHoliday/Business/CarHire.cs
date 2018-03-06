using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    /* 
     * Author: Ovidiu - Andrei Radulescu
     * Car Hiring class containing details of a car hire, such as the start and end dates and driver name
     * Last edited: 10.12.2017
     */
    [Serializable]
    public class CarHire
    {
        //properties of the car hire
        private DateTime start;
        private DateTime end;
        private string driver;

        //empty constructor
        public CarHire() { }

        //constructor that takes all the parameters
        public CarHire(DateTime s, DateTime e, string d)
        {
            this.Start = s;
            this.End = e;
            this.Driver = d;
        }
        //gets and sets the start date
        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }
        //gets and sets the end date
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }
        //gets and sets the Driver's name
        public string Driver
        {
            get { return driver; }
            set
            {
                //a try catch exception in case the entered name is empty or contains characters that are not letters
                if (value.Equals("") || !(value.All(c => Char.IsLetter(c) || c == ' ' || c == '-')))
                {
                    throw new ArgumentException("Name is incorrect");
                }
                driver = value;
            }
        }
    }
}
