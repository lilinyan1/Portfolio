/**
* \file	UIMenu.cs
* \author Ab-code: Becky Linyan Li, Bowen Zhuang, Sekou Gassama, Tuan Ly
* \date 11-21-2014
* \brief contains the user interfaces of the UIMenu class and methods to invoke other class libraries
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AllEmployees;
using TheCompany;
using Supporting;


namespace Presentation
{
    ///
    /// \class UIMenu
    /// \brief This class provides a front-end interface to the rest of the class libraries.
    /// \brief Gathers user inputs and invokes the necessary methods to meet commands.
    ///
    public class UIMenu
    {
        /* ====================================== */
        /*              Private                 */
        /* ====================================== */
        /* -------------- ATTRIBUTES ------------ */
        private ConsoleKeyInfo ckInfo;
        private Container cContainer = new Container();
        private Logging logger = new Logging();
        /* 
        *   METHOD      : MenuRun
        *   DESCRIPTION : This function calculates tax on a retail purchase in Ontario.
        *   PARAMETERS  : double purchaseAmount : untaxed amount
        *                 double hstTaxRate : HST tax rate 
        *                 double pstTaxRate : Provincial tax rate
        *
        *   RETURNS     : double : total tax amount (includes HST and Provincial sales tax)
        *                          or a value of -1.00 on error
        */

        /* ====================================== */
        /*              PUBLIC                   */
        /* ====================================== */
        /* -------------- METHODS ------------ */
        ///
        /// \brief This method controls the flow of processes based on user input
        /// \brief by providing appropriate textual user-interfaces.
        /// \details <b>Details</b> - The user selects options that are laid out in menus. This method
        ///                           performs the input's actions while providing a robust user interface
        ///                           to navigate through the system's functionalities.
        /// \param none
        /// \return none
        /// 
        public void MenuRun()
        {
            bool bQuit = false;

            //These four integers variables hold the return
            //  value of each menu to process the option selected.
            int nRetMMenu = 0, nRetFMMenu = 0, nRetEMMenu = 0, nRetEDMenu = 0;

            while (!bQuit)
            {
                //Open main menu by default, unless flagged to open Manage Employees menu (3).

                    nRetMMenu = MainMenu();
                    switch (nRetMMenu)
                    {
                        //Manage EMS DBase files (Menu 2).
                        case 1:
                            nRetFMMenu = FileManagementMenu();
                            switch (nRetFMMenu)
                            {
                                //Load EMS DBase from file.
                                case 1:
                                    LoadDBase();
                                    break;
                                //Save Employee Set to EMS DBase file.
                                case 2:
                                    SaveDBase();
                                    break;
                                //Return to Main Menu.
                                case 9:
                                    continue;
                            }
                            break;
                        //Manage Employees (Menu 3), re-iterate into else statement.
                        case 2:
                            nRetEMMenu = EmployeeManagementMenu();
                            switch (nRetEMMenu)
                            {
                                //Display Employee Set.
                                case 1:
                                    DisplayEmployeeSet();
                                    continue;
                                //Create a NEW Employee (Opens Menu 4). 
                                case 2:
                                    CreateEmployee(1);
                                    continue;
                                //Modify an EXISTING Employee (Opens Menu 4).
                                case 3:
                                    ModifyEmployee();
                                    continue;
                                //Remove an EXISTING Employee (Opens Menu 4).
                                case 4:
                                    RemoveEmployee();
                                    continue;
                                //Return to Main Menu
                                case 9:
                                    continue;
                            }
                            continue;
                        //Quit.
                        case 9:
                            bQuit = true;
                            break;
                    }

            }

            //When running .exe, these lines prevent the window from closing automatically.
            Console.WriteLine("{Session Complete}\nPress any key to close.");
            Console.ReadKey();
            Console.Clear();
        }

        ///
        /// \brief This method produces a menu of options and returns a valid input choice.
        /// \details <b>Details</b> - MAIN MENU: Options are to manage the database files or manage employees.
        ///                           Both options produce to a new menu.
        /// \param none
        /// \return Int32.Parse(ckInfo.KeyChar.ToString()) - <b>int</b> - integer representation of keystroke input
        /// 
        public int MainMenu()
        {
            int nBreak = 0;
            while (nBreak == 0)
            {
                Console.WriteLine("Menu 1 : MAIN MENU");
                Console.WriteLine("------------------");
                Console.WriteLine("    1. Manage EMS DBase files"); // goes to Menu 2
                Console.WriteLine("    2. Manage Employees"); // goes to menu 3
                Console.WriteLine("    9. Quit");
                ckInfo = Console.ReadKey();
                Console.Clear();
                Console.Write("{0} was read.  ", ckInfo.KeyChar);
                switch (ckInfo.KeyChar)
                {
                    case '1':
                        nBreak = 1;
                        break;
                    case '2':
                        nBreak = 1;
                        break;
                    case '9':
                        nBreak = 1;
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again.\n");
                        break;
                }
            }
            Console.Clear();
            return Int32.Parse(ckInfo.KeyChar.ToString());
        }

        ///
        /// \brief This method produces a menu of options and returns a valid input choice.
        /// \details <b>Details</b> - FILE MANAGEMENT MENU: To load or to save a database from or to a file.
        ///                           Uses the Supporting class library to perform load/saves.
        ///                           LoadDBase & SaveDBase methods used to perform input.
        /// \param none
        /// \return Int32.Parse(ckInfo.KeyChar.ToString()) - <b>int</b> - integer representation of keystroke input
        /// 
        public int FileManagementMenu()
        {
            int nBreak = 0;
            while (nBreak == 0)
            {
                Console.WriteLine("Menu 2 : FILE MANAGEMENT MENU");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("    1. Load EMS DBase from file");
                Console.WriteLine("    2. Save Employee Set to EMS DBase file");
                Console.WriteLine("    9. Return to Main Menu"); // back to Menu 1
                ckInfo = Console.ReadKey();
                Console.Clear();
                Console.Write("{0} was read.  ", ckInfo.KeyChar);
                switch (ckInfo.KeyChar)
                {
                    case '1':
                        nBreak = 1;
                        break;
                    case '2':
                        nBreak = 1;
                        break;
                    case '9':
                        nBreak = 1;
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again.\n");
                        break;
                }
            }
            Console.Clear();
            return Int32.Parse(ckInfo.KeyChar.ToString());
        }

        ///
        /// \brief This method produces a menu of options and returns a valid input choice.
        /// \details <b>Details</b> - EMPLOYEE MANAGEMENT MENU: View employee database, create new employee, modify/remove existing employee.
        ///                           Viewing employee database traverses the container and uses the Details method to output information.
        ///                           CreateEmployee is used to handle prompts for employee attributes required to build an employee.
        ///                           Modify & Remove an existing employee uses the Container's search method to select and modify/remove employee.
        /// \param none
        /// \return Int32.Parse(ckInfo.KeyChar.ToString()) - <b>int</b> - integer representation of keystroke input
        /// 
        public int EmployeeManagementMenu()
        {
            int nBreak = 0;
            while (nBreak == 0)
            {
                Console.WriteLine("Menu 3 : EMPLOYEE MANAGEMENT MENU");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("    1. Display Employee Set");
                Console.WriteLine("    2. Create a NEW Employee"); // goes to Menu 4
                Console.WriteLine("    3. Modify an EXISTING Employee (SIN required)"); // goes to Menu 4
                Console.WriteLine("    4. Remove an EXISTING Employee (SIN required)");
                Console.WriteLine("    9. Return to Main Menu"); // back to Menu 1
                ckInfo = Console.ReadKey();
                Console.Clear();
                Console.Write("{0} was read.  ", ckInfo.KeyChar);
                switch (ckInfo.KeyChar)
                {
                    case '1':
                        nBreak = 1;
                        break;
                    case '2':
                        nBreak = 1;
                        break;
                    case '3':
                        nBreak = 1;
                        break;
                    case '4':
                        nBreak = 1;
                        break;
                    case '9':
                        nBreak = 1;
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again.\n");
                        break;
                }
            }
            Console.Clear();
            return Int32.Parse(ckInfo.KeyChar.ToString());
        }

        ///
        /// \brief This method produces a menu of options and returns a valid input choice.
        /// \details <b>Details</b> - EMPLOYEE DETAILS MENU: CREATE or MODIFY an existing employee, based on employee TYPE.
        ///                           CreateEmployee uses this return value to acquire employee class attributes.  
        ///                           ModifyEmployee uses this return value to modify the employee type.
        /// \param none
        /// \return Int32.Parse(ckInfo.KeyChar.ToString()) - <b>int</b> - integer representation of keystroke input
        /// 
        public int EmployeeDetailsMenu()
        {
            int nBreak = 0;
            while (nBreak == 0)
            {
                Console.WriteLine("Menu 4 : EMPLOYEE DETAILS MENU");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("    1. Specify Base Employee Details");
                Console.WriteLine("    2. Specify Full Time Employee Details");
                Console.WriteLine("    3. Specify Part Time Employee Details");
                Console.WriteLine("    4. Specify Contract Employee Details");
                Console.WriteLine("    5. Specify Seasonal Employee Details");
                Console.WriteLine("    9. Return to Main Menu"); // back to Menu 1
                ckInfo = Console.ReadKey();
                Console.Clear();
                Console.Write("{0} was read.  ", ckInfo.KeyChar);
                switch (ckInfo.KeyChar)
                {
                    case '1':
                        nBreak = 1;
                        break;
                    case '2':
                        nBreak = 1;
                        break;
                    case '3':
                        nBreak = 1;
                        break;
                    case '4':
                        nBreak = 1;
                        break;
                    case '5':
                        nBreak = 1;
                        break;
                    case '9':
                        nBreak = 1;
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again.\n");
                        break;
                }
            }
            Console.Clear();
            return Int32.Parse(ckInfo.KeyChar.ToString());
        }

        ///
        /// \brief This method creates a new employee and prompts user for attribute values.
        /// \details <b>Details</b> - Employee class validates all inputs through its setter methods.
        ///                           This method simply collects information and forwards it to the
        ///                           employee class.  Once employee creation is complete, add to database.
        /// \param nEmployeeType - <b>int</b> - Value passed in to determine which EmployeeType is required
        /// \return none
        /// 
        public void CreateEmployee(int nEmployeeType)
        {
            bool isTypeAcquired = false;
            string lastName = "", firstName = "", socialInsuranceNumber = "", dateOfBirth = "";
            bool isOver = false;

            FulltimeEmployee f = new FulltimeEmployee();
            ParttimeEmployee p = new ParttimeEmployee();
            ContractEmployee c = new ContractEmployee();
            SeasonalEmployee s = new SeasonalEmployee();

            Employee tempEmployeeValidate = new Employee();
            int quitCounter = 0;

            //
            nEmployeeType = EmployeeDetailsMenu();
            if (nEmployeeType != 9)
            {

                //For Base Employee Details option, acquiring employee type is critical.
                if (nEmployeeType == 1)
                {
                    while (!isTypeAcquired)
                    {
                        Console.WriteLine("Press between 1 to 4 to select employee type.");
                        Console.WriteLine("    1. Specify Full Time Employee Details");
                        Console.WriteLine("    2. Specify Part Time Employee Details");
                        Console.WriteLine("    3. Specify Contract Employee Details");
                        Console.WriteLine("    4. Specify Seasonal Employee Details");
                        ckInfo = Console.ReadKey();
                        Console.Clear();
                        Console.Write("{0} was read.  ", ckInfo.KeyChar);
                        switch (ckInfo.KeyChar)
                        {
                            case '1':
                                isTypeAcquired = true;
                                f.SetemployeeType('F');
                                Console.WriteLine("Full time Employee type acquired.\n");
                                break;
                            case '2':
                                isTypeAcquired = true;
                                p.SetemployeeType('P');
                                Console.WriteLine("Part time Employee type acquired.\n");
                                break;
                            case '3':
                                isTypeAcquired = true;
                                c.SetemployeeType('C');
                                Console.WriteLine("Contract Employee type acquired.\n");
                                break;
                            case '4':
                                isTypeAcquired = true;
                                s.SetemployeeType('S');
                                Console.WriteLine("Seasonal Employee type acquired.\n");
                                break;
                            default:
                                Console.WriteLine("Invalid input, please try again.\n");
                                break;
                        }
                    }

                    nEmployeeType += Int32.Parse(ckInfo.KeyChar.ToString()); //This synchronizes Menu 4's return value to the 1-4 option here
                }
                while (!isOver)
                {
                    Console.WriteLine("Please enter a non-blank last name/corporation name \n(valid characters: a-z, A-Z, ', -): ");
                    lastName = Console.ReadLine();
                    if (tempEmployeeValidate.SetlastName(lastName))
                    {
                        if (lastName != "")
                        {
                            Console.WriteLine("Last name accepted [{0}].\n", lastName);
                            break;
                        }

                    }
                    logger.Log("UIMenu", "CreateEmployee(int)", "Employee set last name return false. Value:" + lastName, false);
                    Console.WriteLine("Invalid characters found in last name: [{0}].\n", lastName);

                }
                //Ask for first name if not contract employee
                if (nEmployeeType != 4)
                {
                    while (!isOver)
                    {
                        Console.WriteLine("Please enter a non-blank first name \n(valid characters: a-z, A-Z, ', -): ");
                        firstName = Console.ReadLine();
                        if (tempEmployeeValidate.SetfirstName(firstName))
                        {
                            if (firstName != "")
                            {
                                Console.WriteLine("First name accepted [{0}].\n", firstName);
                                break;
                            }

                        }
                        logger.Log("UIMenu", "CreateEmployee(int)", "Employee set first name return false. Value:" + firstName, false);
                        Console.WriteLine("Invalid characters found in last name: [{0}].\n", firstName);
                    }

                    while (!isOver)
                    {
                        Console.WriteLine("Please enter date of birth (YYYY-MM-DD): ");
                        dateOfBirth = Console.ReadLine();
                        if (tempEmployeeValidate.SetdateOfBirth(dateOfBirth))
                        {
                            if (dateOfBirth != "" && dateOfBirth != "N/A")
                            {
                                Console.WriteLine("Date of birth accepted [{0}].\n", dateOfBirth);
                                break;
                            }
                        }
                        logger.Log("UIMenu", "CreateEmployee(int)", "Employee set date of birth return false. Value:" + dateOfBirth, false);
                        Console.WriteLine("Must be in form of YYYY-MM-DD, \nwhere Y, M, and D each represent a digit: [{0}].\n", dateOfBirth);
                    }

                    quitCounter = 0;
                    while (!isOver)
                    {
                        Console.WriteLine("Please enter Social Insurance Number (9 consecutive digits): ");
                        socialInsuranceNumber = Console.ReadLine();
                        if (tempEmployeeValidate.SetsocialInsuranceNumber(socialInsuranceNumber))
                        {
                            if (tempEmployeeValidate.validateSIN())
                            {
                                Console.WriteLine("SIN accepted [{0}].\n", socialInsuranceNumber);
                                break;
                            }
                        }
                        logger.Log("UIMenu", "CreateEmployee(int)", "Employee social insurance number return false. Value:" + socialInsuranceNumber, false);
                        Console.WriteLine("Must be 9 consecutive digits & a valid Canadian SIN [{0}].\n", socialInsuranceNumber);
                        quitCounter++;
                        if (quitCounter > 2)
                        {
                            if (EndInputs())
                            {
                                isOver = true;
                                break;
                            }
                        }
                    }
                }


                //Gather remaining attributes specific to employee children

                //Full time employee
                if (nEmployeeType == 2)
                {
                    string hire = "", termination = "";
                    float salary = 0;

                    quitCounter = 0;
                    while (!isOver)
                    {
                        Console.WriteLine("Please enter employee hire date (YYYY-MM-DD): ");
                        hire = Console.ReadLine();
                        if (f.SetdateOfHire(hire))
                        {
                            if (hire != "" && hire != "N/A")
                            {
                                Console.WriteLine("Date of hire accepted [{0}].\n", hire);
                                break;
                            }
                        }
                        logger.Log("UIMenu", "CreateEmployee(int)", "FullTimeEmployee set hire date return false. Value:" + hire, false);
                        Console.WriteLine("Must be in form of YYYY-MM-DD, where Y, M, and D each represent a digit): [{0}].\n", hire);
                        quitCounter++;
                        if (quitCounter > 2)
                        {
                            if (EndInputs())
                            {
                                isOver = true;
                                break;
                            }
                        }
                    }

                    while (!isOver)
                    {
                        Console.WriteLine("Please enter employee termination date \n(YYYY-MM-DD, blank, N/A): ");
                        termination = Console.ReadLine();
                        if (f.SetdateOfTermination(termination))
                        {
                            Console.WriteLine("Date of termination accepted [{0}].\n", termination);
                            break;
                        }
                        logger.Log("UIMenu", "CreateEmployee(int)", "FullTimeEmployee set termination date return false. Value:" + termination, false);
                        Console.WriteLine("Must be in form of YYYY-MM-DD, \nwhere Y, M, and D each represent a digit (or blank, N/A): [{0}].\n", termination);
                    }

                    while (!isOver)
                    {
                        Console.WriteLine("Please enter employee salary (numeric, greater than 0): ");
                        //If parsing float fails, salary remains less than 0
                        if (!float.TryParse(Console.ReadLine(), out salary))
                        {
                            salary = -1;
                        }

                        if (f.Setsalary(salary))
                        {
                            Console.WriteLine("Salary accepted [{0}].\n", salary);
                            break;
                        }
                        logger.Log("UIMenu", "CreateEmployee(int)", "FullTimeEmployee set salary return false. Value:" + salary, false);
                        Console.WriteLine("Salary must be numeric, non-zero, and non-negative : [{0}].\n", salary);
                    }

                    if (!isOver)
                    {
                        //Add remaining fields
                        f.SetlastName(lastName);
                        f.SetfirstName(firstName);
                        f.SetsocialInsuranceNumber(socialInsuranceNumber);
                        f.SetdateOfBirth(dateOfBirth);

                        try
                        {
                            if (f.Validate())
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + f.GetfirstName() + "-" + f.GetlastName() + " SIN: " + f.GetsocialInsuranceNumber(), true);
                                if (cContainer.add(f))
                                {
                                    Console.WriteLine("Employee successfully added!  Press any key to continue...");
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Fail to add employee, the SIN number already exists. \nPress any key to continue...");
                                  
                                }
                                Console.ReadKey();
                                Console.Clear();
                               
                            }
                            else
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + f.GetfirstName() + "-" + f.GetlastName() + " SIN: " + f.GetsocialInsuranceNumber(), false);
                                Console.WriteLine("Employee is not valid. Save failed.  Press any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }
                //Part time employee
                else if (nEmployeeType == 3)
                {
                    string hire = "", termination = "";
                    float hourlyRate = 0;

                    quitCounter = 0;
                    while (!isOver)
                    {
                        Console.WriteLine("Please enter employee hire date (YYYY-MM-DD): ");
                        hire = Console.ReadLine();
                        if (p.SetdateOfHire(hire))
                        {
                            if (hire != "" && hire != "N/A")
                            {
                                Console.WriteLine("Date of hire accepted [{0}].\n", hire);
                                break;
                            }
                        }
                        Console.WriteLine("Must be in form of YYYY-MM-DD, \nwhere Y, M, and D each represent a digit: [{0}].\n", hire);
                        quitCounter++;
                        if (quitCounter > 2)
                        {
                            if (EndInputs())
                            {
                                isOver = true;
                                break;
                            }
                        }
                    }

                    while (!isOver)
                    {
                        Console.WriteLine("Please enter employee termination date (YYYY-MM-DD, blank, or N/A): ");
                        termination = Console.ReadLine();
                        if (p.SetdateOfTermination(termination))
                        {
                            Console.WriteLine("Date of termination accepted [{0}].\n", termination);
                            break;
                        }
                        Console.WriteLine("Must be in form of YYYY-MM-DD, where \nY, M, and D each represent a digit (or blank, N/A): [{0}].\n", termination);
                    }

                    while (!isOver)
                    {
                        Console.WriteLine("Please enter employee hourlyRate (numeric, greater than 0): ");
                        //If parsing float fails, salary remains 0
                        if (!float.TryParse(Console.ReadLine(), out hourlyRate))
                        {
                            hourlyRate = -1;
                        }

                        if (p.SethourlyRate(hourlyRate))
                        {
                            Console.WriteLine("hourlyRate accepted [{0}].\n", hourlyRate);
                            break;
                        }
                        Console.WriteLine("hourlyRate must be numeric, non-zero, and non-negative : [{0}].\n", hourlyRate);
                    }

                    if (!isOver)
                    {
                        //Add remaining fields
                        p.SetlastName(lastName);
                        p.SetfirstName(firstName);
                        p.SetsocialInsuranceNumber(socialInsuranceNumber);
                        p.SetdateOfBirth(dateOfBirth);

                        try
                        {
                            if (p.Validate())
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + p.GetfirstName() + "-" + p.GetlastName() + " SIN: " + p.GetsocialInsuranceNumber(), true);
                                if (cContainer.add(p))
                                {
                                    Console.WriteLine("Employee successfully added!  Press any key to continue...");

                                }
                                else
                                {
                                    Console.WriteLine("Fail to add employee, the SIN number already exists. \nPress any key to continue...");

                                }
                                Console.ReadKey();
                                Console.Clear();

                            }
                            else
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + p.GetfirstName() + "-" + p.GetlastName() + " SIN: " + p.GetsocialInsuranceNumber(), false);
                                Console.WriteLine("Employee is not valid. Save failed.  Press any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Failed to add employee: " + e.Message);
                        }
                    }
                }
                //Contract time employee
                else if (nEmployeeType == 4)
                {
                    string start = "", stop = "";
                    float amount = 0;

                    //Add remaining fields
                    c.SetlastName(lastName);
                    c.SetdateOfBirth(dateOfBirth);

                    //Temp amount for validate() to pass at current stage in "input order"
                    c.SetfixedContractAmount(1);


                    quitCounter = 0;
                    while (!isOver)
                    {
                        while (!isOver)
                        {
                            Console.WriteLine("Please enter date of incorporation (YYYY-MM-DD): ");
                            dateOfBirth = Console.ReadLine();
                            if (tempEmployeeValidate.SetdateOfBirth(dateOfBirth))
                            {
                                if (dateOfBirth != "" && dateOfBirth != "N/A")
                                {
                                    Console.WriteLine("Date of incorporation accepted [{0}].\n", dateOfBirth);
                                    break;
                                }
                            }
                            logger.Log("UIMenu", "CreateEmployee(int)", "Employee set date of incorporation return false. Value:" + dateOfBirth, false);
                            Console.WriteLine("Must be in form of YYYY-MM-DD,\nwhere Y, M, and D each represent a digit: [{0}].\n", dateOfBirth);
                        }

                        while (!isOver)
                        {
                            Console.WriteLine("Please enter Business Number (9 consecutive digits): ");
                            socialInsuranceNumber = Console.ReadLine();
                            if (c.SetsocialInsuranceNumber(socialInsuranceNumber))
                            {
                                Console.WriteLine("Business Number accepted [{0}].\n", socialInsuranceNumber);
                                break;
                            }
                            logger.Log("UIMenu", "CreateEmployee(int)", "Employee business number return false. Value:" + socialInsuranceNumber, false);
                            Console.WriteLine("Must be 9 consecutive digits: [{0}].\n", socialInsuranceNumber);
                            quitCounter++;
                            if (quitCounter > 2)
                            {
                                if (EndInputs())
                                {
                                    isOver = true;
                                    break;
                                }
                            }
                        }

                        if (!c.validateSIN())
                        {
                            Console.WriteLine("WARNING: Business Number or Date of incorporation do not match [{0}].\n", socialInsuranceNumber);
                            continue;
                        }
                        break;
                    }

                    quitCounter = 0;
                    while (!isOver)
                    {
                        while (!isOver)
                        {
                            Console.WriteLine("Please enter contract start date (YYYY-MM-DD): ");
                            start = Console.ReadLine();
                            if (c.SetcontractStartDate(start))
                            {
                                if (start != "" || start != "N/A")
                                {
                                    Console.WriteLine("contract start date accepted [{0}].\n", start);
                                    break;
                                }
                            }
                            Console.WriteLine("Must be in form of YYYY-MM-DD, \nwhere Y, M, and D each represent a digit: [{0}].\n", start);
                            quitCounter++;
                            if (quitCounter > 4)
                            {
                                if (EndInputs())
                                {
                                    isOver = true;
                                }
                            }
                        }

                        while (!isOver)
                        {
                            Console.WriteLine("Please enter contract stop date (YYYY-MM-DD): ");
                            stop = Console.ReadLine();
                            if (c.SetcontractStopDate(stop))
                            {
                                if (stop != "" || stop != "N/A")
                                {
                                    Console.WriteLine("Contract stop date accepted [{0}].\n", stop);
                                    break;
                                }
                            }
                            Console.WriteLine("Must be in form of YYYY-MM-DD, \nwhere Y, M, and D each represent a digit: [{0}].\n", stop);
                            quitCounter++;
                            if (quitCounter > 4)
                            {
                                if (EndInputs())
                                {
                                    isOver = true;
                                    break;
                                }
                            }
                        }

                        if (!isOver)
                        {
                            try
                            {
                                c.Validate();
                                Console.WriteLine("Both contract start and end dates are valid.\n");
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            quitCounter++;
                            if (quitCounter > 4)
                            {
                                if (EndInputs())
                                {
                                    isOver = true;
                                }
                            }
                        }
                    }

                    quitCounter = 0;
                    while (!isOver)
                    {
                        Console.WriteLine("Please enter fixed contract amount(numeric, non-negative): ");
                        //If parsing float fails, salary remains 0
                        if (!float.TryParse(Console.ReadLine(), out amount))
                        {
                            amount = 0;
                        }

                        if (c.SetfixedContractAmount(amount))
                        {
                            Console.WriteLine("Amount accepted [{0}].\n", amount);
                            break;
                        }
                        Console.WriteLine("Amount must be numeric, non-zero, and non-negative : [{0}].\n", amount);
                    }

                    if (!isOver)
                    {
                        try
                        {
                            if (c.Validate())
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + c.GetfirstName() + "-" + c.GetlastName() + " SIN: " + c.GetsocialInsuranceNumber(), true);
                                if (cContainer.add(c))
                                {
                                    Console.WriteLine("Employee successfully added!  Press any key to continue...");

                                }
                                else
                                {
                                    Console.WriteLine("Fail to add employee, the SIN/BN number already exists. \nPress any key to continue...");

                                }
                                Console.ReadKey();
                                Console.Clear();

                            }
                            else
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + c.GetfirstName() + "-" + c.GetlastName() + " SIN: " + c.GetsocialInsuranceNumber(), false);
                                Console.WriteLine("Employee is not valid. Save failed.  Press any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Failed to add employee: " + e.Message);
                        }
                    }


                }
                //Seasonal time employee
                else if (nEmployeeType == 5)
                {
                    //Add remaining fields

                    s.SetlastName(lastName);
                    s.SetfirstName(firstName);
                    s.SetsocialInsuranceNumber(socialInsuranceNumber);
                    s.SetdateOfBirth(dateOfBirth);

                    string season = "";
                    float piecePay = 0;

                    quitCounter = 0;
                    while (!isOver)
                    {
                        Console.WriteLine("Please enter the season (one of WINTER SPRING SUMMER FALL): ");
                        season = Console.ReadLine();
                        if (s.Setseason(season))
                        {
                            if (season != "")
                            {
                                Console.WriteLine("Season accepted [{0}].\n", season);
                                break;
                            }
                        }
                        Console.WriteLine("Only winter, spring, summer, or fall are valid inputs \n(non case-sensitive): [{0}].\n", season);
                        quitCounter++;
                        if (quitCounter > 2)
                        {
                            if (EndInputs())
                            {
                                isOver = true;
                                break;
                            }
                        }
                    }

                    while (!isOver)
                    {
                        Console.WriteLine("Please enter piece pay amount (numeric, non-negative): ");
                        //If parsing float fails, salary remains 0
                        if (!float.TryParse(Console.ReadLine(), out piecePay))
                        {
                            piecePay = 0;
                        }

                        if (s.SetpiecePay(piecePay))
                        {
                            Console.WriteLine("Amount accepted [{0}].\n", piecePay);
                            break;
                        }
                        Console.WriteLine("Amount must be numeric, non-zero, and non-negative : [{0}].\n", piecePay);
                    }

                    if (!isOver)
                    {
                        try
                        {
                            if (s.Validate())
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + s.GetfirstName() + "-" + s.GetlastName() + " SIN: " + s.GetsocialInsuranceNumber(), true);
                                if (cContainer.add(s))
                                {
                                    Console.WriteLine("Employee successfully added!  Press any key to continue...");

                                }
                                else
                                {
                                    Console.WriteLine("Fail to add employee, the SIN number has already exists. \nPress any key to continue...");

                                }
                                Console.ReadKey();
                                Console.Clear();

                            }
                            else
                            {
                                logger.Log("UIMenu", "CreateEmployee(int)", "Validate " + s.GetfirstName() + "-" + s.GetlastName() + " SIN: " + s.GetsocialInsuranceNumber(), false);
                                Console.WriteLine("Employee is not valid. Save failed.  \nPress any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Failed to add employee: " + e.Message);
                        }
                    }
                }
            }
        }

        ///
        /// \brief This method uses the Supporting class library to load a database from file.
        /// \details <b>Details</b> - The FileIO class contains methods to open and read the database 
        ///                           from file.  These will be invoked and the database will be saved
        ///                           into the cContainer attribute above.
        /// \param none
        /// \return none
        /// 
        public void LoadDBase()
        {
            try
            {
                bool result = cContainer.loadData();
                if (!result)
                {
                    Console.WriteLine("Load data failed.\n");
                }
                else
                {
                    Console.WriteLine("Load data successfully.\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Load Data failed. " + e.Message + "\n");
            }

        }

        ///
        /// \brief This method uses the Supporting class library to save a database into a file.
        /// \details <b>Details</b> - The FileIO class contains methods to write and save a database to 
        ///                           a file.  These will be invoked and cContainer database will be saved
        ///                           into a file.
        /// \param none
        /// \return none
        /// 
        public void SaveDBase()
        {
            try
            {
                bool result = cContainer.saveData();
                if (!result)
                {
                    Console.WriteLine("Save data failed.\n");
                }
                else
                {
                    Console.WriteLine("Save data successfully.\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error: Save Data failed. " + e.Message + "\n");
            }
        }

        ///
        /// \brief This method displays all employees within the database (valid and invalid).
        /// \details <b>Details</b> - Employees will be displayed along with their full attributes,
        ///                           one employee at a time through the database list.  The Details
        ///                           method is used from the AllEmployee class.
        /// \param none
        /// \return none
        /// 
        public void DisplayEmployeeSet()
        {
            try
            {
                cContainer.traverseEmployee();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Display all employee failed. " + e.Message);
            }

        }

        ///
        /// \brief This method will modify an existing employee
        /// \details <b>Details</b> - User is prompt for a specific employee type to modify.  
        ///                           If employee exists, proceed to modify the employee.
        /// \param none
        /// \return none
        /// 
        public void ModifyEmployee()
        {
            bool isloop = true;
            Employee employee = null;
            Employee cEmployee = null; 
            while (isloop)
            {
                Console.WriteLine("Please enter the SIN of the employee you want to modify. \n(press Enter key to go back to main menu.)");
                string sin = Console.ReadLine(); 
                string sinToBN = "";
                sin.Replace("\n", "");
                if (sin.Trim() == "")
                {
                    isloop = false;
                    Console.Clear();
                    Console.WriteLine("Enter pressed. Returning to Main Menu ...\n");
                    break;
                }
                //remove all unwanted characters and add space for format
                sin = Regex.Replace(sin, "[^0-9]", "");
                if (sin.Length == 9) {
                    sinToBN = sin.Insert(5, " ");
                    sin = sin.Insert(3, " ");
                    sin = sin.Insert(7, " ");
                }
                else
                {
                    Console.WriteLine("The employee with the SIN " + sin + " does not exist.\n");
                    continue;
                }
               
                    employee = cContainer.search(sin);
                    cEmployee = cContainer.search(sinToBN);

                    if ((employee == null) && (cEmployee == null))
                    {
                        Console.WriteLine("The employee with the SIN " + sin + " does not exist.\n");
                        continue;
                    }
                    else
                    {
                        if (employee == null)
                        {
                            employee = cEmployee;

                        }

                        employee.Details();
                        logger.Log("UIMenu", "ModifyEmployee", "Employee detail is called:\n " + employee.ToString(), true);
                        char eType = employee.GetemployeeType();

                        switch (eType)
                        {

                            case Employee.FULL_TIME:
                                modifyFullTimeEmployee(ref employee);
                                break;

                            case Employee.PART_TIME:
                                modifyPartTimeEmployee(ref employee);
                                break;

                            case Employee.CONTRACT:
                                modifyContractEmployee(ref employee);
                                break;

                            case Employee.SEASONAL:
                                modifySeasonaleEmployee(ref employee);
                                break;

                        } 
                    }
             


            }


        }
        ///
        /// \brief This method will modify an employee considering it as a base class
        /// \details <b>Details</b> -  modiy fist name, last name,date of birth. SIN and type of employee can not be modified
        /// \param ref Employee employee
        /// \return none
        /// 
        private void modifyEmployee(ref Employee employee)
        {
            bool isLoop = true;
            if (employee.GetemployeeType() != Employee.CONTRACT)
            {
               
                while (isLoop)
                {
                    Console.WriteLine("Please enter the first name (valid characters: a-z, A-Z, ', -): ");
                    string name = Console.ReadLine();
                    if (employee.SetfirstName(name))
                    {
                        if (name != "")
                        {
                            isLoop = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("First Name is not valid.\n");
                        logger.Log("UIMenu", "modifyEmployee(ref Employee employee)", "Employee set first name return false. Value:" + name, false);
                    }
                }
                //last Name
                isLoop = true;
                while (isLoop)
                {
                    Console.WriteLine("Please enter the last name (valid characters: a-z, A-Z, ', -): ");
                    string name = Console.ReadLine();
                    if (employee.SetlastName(name))
                    {
                        if (name != "")
                        {
                            isLoop = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Last Name is not valid.\n");
                        logger.Log("UIMenu", "modifyEmployee(ref Employee employee)", "Employee set last name return false. Value:" + name, false);
                    }
                }

                isLoop = true;
                //dateOfBirth
                while (isLoop)
                {
                    Console.WriteLine("Please enter date of birth (YYYY-MM-DD): ");
                    string date = Console.ReadLine();

                    if (employee.SetdateOfBirth(date))
                    {
                        if (date != "N/A" && date != "")
                        { 
                            isLoop = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Date of birth is not valid.\n");
                        logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set date of birth return false. Value:" + date, false);
                    }
                }
            }
            else
            {
                isLoop = true;
                while (isLoop)
                {
                    Console.WriteLine("Please enter the company name (valid characters: a-z, A-Z, ', -): ");
                    string name = Console.ReadLine();
                    if (employee.SetlastName(name))
                    {
                        if (name != "")
                        {
                            isLoop = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Company Name is not valid.\n");
                        logger.Log("UIMenu", "modifyEmployee(ref Employee employee)", "Employee set last name return false. Value:" + name, false);
                    }
                }
            }
           

            
            
        }
        ///
        /// \brief This method will modify an fulltime employee  
        /// \details <b>Details</b> -  it first copy the value to a new fulltime employee object, then pass the object to container. If the object is valid then modify successd
        /// \param ref Employee employee
        /// \return none
        /// 
        private void modifyFullTimeEmployee(ref Employee employee)
        {

            
            FulltimeEmployee femployee = (FulltimeEmployee)employee;
            FulltimeEmployee newEmployee = new FulltimeEmployee();

            if (!newEmployee.SetfirstName(femployee.GetfirstName()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set first name return false. Value:" + femployee.GetfirstName(), false);
            }
           
            if (!newEmployee.SetlastName(femployee.GetlastName()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set last name return false. Value:" + femployee.GetlastName(), false);
            }
            
            if (!newEmployee.SetdateOfHire(femployee.GetdateOfHire()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set date of hire return false. Value:" + femployee.GetdateOfHire(), false);
            }

            if (!newEmployee.SetdateOfTermination(femployee.GetdateOfTermination()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set date of termination return false. Value:" + femployee.GetdateOfTermination(), false);
            }
            if (!newEmployee.SetdateOfBirth(femployee.GetdateOfBirth()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set date of birth return false. Value:" + femployee.GetdateOfBirth(), false);
            }
            if (!newEmployee.SetsocialInsuranceNumber(femployee.GetsocialInsuranceNumber()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set social Insurance Number return false. Value:" + femployee.GetsocialInsuranceNumber(), false);
            }
            if (!newEmployee.Setsalary(femployee.Getsalary()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set social Salary return false. Value:" + femployee.Getsalary(), false);
            }
            if (!newEmployee.SetemployeeType(femployee.GetemployeeType()))
            {
                logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set social Salary return false. Value:" + femployee.GetemployeeType(), false);
            }
            Employee tempEmployee = newEmployee;
            this.modifyEmployee(ref tempEmployee);
            //hire date
            bool isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter employee hire date (YYY-MM-DD): ");
                string date = Console.ReadLine();
                if (newEmployee.SetdateOfHire(date))
                {
                    if (date != "N/A" && date != "")
                    {
                        isLoop = false;
                    }
                }
                else
                {
                    logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set data of hire return false. Value:" + date, false);
                    Console.WriteLine("Employee hire date is not valid.\n");
                }
            }
            //termation date
            isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter employee termination date (YYY-MM-DD): ");
                string date = Console.ReadLine();
                if (newEmployee.SetdateOfTermination(date))
                {
                    isLoop = false;
                }
                else
                {
                    Console.WriteLine("Employee termination date is not valid.\n");
                    logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set termination date return false. Value:" + date, false);
                }
            }
            // salary
            isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter employee salary (numeric, non-negative): ");
                string salarystr = Console.ReadLine();
                try
                {
                    float salary = float.Parse(salarystr);

                    if (newEmployee.Setsalary(salary))
                    {
                        isLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Employee salary is not valid.\n");
                        logger.Log("UIMenu", "modifyFullTimeEmployee", "Employee set salary return false. Value:" + salary, false);
                    }
                }
                catch (Exception e)
                {
                    
                    Console.WriteLine("Employee salary is not valid:\n" + e.Message + "\n");
                }

            }

            try
            {
                cContainer.modify(newEmployee);
                Console.WriteLine("Modify employee successfully. \n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Modify employee failed: \n" + e.Message + "\n");
            }
            
        }
        ///
        /// \brief This method will modify a parttime employee  
        /// \details <b>Details</b> -  it first copy the value to a new fulltime employee object, then pass the object to container. If the object is valid then modify successd
        /// \param ref Employee employee
        /// \return none
        ///
        private void modifyPartTimeEmployee(ref Employee employee)
        {
            ParttimeEmployee femployee = (ParttimeEmployee)employee;
            ParttimeEmployee newEmployee = new ParttimeEmployee();

            if (!newEmployee.SetfirstName(femployee.GetfirstName()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set first name return false. Value:" + femployee.GetfirstName(), false);
            }

            if (!newEmployee.SetlastName(femployee.GetlastName()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set last name return false. Value:" + femployee.GetlastName(), false);
            }

            if (!newEmployee.SetdateOfHire(femployee.GetdateOfHire()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set date of hire return false. Value:" + femployee.GetdateOfHire(), false);
            }

            if (!newEmployee.SetdateOfTermination(femployee.GetdateOfTermination()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set date of termination return false. Value:" + femployee.GetdateOfTermination(), false);
            }
            if (!newEmployee.SetdateOfBirth(femployee.GetdateOfBirth()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set date of birth return false. Value:" + femployee.GetdateOfBirth(), false);
            }
            if (!newEmployee.SetsocialInsuranceNumber(femployee.GetsocialInsuranceNumber()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set social Insurance Number return false. Value:" + femployee.GetsocialInsuranceNumber(), false);
            }
             
            if (!newEmployee.SetemployeeType(femployee.GetemployeeType()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set social Salary return false. Value:" + femployee.GetemployeeType(), false);
            }

            if (!newEmployee.SethourlyRate(femployee.GethourlyRate()))
            {
                logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set hourly rate return false. Value:" + femployee.GethourlyRate(), false);
            } 
            Employee tempEmployee = newEmployee;
            this.modifyEmployee(ref tempEmployee);  
            //hire date
            bool isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter employee hire date (YYY-MM-DD): ");
                string date = Console.ReadLine();
                if (newEmployee.SetdateOfHire(date))
                {
                    if (date != "N/A" && date != "")
                    {
                        isLoop = false;
                    }
                }
                else
                {
                    logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set date of hire return false. Value:" + date, false);
                    Console.WriteLine("Employee hire date is not valid.\n");
                }
            }
            //termation date
            isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter employee termination date (YYY-MM-DD): ");
                string date = Console.ReadLine();
                if (newEmployee.SetdateOfTermination(date))
                {
                    isLoop = false;
                }
                else
                {
                    logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set date of termination return false. Value:" + date, false);
                    Console.WriteLine("Employee termination date is not valid.\n");
                }
            }
            // salary
            isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter employee hourly rate (numeric, non-negative): ");
                string salarystr = Console.ReadLine();
                try
                {
                    float salary = float.Parse(salarystr);

                    if (newEmployee.SethourlyRate(salary))
                    {
                        isLoop = false;
                    }
                    else
                    {
                        logger.Log("UIMenu", "modifyPartTimeEmployee", "Employee set hourly rate return false. Value:" + salarystr, false);
                        Console.WriteLine("Employee hourly rate is not valid.\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Employee hourly rate is not valid:\n" + e.Message +"\n");
                }

            }

            try
            {
                cContainer.modify(newEmployee);
                Console.WriteLine("Modify Employee successfully.\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Modify Employee failed:\n" + e.Message + "\n");
            }
        }
        ///
        /// \brief This method will modify a contract employee  
        /// \details <b>Details</b> -  it first copy the value to a new fulltime employee object, then pass the object to container. If the object is valid then modify successd
        /// \param ref Employee employee
        /// \return none
        ///
        private void modifyContractEmployee(ref Employee employee)
        {
            ContractEmployee femployee = (ContractEmployee)employee;
            ContractEmployee newEmployee = new ContractEmployee();
            if (!newEmployee.SetfirstName(femployee.GetfirstName()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set first name return false. Value:" + femployee.GetfirstName(), false);
            }

            if (!newEmployee.SetlastName(femployee.GetlastName()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set last name return false. Value:" + femployee.GetlastName(), false);
            }
             
            if (!newEmployee.SetdateOfBirth(femployee.GetdateOfBirth()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set date of birth return false. Value:" + femployee.GetdateOfBirth(), false);
            }
            if (!newEmployee.SetsocialInsuranceNumber(femployee.GetsocialInsuranceNumber()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set social Insurance Number return false. Value:" + femployee.GetsocialInsuranceNumber(), false);
            }

            if (!newEmployee.SetemployeeType(femployee.GetemployeeType()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set social Salary return false. Value:" + femployee.GetemployeeType(), false);
            }

            if (!newEmployee.SetfixedContractAmount(femployee.GetfixedContractAmount()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set social Salary return false. Value:" + femployee.GetfixedContractAmount(), false);
            }
            if (!newEmployee.SetcontractStartDate(femployee.GetcontractStartDate()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set contract start date return false. Value:" + femployee.GetcontractStartDate(), false);
            }
            if (!newEmployee.SetcontractStopDate(femployee.GetcontractStopDate()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set contract stop date return false. Value:" + femployee.GetcontractStopDate(), false);
            } 
            Employee tempEmployee = newEmployee;
            this.modifyEmployee(ref tempEmployee);

            //contract start date
            bool isLoop = true;
            while(true)
            {
                while (isLoop)
                {
                    Console.WriteLine("\nPlease enter contract start date (YYY-MM-DD): ");
                    string date = Console.ReadLine();
                    if (newEmployee.SetcontractStartDate(date))
                    {
                        if (date != "N/A" && date != "")
                        { 
                            isLoop = false;
                        }
                    }
                    else
                    {
                        logger.Log("UIMenu", "modifyContractEmployee", "Employee set contract start date return false. Value:" + date, false);
                        Console.WriteLine("Contract start date is not valid.\n");
                    }
                }
                //termation date
                isLoop = true;
                while (isLoop)
                {
                    Console.WriteLine("Please enter contract stop date (YYY-MM-DD): ");
                    string date = Console.ReadLine();
                    if (newEmployee.SetcontractStopDate(date))
                    {
                        if (date != "N/A" && date != "")
                        {
                            isLoop = false;
                        }
                    }
                    else
                    {
                        logger.Log("UIMenu", "modifyContractEmployee", "Employee set contract stop date return false. Value:" + date, false);
                        Console.WriteLine("Contract termination date is not valid.\n");
                    }
                }

                try
                {
                    bool isValid = newEmployee.Validate();
                    logger.Log("UIMenu", "modifyContractEmployee", "Validate " + employee.GetfirstName() + "-" + employee.GetlastName() + " SIN: " + employee.GetsocialInsuranceNumber(), isValid);
                    Console.WriteLine("Contract start and termination dates are valid.\n");
                    break;
                }
                catch(Exception e)
                {
                    logger.Log("UIMenu", "modifyContractEmployee", "Validate " + employee.GetfirstName() + "-" + employee.GetlastName() + " SIN: " + employee.GetsocialInsuranceNumber(), false);
                    Console.WriteLine(e.Message + "\n");
                }

            }
            // salary
            isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter fixed contract amount(numeric, non-negative): ");
                string salarystr = Console.ReadLine();
                try
                {
                    float salary = float.Parse(salarystr);

                    if (newEmployee.SetfixedContractAmount(salary))
                    {
                        isLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Contract amount is not valid.\n");
                        logger.Log("UIMenu", "modifyContractEmployee", "Employee set contract amount return false. Value:" + salarystr, false);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Contract amount is not valid: \n" + e.Message + "\n");
                }

            }

            try
            {
                cContainer.modify(newEmployee);
                Console.WriteLine("Modify Employee successfully.\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Modify Employee failed:\n" + e.Message + "\n");
            }
            
        }
        ///
        /// \brief This method will modify an seasonal employee  
        /// \details <b>Details</b> -  it first copy the value to a new fulltime employee object, then pass the object to container. If the object is valid then modify successd
        /// \param ref Employee employee
        /// \return none
        ///
        private void modifySeasonaleEmployee(ref Employee employee)
        {
            SeasonalEmployee femployee = (SeasonalEmployee)employee;
            SeasonalEmployee newEmployee = new SeasonalEmployee();

            if (!newEmployee.SetfirstName(femployee.GetfirstName()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set first name return false. Value:" + femployee.GetfirstName(), false);
            }

            if (!newEmployee.SetlastName(femployee.GetlastName()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set last name return false. Value:" + femployee.GetlastName(), false);
            }

            if (!newEmployee.SetdateOfBirth(femployee.GetdateOfBirth()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set date of birth return false. Value:" + femployee.GetdateOfBirth(), false);
            }
            if (!newEmployee.SetsocialInsuranceNumber(femployee.GetsocialInsuranceNumber()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set social Insurance Number return false. Value:" + femployee.GetsocialInsuranceNumber(), false);
            }

            if (!newEmployee.SetemployeeType(femployee.GetemployeeType()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set employee type return false. Value:" + femployee.GetemployeeType(), false);
            }

            if (!newEmployee.SetpiecePay(femployee.GetpiecePay()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set piece pay return false. Value:" + femployee.GetpiecePay(), false);
            }
            if (!newEmployee.Setseason(femployee.Getseason()))
            {
                logger.Log("UIMenu", "modifyContractEmployee", "Employee set season return false. Value:" + femployee.Getseason(), false);
            }
            Employee tempEmployee = newEmployee;
            this.modifyEmployee(ref tempEmployee); 

            bool isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter the season (case insensitive: WINTER SPRING SUMMER FALL): ");
                string date = Console.ReadLine();
                if (newEmployee.Setseason(date))
                {
                    if (date != "N/A" && date != "")
                    {
                        isLoop = false;
                    }
                }
                else
                {
                    Console.WriteLine("Season is not valid.\n");
                    logger.Log("UIMenu", "modifyContractEmployee", "Employee set season return false. Value:" + date, false);
                }
            }
            isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("Please enter piece pay amount (numeric, non-negative): ");
                float salary;

                if (float.TryParse(Console.ReadLine(), out salary))
                {
                    if (newEmployee.SetpiecePay(salary))
                    {
                        isLoop = false;
                    }
                    else
                    {
                        logger.Log("UIMenu", "modifyContractEmployee", "Employee set piece pay return false. Value:" + salary, false);
                        Console.WriteLine("Piece pay amout is not valid.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Piece pay amout is not valid.\n");
                }

            }

            try
            {
                cContainer.modify(newEmployee);
                Console.WriteLine("Modify Employee successfully.\n" );
            }
            catch (Exception e)
            {
                Console.WriteLine("ModifyEmployee failed: \n" + e.Message + "\n");
            }
        }

        ///
        /// \brief This method will remove an existing employee found by a social insurance number.
        /// \details <b>Details</b> - The Container class found in TheCompany class library provides
        ///                           a method to search for an employee given a SIN.  Once found,
        ///                           user is prompt to confirm removal of employee.
        /// \param none
        /// \return none
        /// 
        public void RemoveEmployee()
        {
            bool isloop = true;
            Employee employee = null;
            Employee cEmployee = null;
            //search employee by sin first
            while (isloop)
            {
                Console.WriteLine("Please enter the SIN of the employee you want to remove. \n(press Enter key to go back to main menu.)");
                string sin = Console.ReadLine();
                string sinToBN = "";
                sin.Replace("\n", "");
                if (sin.Trim() == "")
                {
                    return;
                }
                //remove all unwanted characters and add space for format
                
              
               sin = Regex.Replace(sin, "[^0-9]", "");
               if (sin.Length == 9)
               {
                    sinToBN = sin.Insert(5, " ");
                    sin = sin.Insert(3, " ");
                    sin = sin.Insert(7, " ");
               }
               else
               {
                    Console.WriteLine("The employee with the SIN " + sin + " does not exist.\n");
                    continue;
               }
                
               
                

               employee = cContainer.search(sin);
               cEmployee = cContainer.search(sinToBN); 
               if ( (employee == null) && (cEmployee==null) )
               {
                    Console.WriteLine("Employee with SIN " + sin + " does not exist. \nDo you want to delete another employee? If so, enter Y.");
                    string answer = Console.ReadLine();
                    if (answer.ToUpper() != "Y")
                    {
                        isloop = false;
                    }

                }
                else
                {
                    //display the employee
                    if (employee == null)
                    {
                        employee = cEmployee;
                    }
                    employee.Details();
                    logger.Log("UIMenu", "RemoveEmployee", "Employee detail is called:\n " + employee.ToString(), true);
                    Console.WriteLine("Do you realy want to delete this employee? If so, enter Y.");

                    string answer = Console.ReadLine();
                    if (answer.ToUpper() != "Y")
                    {
                        continue;
                    }
                    else
                    {
                        isloop = false;
                    }
                }
            }

            //if employee is not null delete it 
            try
            {
                if (employee != null)
                {
                    bool result = cContainer.remove(employee);
                    if (result)
                    {
                        Console.WriteLine("Delete employee successfully.\n");
                    }
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("Delete employee failed:\n" + e.Message + "\n");
            }


        }

        ///
        /// \brief This method produces a menu of options and returns a valid input choice.
        /// \details <b>Details</b> - EMPLOYEE MANAGEMENT MENU: View employee database, create new employee, modify/remove existing employee.
        ///                           Viewing employee database traverses the container and uses the Details method to output information.
        ///                           CreateEmployee is used to handle prompts for employee attributes required to build an employee.
        ///                           Modify & Remove an existing employee uses the Container's search method to select and modify/remove employee.
        /// \param none
        /// \return Int32.Parse(ckInfo.KeyChar.ToString()) - <b>int</b> - integer representation of keystroke input
        /// 
        public bool EndInputs()
        {
            Console.WriteLine("[You seem to be having trouble.]\n[Return to main menu?]\n[If so, press 1.]\n");
            ckInfo = Console.ReadKey();
            Console.WriteLine();
            if (ckInfo.KeyChar == '1')
            {
                Console.Clear();
                Console.WriteLine("1 Pressed. Returning to Main Menu... \n");
                return true;
            }

            return false;
        }


    }
}
