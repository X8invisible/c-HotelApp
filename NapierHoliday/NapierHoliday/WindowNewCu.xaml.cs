using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using Data;

namespace NapierHoliday
{
/* 
* Author: Ovidiu - Andrei Radulescu
* The New Customer window, which is also the start window, where you add a new customer.
* Last edited: 10.12.2017
*/
    public partial class WindowNewCu : Window
    {
        //properties of the window
        public DataHolderFacadeSingleton inst = DataHolderFacadeSingleton.Instance;
        public NumberFactorySingleton numberInst = NumberFactorySingleton.Instance;
        public WindowNewCu()
        {
            InitializeComponent();
            //gets the data from xmls
            inst.GetData("customer");
            inst.GetData("booking");
            //shows all customers on the data grid
            dgridCustomers.ItemsSource = inst.Customers;
            //updates the number factory in order to have the correct refference numbers for customers and bookings
            numberInst.UpdateNumbers("customer");
            numberInst.UpdateNumbers("booking");
            
        }

        //creates a new customer
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //gets the data and stores the customer
                string name = txtName.Text;
                string addr = txtAddr.Text;
                Customer cu = new Customer(numberInst.CustomerNo, name, addr);
                inst.AddCustomer(cu);
                inst.SaveData("customer");
                MessageBox.Show("New customer created successfully. Customer number is:" + cu.CustomerNo);
                dgridCustomers.Items.Refresh();
            }
            //exceptions from the customer class setters
            catch (Exception ep)
            {
                //rolls back the refference number used for the wrong customer
                numberInst.CustomerGoBack();
                MessageBox.Show(ep.Message);
            }
        }

        //goes to the edit customer window, when a customer is double clicked in a data grid
        private void dgridCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgridCustomers.SelectedIndex != -1)
            {
                var currCust = dgridCustomers.SelectedItem as Customer;
                //stores the current customer's number for easy access in the edit window
                inst.CurrentCustomer = currCust.CustomerNo;
                WindowEditCu edit = new WindowEditCu();
                edit.Show();
                this.Close();
            }
        }
    }
}
