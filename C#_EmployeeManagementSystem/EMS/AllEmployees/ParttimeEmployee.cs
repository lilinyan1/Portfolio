/**
* \file	ParttimeEmployee.cs
* \author Ab-Code: Sekou Diaby, Tuan Ly, Becky Linyan Li, Bowen Zhuang
* \date 21-11-2014
* \brief contains all the methods and attributes to handle a part time Employee. 
* \brief The part time Employee is the child class of the parent Employee clas.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
namespace AllEmployees
{
    ///
    /// \class ParttimeEmployee
    /// \brief to model the attributes and behaviour of an ParttimeEmployee, 
    /// \brief accessor and mutators for the attributes, validates all the fields of the employee and  
    /// \biref shows the details of the part time Employee.
    ///
    public class ParttimeEmployee : Employee
    {
        /* ====================================== */
        /*              PRIVATE                   */
        /* ====================================== */
        /* -------------- ATTRIBUTES ------------ */
        private string dateOfHire;                  ///< \brief the date the employee was hired
        private string dateOfTermination;           ///< \brief the date the employee will be terminated
        private float hourlyRate;                   ///< \brief the hourly pay rate for the employee 

        /* ====================================== */
        /*              PUBLIC                   */
        /* ====================================== */
        ///
        /// \brief This function is the constructor of the ParttimeEmployee and it set the initial value for data members to blank
        /// \details <b>Details</b> 
        /// \param  no param
        /// \return no returns
        ///
        public ParttimeEmployee()
        {
            bool empTypeRes = SetemployeeType(PART_TIME);
            if (empTypeRes)
            {
                dateOfHire = "";
                dateOfTermination = "";
                hourlyRate = 0;
            }
        }

        ///
        /// \brief This function is the constructor of the ParttimeEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param DOH - <b>string </b> -the date the employee was hired
        /// \param DOT - <b>string </b> -the date the employee will be terminated
        /// \param newHrate - <b>float </b> -the hourly pay rate for the employee 
        /// /// \return no returns
        ///
        public ParttimeEmployee(string DOH, string DOT, float newHrate)
        {
              bool empTypeRes = SetemployeeType(PART_TIME);
              if (empTypeRes)
              {
                  dateOfHire = DOH;
                  dateOfTermination = DOT;
                  hourlyRate = newHrate;
              }
        }

        ///
        /// \brief This function is the constructor of the ParttimeEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param fName - <b>string </b> -the first name for the employee
        /// \param lName - <b>string </b> -the last name for the employee
        /// /// \return no returns
        ///
        public ParttimeEmployee(string fName, string lName)
        {
              bool empTypeRes = SetemployeeType(PART_TIME);
              if (empTypeRes)
              {
                  firstName = fName;
                  lastName = lName;
              }
        }

        ///
        /// \brief This function sets the employee's date of hire to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newDOH  - <b>string</b> - The date the employee was hired 
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetdateOfHire(string newDOH)
        {
            bool ret = false;
            if (newDOH != "")
            {
                //Date of Hire can be NA
                if (newDOH == "N/A")
                {
                    dateOfHire = newDOH;
                    ret = true;
                }
                else
                {
                    // check if Date of hire is a real date
                    if (validateDate(newDOH))
                    {
                        dateOfHire = newDOH;
                        ret = true;
                    }
                }
            }
            else
            {
                // date of Hire can be empty
                dateOfHire = newDOH;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function sets the employee's date of termination to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newDOT  - <b>string</b> - The date the employee is going to be terminated 
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetdateOfTermination(string newDOT)
        {
            bool ret = false;
            if (newDOT != "")
            {
                //Date of termination can be NA
                if (newDOT == "N/A")
                {
                    dateOfTermination = newDOT;
                    ret = true;
                }
                else
                {
                    // check if Date of hire is a real date
                    if (validateDate(newDOT))
                    {
                        dateOfTermination = newDOT;
                        ret = true;
                    }
                }
            }
            else
            {
                //Date of termination can be empty
                dateOfTermination = newDOT;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function sets the employee's hourly rate to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param float  - <b>float</b> - The pay rate for the employee
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set. 
        ///
        public bool SethourlyRate(float newHourlyRate)
        {
            bool ret = false;
            //hourlyRate must Not be negative
            if (newHourlyRate >= 0)
            {
                hourlyRate = newHourlyRate;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function gets the parttime employee's dateOfHire and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  dateOfHire - <b>string</b> -The dateOfHire of the created parttime employee object.
        ///
        public string GetdateOfHire()
        {
            return dateOfHire;
        }

        ///
        /// \brief This function gets the parttime employee's dateOfTermination and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  dateOfTermination - <b>string</b> -The dateOfTermination of the created parttime employee object.
        ///
        public string GetdateOfTermination()
        {
            return dateOfTermination;
        }

        ///
        /// \brief This function gets the parttime employee's hourlyRate and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  hourlyRate - <b>float</b> -The hourlyRate of the created parttime employee object.
        ///
        public float GethourlyRate()
        {
            return hourlyRate;
        }

        ///
        /// \brief This function validates all the fields of a Part time employee
        /// \details <b>Details</b> -It will validate all the parents attributes, 
        ///                          than it validates all the attributes specific 
        ///                          to the class. If all field are valid, it return  
        ///                          a bool and set the employee to be valid.
        ///
        /// \param no parameters
        /// \return  ret - <b>bool</b> -The result to see if the part time employee is valid.
        ///
        public override bool Validate()
        {
            string exceptions = "" ;
            bool ret = true;
            if (employeeType != 'P')
            {
                ret = false;
                exceptions += "\nThe employee must be a part time employee\n";
            }
            //horly rate must be positive and not zero
            if (hourlyRate <= 0)
            {
                exceptions += "Hourly Rate cannot be negative or zero\n";
                ret = false;
            }

            // First name cannot be blank to be a valid part time employee
            if (firstName != "")
            {
                // part time employee's First name must only contain aplhabets, hyphen and dashes
                if (Regex.IsMatch(firstName, "[^a-zA-Z-']"))
                {
                    exceptions += "First Name must only contain alphabets, hyphen and dashes\n";
                    ret = false;
                }
            }
            else
            {
                exceptions += "First Name must not be empty and must only contain alphabets, hyphen and dashes";
                ret = false;
            }
            // Last name cannot be blank to be a valid part time employee
            if (lastName != "")
            {
                // part time employee's Last name must only contain aplhabets, hyphen and dashes
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

            //Date of birth must not be empty to be a valid part time employee
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
            // part time employee's SIN must be valid
            bool isFTSINValid = validateSIN();
            if (!isFTSINValid)
            {
                exceptions += "The SIN is not valid\n";
                ret = false;
            }

            bool isFTDateValid;
            // part time employee's date of Hire must not be empty
            if (dateOfHire != "")
            {
                isFTDateValid = validateDate(dateOfHire);
                if (!isFTDateValid)
                {
                    exceptions += "The date of Hire is not a valid date\n";
                    ret = false;
                }
            }
            else
            {
                exceptions += "The date of hire is not a valid date\n";
                ret = false;
            }

            // date of termination may be blank or a valid date
            if (dateOfTermination != "")
            {
                isFTDateValid = validateDate(dateOfTermination);
                if (!isFTDateValid)
                {
                    exceptions += "The date of termination is not a valid date\n";
                    ret = false;
                }
            }
            
            if (dateOfBirth != "" && dateOfHire != "" )
            {
                int fire = 0;
                //check if the employee is not too old or too young
                int birth = Convert.ToInt32(dateOfBirth.Substring(0, 4));
                int hire = Convert.ToInt32(dateOfHire.Substring(0, 4));
                if (dateOfTermination != "")
                {
                    fire = Convert.ToInt32(dateOfTermination.Substring(0, 4));
                }
                if ((hire - birth) > 100)
                {
                    ret = false;
                    exceptions += "Employee's age must not be larger than 100\n";
                }
                if (fire != 0)
                {
                    if ((fire - hire) > 100)
                    {
                        ret = false;
                        exceptions += "Employee's Work period must not be larger than 100\n";
                    }
                }
                if ((hire - birth) < 14)
                {
                    ret = false;
                    exceptions += "Employee must be 14 years or older to start work\n";
                }

                if ((hire < birth))
                {
                    ret = false;
                    exceptions += "Employee Cannot start work before their birth date\n";
                }

                if (fire != 0)
                {
                    if (fire < hire)
                    {
                        ret = false;
                        exceptions += "Employee Cannot be fired when they have not started working\n";
                    }
                }

                if (birth > 2014)
                {
                    ret = false;
                    exceptions += "Employee cannot be born on a date that has not occured\n";
                }

                if (hire > 2014)
                {
                    ret = false;
                    exceptions += "Employee cannot be hired on a date that has not occured\n";
                }
                if (fire != 0)
                {
                    if (fire > 2014)
                    {
                        ret = false;
                        exceptions += "Employee cannot be fired on a date that has not occured\n";
                    }
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
        ///                          part time employee is valid.
        /// \param   no params
        /// \return  no returns
        ///
        public override void Details()
        {
            // display employee details
            string type = "";
            if (employeeType == PART_TIME)
            {
                type = "Part Time";
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
            Console.WriteLine(" Date Of Hire            | {0}", dateOfHire);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Date Of Termination     | {0}", dateOfTermination);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Hourly Rate             | {0}", hourlyRate);
            Console.WriteLine("                         |");
            Console.WriteLine("************************************************************\n");
            /*Log in file after details*/
            /* cant log it from here*/
            
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
            if (employeeType == PART_TIME)
            {
                type = "Part Time";
            }
            else
            {
                type += employeeType;
            }
            empStr +="\r\n************************************************************\r\n";
            empStr +="                 EMPLOYEE DETAILS  \r\n";
            empStr +="                 Is Employee Valid ?"+ isValidEmp+"\r\n"; 
            empStr +="*************************************************************\r\n";
            empStr +="                         |\r\n";
            empStr +=" Employee Type           | "+type+"\r\n";
            empStr +="                         |\r\n";
            empStr +="-------------------------|----------------------------------\r\n";
            empStr +="                         |\r\n";
            empStr +=" First Name              | "+firstName+"\r\n";
            empStr +="                         |\r\n";
            empStr +="-------------------------|----------------------------------\r\n";
            empStr +="                         |\r\n";
            empStr +=" Last Name               | "+lastName+"\r\n";
            empStr +="                         |\r\n";
            empStr +="-------------------------|----------------------------------\r\n";
            empStr +="                         |\r\n";
            empStr +=" Date Of Birth           | "+dateOfBirth+"\r\n";
            empStr +="                         |\r\n";
            empStr +="-------------------------|----------------------------------\r\n";
            empStr +="                         |\r\n";
            empStr +=" Social Insurance Number | "+socialInsuranceNumber+"\r\n";
            empStr +="                         |\r\n";
            empStr +="-------------------------|----------------------------------\r\n";
            empStr +="                         |\r\n";
            empStr +=" Date Of Hire            | "+dateOfHire+"\r\n";
            empStr +="                         |\r\n";
            empStr +="-------------------------|----------------------------------\r\n";
            empStr +="                         |\r\n";
            empStr +=" Date Of Termination     | "+dateOfTermination+"\r\n";
            empStr +="                         |\r\n";
            empStr +="-------------------------|----------------------------------\r\n";
            empStr +="                         |\r\n";
            empStr +=" Hourly Rate             | "+hourlyRate+"\r\n";
            empStr +="                         |\r\n";
            empStr +="************************************************************\r\n";
            return empStr;
        }
       
    }
}
