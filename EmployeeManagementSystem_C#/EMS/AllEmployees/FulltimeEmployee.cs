/**
* \file	FulltimeEmployee.cs
* \author Ab-Code: Sekou Diaby, Tuan Ly, Becky Linyan Li, Bowen Zhuang
* \date 21-11-2014
* \brief contains all the methods and attributes to handle a Full time Employee. 
* \brief The Full time Employee is the child class of the parent Employee clas.
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
    /// \class FulltimeEmployee
    /// \brief to model the attributes and behaviour of an FulltimeEmployee, 
    /// \brief accessor and mutators for the attributes, validates all the fields of the employee and  
    /// \biref shows the details of the FulltimeEmployee.
    ///
    public class FulltimeEmployee : Employee
    {
        /* ====================================== */
        /*              PRIVATE                   */
        /* ====================================== */
        /* -------------- ATTRIBUTES ------------ */
        private string dateOfHire;                  ///< \brief the date the employee was hired
        private string dateOfTermination;           ///< \brief the date the employee will be terminated
        private float salary;                       ///< \brief the salary for the employee 

        /* ====================================== */
        /*              PUBLIC                   */
        /* ====================================== */
        ///
        /// \brief This function is the constructor of the FulltimeEmployee and it set the initial value for data members to blank
        /// \details <b>Details</b> 
        /// \param  no param
        /// \return no returns
        ///
        public FulltimeEmployee()
        {
            bool empTypeRes = SetemployeeType(FULL_TIME);
            if (empTypeRes)
            {
                dateOfHire = "";
                dateOfTermination = "";
                salary = 0;
            }
        }

        ///
        /// \brief This function is the constructor of the FulltimeEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param DOH - <b>string </b> -the date the employee was hired
        /// \param DOT - <b>string </b> -the date the employee will be terminated
        /// \param newSalary - <b>float </b> -the salary for the employee
        /// /// \return no returns
        ///
        public FulltimeEmployee(string DOH, string DOT, float newSalary)
        {
            bool empTypeRes = SetemployeeType(FULL_TIME);
            if (empTypeRes)
            {
                dateOfHire = DOH;
                dateOfTermination = DOT;
                salary = newSalary;
            }
        }

        ///
        /// \brief This function is the constructor of the FulltimeEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param fName - <b>string </b> -the first name for the employee
        /// \param lName - <b>string </b> -the last name for the employee
        /// /// \return no returns
        ///
        public FulltimeEmployee(string fName, string lName)
        {
            bool empTypeRes = SetemployeeType(FULL_TIME);
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
        /// \brief This function sets the employee's salary to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newSalary  - <b>float</b> - The salary of the employee
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool Setsalary(float newSalary)
        {
            bool ret = false;
            //salary must be 0 or larger
            if (newSalary >= 0)
            {
                salary = newSalary;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function gets the fulltime employee's dateOfHire and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  dateOfHire - <b>string</b> -The dateOfHire of the created fulltime employee object.
        ///
        public string GetdateOfHire()
        {
            return dateOfHire;
        }

        ///
        /// \brief This function gets the fulltime employee's dateOfTermination and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  dateOfTermination - <b>string</b> -The dateOfTermination of the created fulltime employee object.
        ///
        public string GetdateOfTermination()
        {
            return dateOfTermination;
        }

        ///
        /// \brief This function gets the fulltime employee's salary and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  salary - <b>float</b> -The salary of the created fulltime employee object.
        ///
        public float Getsalary()
        {
            return salary;
        }

        ///
        /// \brief This function validates all the fields of a Full time employee
        /// \details <b>Details</b> -It will validate all the parents attributes, 
        ///                          than it validates all the attributes specific 
        ///                          to the class. If all field are valid, it return  
        ///                          a bool and set the employee to be valid.
        ///
        /// \param no parameters
        /// \return  ret - <b>bool</b> -The result to see if the full time employee is valid.
        ///
        public override bool Validate()
        {
            string exceptions = "";
            bool ret = true;
            // employee type for Full time 
            if (employeeType != FULL_TIME)
            {
                ret = false;
                exceptions += "\nThe employee must be a full time employee\n";
            }
            // First name cannot be blank to be a valid Full time employee
            if (firstName != "")
            {
                // Full time employee's First name must only contain aplhabets, hyphen and dashes
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
            // Last name cannot be blank to be a valid Full time employee
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

            //Date of birth must not be empty to be a valid Full time employee
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
            // Full time employee's SIN must be valid
            bool isFTSINValid = validateSIN();
            if (!isFTSINValid)
            {
                exceptions += "The SIN is not valid\n";
                ret = false;
            }

            // Full time employee's salary must be bigger than zero
            if (salary <= 0)
            {
                exceptions += "The Salary cannot be negative or a zero\n";
                ret = false;
            }
            bool isFTDateValid;
            // Full time employee's date of Hire must not be empty
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

            //check if the employee is not too old or too young
            if (dateOfBirth != "" && dateOfHire != "" )
            {
                int fire = 0;
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
        ///                          full time employee is valid.
        /// \param   no params
        /// \return  no returns
        ///
        public override void Details()
        {
            // display employee details
            string type = "";
            if (employeeType == FULL_TIME)
            {
                type = "Full Time";
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
            Console.WriteLine(" Salary                  | {0}", salary);
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
            if (employeeType == FULL_TIME)
            {
                type = "Full Time";
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
            empStr +=" Salary                  | "+salary+"\r\n";
            empStr +="                         |\r\n";
            empStr +="************************************************************\r\n";
            return empStr;
        }
       
    }
}
