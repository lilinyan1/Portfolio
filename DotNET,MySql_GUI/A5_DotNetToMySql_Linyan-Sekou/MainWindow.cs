/*
* FILE:             MainWindow.cs
* PROJECT:          PROG2110 - Relational Databases - Assignment 5
* PROGRAMMER:       Becky Linyan Li & Sekou Gassama 
* AVAILABLE DATE:   11-14-2014
* DESCRIPTION:      This .NET application access a MySql database called northwind, which has to
 *                  be installed before running this program. It performs the following tasks:
 *                  1. update any customer record by customer ID
 *                  2. display all customer information, sorted by company name, contact name or city
 *                  3. create new customer information
 *                  4. delete customer records by customer ID
 *                  5. handle error appropriately
*
*/

//common namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// MySql namespaces
using MySql.Data;
using MySql.Data.MySqlClient;


/*
*   NAME    : A5_DotNetToMySql
*   PURPOSE : access to northwind database and perform required tasks
*/
namespace A5_DotNetToMySql
{
    /*
    *   NAME    : MainWindow
    *   PURPOSE : access to northwind database and perform required tasks
    */
    public partial class MainWindow : Form
    {
        // global variables which are used in many event handlers
        MySqlDataAdapter daCustomer;
        DataSet dsCustomer;
        MySqlConnection conn;

        public MainWindow()
        {
            InitializeComponent();
        }


        /*
        *   NAME    : MainWindow_Load
        *   PURPOSE : connect to database as the main window is loaded up
        */
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // connection configuration
            string connStr = "server=localhost;user=root;database=northwind;port=3306;password=Conestoga1;";
            conn = new MySqlConnection(connStr);
        }


        /*
        *   NAME    : MainWindow_Load
        *   PURPOSE : resize the data containter's width as the main window's width changes
        */
        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            dataGridView_displayAll.Width = (this.Width - 100);
            dataGridView_find.Width = (this.Width - 100);
            textBox_findMsg.Width = (this.Width - 100);
            tabControl.Width = this.Width;
        }


        /*
        *   NAME    : list_sortBy_SelectionChangeCommitted
        *   PURPOSE : apply different sort methods to the display of customers
        */
        private void list_sortBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (list_sortBy.SelectedItem.ToString() == "Company name")
            {
                try
                {
                    string sql = "SELECT * FROM customers ORDER BY CompanyName";
                    daCustomer = new MySqlDataAdapter(sql, conn);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(daCustomer);

                    dsCustomer = new DataSet();
                    daCustomer.Fill(dsCustomer, "customers");
                    dataGridView_displayAll.DataSource = dsCustomer;
                    dataGridView_displayAll.DataMember = "customers";
                }
                catch (Exception ex)
                {
                    textBox_insertMsg.Text = ex.ToString();
                }
            }
            else if (list_sortBy.SelectedItem.ToString() == "Contact name")
            {
                try
                {
                    string sql = "SELECT * FROM customers ORDER BY ContactName";
                    daCustomer = new MySqlDataAdapter(sql, conn);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(daCustomer);

                    dsCustomer = new DataSet();
                    daCustomer.Fill(dsCustomer, "customers");
                    dataGridView_displayAll.DataSource = dsCustomer;
                    dataGridView_displayAll.DataMember = "customers";
                }
                catch (Exception ex)
                {
                    textBox_insertMsg.Text = ex.ToString();
                }
            }
            else if (list_sortBy.SelectedItem.ToString() == "City")
            {
                try
                {
                    string sql = "SELECT * FROM customers ORDER BY City";
                    daCustomer = new MySqlDataAdapter(sql, conn);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(daCustomer);

                    dsCustomer = new DataSet();
                    daCustomer.Fill(dsCustomer, "customers");
                    dataGridView_displayAll.DataSource = dsCustomer;
                    dataGridView_displayAll.DataMember = "customers";
                }
                catch (Exception ex)
                {
                    textBox_insertMsg.Text = ex.ToString();
                }
            }
        }


        /*
        *   NAME    : button_find_Click
        *   PURPOSE : find customer by company name (partial or full match)
        */
        private void button_find_Click(object sender, EventArgs e)
        {
            // input validation
            if (textBox_find.Text.ToString().Trim() == "")
            {
                textBox_findMsg.Text = "Your entry shouldn't be empty";
            }
            else
            {
                // find customer in database
                textBox_findMsg.Text = "";
                string sql = "SELECT * FROM customers WHERE CompanyName LIKE '%" + textBox_find.Text.ToString() + "%'";
                try
                {

                    daCustomer = new MySqlDataAdapter(sql, conn);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(daCustomer);

                    dsCustomer = new DataSet();
                    daCustomer.Fill(dsCustomer, "customers");
                    dataGridView_find.DataSource = dsCustomer;
                    dataGridView_find.DataMember = "customers";

                }
                catch (Exception ex)
                {
                    textBox_findMsg.Text = ex.ToString();
                }
            }

        }


        /*
        *   NAME    : button_insert_Click
        *   PURPOSE : create new customer
        */
        private void button_insert_Click(object sender, EventArgs e)
        {
            // input validation
            bool isValid = true;
            //INSERT INTO customers VALUES('ALFKI', 'Alfreds Futterkiste', 'Maria Anders', 'Sales Representative', 'Obere Str. 57', 'Berlin', '', '12209', 'Germany', '030-0074321', '030-0076545');
            if (textBox_cstmrID.Text.Trim() == "")
            {
                textBox_insertMsg.Text = "The customer ID shouldn't be empty";
                isValid = false;
            }
            if (textBox_cstmrID.Text.Length > 5)
            {
                textBox_insertMsg.Text = "The length of the customer ID should be less than or equals to 5";
                isValid = false;
            }
            if (textBox_CmpName.Text.Length > 40)
            {
                textBox_insertMsg.Text = "The length of the company name should be less than or equals to 40";
                isValid = false;
            }
            if (textBox_CntctName.Text.Length > 30)
            {
                textBox_insertMsg.Text = "The length of the contact name should be less than or equals to 30";
                isValid = false;
            }
            if (textBox_cntctTitle.Text.Length > 30)
            {
                textBox_insertMsg.Text = "The length of the contact title should be less than or equals to 30";
                isValid = false;
            }
            if (textBox_address.Text.Length > 60)
            {
                textBox_insertMsg.Text = "The length of the address should be less than or equals to 60";
                isValid = false;
            }
            if (textBox_city.Text.Length > 15)
            {
                textBox_insertMsg.Text = "The length of the city should be less than or equals to 15";
                isValid = false;
            }
            if (textBox_region.Text.Length > 15)
            {
                textBox_insertMsg.Text = "The length of the region should be less than or equals to 15";
                isValid = false;
            }
            if (textBox_postalCode.Text.Length > 10)
            {
                textBox_insertMsg.Text = "The length of the postal code should be less than or equals to 10";
                isValid = false;
            }
            if (textBox_country.Text.Length > 15)
            {
                textBox_insertMsg.Text = "The length of the country should be less than or equals to 15";
                isValid = false;
            }
            if (textBox_phone.Text.Length > 24)
            {
                textBox_insertMsg.Text = "The length of the phone number should be less than or equals to 24";
                isValid = false;
            }
            if (textBox_fax.Text.Length > 24)
            {
                textBox_insertMsg.Text = "The length of the fax number should be less than or equals to 24";
                isValid = false;
            }

            // create customer in database
            if (isValid == true)
            {
                textBox_insertMsg.Text = "";

                string sql = "INSERT INTO customers VALUES('" + textBox_cstmrID.Text.ToString() + "','" + textBox_CmpName.Text.ToString() +
                    "','" + textBox_CntctName.Text.ToString() + "','" + textBox_cntctTitle.Text.ToString() + "','" + textBox_address.Text.ToString() + "','"
                    + textBox_city.Text.ToString() + "','" + textBox_region.Text.ToString() + "','" + textBox_postalCode.Text.ToString() + "','"
                     + textBox_country.Text.ToString() + "','" + textBox_phone.Text.ToString() + "','" + textBox_fax.Text.ToString() + "')";
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    //daCustomer = new MySqlDataAdapter(sql, conn);
                    //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCustomer);
                    MySqlCommand mysqlCommand = new MySqlCommand();
                    mysqlCommand.Connection = conn;
                    mysqlCommand.CommandText = sql;
                    mysqlCommand.ExecuteNonQuery();
                    textBox_insertMsg.Text = "Create new customer successfully";
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.ToString();
                    if (errorMsg.Contains("for key 'PRIMARY'"))
                    {
                        textBox_insertMsg.Text ="Repetitive customer ID is not allowed";
                    }
                    //textBox_insertMsg.Text = ex.ToString();
                    
                }
            }
        }


        /*
        *   NAME    : tabControl_Click
        *   PURPOSE : update the display of the customer list when switching tabs
        */
        private void tabControl_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM customers";
                daCustomer = new MySqlDataAdapter(sql, conn);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daCustomer);

                dsCustomer = new DataSet();
                daCustomer.Fill(dsCustomer, "customers");
                dataGridView_displayAll.DataSource = dsCustomer;
                dataGridView_displayAll.DataMember = "customers";
            }
            catch (Exception ex)
            {
                textBox_insertMsg.Text = ex.ToString();
            }
        }


        /*
        *   NAME    : button__updateByID_Click
        *   PURPOSE : find customer by entering their customer ID, unhide textboxes
        *            and retrieve existing customer information to prepare for updating
        */
        private void button__updateByID_Click(object sender, EventArgs e)
        {
            // input validation
            textBox_updateMsg.Text = "";
            string sql = "SELECT * FROM customers WHERE CustomerID = '" + textBox_updateByID.Text + "'";

            bool isValid = true;
            if (textBox_updateByID.Text.Trim() == "")
            {
                textBox_updateMsg.Text = "The customer ID shouldn't be empty";
                isValid = false;
            }
            if (textBox_updateByID.Text.Length > 5)
            {

                textBox_updateMsg.Text = "The length of the customer ID should be less than or equals to 5";
                isValid = false;
            }

            // find customer in database
            if (isValid == true)
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    MySqlCommand mysqlCommand = new MySqlCommand();
                    mysqlCommand.Connection = conn;
                    mysqlCommand.CommandText = sql;
                    MySqlDataReader reader = mysqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        // enable update textboxes
                        panel_update.Visible = true;

                        // reset values of update textboxes
                        textBox_updateCmpName.Text = "";
                        textBox_updateCntctName.Text = "";
                        textBox_updateCntctTitle.Text = "";
                        textBox_updateAdd.Text = "";
                        textBox_updateCity.Text = "";
                        textBox_updateRegion.Text = "";
                        textBox_updatePstlCode.Text = "";
                        textBox_updateCountry.Text = "";
                        textBox_updatePhone.Text = "";
                        textBox_updateFax.Text = "";

                        // set value to update textboxes
                        textBox_updateCmpName.Text = reader["CompanyName"].ToString();
                        textBox_updateCntctName.Text = reader["ContactName"].ToString();
                        textBox_updateCntctTitle.Text = reader["ContactTitle"].ToString();
                        textBox_updateAdd.Text = reader["Address"].ToString();
                        textBox_updateCity.Text = reader["City"].ToString();
                        textBox_updateRegion.Text = reader["Region"].ToString();
                        textBox_updatePstlCode.Text = reader["PostalCode"].ToString();
                        textBox_updateCountry.Text = reader["Country"].ToString();
                        textBox_updatePhone.Text = reader["Phone"].ToString();
                        textBox_updateFax.Text = reader["Fax"].ToString();
                    }
                    else
                    {
                        // disable textboxes for updates
                        panel_update.Visible = false;
                        textBox_updateMsg.Text = "No match";
                    }

                    // the current reader must be closed before the next read attempt
                    reader.Close();

                }
                catch (Exception ex)
                {
                    textBox_updateMsg.Text = ex.ToString();
                }
            }
        }


        /*
        *   NAME    : button_update_Click
        *   PURPOSE : update customer's information
        */
        private void button_update_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            if (textBox_updateCmpName.Text.Length > 40)
            {
                textBox_updateMsg.Text = "The length of the company name should be less than or equals to 40";
                isValid = false;
            }
            if (textBox_updateCntctName.Text.Length > 30)
            {
                textBox_updateMsg.Text = "The length of the contact name should be less than or equals to 30";
                isValid = false;
            }
            if (textBox_updateCntctTitle.Text.Length > 30)
            {
                textBox_updateMsg.Text = "The length of the contact title should be less than or equals to 30";
                isValid = false;
            }
            if (textBox_updateAdd.Text.Length > 60)
            {
                textBox_updateMsg.Text = "The length of the address should be less than or equals to 60";
                isValid = false;
            }
            if (textBox_updateCity.Text.Length > 15)
            {
                textBox_updateMsg.Text = "The length of the city should be less than or equals to 15";
                isValid = false;
            }
            if (textBox_updateRegion.Text.Length > 15)
            {
                textBox_updateMsg.Text = "The length of the region should be less than or equals to 15";
                isValid = false;
            }
            if (textBox_updatePstlCode.Text.Length > 10)
            {
                textBox_updateMsg.Text = "The length of the postal code should be less than or equals to 10";
                isValid = false;
            }
            if (textBox_updateCountry.Text.Length > 15)
            {
                textBox_updateMsg.Text = "The length of the country should be less than or equals to 15";
                isValid = false;
            }
            if (textBox_updatePhone.Text.Length > 24)
            {
                textBox_updateMsg.Text = "The length of the phone number should be less than or equals to 24";
                isValid = false;
            }
            if (textBox_updateFax.Text.Length > 24)
            {
                textBox_updateMsg.Text = "The length of the fax number should be less than or equals to 24";
                isValid = false;
            }

            if (isValid == true)
            {
                textBox_updateMsg.Text = "";

                //UPDATE customers SET CompanyName = 'abc',PostalCode ='N2M9K4' WHERE CustomerID = 12;

                string sql = "UPDATE customers SET CompanyName = '" + textBox_updateCmpName.Text + "', ContactName = '" +
                    textBox_updateCntctName.Text + "', ContactTitle = '" + textBox_updateCntctTitle.Text + "', Address = '" +
                     textBox_updateAdd.Text + "', City = '" + textBox_updateCity.Text + "', Region = '"
                      + textBox_updateRegion.Text + "', PostalCode = '" + textBox_updatePstlCode.Text + "', Country = '" +
                      textBox_updateCountry.Text + "', Phone = '" + textBox_updatePhone.Text + "', Fax = '" +
                      textBox_updateFax.Text + "' WHERE CustomerID = '" + textBox_updateByID.Text + "'";

                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    //daCustomer = new MySqlDataAdapter(sql, conn);
                    //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCustomer);
                    MySqlCommand mysqlCommand = new MySqlCommand();
                    mysqlCommand.Connection = conn;
                    mysqlCommand.CommandText = sql;
                    mysqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    //string errorMsg = ex.ToString();
                    //if (errorMsg.Contains("for key 'PRIMARY'"))
                    //{
                    //    textBox_insertMsg.Text = "Repetitive customer ID is not allowed";
                    //}
                    textBox_updateMsg.Text = ex.ToString();

                }
            }
        }


        /*
        *   NAME    : button_deleteByID_Click
        *   PURPOSE : delete customer's information by customer ID
        */
        private void button_deleteByID_Click(object sender, EventArgs e)
        {
            textBox_deleteMsg.Text = "";
            string sql = "DELETE FROM customers WHERE CustomerID = '" + textBox_deleteByID.Text + "'";

            bool isValid = true;
            if (textBox_deleteByID.Text.Trim() == "")
            {
                textBox_deleteMsg.Text = "The customer ID shouldn't be empty";
                isValid = false;
            }
            if (textBox_deleteByID.Text.Length > 5)
            {

                textBox_deleteMsg.Text = "The length of the customer ID should be less than or equals to 5";
                isValid = false;
            }

            if (isValid == true)
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    MySqlCommand mysqlCommand = new MySqlCommand();
                    mysqlCommand.Connection = conn;
                    mysqlCommand.CommandText = sql;
                    int numDeleted = mysqlCommand.ExecuteNonQuery();
                    if (numDeleted > 0)
                    {
                        textBox_deleteMsg.Text = "Record is deleted successfully";
                    }
                    else
                    {
                        textBox_deleteMsg.Text = "No record is deleted, the customer ID does not exist in the database";
                    }

                }
                catch (Exception ex)
                {
                    //string errorMsg = ex.ToString();
                    //if (errorMsg.Contains("for key 'PRIMARY'"))
                    //{
                    //    textBox_insertMsg.Text = "Repetitive customer ID is not allowed";
                    //}
                    textBox_deleteMsg.Text = ex.ToString();

                }
            }
        }
    }

}
