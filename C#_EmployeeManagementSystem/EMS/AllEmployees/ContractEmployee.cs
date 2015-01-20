/**
* \file	ContractEmployee.cs
* \author Ab-Code: Sekou Diaby, Tuan Ly, Becky Linyan Li, Bowen Zhuang
* \date 21-11-2014
* \brief contains all the methods and attributes to handle a contract Employee. 
* \brief The contract Employee is the child class of the parent Employee clas.
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
    /// \class ContractEmployee
    /// \brief to model the attributes and behaviour of an ContractEmployee, 
    /// \brief accessor and mutators for the attributes, validates all the fields of the employee and  
    /// \biref shows the details of the ContractEmployee. Validates its own Social insurance number.
    ///
    public class ContractEmployee : Employee
    {
        /* ====================================== */
        /*              PRIVATE                   */
        /* ====================================== */
        /* -------------- ATTRIBUTES ------------ */
        private string contractStartDate;           ///< \brief the start date of the contract for the employee
        private string contractStopDate;            ///< \brief the stop date of the contract for the employee
        private float fixedContractAmount;          ///< \brief the fixed amount of the contract for the employee

        /* ====================================== */
        /*              PUBLIC                   */
        /* ====================================== */
        ///
        /// \brief This function is the constructor of the ContractEmployee and it set the initial value for data members to blank
        /// \details <b>Details</b> 
        /// \param  no param
        /// \return no returns
        ///
        public ContractEmployee()
        {
             bool empTypeRes = SetemployeeType(CONTRACT);
             bool fNameRes = SetfirstName("");
             if (empTypeRes)
             {
                 if (fNameRes)
                 {
                     contractStartDate = "";
                     contractStopDate = "";
                     fixedContractAmount = 0;
                 }
             }
        }

        ///
        /// \brief This function is the constructor of the ContractEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param conStartDate - <b>string </b> -the start date of the contract for the employee
        /// \param conStopDate - <b>string </b> -the stop date of the contract for the employee
        /// \param newAmount - <b>string </b> -the fixed amount of the contract for the employee
        /// /// \return no returns
        ///
        public ContractEmployee( string conStartDate, string conStopDate, float newAmount)
        {
            bool empTypeRes = SetemployeeType(CONTRACT);
            bool fNameRes = SetfirstName("");
            if (empTypeRes)
            {
                if (fNameRes)
                {
                    contractStartDate = conStartDate;
                    contractStopDate = conStopDate;
                    fixedContractAmount = newAmount;
                }
            }
        }

        ///
        /// \brief This function is the constructor of the ContractEmployee and it set the initial value for data members to parameters
        /// \details <b>Details</b> 
        /// \param fName - <b>string </b> -the first name for the employee
        /// \param lName - <b>string </b> -the last name for the employee
        /// /// \return no returns
        ///
        public ContractEmployee(string fName, string lName)
        {
            if (SetemployeeType(CONTRACT))
            {
                if (fName == "")
                {
                    firstName = fName;
                }
                lastName = lName;
            }
        }

        ///
        /// \brief This function sets the employee's start date of contract to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newConStartDate  - <b>string</b>-the contract start date of the employee 
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetcontractStartDate(string newConStartDate)
        {
            bool ret = true;
            //contract start date can be blank and NA or a real date
            if (newConStartDate != "")
            {
                if (newConStartDate == "N/A")
                {
                    contractStartDate = newConStartDate;
                    ret = true;
                }
                else
                {
                    if (validateDate(newConStartDate))
                    {
                        contractStartDate = newConStartDate;
                        ret = true;
                    }
                }
            }
            return ret;
        }

        ///
        /// \brief This function sets the employee's stop date of contract to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newConStopDate  - <b>string</b> - The stop date for the contract  
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetcontractStopDate(string newConStopDate)
        {
            bool ret = true;
            //contract stop date can be blank and NA or a real date
            if (newConStopDate != "")
            {
                if (newConStopDate == "N/A")
                {
                    contractStopDate = newConStopDate;
                    ret = true;
                }
                else
                {
                    if (validateDate(newConStopDate))
                    {
                        contractStopDate = newConStopDate;
                        ret = true;
                    }
                }
            }
            return ret;
        }

        ///
        /// \brief This function sets the employee's fixed contract amount to the parameter passed in
        /// \details <b>Details</b> 
        ///
        /// \param newAmount  - <b>float</b> - The fixed amount for the contract  
        /// \return  ret - <b>bool</b> -The result to see if the attribute was set.
        ///
        public bool SetfixedContractAmount(float newAmount)
        {
            bool ret = false;
            // fixed amount must be a positive number
            if (newAmount >= 0)
            {
                fixedContractAmount = newAmount;
                ret = true;
            }
            return ret;
        }

        ///
        /// \brief This function gets the Contract employee's contractStartDate and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  contractStartDate - <b>string</b> -The contractStartDate of the created contract employee object.
        ///
        public string GetcontractStartDate()
        {
            return contractStartDate;
        }

        ///
        /// \brief This function gets the Contract employee's contractStopDate and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  contractStopDate - <b>string</b> -The contractStopDate of the created contract employee object.
        ///
        public string GetcontractStopDate()
        {
            return contractStopDate;
        }

        ///
        /// \brief This function gets the Contract employee's fixedContractAmount and returns it.
        /// \details <b>Details</b> 
        ///
        /// \param  no params
        /// \return  fixedContractAmount - <b>float</b> -The fixedContractAmount of the created Contract employee object.
        ///
        public float GetfixedContractAmount()
        {
            return fixedContractAmount;
        }

        ///
        /// \brief This function validates all the fields of a contract employee
        /// \details <b>Details</b> -It will validate all the parents attributes, 
        ///                          than it validates all the attributes specific 
        ///                          to the class. If all field are valid, it return  
        ///                          a bool and set the employee to be valid.
        ///
        /// \param no parameters
        /// \return  ret - <b>bool</b> -The result to see if the contract employee is valid.
        ///
        public override bool Validate()
        {
            bool ret = true;
            string exceptions = "";
            if (employeeType != CONTRACT)
            {
                ret = false;
                exceptions += "\nThe employee must be a Contract employee\n";
            }
            // last name cannot be blank to be a valid Full time employee
            if (lastName != "")
            {
                // Contract employee's last name must only contain aplhabets, hyphen and dashes
                if (Regex.IsMatch(lastName, "[^a-zA-Z-']"))
                {
                    exceptions += "Last/Company Name must only contain alphabets, hyphen and dashes\n";
                    ret = false;
                }
            }
            else
            {
                exceptions += "Last/Company Name must not be empty and must only contain alphabets, hyphen and dashes\n";
                ret = false;
            }

            //Date of birth must not be empty to be a valid Full time employee
            if (dateOfBirth != "")
            {
                bool isDateValid = validateDate(dateOfBirth);
                if (!isDateValid)
                {
                    exceptions += "The date of birth must be a valid date\n";
                    ret = false;
                }
            }
            else
            {
                exceptions += "The date of birth must be valid date\n";
                ret = false;
            }
            // Contract time employee's SIN must be valid
            bool isFTSINValid = validateSIN();
            if (!isFTSINValid)
            {
                exceptions += "The Business Number is not valid\n";
                ret = false;
            }            // fixed amount must be bigger than zero
            if (fixedContractAmount <= 0)
            {
                exceptions += "The contract amount must not be a negative number or a zero\n";
                ret = false;
            }
            bool isFTDateValid;
            if (contractStartDate == "")
            {
                exceptions += "The contract start date must not be empty\n";
                ret = false;
            }
            else
            {
                // must be a valid date
                isFTDateValid = validateDate(contractStartDate);
                if (!isFTDateValid)
                {
                    exceptions += "The contract start date is not a valid date\n";
                    ret = false;
                }
            }
            // must be a valid date
            isFTDateValid = validateDate(contractStopDate);
            if (!isFTDateValid)
            {
                exceptions += "The contract stop date is not a valid date\n";
                ret = false;
            }
            if (dateOfBirth != "" && contractStartDate != "" && contractStopDate != "")
            {
                //check if the employee is not too old or too young
                int birth = Convert.ToInt32(dateOfBirth.Substring(0, 4));
                int hire = Convert.ToInt32(contractStartDate.Substring(0, 4));
                int fire = Convert.ToInt32(contractStopDate.Substring(0, 4));

                if ((hire < birth))
                {
                    ret = false;
                    exceptions += "The Contract Employee's contract start date must be later than their birth date\n";
                }

                if (fire < hire)
                {
                    ret = false;
                    exceptions += "The Contract Employee's contract stop date must not be  earlier than the contract start date\n";
                }

                if (birth > 2014)
                {
                    ret = false;
                    exceptions += "The Contract Employee cannot be born on a date that has not passed yet\n";
                }

                if (hire > 2014)
                {
                    ret = false;
                    exceptions += "The Contract Employee cannot be hired on a date that has not passed yet\n";
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
        /// \brief This function validates the Business number passed in as a parameter. 
        /// \details <b>Details</b> -The function checks if the Business number is not empty, than checks 
        ///                          if the Business number is the right format. if it is not in the right format
        ///                          it puts the Business number in the right format and It finally checks if the Business number 
        ///                          is valid before returning the result of the validation
        /// \param   no params
        /// \return  ret - <b>bool</b> -The result of the date validation.
        ///
        public override bool validateSIN()
        {
            bool ret = false;
            // check to see if SIN/BN is not empty before proceeding
            if (socialInsuranceNumber != "" && dateOfBirth.Length == 10)
            {
                //remove all unwanted characters
                socialInsuranceNumber = Regex.Replace(socialInsuranceNumber, "[^0-9]", "");
                // check if the first 2 digit of the Business Number is the same as the last 2 digits of the year in the DOI/DOB  
                if (dateOfBirth.ElementAt(2) == socialInsuranceNumber.ElementAt(0) 
                    && dateOfBirth.ElementAt(3) == socialInsuranceNumber.ElementAt(1))
                {
                    if (socialInsuranceNumber.Length == 9)
                    {
                        // put the BN in a format to be validated
                        socialInsuranceNumber = socialInsuranceNumber.Insert(3, " ");
                        socialInsuranceNumber = socialInsuranceNumber.Insert(7, " ");
                        if (socialInsuranceNumber.Length == 11)
                        {
                            char checkDigit = socialInsuranceNumber.ElementAt(socialInsuranceNumber.Length - 1);
                            int sumOfAlt1 = 0, sumOfAlt2 = 0, sumOfAlt3 = 0, sumOfAlt4 = 0;
                            // getting the sum of the alternates
                            sumOfAlt1 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(1))) * 2;
                            sumOfAlt2 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(4))) * 2;
                            sumOfAlt3 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(6))) * 2;
                            sumOfAlt4 = ((int)Char.GetNumericValue(socialInsuranceNumber.ElementAt(9))) * 2;

                            int totalOfSums = 0;
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
                            // checking single instance of having a zero as check digit
                            if (totalOfSums % 10 == 0)
                            {
                                if (checkDigit == '0')
                                {
                                    ret = true;
                                }
                            }
                            else
                            {
                                //getting highest number ending zero after the total of sums
                                string temp = totalOfSums.ToString();
                                int x = (int)Char.GetNumericValue(temp.ElementAt(0));
                                x = (x + 1) * 10;
                                // if the check digit equals the substraction of the total and the next highest
                                // number ending with zero
                                if ((int)Char.GetNumericValue(checkDigit) == (x - totalOfSums))
                                {
                                    ret = true;
                                }
                            }
                            //remove all unwanted characters
                            socialInsuranceNumber = Regex.Replace(socialInsuranceNumber, "[^0-9]", "");
                            // add the space for the format required for the output
                            socialInsuranceNumber = socialInsuranceNumber.Insert(5, " ");
                        }
                    }
                }
            }
            return ret;
        }

        ///
        /// \brief This function displays all attributes of an employee. 
        /// \details <b>Details</b> -The child function is displays all
        ///                          the attributes and displays if the 
        ///                          contract employee is valid.
        /// \param   no params
        /// \return  no returns
        ///
        public override void Details()
        {
            // display employee details
            string type = "";
            if (employeeType == CONTRACT)
            {
                type = "Contract";
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
            Console.WriteLine(" Company Name            | {0}", lastName);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Date Of Incorporation   | {0}", dateOfBirth);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Social Insurance Number | {0}", socialInsuranceNumber);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Contract Start Date     | {0}", contractStartDate);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine(" Contract Stop Date      | {0}", contractStopDate);
            Console.WriteLine("                         |");
            Console.WriteLine("-------------------------|----------------------------------");
            Console.WriteLine("                         |");
            Console.WriteLine("Contract Amount          | {0}", fixedContractAmount);
            Console.WriteLine("                         |");
            Console.WriteLine("************************************************************\n");
            /*Log in file after details*/
        }

        ///
        /// \brief This function puts all attributes of an employee into a string to be logged in the logfile. 
        /// \details <b>Details</b> - concatenates if the contract employee is valid, all
        ///                          the attributes and sends them to the log file 
        ///                          
        /// \param   no params
        /// \return  no returns
        ///
        public override string ToString()
        {
            string empStr = "";
            string type = "";
            if (employeeType == CONTRACT)
            {
                type = "Contract";
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
            empStr += " Company Name            | " + lastName + "\r\n";
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
            empStr += "                         |\r\n";
            empStr += " Contract Start Date     | " + contractStartDate + "\r\n";
            empStr += "                         |\r\n";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |\r\n";
            empStr += " Contract Stop Date      | " + contractStopDate + "\r\n";
            empStr += "                         |\r\n";
            empStr += "-------------------------|----------------------------------\r\n";
            empStr += "                         |\r\n";
            empStr += " Contract Amount         | " + fixedContractAmount + "\r\n";
            empStr += "                         |\r\n";
            empStr += "************************************************************\r\n";
            return empStr;
        }
    }
}
