/**
* \file	Employee.cs
* \author Ab-Code: Sekou Diaby, Tuan Ly, Becky Linyan Li, Bowen Zhuang
* \date 21-11-2014
* \brief contains all the child classes of Employee. 
* \brief The Employee is the parent class where all the child class are derived from
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime;  

namespace AllEmployees
{
    ///
    /// \class Employee
    /// \brief to model the attributes and behaviour of an Employee, 
    /// \brief accessor and mutators for the attributes, validates 
    /// \brief the social insurance number, the date and prints out 
    /// \biref the details of the employee.
    ///
    public class Employee
    {
        /* ====================================== */
        /*              Protected                 */
        /* ====================================== */
        /* -------------- ATTRIBUTES ------------ */
        protected char employeeType;                ///< \brief the type of the employee
        protected string firstName;                 ///< \brief the first name of the employee
        protected string lastName;                  ///< \brief the last name of the employee
        protected string socialInsuranceNumber;     ///< \brief the social insurance number of the employee
        protected string dateOfBirth;               ///< \brief the date of birth of the employee
        protected bool isValidEmp;                  ///< \brief the validity employee

        public const char FULL_TIME = 'F';
        public const char PART_TIME = 'P';
        public const char SEASONAL = 'S';
        public const char CONTRACT = 'C';

        /* ====================================== */
        /*              PUBLIC                   */
        /* ====================================== */
        ///
        /// \brief This function is the constructor of the parent Employee and it set the initial value for data members to blank
        /// \details <b>Details</b> 
        /// \param  no param
        /// \return no returns
        ///
        public Employee()
        {
            isValidEmp = false;
            employeeType = '\0';
            firstName = "";
            lastName = "";
            socialInsuranceNumber = "";
            dateOfBirth = "";
        }

        ///
        /// \brief This function is the constructor of the Employee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param eType - <b>char </b> -the type of employee
        /// \param fName - <b>string </b> -the first name of the employee
        /// \param lName - <b>string </b> -the last name of the employee
        /// \param SIN - <b>string </b> -the social insurance number of the employee
        /// \param DOB - <b>string </b> -the date of birth of the employee
        /// /// \return no returns
        ///
        public Employee(char eType, string fName, string lName, string SIN, string DOB)
        {
            employeeType = eType;
            firstName = fName;
            lastName = lName;
            socialInsuranceNumber = SIN ;
            dateOfBirth = DOB;
            isValidEmp = false;
        }

        ///
        /// \brief This function set the employee type to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param empType  - <b>char</b> the emloyee type   
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetemployeeType(char empType)
        {
            bool ret = false;
            // Employee type cannot be null
            if (empType != '\0')
            {
                // Employee type must be one of the four employee type
                if (empType == FULL_TIME || empType == PART_TIME || empType == CONTRACT || empType == SEASONAL)
                {
                    employeeType = empType;
                    ret = true;
                }
            }
            return ret;
        }

        ///
        /// \brief This function set the employee first name to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param fName - <b>string </b> -the first name of the employee
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        /// 
        public bool SetfirstName(string fName)
        {
            bool ret = false;
            // if firstname is not empty
            if (fName != "")
            {
                //First name may only contain alphbates upper and lower case, apostrophe and hyphen
                if (!Regex.IsMatch(fName, "[^a-zA-Z-']"))
                {
                    firstName = fName;
                    ret = true;
                }
            }
            else
            {
                //firstname can be blank
                firstName = fName;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function set the employee last name to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param lName - <b>string </b> -the last name of the employee
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///       
        public bool SetlastName(string lName)
        {
            bool ret = false;
            if (lName != "")
            {
                //Last name may only contain alphbates upper and lower case, apostrophe and hyphen
                if (!Regex.IsMatch(lName, "[^a-zA-Z-']"))
                {
                    lastName = lName;
                    ret = true;
                }
            }
            else
            {
                //last Name can be blank
                lastName = lName;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function set the employee's social insurance number to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param SIN  - <b>string</b> - the social insurance number of the employee  
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set. 
        ///
        public bool SetsocialInsuranceNumber(string SIN)
        {
            bool ret = false;
            // if SIn is not empty 
            if (SIN != "")
            {
                //if SIN contains 9 numeric digits
                MatchCollection matches = Regex.Matches(SIN, "[0-9]");
                if (matches.Count == 9)
                {
                    socialInsuranceNumber = SIN;
                    ret = true;
                }
            }
            else
            {
                //SIN can be blank
                socialInsuranceNumber = SIN;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function set the employee's date of birth to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param DOB  - <b>string</b> - the date of birth of the employee.
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetdateOfBirth(string DOB)
        {
            bool ret = false;
            //if Date of birth is not blank
            if (DOB != "")
            {
                // Date of birth can contain NA
                if (DOB == "N/A")
                {
                    dateOfBirth = DOB;
                    ret = true;
                }
                else
                {
                    //if Date of birth is a valid date
                    if ( validateDate(DOB))
                    {
                        dateOfBirth = DOB;
                        ret = true;
                    }
                }
            }
            else
            {
                // Date of birth can blank
                dateOfBirth = DOB;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function gets the employeetype and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  employeeType - <b>char</b> -The employee type of the created employee object.
        ///
        public char GetemployeeType()
        {
            return employeeType;
        }

        ///
        /// \brief This function gets the employee's dateOfBirth and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  dateOfBirth - <b>string</b> -The dateOfBirth of the created employee object.
        ///
        public string GetdateOfBirth()
        {
            return dateOfBirth;
        }

        ///
        /// \brief This function gets the employee's socialInsuranceNumber and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  socialInsuranceNumber - <b>string</b> -The socialInsuranceNumber of the created employee object.
        ///
        public string GetsocialInsuranceNumber()
        {
            return socialInsuranceNumber;
        }

        ///
        /// \brief This function gets the employee's lastName and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  lastName - <b>string</b> -The lastName of the created employee object.
        ///
        public string GetlastName()
        {
            return lastName;
        }

        ///
        /// \brief This function gets the employee's firstName and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  firstName - <b>string</b> -The firstName of the created employee object.
        ///
        public string GetfirstName()
        {
            return firstName;
        }

        ///
        /// \brief This function validates the date passed in as a parameter. 
        /// \details <b>Details</b> -The function checks if the date is not empty, than checks 
        ///                          if the date is the right format. It finally checks if the date 
        ///                          is valid before return the result of the vlaidation
        /// \param date  - <b>string</b> - the date provided to be validated.
        /// \return  ret - <b>bool</b> -The result of the date validation.
        ///
        public bool validateDate(string date)
        {
            bool ret = false;
            if ((date != "" || date != "N/A")&& date.Length == 10)
            {
                DateTime p;
                string pattern = "yyyy-mm-dd";
                // validating format first
                if (DateTime.TryParseExact(date, pattern, null, DateTimeStyles.None, out p))
                {
                    DateTime temp;
                    //validate date value
                    if (DateTime.TryParse(date, out temp))
                    {
                        ret = true;
                    }
                    else
                    {
                        int year = Int16.Parse((date.Substring(0,4)));
                        if (DateTime.IsLeapYear(year))
                        {
                            ret = true;
                        }
                    }
                }
                //put date in right format
                else
                {
                    //remove all unwanted characters and add space for format
                    date = Regex.Replace(socialInsuranceNumber, "[^0-9./-]", "");
                    DateTime temp;
                    if (date.Length == 10)
                    {
                        //validate date value
                        if (DateTime.TryParse(date, out temp))
                        {
                            CultureInfo cultures = CultureInfo.CreateSpecificCulture("de-DE");
                            date = temp.ToString("u", cultures);
                            ret = true;
                        }
                        else
                        {
                            int year = Int16.Parse((date.Substring(6, 4)));
                            if (DateTime.IsLeapYear(year))
                            {
                                ret = true;
                            }
                        }
                    }

                }
            }
            return ret;
        }

        ///
        /// \brief This function validates the social insurance number passed in as a parameter. 
        /// \details <b>Details</b> -The function checks if the social insurance number is not empty, than checks 
        ///                          if the social insurance number is the right format. if it is not in the right format
        ///                          it puts the SIN in the right format and It finally checks if the social insurance number 
        ///                          is valid before return the result of the validation
        /// \param   no params
        /// \return  ret - <b>bool</b> -The result of the date validation.
        ///
        public virtual bool validateSIN()
        {
            bool ret = false;
            if (socialInsuranceNumber != "")
            {
                //remove all unwanted characters and add space for format
                socialInsuranceNumber = Regex.Replace(socialInsuranceNumber, "[^0-9]", "");
                //if SIN is not empty and is the right length 
                if (socialInsuranceNumber.Length == 9 && socialInsuranceNumber != "" )
                {
                    socialInsuranceNumber = socialInsuranceNumber.Insert(3, " ");
                    socialInsuranceNumber = socialInsuranceNumber.Insert(7, " ");

                    char checkDigit = socialInsuranceNumber.ElementAt(socialInsuranceNumber.Length - 1);
                    int sumOfAlt1 = 0, sumOfAlt2 = 0, sumOfAlt3 = 0, sumOfAlt4 = 0;
                    // getting the sum of the alternates
                    sumOfAlt1 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(1))) * 2;
                    sumOfAlt2 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(4))) * 2;
                    sumOfAlt3 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(6))) * 2;
                    sumOfAlt4 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(9)))* 2;

                    int  totalOfSums = 0;
                    //getting the single digits to add 
                    string intToAdd = sumOfAlt1.ToString() + sumOfAlt2.ToString() + sumOfAlt3.ToString() + sumOfAlt4.ToString()
                        + socialInsuranceNumber.ElementAt(0).ToString() + socialInsuranceNumber.ElementAt(2).ToString() +
                                socialInsuranceNumber.ElementAt(5).ToString() + socialInsuranceNumber.ElementAt(8).ToString();
                    //adding all the single digits to obtain from the sum of the first alternates 
                    // to the sum of the secong alternates.
                    foreach (char c in intToAdd)
                    {
                        totalOfSums = totalOfSums + ((int)Char.GetNumericValue(c));
                    }                   
                    if (totalOfSums % 10 == 0)
                    {
                        if (checkDigit == '0')
                        {
                            ret = true;
                        }
                    }
                    else
                    {
                        //getting the next larger number ending with zero larger
                        string temp = totalOfSums.ToString();
                        int x = (int)Char.GetNumericValue(temp.ElementAt(0));
                        x = (x + 1) * 10;
                        // if the check digit equals to the substraction of the total sum from the next highest
                        // number ending with zero, then SIN is valid
                        if ((int)Char.GetNumericValue(checkDigit) == (x - totalOfSums))
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        ///
        /// \brief This function validates all the fields of an Employee 
        /// \details <b>Details</b> -The parent function is declared ad virtual 
        ///                          so that the child classes can inherit it and
        ///                          modify the function as required.
        ///
        /// \param no parameters
        /// \return  ret - <b>bool</b> -The result to see if the seasonal employee is valid.
        ///
        public virtual bool Validate()
        {
            return false;
        }

        ///
        /// \brief This function sets up the display of an employee's details. 
        /// \details <b>Details</b> -The parent function is declared ad virtual 
        ///                          so that the child classes can inherit it and
        ///                          modify the function as required.
        /// \param   no params
        /// \return  no returns
        ///
        public virtual void Details()
        {
          
        }

        ///
        /// \brief This function puts all attributes of an employee into a string to be logged in the logfile. 
        /// \details <b>Details</b> - concatenates if the employee is valid, all
        ///                          the attributes and sends them to the log file 
        ///                          
        /// \param   no params
        /// \return  no returns
        ///
        public virtual string ToString()
        {
            return "";
        }
    }
}
