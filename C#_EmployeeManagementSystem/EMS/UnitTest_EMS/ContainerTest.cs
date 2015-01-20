using System;
using AllEmployees;
using TheCompany;
using Presentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_EMS
{
    [TestClass]
    public class ContainerTest
    {
         
        /**
         \details Test Identifier: Container_UnitTest_1
         \verbatim Description <b>Description</b> This function tests a method from the container class called add. it test that the method by add a valid employee to container
        
                 Test Conduction: Automatic
                 Test Type: Normal Test
                 Sample Data: 
                 First name: Bill White
                 Last name: White
                 SIN: 193456787
                 Employee Type: Employee.FULL_TIME
                 Salary: 10000
                 Date of hire: "2014-12-01"
                 Birthday: "1986-09-08"
                 Expected Outcome: the add method returns true;
                 Actual Test results: the add method returns true;
        \endverbatim
        */
        [TestMethod]
        public void Container_UnitTest_1()
        {
            // arrange
            bool expected = true;
            FulltimeEmployee employee = new FulltimeEmployee();
            employee.SetfirstName("Bill");
            employee.SetlastName("White");
            employee.SetsocialInsuranceNumber("193456787");
            employee.SetemployeeType(Employee.FULL_TIME);
            employee.Setsalary(10000);
            employee.SetdateOfHire("2014-12-01");
            employee.SetdateOfBirth("1986-09-08");
            bool actual = true;
            if ( employee.Validate() )
            {
                Container contianer = new Container();
              
                try
                {
                    actual = contianer.add(employee);
                }
                catch (Exception e)
                {
                    actual = false;
                }
            } 

            // assert
            Assert.AreEqual(expected, actual, "Add an employee failed.");
        }
        /**
         \details  Test Identifier: Container_UnitTest_2
         \verbatim  Description <b>Description</b> This function tests a method from 
                     the container class called add. it test that 
                     method by add two valid employee with the same SIN to container
        
                     Test Conduction: Automatic
                     Test Type: Normal Test
                     Sample Data: 
                     Employee One:
                         Last name: White
                         SIN: 193456787
                         Employee Type: Employee.FULL_TIME
                         Salary: 10000
                         Date of hire: "2014-12-01"
                         Birthday: "1986-09-08" 
                     Employee Two:
                         Last name: White
                         SIN: 193456787
                         Employee Type: Employee.FULL_TIME
                         Salary: 10000
                         Date of hire: "2014-12-01"
                         Birthday: "1986-09-08"    
                     Expected Outcome: the add method returns false;
                     Actual Test results: the add method returns false;
        \endverbatim
        */
        [TestMethod]
        public void Container_UnitTest_2()
        {
            // arrange
            bool expected = false;
            FulltimeEmployee employee1 = new FulltimeEmployee();
            employee1.SetfirstName("Bill");
            employee1.SetlastName("White");
            employee1.SetsocialInsuranceNumber("193456787");
            employee1.SetemployeeType(Employee.FULL_TIME);
            employee1.Setsalary(10000);
            employee1.SetdateOfHire("2014-12-01");
            employee1.SetdateOfBirth("1999-09-08");
            FulltimeEmployee employee2 = new FulltimeEmployee();
            employee2.SetfirstName("Bill");
            employee2.SetlastName("White");
            employee2.SetsocialInsuranceNumber("193456787");
            employee2.SetemployeeType(Employee.FULL_TIME);
            employee2.Setsalary(10000);
            employee2.SetdateOfHire("2014-12-01");
            employee2.SetdateOfBirth("1999-09-08");
            bool actual = true;
            
            Container container = new Container();
           

           try
           {
                container.add(employee1);
                actual = container.add(employee2);
           }
           catch (Exception e)
           {
                actual = false;
           }

            // assert
            Assert.AreEqual(expected, actual, "Add two employee with same attributes successfully.");
        } 
        /*
         \details Test Identifier: Container_UnitTest_3
         \verbatim Description <b>Description</b> This function tests a method from 
                     the container class called remove(Employee e). Remove an employee
                     from the container which contains no employee.
                     Test Conduction: Automatic
                     Test Type: Boundry Test
                     Sample Data:  
                     FirstName : Hello
                     LastName  : Kitty
                     Social Insurance Number :193456787 
                     Employee Type : Full time
                     Salary :  10000 
                     Date Of Hire : 2014-12-01 
                     DateOfBirth : 1999-09-08
                     Expected Outcome:  remove() method returns false;
                     Actual Test results: remove() method returns false; 
         \endverbatim
        */
        [TestMethod]
        public void Container_UnitTest_3()
        {

            bool expected = false;
            // arrange 
            FulltimeEmployee employee = new FulltimeEmployee();
            employee.SetfirstName("Hello");
            employee.SetlastName("Kitty");
            employee.SetsocialInsuranceNumber("193456787");
            employee.SetemployeeType(Employee.FULL_TIME);
            employee.Setsalary(10000);
            employee.SetdateOfHire("2014-12-01");
            employee.SetdateOfBirth("1999-09-08"); 

            Container contianer = new Container();
            bool actual = true;

            try
            {
                actual = contianer.remove(employee);
            }
            catch (Exception e)
            {
                actual = false;
            }
            // assert
            Assert.AreEqual(expected, actual, "Remove employee from an empty container successfully");
        }
        
         /*
         \details Test Identifier: Container_UnitTest_4()
         \verbatim Description <b>Description</b> This function tests a method from the container class called add(Employee e). Add a null to container.
                     Test Conduction: Automatic
                     Test Type: Exception Test
                     Sample Data: 
                             null
                     Expected Outcome:  add method throws a exception
                     Actual Test results: add method throws a exception
         \endverbatim
        */
        [TestMethod]
        public void Container_UnitTest_4()
        {
            bool expected = false;
            // arrange  

            Container contianer = new Container();
            bool actual = true;

            try
            {
                actual = contianer.add(null);
            }
            catch (Exception e)
            {
                actual = false;
            }
            // assert
            Assert.AreEqual(expected, actual, "add a null to container successfully.");
        }

        /*
         \details Test Identifier: Container_UnitTest_5()
         
         Description <b>Description</b> This function tests a method from 
         the container class called add(Employee e). Add a invilaid employee to container.
         Test Conduction: Automatic
         Test Type: Exception Test
         Sample Data: a invalid employee 
                     FirstName : Hello
                     LastName  : Kitty
                     Social Insurance Number :123456789 
                     Employee Type : Full time
                     Salary :  10000 
                     Date Of Hire : 2014-12-01 
                     DateOfBirth : 1999-09-08  
         Expected Outcome:  add method throws a exception
         Actual Test results: add method throws a exception
         \endverbatim
        */
        [TestMethod]
        public void Container_UnitTest_5()
        {
            bool expected = false;
            // arrange 
            FulltimeEmployee employee = new FulltimeEmployee();
            employee.SetfirstName("Hello");
            employee.SetlastName("Kitty");
            employee.SetsocialInsuranceNumber("123456789");
            employee.SetemployeeType(Employee.FULL_TIME);
            employee.Setsalary(10000);
            employee.SetdateOfHire("2014-12-01");
            employee.SetdateOfBirth("1999-09-08");

            Container contianer = new Container();
            bool actual = true;

            try
            {
                actual = contianer.add(employee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                actual = false;
            }
            // assert
            Assert.AreEqual(expected, actual, "Add an invalid successfully");
        }
        
    }
}

