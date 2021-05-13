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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer;
using BusinessLayer;

namespace PresentationLayer
{
    /* 
     * Author:              Lewis Watson - 40432878
     * Description:         MainWinow Code for Methods Called
     * Date modified:       7/12/2020
    */
    public partial class MainWindow : Window
    {
        // Create Datafacade object
        DataFacade _dataFacade = new DataFacade();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ind_submit_Click(object sender, RoutedEventArgs e)
        {
            
            bool teltest = int.TryParse(inp_ind_tel.Text, out int integervalue);

            if (teltest)
            {
                if (inp_ind_name.Text != "" && inp_ind_tel.Text != "")
                {
                    _dataFacade.NewIndividual(inp_ind_name.Text, inp_ind_tel.Text);
                    MessageBox.Show($"New Individual Name: {inp_ind_name.Text}, Tel: {inp_ind_tel.Text}", "Individual Data Updated!");

                    inp_ind_name.Text = "";
                    inp_ind_tel.Text = "";
                }
                else
                {
                    MessageBox.Show($"Invalid Input", "Try Again");
                }
            } else
            {
                MessageBox.Show($"Invalid Telephone Number!", "Try Again");
            }

        }  

        private void loc_submit_Click(object sender, RoutedEventArgs e)
        {

            if (inp_loc_name.Text != "" && inp_loc_adr.Text != "")
            {
                _dataFacade.NewLocation(inp_loc_name.Text, inp_loc_adr.Text);
                MessageBox.Show($"New Location Name: {inp_loc_name.Text}, Address: {inp_loc_adr.Text}", "Location Data Updated!");
                inp_loc_name.Text = "";
                inp_loc_adr.Text = "";
            } else
            {
                MessageBox.Show($"Invalid Input", "Try Again");
            }
        }

        private void inp_ind_name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void inp_ind_tel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void imp_loc_adr_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void inp_loc_name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ll_contact_date.SelectedDate != null)
            {
                string[] ind = ll_individual.Text.Split(' ');
                string[] loc = li_location.Text.Split(' ');
                //MessageBox.Show($"{ind[0]}  {loc[0]}  {DateTime.Parse(ll_contact_date.Text)}", "DEBUG");
                _dataFacade.LogVisit(int.Parse(loc[0]), DateTime.Parse(ll_contact_date.Text), int.Parse(ind[0]));
                MessageBox.Show("Contact Event has been logged.", "Success");
                ll_contact_date.SelectedDate = null;

                li_location.SelectedIndex = -1;
                ll_individual.SelectedIndex = -1;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            q_output.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string[] ind = qry_ind_cmb.Text.Split(' ');
            //MessageBox.Show($"{ind[0]}  {DateTime.Parse(q_i_sdate.Text)}  {DateTime.Parse(q_i_edate.Text)}", "DEBUG");

            List<string> lines = _dataFacade.QueryContacts(DateTime.Parse(q_i_sdate.Text), DateTime.Parse(q_i_edate.Text), int.Parse(ind[0]));

            foreach (string line in lines)
            {
                q_output.Text += line;
            }
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void li_first_individual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(li_first_individual.SelectedIndex==-1))
            {
                if (li_first_individual.SelectedIndex == li_second_individual.SelectedIndex)
                {
                    li_second_individual.SelectedIndex = -1;
                    li_first_individual.SelectedIndex = -1;
                    MessageBox.Show($"Cannot select an individual twice...", "Try Again");
                }
            }
        }

        private void UpdateLiIndCmb(object sender, EventArgs e)
        {
            // Clear the combo box
            li_first_individual.Items.Clear();
            // Loop through each customer in the customer list
            foreach (Individual ind in _dataFacade.GetInds())
            {
                // Add the current customer to the combo box list of customers
                li_first_individual.Items.Add($"{ind.Individual_id} - {ind.Name}");
            }
        }

        private void UpdateLiInd2Cmb(object sender, EventArgs e)
        {
            // Clear the combo box
            li_second_individual.Items.Clear();
            // Loop through each customer in the customer list
            foreach (Individual ind in _dataFacade.GetInds())
            {
                // Add the current customer to the combo box list of customers
                li_second_individual.Items.Add($"{ind.Individual_id} - {ind.Name}");
            }
        }

        private void ll_individual_cmb(object sender, EventArgs e)
        {
            // Clear the combo box
            ll_individual.Items.Clear();
            // Loop through each customer in the customer list
            foreach (Individual ind in _dataFacade.GetInds())
            {
                // Add the current customer to the combo box list of customers
                ll_individual.Items.Add($"{ind.Individual_id} - {ind.Name}");
            }
        }

        private void li_location_cmb(object sender, EventArgs e)
        {
            // Clear the combo box
            li_location.Items.Clear();
            // Loop through each customer in the customer list
            foreach (Location loc in _dataFacade.GetLocs())
            {
                // Add the current customer to the combo box list of customers
                li_location.Items.Add($"{loc.Location_id} - {loc.Name}");
            }
        }

        private void qry_ind_cmb_open(object sender, EventArgs e)
        {
            // Clear the combo box
            qry_ind_cmb.Items.Clear();
            // Loop through each customer in the customer list
            foreach (Individual ind in _dataFacade.GetInds())
            {
                // Add the current customer to the combo box list of customers
                qry_ind_cmb.Items.Add($"{ind.Individual_id} - {ind.Name}");
            }
        }

        private void qry_loc_cmb_open(object sender, EventArgs e)
        {
            // Clear the combo box
            qry_loc_cmb.Items.Clear();
            // Loop through each customer in the customer list
            foreach (Location loc in _dataFacade.GetLocs())
            {
                // Add the current customer to the combo box list of customers
                qry_loc_cmb.Items.Add($"{loc.Location_id} - {loc.Name}");
            }
        }

        private void changedselection_log_second(object sender, SelectionChangedEventArgs e)
        {
            if (!(li_second_individual.SelectedIndex == -1))
            {
                if (li_first_individual.SelectedIndex == li_second_individual.SelectedIndex)
                {
                    li_second_individual.SelectedIndex = -1;
                    li_first_individual.SelectedIndex = -1;
                    MessageBox.Show($"Cannot select an individual twice...", "Try Again");
                }
            }
        }

        private void btn_log_contact_Click(object sender, RoutedEventArgs e)
        {

            if (li_contact_date.SelectedDate != null)
            {
                string[] ind1 = li_first_individual.Text.Split(' ');
                string[] ind2 = li_second_individual.Text.Split(' ');
                //MessageBox.Show($"{ind1[0]}  {ind2[0]}  {DateTime.Parse(li_contact_date.Text)}", "DEBUG");
                _dataFacade.LogContact(int.Parse(ind2[0]), DateTime.Parse(li_contact_date.Text), int.Parse(ind1[0]));
                MessageBox.Show("Contact Event has been logged.", "Success");
                li_contact_date.SelectedDate = null;
            }

        }

        private void q_output_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_q_loc_Click(object sender, RoutedEventArgs e)
        {
            string[] loc = qry_loc_cmb.Text.Split(' ');
            //MessageBox.Show($"{loc[0]}  {DateTime.Parse(q_l_sdate.Text)}  {DateTime.Parse(q_l_edate.Text)}", "DEBUG");

            List<string> lines = _dataFacade.QueryLocation(DateTime.Parse(q_l_sdate.Text), DateTime.Parse(q_l_edate.Text), int.Parse(loc[0]));

            foreach (string line in lines)
            {
                q_output.Text += line;
            }
        }
    }
}
