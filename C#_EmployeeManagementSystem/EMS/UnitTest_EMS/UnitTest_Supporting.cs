///
/// \brief contains 8 testing cases for the supporting class library
/// 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supporting;
using AllEmployees;
using System.Collections.Generic;

namespace UnitTest_EMS
{
    [TestClass]
    public class UnitTest_Supporting
    {
        FileIO fileIO = new FileIO();
        Logging logger = new Logging();
        const string dBaseFileName = "ems.dBase.csv";  ///< \breif the name of database file
        const string dBaseFileExtension = "*.csv";     ///< \breif the name of database file extension
        const string dBaseFolderName = "DBase";        ///< \breif the name of database folder


        /**
        \details    Test identifier:       FileIO_UnitTest_1 
        \verbatim
                    Description:                        This function tests a method from the FileIO class called fileIO.CreateFile(). It tests if the file is successfully created
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         File Name:      log.txt
                                                        Folder Name:    log
                                                        File Filter:    *.txt   
                    Expected outputs:                   the CreateFile() method returns true;   
                    Observed outputs:                   the CreateFile() method returns true;     
         \endverbatim
         */
        [TestMethod]
        public void FileIO_UnitTest_1()
        {
            // arrange
            bool expected = true;
            bool actual = false;

            
            try
            {
                actual = fileIO.CreateFile("log.txt", "log", "*.txt");
            }
            catch (Exception e)
            {
                actual = false;
            }
            

            // assert
            Assert.AreEqual(expected, actual, "Fail to create a file.");
        }


        /**
        \details    Test identifier:       FileIO_UnitTest_2 
        \verbatim
            Description:                        This function tests a method from the container class called WriteDatabase(). it test if the container write to database file
            Method of execution:                Automatic  
            Test Type:                          Normal Test
            Input data:                         A data container that contains a employee whose name is Bill White  
            Expected outputs:                   the WriteDatabase() method returns true;   
            Observed outputs:                   the WriteDatabase() method returns true;     
        \endverbatim
        */
        [TestMethod]
        public void FileIO_UnitTest_2()
        {
            // arrange
            bool expected = true;
            bool actual = false;
            FulltimeEmployee employee = new FulltimeEmployee();
            employee.SetfirstName("Bill");
            employee.SetlastName("White");
            employee.SetsocialInsuranceNumber("193456787");
            employee.SetemployeeType(Employee.FULL_TIME);
            employee.Setsalary(10000);
            employee.SetdateOfHire("2014-12-01");
            employee.SetdateOfBirth("1986-09-08");
            Dictionary<string, Employee> employees = new Dictionary<string,Employee>();
            employees.Add(employee.GetsocialInsuranceNumber(),employee);

            try
            {
                actual = fileIO.WriteDatabase(employees);
            }
            catch (Exception e)
            {
                actual = false;
            }


            // assert
            Assert.AreEqual(expected, actual, "Write Database failed.");
        }


        /**
        \details    Test identifier:       FileIO_UnitTest_3 
        \verbatim
            Description:                        This function tests a method from the FileIO class called WriteDatabase(). It tests if a null input is properly handled
            Method of execution:                Automatic  
            Test Type:                          Exception Test
            Input data:                         null input
            Expected outputs:                   the WriteDatabase() method returns false; 
            Observed outputs:                   the WriteDatabase() method returns false;     
        \endverbatim
        */
        [TestMethod]
        public void FileIO_UnitTest_3()
        {
            // arrange
            bool expected = false;
            bool actual = true;


            try
            {
                actual = fileIO.WriteDatabase(null);
            }
            catch (Exception e)
            {
                actual = true;
            }


            // assert
            Assert.AreEqual(expected, actual, "FileIO.WriteDatabase() failed to handle null input.");
        }

        /**
        \details    Test identifier:       FileIO_UnitTest_4 
        \verbatim
            Description:                        This function tests a method from the FileIO class called ReadDatabase(). It tests if an empty data container is properly handled
            Method of execution:                Automatic  
            Test Type:                          Normal Test
            Input data:                         an empty data container
            Expected outputs:                   the ReadDatabase() method returns true;
            Observed outputs:                   the ReadDatabase() method returns true;     
        \endverbatim
        */
        [TestMethod]
        public void FileIO_UnitTest_4()
        {
            Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
            // arrange
            bool expected = true;
            bool actual = true;

            try
            {
                actual = fileIO.ReadDatabase(ref employees);
            }
            catch (Exception e)
            {
                actual = false;
            }


            // assert
            Assert.AreEqual(expected, actual, "FileIO.ReadDatabase() failed to handle data container.");
        }


        /**
        \details    Test identifier:       Log_UnitTest_1 
        \verbatim
            Description:                        This function tests a method from the Logging class called Log(). It tests if the log file is successfully created
            Method of execution:                Automatic  
            Test Type:                          Normal Test
            Input data:                         Class Name:      Employee
                                                File Name:       Validator
                                                Event:           Bill - 122334345
                                                Event Result:    true
            Expected outputs:                   the Log() method created a log file with appropriate name
            Observed outputs:                   a log file with appropriate name exists     
        \endverbatim
        */
        [TestMethod]
        public void Log_UnitTest_1()
        {
            // arrange
            bool expected = true;
            bool actual = false;

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string fileName = "ems." + date + ".log";

            try
            {
                logger.Log("Employee", "Validator", "Bill - 122334345", true);
                if (fileIO.GetStreamWriter(fileName, "log", "*.log") != null)
                {
                    actual = true;
                }
            }
            catch (Exception e)
            {
                    actual = false;
                
            }

            // assert
            Assert.AreEqual(expected, actual, "Fail to log an event.");
        }


        /**
        \details    Test identifier:       Log_UnitTest_2 
        \verbatim
            Description:                        This function tests a method from the Logging class called Log(). It tests if the log file is successfully created
            Method of execution:                Automatic  
            Test Type:                          Normal Test
            Input data:                         Class Name:      Employee
                                                File Name:       Validator
                                                Event:           Bill - 122334345
                                                Event Result:    false
            Expected outputs:                   the Log() method created a log file with appropriate name
            Observed outputs:                   a log file with appropriate name exists    
        \endverbatim
        */
        [TestMethod]
        public void Log_UnitTest_2()
        {
            // arrange
            bool expected = true;
            bool actual = false;

            string fileName = "ems.2.log";

            try
            {
                logger.Log("Employee", "Validator", "Bill - 122334345", false);
                if (fileIO.GetStreamWriter(fileName, "log", "*.log") != null)
                {
                    actual = true;
                }
            }
            catch (Exception e)
            {
                actual = false;

            }

            // assert
            Assert.AreEqual(expected, actual, "Fail to log an event.");
        }


        /**
        \details    Test identifier:       Log_UnitTest_3 
        \verbatim
            Description:                        TThis function tests a method from the Logging class called Log(). It tests if null strings are handled
            Method of execution:                Automatic  
            Test Type:                          Exception Test
            Input data:                         Class Name:      null
                                                File Name:       null
                                                Event:           null
                                                Event Result:    false
            Expected outputs:                   the Log() method created a log file with appropriate name
            Observed outputs:                   a log file with appropriate name exists    
        \endverbatim
        */
        [TestMethod]
        public void Log_UnitTest_3()
        {
            // arrange
            bool expected = true;
            bool actual = false;

            string fileName = "ems.3.log";

            try
            {
                logger.Log(null, null, null, false);
                if (fileIO.GetStreamWriter(fileName, "log", "*.log") != null)
                {
                    actual = true;
                }
            }
            catch (Exception e)
            {
                actual = false;

            }

            // assert
            Assert.AreEqual(expected, actual, "Fail to log an event.");
        }


        /**
        \details    Test identifier:       Log_UnitTest_4 
        \verbatim
            Description:                        This function tests a method from the Logging class called Log(). It tests if empty strings are handled
            Method of execution:                Automatic  
            Test Type:                          Exception Test
            Input data:                         Class Name:      empty
                                                File Name:       empty
                                                Event:           empty
                                                Event Result:    false
            Expected outputs:                   the Log() method created a log file with appropriate name
            Observed outputs:                   a log file with appropriate name exists    
        \endverbatim
        */
        [TestMethod]
        public void Log_UnitTest_4()
        {
            // arrange
            bool expected = true;
            bool actual = false;

            string fileName = "ems.4.log";

            try
            {
                logger.Log("", "", "", false);
                if (fileIO.GetStreamWriter(fileName, "log", "*.log") != null)
                {
                    actual = true;
                }
            }
            catch (Exception e)
            {
                actual = false;

            }

            // assert
            Assert.AreEqual(expected, actual, "Fail to log an event.");
        }
    }
}
