using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCompany;
using AllEmployees;

namespace UnitTest_EMS
{
    [TestClass]
    public class UIMenuTest
    {




        /**
        \details    Test identifier:       MainMenu_Stub_Case_1 
        \verbatim
                    Description:                        This method tests a stub created from the MainMenu method in the UIMenu class.  The stub 
                                                        receives input as a parameter rather than standard input.  Tests a valid parameter (1).
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         int:            input = 1
                    Expected outputs:                   the MainMenu_Stub(input) method returns 1;   
                    Observed outputs:                   the MainMenu_Stub(input) method returns 1;   
         \endverbatim
         */
        [TestMethod]
        public void MainMenu_Stub_Case_1()
        {
            // act 
            int input = 1;
            int expected = 1;

            // arrange
            int actual = MainMenu_Stub(input);

            // assert
            Assert.AreEqual(expected, actual, "expected {0}, got {1}.", expected, actual);

        }





        /**
        \details    Test identifier:       MainMenu_Stub_Case_6
        \verbatim
                    Description:                        This method tests a stub created from the MainMenu method in the UIMenu class.  The stub 
                                                        receives input as a parameter rather than standard input.  Tests an invalid parameter (6).
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         int:            input = 6
                    Expected outputs:                   exception string returning "Invalid input, please try again."
                    Observed outputs:                   exception string returning "Invalid input, please try again."
         \endverbatim
         */
        [TestMethod]
        public void MainMenu_Stub_Case_6()
        {
            // act 
            int input = 6;
            int expected = 6;
            try
            {
                // arrange
                int actual = MainMenu_Stub(input);

                Assert.AreEqual(expected, actual, "expected {0}, got {1}.", expected, actual);
            }
            catch (Exception e)
            {
                // assert
                Assert.Fail("Invalid input, please try again.");
                StringAssert.Contains(e.Message, "Invalid input, please try again.");
            }
        }





        /**
        \details    Test identifier:       CreateEmployee_When_Adding_Valid_FullTimeEmployee
        \verbatim
                    Description:                        This method unit tests the ability to add an employee into the container.  The procedure initializes an employee
                                                        with valid information to be added.  The container class is then used to verify status of adding the employee.
                                                        The CreateEmployee method fround in UIMenu requires this ability.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         First Name   : "Bob"
                                                        Last Name    : Vu
                                                        Salary       : 10
                                                        SIN          : "046454286"
                                                        DOB          : "1982-02-02"
                                                        DOHire       : "2002-02-02"
                                                        DOTermination: "2006-02-02"
                    Expected outputs:                   true (Employee has been added)
                    Observed outputs:                   true (Employee has been added)
         \endverbatim
         */
        [TestMethod]
        public void CreateEmployee_When_Adding_Valid_FullTimeEmployee()
        {
            // act 
            Container c = new Container();
            FulltimeEmployee input = new FulltimeEmployee();
            bool expected = true;
            bool actual;

            input.SetfirstName("Bob");
            input.SetlastName("Vu");
            input.Setsalary(10);
            input.SetsocialInsuranceNumber("046454286");
            input.SetdateOfBirth("1982-02-02");
            input.SetdateOfHire("2002-02-02");
            input.SetdateOfTermination("2006-02-02");

            // arrange
            actual = c.add(input);

            // assert
            Assert.AreEqual(expected, actual, "expected {0}, got {1}.", expected, actual);

        }





        /**
        \details    Test identifier:       CreateEmployee_When_Adding_Invalid_FullTimeEmployee
        \verbatim
                    Description:                        This method unit tests the ability to add an employee into the container.  The procedure initializes an employee
                                                        with invalid information to be added.  The container class is then used to verify status of adding the employee.
                                                        The CreateEmployee method fround in UIMenu requires this ability.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         First Name   : ""
                                                        Last Name    : "Vu"
                                                        Salary       : 10
                                                        SIN          : "046454286"
                                                        DOB          : "1982-02-02"
                                                        DOHire       : "2002-02-02"
                                                        DOTermination: "2006-02-02"
                    Expected outputs:                   "The employee is not valid."
                    Observed outputs:                   "The employee is not valid."
         \endverbatim
         */
        [TestMethod]
        public void CreateEmployee_When_Adding_Invalid_FullTimeEmployee()
        {
            // act 
            Container c = new Container();
            FulltimeEmployee input = new FulltimeEmployee();
            bool expected = false;
            bool actual;

            input.SetfirstName("");     // no valid full time employee should have blank name
            input.SetlastName("Vu");
            input.Setsalary(10);
            input.SetsocialInsuranceNumber("046454286");
            input.SetdateOfBirth("1982-02-02");
            input.SetdateOfHire("2002-02-02");
            input.SetdateOfTermination("2006-02-02");

            try
            {
                // arrange
                actual = c.add(input);
                Assert.AreEqual(expected, actual, "expected {0}, got {1}.", expected, actual);
            }
            catch (Exception e)
            {
                // assert
                StringAssert.Contains(e.Message, "The employee is not valid.");
            }

        }





        /**
        \details    Stub Method:       MainMenu_Stub(int nInput)
        \verbatim
                    Description:                        This method is the stub created based on the MainMenu method found in the UIMenu class.
                                                        It generates its user input through a parameter as opposed to standard input (for automatic
                                                        testing).
                    Input data:                         nInput : "user" input
         \endverbatim
         */
        public int MainMenu_Stub(int nInput)
        {
            int nBreak = 0;
            while (nBreak == 0)
            {
                //Console.WriteLine("Menu 1 : MAIN MENU");
                //Console.WriteLine("------------------");
                //Console.WriteLine("    1. Manage EMS DBase files"); // goes to Menu 2
                //Console.WriteLine("    2. Manage Employees"); // goes to menu 3
                //Console.WriteLine("    9. Quit");
                //ckInfo = Console.ReadKey();
                //Console.Clear();
                //Console.Write("{0} was read.  ", ckInfo.KeyChar);
                switch (nInput)
                {
                    case 1:
                        nBreak = 1;
                        break;
                    case 2:
                        nBreak = 1;
                        break;
                    case 9:
                        nBreak = 1;
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again.\n");
                        nBreak = 1;
                        break;
                }
            }
            //Console.Clear();
            //return Int32.Parse(ckInfo.KeyChar.ToString());
            return nInput;
        }

        ///
        /// \METHODS NOT TESTED IN UIMenu.cs : 
        /// \MenuRun()
        /// \Reason: Requires integration from many methods,
        ///          no "units" to test.  Possible tests (to 
        ///          gather input) completed by MainMenu() tests.
        ///          
        /// \FileManagementMenu()
        /// \Reason: MainMenu() was tested and essentially 
        ///          replicates this method.
        ///          
        /// \EmployeeManagementMenu()
        /// \Reason: MainMenu() was tested and essentially
        ///          replicates this method.
        ///      
        /// \EmployeeDetailsMenu()
        /// \Reason: MainMenu() was tested and essentially 
        ///          replicates this method.
        ///    
        /// \LoadDBase()
        /// \Reason: Method integrates AllEmployees, TheCompany,
        ///          and Supporting class libraries to run the bulk
        ///          of the processing.  Testing done in multiple other 
        ///          locations.
        ///          
        /// \SaveDBase()
        /// \Reason: Method integrates AllEmployees, TheCompany,
        ///          and Supporting class libraries to run the bulk
        ///          of the processing.  Testing done in multiple other
        ///          locations.
        ///      
        /// \DisplayEmployeeSet()
        /// \Reason: Takes no parameters, returns no values.
        ///          Method simply displays a table with information.
        ///  
        /// \ModifyEmployee()
        /// \Reason: Unit test involves testing the SEARCH functionality
        ///          of the container class.  Method already tested.
        ///
        /// \RemoveEmployee()
        /// \Reason: No units to break up and test.
        ///
    }
}
