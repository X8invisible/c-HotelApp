using BusinessObjects;
using Data;
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

namespace NapierHoliday
{
/* 
* Author: Ovidiu - Andrei Radulescu
* The Booking window, where the user creates or edits a booking
* Last edited: 10.12.2017
*/
    public partial class WindowBooking : Window
    {
        //properties of the window
        private DataHolderFacadeSingleton temp = DataHolderFacadeSingleton.Instance;
        //creates a new booking
        private Booking newBooking = new Booking();
        //keeps a list of the current guests
        private List<Guest> currGuests = new List<Guest>();
        //a flag for checking the booking doesn't clash with others
        private bool chateauCheck = false;
        private NumberFactorySingleton instance = NumberFactorySingleton.Instance;
        public WindowBooking()
        {
            InitializeComponent();
            //gets the customer the booking is being made for
            List<Customer> customers = temp.Customers;
            int cuNo = temp.CurrentCustomer;
            Customer currentCustomer = temp.GetCustomer(cuNo);
            //displays the customer's name on the screen
            lblDisplay.Content += currentCustomer.Name;
            //if the window was opened for editing a booking, get the data for it
            if(temp.CurrentBooking != 0)
            {
                newBooking = temp.GetBooking(temp.CurrentBooking);
                dateArrival.SelectedDate = newBooking.Arrival;
                dateDeparture.SelectedDate = newBooking.Departure;
                comboChaletNo.SelectedIndex = (newBooking.ChaletID)-1;
                if (newBooking.Breakfast)
                    chkBreakfast.IsChecked = true;
                if (newBooking.Evening)
                    chkEvening.IsChecked = true;
                if (newBooking.Hire)
                {
                    chkCar.IsChecked = true;
                    dateStart.SelectedDate = newBooking.Car.Start;
                    dateEnd.SelectedDate = newBooking.Car.End;
                    txtDriver.Text = newBooking.Car.Driver;
                }   
                //gets the guest details based on the passport number
                foreach(Guest guest in newBooking.GuestList)
                {
                        currGuests.Add(guest);
                }
                //sets the guest combo box count to match the number of guests
                if (currGuests.Count != 0)
                    comboGuests.SelectedIndex = (currGuests.Count) - 1;
            }
            //sets the data grid to display the guests
            gridGuests.ItemsSource = currGuests;

        }

        //method for adding a guest to a booking
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            //if the user hasn't selected how many guests, ask for the number
            if (comboGuests.SelectedIndex == -1)
                MessageBox.Show("Please specify the number of guests");
            //checks if the maximum of guests specified by the user has been reached
            else if (currGuests.Count < ((comboGuests.SelectedIndex) + 1))
            {
                //try catch block for adding a guest
                try
                {
                    //gets name,address and passport
                    string guestName = txtGName.Text;
                    string guestPassport = txtGPassport.Text;
                    int guestAge = Convert.ToInt32(txtGAge.Text);
                    //creates a new guest with the specified details
                    Guest tempGuest = new Guest(guestName, guestPassport, guestAge);
                    bool duplicateFlag = false;
                    foreach (Guest gu in currGuests)
                    {
                        if (gu.PassportNo == tempGuest.PassportNo)
                            duplicateFlag = true;
                    }
                    //checks if the guest hasn't been added before
                    if (!duplicateFlag)
                    {
                            currGuests.Add(tempGuest);
                            gridGuests.Items.Refresh();
                            txtGName.Clear();
                            txtGPassport.Clear();
                            txtGAge.Clear();

                    }
                    else MessageBox.Show("A guest with that passport already exists");
                }
                //error catch for incorrect age input
                catch (FormatException ef)
                {
                    MessageBox.Show("Input is in a wrong format", "Incorrect Input",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //error catch for custom errors implemented in the guest setters
                catch (Exception ep)
                {
                    MessageBox.Show(ep.Message);
                }
            }
            else MessageBox.Show("You have already added enough guests");

        }

        //checks that the user doesn't select fewer guests than already added
        private void comboGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((comboGuests.SelectedIndex) + 1) < currGuests.Count)
            {
                MessageBox.Show("You have already added more guests");
                comboGuests.SelectedIndex = ((currGuests.Count) - 1);
            }
        }

        //removes a guest from the booking
        private void btnRemGuest_Click(object sender, RoutedEventArgs e)
        {
            if(gridGuests.SelectedIndex != -1)
            {
                var guestDel = gridGuests.SelectedItem as Guest;
                currGuests.Remove(guestDel);
                if(temp.CurrentBooking !=0)
                    newBooking.GuestList.Remove(guestDel);
                gridGuests.Items.Refresh();
            }
                
        }

        //saves the booking
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           //checks if the user has verified the chateau is free for booking 
            if (!chateauCheck)
            {
                MessageBox.Show("Please check if selected chateau is available for booking", 
                    "No Chateau Selected", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            else
            {
                if ((bool)chkCar.IsChecked && !(dateStart.SelectedDate >= dateArrival.SelectedDate && dateEnd.SelectedDate <= dateDeparture.SelectedDate))

                    {
                        MessageBox.Show("Car hiring dates must be in the booking interval", "Incorrect date");
                    }
                else
                {
                    //stores the data to the booking
                    newBooking.Arrival = dateArrival.SelectedDate.Value;
                    newBooking.Departure = dateDeparture.SelectedDate.Value;
                    newBooking.ChaletID = ((comboChaletNo.SelectedIndex) + 1);
                    newBooking.Breakfast = (bool)chkBreakfast.IsChecked;
                    newBooking.Evening = (bool)chkEvening.IsChecked;
                    newBooking.Hire = (bool)chkCar.IsChecked;
                    foreach (Guest guest in currGuests)
                    {
                        if (newBooking.GuestList.IndexOf(guest) == -1)
                            newBooking.AddGuest(guest);
                    }
                    //if this is a new booking, add it to the lists
                    if (temp.CurrentBooking == 0)
                    {
                        newBooking.BookingRefference = instance.BookingRef;
                        List<Customer> customers = temp.Customers;
                        int cuNo = temp.CurrentCustomer;
                        Customer currentCustomer = temp.GetCustomer(cuNo);
                        currentCustomer.AddBooking(newBooking.BookingRefference);
                        temp.AddBooking(newBooking);
                    }
                    //saves the data since it has been modified
                    temp.SaveData("booking");
                    temp.SaveData("customer");
                    WindowNewCu customer = new WindowNewCu();
                    customer.Show();
                    this.Close();
                }
               

            }
        }

        //checks if the chateau is free for booking
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            //can't check if there's no chateau selected
            if (comboChaletNo.SelectedIndex == -1)
                MessageBox.Show("Please select a chateau", "No chateau selected",MessageBoxButton.OK,MessageBoxImage.Information);
            else
            {
                try
                {
                    //checks for date clashing using the method in the data holder class
                    chateauCheck = !(temp.DateClash(dateArrival.SelectedDate.Value, dateDeparture.SelectedDate.Value, ((comboChaletNo.SelectedIndex) + 1)));
                    if (chateauCheck)
                    {
                        MessageBox.Show("The selected chateau is available","Chateau Found", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                    else
                        MessageBox.Show("The selected chateau isn't available between those dates", "No Chateau Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (InvalidOperationException ee)
                {
                    MessageBox.Show("Date cannot be empty", "Empty Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        //calculates the cost of the booking
        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //gets the number of days the booking has been made for
                int days = (int)(dateDeparture.SelectedDate.Value - dateArrival.SelectedDate.Value).TotalDays;
                int breakfastPrice = 0;
                int eveningPrice = 0;
                int carPrice = 0;
                //sets the prices for extras if the user has selected any
                if ((bool)chkBreakfast.IsChecked)
                    breakfastPrice = 5;
                if ((bool)chkEvening.IsChecked)
                    eveningPrice = 10;
                if ((bool)chkCar.IsChecked)
                    //car prices are calculated on the start and end of hire dates
                    carPrice = 50 * (int)(newBooking.Car.End - newBooking.Car.Start).TotalDays;
                //calculates the whole total
                int total = (60 + (currGuests.Count) * 25 + (currGuests.Count) * eveningPrice + (currGuests.Count) * breakfastPrice) * days + carPrice;
                //prints the price
                txtPrice.Text = "£" + total.ToString();
            }
            catch (InvalidOperationException ee)
            {
                MessageBox.Show("Date cannot be empty", "Empty Date", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //opens a window for adding the car hiring
        private void chkCar_Checked(object sender, RoutedEventArgs e)
        {
            carHireGroup.IsEnabled = true;

        }

        //if the option has been unchecked, delete the hiring details
        private void chkCar_Unchecked(object sender, RoutedEventArgs e)
        {
            carHireGroup.IsEnabled = false;
            dateStart.SelectedDate = null;
            dateEnd.SelectedDate = null;
            txtDriver.Clear();
            newBooking.Car = new CarHire();
        }

        //closes the window without making modifications
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            WindowEditCu customer = new WindowEditCu();
            customer.Show();
            this.Close();
        }

        //confirms the details of a car hiring
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //checks if the car hire date coincides with the booking date
                if(dateStart.SelectedDate >= dateArrival.SelectedDate && dateEnd.SelectedDate <= dateDeparture.SelectedDate)
                {
                    //adds the car hire details to the booking
                    newBooking.Car.Start = dateStart.SelectedDate.Value;
                    newBooking.Car.End = dateEnd.SelectedDate.Value;
                    newBooking.Car.Driver = txtDriver.Text;
                }
                else
                {
                    MessageBox.Show("Car hiring dates must be in the booking interval", "Incorrect date");
                }
                
            }
            //try catch exceptions for wrong date and driver name
            catch (InvalidOperationException ee)
            {
                MessageBox.Show("Date cannot be empty", "Empty Date", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message, "Incorrect Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //rules for booking dates
        private void dateArrival_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //checks for arrival date to be earlier than departure
            if (dateDeparture.SelectedDate != null && dateArrival.SelectedDate > dateDeparture.SelectedDate)
            {
                MessageBox.Show("Arrival Date must be earlier than Departure Date", "Incorrect Date");
                dateArrival.SelectedDate = null;
            }
            //checks the arrival date to be later than today
            if (dateArrival.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("You can not book a date that has passed", "Date in the Past");
                dateArrival.SelectedDate = null;
            }
        }

        private void dateDeparture_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //checks for the departure to be later than arrival date
            if (dateArrival.SelectedDate != null && dateArrival.SelectedDate > dateDeparture.SelectedDate)
            {
                MessageBox.Show("Departure Date must be later than Arrival Date", "Incorrect Date");
                dateDeparture.SelectedDate = null;
            }
            //checks the arrival date to be later than today
            if (dateDeparture.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("You can not book a date that has passed", "Date in the Past");
                dateDeparture.SelectedDate = null;
            }

        }

        //rules for car hire dates
        private void dateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //checks the start date to be earlier than end date
            if (dateEnd.SelectedDate != null && dateStart.SelectedDate > dateEnd.SelectedDate)
            {
                MessageBox.Show("Start Date must be earlier than End Date", "Incorrect Date");
                dateStart.SelectedDate = null;
            }
            //checks the start date to be later than today
            if (dateStart.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("You can not book a date that has passed", "Date in the Past");
                dateStart.SelectedDate = null;
            }

        }

        private void dateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //checks end date to be later than start date
            if (dateStart.SelectedDate != null && dateStart.SelectedDate > dateEnd.SelectedDate)
            {
                MessageBox.Show("End Date must be later than Start Date", "Incorrect Date");
                dateEnd.SelectedDate = null;
            }
            //checks end date to be later than today
            if (dateEnd.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("You can not book a date that has passed", "Date in the Past");
                dateEnd.SelectedDate = null;
            }

        }

        //adds a guest to the corresponding fields for editing details
        private void gridGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(gridGuests.SelectedIndex != -1)
            {
                var guestEdit = gridGuests.SelectedItem as Guest;
                txtGName.Text = guestEdit.Name;
                txtGPassport.Text = guestEdit.PassportNo;
                txtGAge.Text = guestEdit.Age.ToString();
            }
        }

        //saves changes to a guest
        private void btnChangeGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest newGuest = new Guest(txtGName.Text, txtGPassport.Text, Convert.ToInt32(txtGAge.Text));
                //checks if a guest was selected for changing
                if (gridGuests.SelectedIndex != -1)
                {
                    //deletes old guest, adds the new one
                    var guestDelete = gridGuests.SelectedItem as Guest;
                    currGuests.Remove(guestDelete);
                    if (temp.CurrentBooking != 0)
                        newBooking.GuestList.Remove(guestDelete);
                    currGuests.Add(newGuest);
                    //clears the form
                    txtGName.Clear();
                    txtGPassport.Clear();
                    txtGAge.Clear();
                    gridGuests.Items.Refresh();

                }
                else
                {
                    MessageBox.Show("No guest selected for changing");
                }
            }
             //error catch for incorrect age input
            catch (FormatException ef)
            {
                MessageBox.Show("Input is in a wrong format", "Incorrect Input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //error catch for custom errors implemented in the guest setters
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }

        }
    }
}
