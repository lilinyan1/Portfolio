/**
* \file	SeasonalEmployee.cs
* \author Ab-Code: Sekou Diaby, Tuan Ly, Becky Linyan Li, Bowen Zhuang
* \date 21-11-2014
* \brief contains all the methods and attributes to handle a Seasonla Employee. 
* \brief The Seasonal Employee is the child class of the parent Employee clas.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AllEmployees
{
    ///
    /// \class SeasonalEmployee
    /// \brief to model the attributes and behaviour of an SeasonalEmployee, 
    /// \brief accessor and mutators for the attributes, validates all the fields of the employee and  
    /// \biref shows the details of the SeasonalEmployee.
    ///
    public class SeasonalEmployee : Employee
    {
        /* ====================================== */
        /*              PRIVATE                   */
        /* ====================================== */
        /* -------------- ATTRIBUTES ------------ */
        private string season;                      ///< \brief the season of work for the employee
        private float piecePay;                     ///< \brief the Peice pay for the employee

        /* ====================================== */
        /*              PUBLIC                   */
        /* ====================================== */
        ///
        /// \brief This function is the constructor of the SeasonalEmployee and it set the initial value for data members to blank
        /// \details <b>Details</b>
        /// \param  no param
        /// \return no returns
        ///
        public SeasonalEmployee()
        {
             bool empTypeRes = SetemployeeType(SEASONAL);
             if (empTypeRes)
             {
                 season = "";
                 piecePay = 0;
             }
        }

        ///
        /// \brief This function is the constructor of the SeasonalEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param newSeason - <b>string </b> -the season of work for the employee
        /// \param newPiecePay - <b>float </b> -the Peice pay for the employee
        /// /// \return no returns
        ///
        public SeasonalEmployee(string newSeason, float newPiecePay)
        {
             bool empTypeRes = SetemployeeType(SEASONAL);
             if (empTypeRes)
             {
                 season = newSeason;
                 piecePay = newPiecePay;
             }
        }

        ///
        /// \brief This function is the constructor of the SeasonalEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param fName - <b>string </b> -the first name for the employee
        /// \param lName - <b>string </b> -the last name for the employee
        /// /// \return no returns
        ///
        public SeasonalEmployee(string fName, string lName)
        {
            bool empTypeRes = SetemployeeType(SEASONAL);
             if (empTypeRes)
             {
                 firstName = fName;
                 lastName = lName;
             }
        }

        ///
        /// \brief This function sets the employee's work season to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newSeason  - <b>string</b> - The season of work for the employee
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool Setseason(string newSeason)
        {
            bool ret = false;
            if (newSeason != "")
            {
                //string allSeason = "FALL WINTER SUMMER SPRING";
                if (newSeason.ToUpper() == "FALL")
                {
                    season = newSeason;
                    ret = true;
                }
                else if (newSeason.ToUpper() == "WINTER")
                {
                    season = newSeason;
                    ret = true;
                }
                else if (newSeason.ToUpper() == "SUMMER")
                {
                    season = newSeason;
                    ret = true;
                }
                else if (newSeason.ToUpper() == "SPRING")
                {
                    season = newSeason;
                    ret = true;
                }
            }
            else
            {
                season = newSeason;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function sets the employee's Piece Pay to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newPiecePay  - <b>float</b> - The pay for the employee
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetpiecePay(float newPiecePay)
        {
            bool ret = false;
            if (newPiecePay >= 0)
            {
                piecePay = newPiecePay;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function gets the Seasonal employee's season and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  season - <b>string</b> -The season of the created Seasonal employee object.
        ///
        public string Getseason()
        {
            return season;
        }

        ///
        /// \brief This function gets the Seasonal employee's piecePay and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  piecePay - <b>float</b> -The piecePay of the created Seasonal employee object.
        ///
        public float GetpiecePay()
        {
            return piecePay;
        }

        ///
        /// \brief This function validates all the fields of a seasonal time employee
        /// \details <b>Details</b> -It will validate all the parents attributes, 
        ///                          than it validates all the attributes specific 
        ///                          to the class. If all field are valid, it return  
        ///                          a bool and set the employee to be valid.
        ///
        /// \param no parameters
        /// \return  ret - <b>bool</b> -The result to see if the seasonal employee is valid.
        ///
        public override bool Validate()
        {
            string exceptions = "";
            bool ret = true;
            if (employeeType != SEASONAL)
            {
                ret = false;
                exceptions += "\nThe employee must be a Seasonal employee\n";
            }            // First name cannot be blank to be a valid Seasonal time employee
            if (firstName != "")
            {
                // Seasonal employee's First name must only contain aplhabets, hyphen and dashes
                if (Regex.IsMatch(firstName, "[^a-zA-Z-']"))
                {
                    exceptions += "First Name must only contain alphabets, hyphen and dashes\n";
                    ret = false;
                }
            }
            else
            {
                exceptions += "First Name must not be empty and must only contain alphabets, hyphen and dashes\n";
                ret = false;
            }
            // Last name cannot be blank to be a valid Seasonal time employee
            if (lastName != "")
            {
                // Full time employee's Last name must only contain aplhabets, hyphen and dashes
                if (Regex.IsMatch(lastName, "[^a-zA-Z-']"))
                {
                    exceptions += "Last Name must only contain alphabets, hyphen and dashes\n";
                    ret = false;
                }
            }
            else
            {
                exceptions += "Last Name must not be empty and must only contain alphabets, hyphen and dashes\n";
                ret = false;
            }

            //Date of birth must not be empty to be a valid Seasonal employee
            if (dateOfBirth != "")
            {
                bool isDateValid = validateDate(dateOfBirth);
                if (!isDateValid)
                {
                    exceptions += "The date of birth is not a valid date\n";
                    ret = false;
                }
            }
            else
            {
                exceptions += "The date of birth is not a valid date\n";
                ret = false;
            }

            // Seasonal employee's SIN must be valid
            bool isFTSINValid = validateSIN();
            if (!isFTSINValid)
            {
                exceptions += "The SIN is not valid\n";
                ret = false;
            }
            if (piecePay <= 0)
            {
                exceptions += "The Piece pay cannot be Negative\n";
                ret = false;
            }
            bool isDigitPresent = season.Any(c => char.IsDigit(c));
            if (season == "" || isDigitPresent == true)
            {
                exceptions += "The Season provide is not a valid season\n";
                ret = false;
            }
            string allSeason = "FALL WINTER SUMMER SPRING";
            if (!(allSeason.Contains(season.ToUpper())))
            {
                exceptions += "The Season provided is not a valid season\n";
                ret = false;
            }
            if (dateOfBirth != "")
            {
                int birth = Convert.ToInt32(dateOfBirth.Substring(0, 4));
                if (birth > 2014)
                {
                    ret = false;
                    exceptions += "Employee cannot be born on a date that has not occured\n";
                }
            }

            if (ret)
                isValidEmp = true;
            else
            {
                isValidEmp = false;
                throw new Exception(exceptions);
            } 

            return ret;
        }

        ///
        /// \brief This function displays all attributes of an employee. 
        /// \details <b>Details</b> -The child function is displays all
        ///                          the attributes and displays if the 
        ///                          Seasonal employee is valid.
        /// \param   no params
        /// \return  no returns
        ///
        public override void Details()
        {
            // display employee details
            string type = "";
            if (employeeType == SEASONAL)
            {
                type = "Seasonal";
            }
            else
            {
                type += employeeType;
            }
            Console.WriteLine("\n************************************************************");
            Console.WriteLine("                 EMPLOYEE DETAILS  ");
            Console.WriteLine("                 Is Employee Valid ? {0}", isValidEmp);
            Console.WriteLine("*************************************************************");
            Console.WriteLine("                         |");
            Console.WriteLine(" Employee Type           | {0}", type);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" First Name              | {0}", firstName);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Last Name               | {0}", lastName);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Date Of Birth           | {0}", dateOfBirth);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Social Insurance Number | {0}", socialInsuranceNumber);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Season                  | {0}", season);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Piece Pay               | {0}", piecePay);
            Console.WriteLine("                         |");
            Console.WriteLine("************************************************************\n");
            /*Log in file after details*/
        }

        ///
        /// \brief This function puts all attributes of an employee into a string to be logged in the logfile. 
        /// \details <b>Details</b> - concatenates if the employee is valid, all
        ///                          the attributes and sends them to the log file 
        ///                          
        /// \param   no params
        /// \return  no returns
        ///
        public override string ToString()
        {
            string empStr = "";
            string type = "";
            if (employeeType == SEASONAL)
            {
                type = "Seasonal";
            }
            else
            {
                type += employeeType;
            }
            empStr += "\r\n************************************************************\r\n";
            empStr += "                 EMPLOYEE DETAILS  \r\n";
            empStr += "                 Is Employee Valid ?" + isValidEmp + "\r\n";
            empStr += "*************************************************************\r\n";
            empStr += "                         |\r\n";
            empStr += " Employee Type           | " + type + "\r\n";
            empStr += "                         |\r\n";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |\r\n";
            empStr += " First Name              | " + firstName + "\r\n";
            empStr += "                         |\r\n";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |\r\n";
            empStr += " Last Name               | " + lastName + "\r\n";
            empStr += "                         |\r\n";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |\r\n";
            empStr += " Date Of Birth           | " + dateOfBirth + "\r\n";
            empStr += "                         |\r\n";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |\r\n";
            empStr += " Social Insurance Number | " + socialInsuranceNumber + "\r\n";
            empStr += "                         |\r\n";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |";
            empStr += " Season                  | " + season + "\r\n";
            empStr += "                         |";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |";
            empStr += " Piece Pay               | "+ piecePay+ "\r\n";
            empStr += "                         |";
            empStr += "************************************************************\r\n";
            return empStr;
        }
       
    }
}
