using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects

{
    /* 
  * Author: Ovidiu - Andrei Radulescu
  * Guest class containing details of a customer, such as the name, address and age
  * Last edited: 10.12.2017
  */
    [Serializable]
    public class Guest
    {
        //properties of guest
        private string name;
        private string passportNo;
        private int age;

        //empty constructor
        public Guest() { }
        //constructor that takes all parameters
        public Guest(string n, string p, int a)
        {
            this.Name = n;
            this.PassportNo = p;
            this.Age = a;
        }
        //gets and sers the name
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

        //gets and sets the passport number
        public string PassportNo
        {
            get { return passportNo; }
            set
            {
                //a try catch exception in case the entered passport is empty
                //or contains characters that are not letters or digits
                if (value.Equals("") || !(value.All(c => Char.IsLetter(c) || Char.IsDigit(c)) ) )
                {
                    throw new ArgumentException("Passport is incorrect");
                }
                passportNo = value;
            }
        }
        //gets and sets the age
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

    }
}
