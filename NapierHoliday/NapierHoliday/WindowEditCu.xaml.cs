using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Data;
using BusinessObjects;

namespace NapierHoliday
{
   /* 
 * Author: Ovidiu - Andrei Radulescu
 * The Edit Customer window, where the user edits/deletes a customer's details and adds/edits/deletes bookings
 * Last edited: 10.12.2017
 */
    public partial class WindowEditCu : Window
    {
        //propertires of the window
        private DataHolderFacadeSingleton instance = DataHolderFacadeSingleton.Instance;
        private NumberFactorySingleton numbers = NumberFactorySingleton.Instance;
        private List<Booking> bookings = new List<Booking>();
        private Customer currentCustomer;

        public WindowEditCu()
        {
            InitializeComponent();
            //gets the up to date data from the xml
            instance.GetData("customer");
            instance.GetData("booking");
            //gets the customer list and selects the customer that needs to be edited
            List<Customer> customers = instance.Customers;
            int cuNo = instance.CurrentCustomer;
            currentCustomer = instance.GetCustomer(cuNo);
            //gets all the bookings of the current customer from the list of bookings
            foreach(Booking temp in instance.Bookings)
            {
                bool exists = currentCustomer.Bookings.IndexOf(temp.BookingRefference) != -1;
                if (exists)
                {
                    bookings.Add(temp);
                }
            }
            //puts the name of the customer in the text fields, if the user chooses to edit them
            txtName.Text = currentCustomer.Name;
            txtAddr.Text = currentCustomer.Address;
            //puts all the bookings in the data grid
            dgridBookings.ItemsSource = bookings;
        }

        //edits a customer details
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Customer> customers = instance.Customers;
                int cuNo = instance.CurrentCustomer;
                Customer currentCustomer = instance.GetCustomer(cuNo);
                currentCustomer.Name = txtName.Text;
                currentCustomer.Address = txtAddr.Text;
                instance.SaveData("customer");
                MessageBox.Show("Changes saved succesfully", "Changes saved");
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }

        }

        //makes a new booking
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //points to the fact that we're making a new booking, and not editing one
            instance.CurrentBooking = 0;
            //opens the booking window, then closes this one
            WindowBooking booking = new WindowBooking();
            booking.Show();
            this.Close();
        }

        //deletes a selected booking from the data grid
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            //checks if a booking was selected
            if(dgridBookings.SelectedIndex != -1)
            {
                //gets the booking from the grid and removes it from the booking list, and customer's list
                var bookingDel = dgridBookings.SelectedItem as Booking;
                bookings.Remove(bookingDel);
                List<Customer> customers = instance.Customers;
                int cuNo = instance.CurrentCustomer;
                Customer currentCustomer = instance.GetCustomer(cuNo);
                currentCustomer.Bookings.Remove(bookingDel.BookingRefference);
                instance.Bookings.Remove(bookingDel);
                //saves the data
                instance.SaveData("customer");
                instance.SaveData("booking");
                numbers.UpdateNumbers("booking");
                dgridBookings.Items.Refresh();

            }
        }

        //opens the booking window for editing a booking, when a booking is double clicked
        private void dgridBookings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgridBookings.SelectedIndex != -1)
            {
                //gets the booking to be edited
                var currBooking = dgridBookings.SelectedItem as Booking;
                //stores it's refference number for easy finding in the booking window
                instance.CurrentBooking = currBooking.BookingRefference;
                WindowBooking booking = new WindowBooking();
                booking.Show();
                this.Close();
            }
            
        }

        //this is a button for going back to the main window
        private void btnArrowBack_Click(object sender, RoutedEventArgs e)
        {
            WindowNewCu back = new WindowNewCu();
            back.Show();
            this.Close();
        }

        //a button for deleting a customer
        private void btnDelCu_Click(object sender, RoutedEventArgs e)
        {
            //the customer must be deleted only if the customer has no bookings
            if(currentCustomer.Bookings.Count() != 0)
            {
                MessageBox.Show("You can only delete a customer that has no bookings", "Customer has bookings");
            }
            else
            {
                instance.Customers.Remove(currentCustomer);
                instance.SaveData("customer");
                WindowNewCu cust = new WindowNewCu();
                cust.Show();
                this.Close();
            }
        }
    }
}
