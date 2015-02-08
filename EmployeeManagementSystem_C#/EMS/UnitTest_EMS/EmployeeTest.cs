using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllEmployees;

namespace UnitTest_EMS
{
    [TestClass]
    public class EmployeeTest
    {
        public const string testFailMessage = "The expected result did not macth the actual result";


        /**
        \details    Test identifier:       validateDate_String_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called validateDate(string date). it test that 
                                                        The CreateEmployee method fround in UIMenu requires this ability.
                                                        the method recognises a valid date from an invalid date.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         Date : "1992-03-16"
                    Expected outputs:                   true for a valid date. 
                    Observed outputs:                   true for a valid date. 
         \endverbatim
         */
        [TestMethod]
        public void validateDate_String_1()
        {
            Employee testObj= new Employee();
            //arrange
            string date = "1992-03-16";
            bool expected = true; 
            //act
            bool actual = testObj.validateDate(date);
            //assert
            Assert.AreEqual(expected, actual, "", testFailMessage); 
        }


        /**
        \details    Test identifier:       validateDate_String_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called validateDate(string date). it test that 
                                                        The CreateEmployee method fround in UIMenu requires this ability.
                                                        the method recognises a valid date from an invalid date.  Exception thrown
                                                        if invalid date found.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         Date : "1993-16"
                    Expected outputs:                   Exception Message: The Date provided is invalid. 
                    Observed outputs:                   Exception Message: The Date provided is invalid. 
         \endverbatim
         */
        [TestMethod]
        public void validateDate_String_2()
        {
            Employee testObj = new Employee();
            //arrange
            string date = "1993-16";
            bool actual;
            bool expected = false;
            try
            {
                //act
                actual = testObj.validateDate(date);
                Assert.AreEqual(expected, actual, "", testFailMessage);
            }
            catch (Exception e)
            {
                Assert.Fail("The Date provided is invalid");
                //assert
                StringAssert.Contains(e.Message, "The Date provided is invalid");
            }
        }


        /**
        \details    Test identifier:       validateSIN_String_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called validateSIN(). it test that 
                                                        the method recognises a valid SIN from an invalid SIN.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         SIN : "193-456-787"
                    Expected outputs:                   true for a valid SIN. 
                    Observed outputs:                   true for a valid SIN. 
         \endverbatim
         */
        [TestMethod]
        public void validateSIN_String_1()
        {
            //arrange
            Employee testObj = new Employee();
            string SIN = "046-454-286";
            testObj.SetsocialInsuranceNumber(SIN);
            bool expected = true;
            //act
            bool actual = testObj.validateSIN();
            //assert
            Assert.AreEqual(expected, actual, "", testFailMessage);
        }


        /**
        \details    Test identifier:       validateSIN_String_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called validateSIN(). it test that 
                                                        the method recognises a valid SIN from an invalid SIN.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         SIN : "1953-456-787"
                    Expected outputs:                   Exception Message: "The Sin provided is invalid". 
                    Observed outputs:                   Exception Message: "The Sin provided is invalid". 
         \endverbatim
         */
        [TestMethod]
        public void validateSIN_String_2()
        {
            Employee testObj = new Employee();
            //arrange
            string SIN = "1953-456-787";
            testObj.SetsocialInsuranceNumber(SIN);
            bool actual;
            bool expected = false;
            try
            {
                //act
                actual = testObj.validateSIN();
                Assert.AreEqual(expected, actual, "", testFailMessage);
            }
            catch (Exception e)
            {
                Assert.Fail("The Sin provided is invalid");
                //assert
                StringAssert.Contains(e.Message, "The SIN provided is invalid");
            }
        }



        /**
        \details    Test identifier:       Validate_ContractEmployee_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the contract employee class called validate() which validate all the attributes
                                                        of a Contract employee. It will test that the temp employee is a valid one. 
                                                        the method recognises a valid Contract employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         lastname:Diaby
                                                        dateOfBirth:1992-03-16
                                                        socialInsuranceNumber:123456782
                                                        dateOfHire:2005-03-16
                                                        dateOfTermination:2008-03-26
                                                        fixedContractAmount: 2000000            
                    Expected outputs:                   a true valid Contract employee
                    Observed outputs:                   a true valid Contract employee
         \endverbatim
         */
        [TestMethod]
        public void Validate_ContractEmployee_1()
        {
            //assign 
            ContractEmployee tempObj = new ContractEmployee();
            tempObj.SetemployeeType('C');
            tempObj.SetlastName("Diaby");
            tempObj.SetdateOfBirth("1912-03-16");
            tempObj.SetsocialInsuranceNumber("123456782");
            tempObj.SetcontractStartDate("2005-03-16");
            tempObj.SetcontractStopDate("2008-03-26");
            tempObj.SetfixedContractAmount((float)2000000);
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.Validate();
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }



        /**
        \details    Test identifier:       Validate_ContractEmployee_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the contract employee class called validate() which validate all the attributes
                                                        of a Contract employee. It will test that the temp employee is a valid one. 
                                                        the method recognises a valid Contract employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         lastname:Diaby
                                                        dateOfBirth:1992-03-16
                                                        socialInsuranceNumber:123456782
                                                        dateOfHire:2005-03-16
                                                        dateOfTermination:2008-03-26
                                                        fixedContractAmount: 2000000            
                    Expected outputs:                   Exception Message:The Business Number is not valid
                    Observed outputs:                   Exception Message:The Business Number is not valid
         \endverbatim
         */
        [TestMethod]
        public void Validate_ContractEmployee_2()
        {
            //assign 
            ContractEmployee tempObj = new ContractEmployee();
            tempObj.SetemployeeType('C');
            tempObj.SetlastName("Diaby");
            tempObj.SetfirstName("123");
            tempObj.SetdateOfBirth("1992-03-16");
            tempObj.SetsocialInsuranceNumber("123456789");
              tempObj.SetcontractStartDate("2005-03-16");
            tempObj.SetcontractStopDate("2008-03-26");
            tempObj.SetfixedContractAmount((float)2000000);
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.Validate();
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                
                StringAssert.Contains(e.Message, "The Business Number is not valid");
            }
        }


        /**
        \details    Test identifier:       SetfixedContractAmount_Float_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the contract employee class called SetfixedContract() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         houfixedContractAmount: 20000
     
                    Expected outputs:                   true for a valid rate.
                    Observed outputs:                   true for a valid rate.
         \endverbatim
         */
        [TestMethod]
        public void SetfixedContractAmount_Float_1()
        {
            //assign 
            ContractEmployee tempObj = new ContractEmployee();
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.SetfixedContractAmount((float)20000);
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }


        /**
        \details    Test identifier:       SetfixedContractAmount_Float_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the contract employee class called SetfixedContract() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         houfixedContractAmount: -20000
     
                    Expected outputs:                   true for a valid rate.
                    Observed outputs:                   true for a valid rate.
         \endverbatim
         */
        [TestMethod]
        public void SetfixedContractAmount_Float_2()
        {
            //assign 
            ContractEmployee tempObj = new ContractEmployee();
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.SetfixedContractAmount((float)(-20000));
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                Assert.Fail("The fixed contract amount is invalid");
                StringAssert.Contains(e.Message, "The fixed contract amount is invalid");
            }
        }


        /**
        \details    Test identifier:       Validate_FullTimeEmployee_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the Fulltime employee class called validate() which validate all the attributes
                                                        of a Full time employee. It will test that the temp employee is a valid one. 
                                                        the method recognises a valid Full employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         firstname:Sekou
                                                        lastname:Diaby
                                                        dateOfBirth:1982-03-16
                                                        socialInsuranceNumber:193-456-787
                                                        dateOfHire:2005-03-16
                                                        dateOfTermination:2008-03-26
                                                        salary: 11.25
                    Expected outputs:                   a true valid Fulltime employee
                    Observed outputs:                   a true valid Fulltime employee
         \endverbatim
         */
        [TestMethod]
        public void Validate_FullTimeEmployee_1()
        {
            //assign 
            FulltimeEmployee tempObj = new FulltimeEmployee();
            tempObj.SetemployeeType('F');
            tempObj.SetlastName("Diaby");
            tempObj.SetfirstName("Sekou");
            tempObj.SetdateOfBirth("1982-03-16");
            tempObj.SetsocialInsuranceNumber("193-456-787");
            tempObj.SetdateOfHire("2005-03-16");
            tempObj.SetdateOfTermination("2008-03-26");
            tempObj.Setsalary((float)11.25);
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.Validate();
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }


        /**
        \details    Test identifier:       Validate_FullTimeEmployee_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the Fulltime employee class called validate() which validate all the attributes
                                                        of a Full time employee. It will test that the temp employee is a valid one. 
                                                        the method recognises a valid Full employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         firstname:123
                                                        lastname:Diaby
                                                        dateOfBirth:1982-03-16
                                                        socialInsuranceNumber:193-456-787
                                                        dateOfHire:2005-03-16
                                                        dateOfTermination:2008-03-26
                                                        salary: 11.25
                    Expected outputs:                   Exception Message:First Name must not be empty and must only contain alphabets, hyphen and dashes
                    Observed outputs:                   Exception Message:First Name must not be empty and must only contain alphabets, hyphen and dashes
         \endverbatim
         */
        [TestMethod]
        public void Validate_FullTimeEmployee_2()
        {
            //assign 
            FulltimeEmployee tempObj = new FulltimeEmployee();
            tempObj.SetemployeeType('F');
            tempObj.SetlastName("Diaby");
            tempObj.SetfirstName("123");
            tempObj.SetdateOfBirth("1982-03-16");
            tempObj.SetsocialInsuranceNumber("193-456-787");
            tempObj.SetdateOfHire("2005-03-16");
            tempObj.SetdateOfTermination("2008-03-26");
            tempObj.Setsalary((float)11.25);
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.Validate();
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "First Name must not be empty and must only contain alphabets, hyphen and dashes");
            }
        }


        /**
        \details    Test identifier:       Setsalary_Float_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called Setsalary() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         salary: 1100.25
                    Expected outputs:                   true for a valid salary.
                    Observed outputs:                   true for a valid salary.
         \endverbatim
         */
        [TestMethod]
        public void Setsalary_Float_1()
        {
            //assign 
            FulltimeEmployee tempObj = new FulltimeEmployee();
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.Setsalary((float)1100.25);
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }


        /**
        \details    Test identifier:       SethourlyRate_Float_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called Setsalary() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         salary: -1100.25
                    Expected outputs:                   Exception Message:The salary is invalid
                    Observed outputs:                   Exception Message:The salary is invalid
         \endverbatim
         */
        [TestMethod]
        public void Setsalary_Float_2()
        {
            //assign 
            FulltimeEmployee tempObj = new FulltimeEmployee();
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.Setsalary((float)(-1100.25));
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                Assert.Fail("The salary is invalid");
                StringAssert.Contains(e.Message, "The salary is invalid");
            }
        }



        /**
        \details    Test identifier:       Validate_PartTimeEmployee_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called validate() which validate all the attributes
                                                        of a part time employee. It will test that the temp employee is a valid one. 
                                                        the method recognises a valid partime employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         firstname:Sekou
                                                        lastname:Diaby
                                                        dateOfBirth:1982-03-16
                                                        socialInsuranceNumber:193-456-787
                                                        dateOfHire:2005-03-16
                                                        dateOfTermination:2008-03-26
                                                        salary: 11.25
                    Expected outputs:                   a true valid parttime employee
                    Observed outputs:                   a true valid parttime employee
         \endverbatim
         */
        [TestMethod]
        public void Validate_PartTimeEmployee_1()
        {
            //assign 
            ParttimeEmployee tempObj = new ParttimeEmployee();
            tempObj.SetemployeeType('P');
            tempObj.SetlastName("Diaby");
            tempObj.SetfirstName("Sekou");
            tempObj.SetdateOfBirth("1982-03-16");
            tempObj.SetsocialInsuranceNumber("193-456-787");
            tempObj.SetdateOfHire("2005-03-16");
            tempObj.SetdateOfTermination("2008-03-26");
            tempObj.SethourlyRate((float)11.25);
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.Validate();
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }


        /**
        \details    Test identifier:       Validate_PartTimeEmployee_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called validate() which validate all the attributes
                                                        of a part time employee. It will test that the temp employee is a valid one. 
                                                        the method recognises a valid partime employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         firstname:Sekou
                                                        lastname:Diaby
                                                        dateOfBirth:1992-03-16
                                                        socialInsuranceNumber:193-456-787
                                                        dateOfHire:2005-03-16
                                                        dateOfTermination:2008-03-26
                                                        salary: 11.25
                    Expected outputs:                   Exception Message:Employee must be 14 years or older to start work
                    Observed outputs:                   Exception Message:Employee must be 14 years or older to start work
         \endverbatim
         */
        [TestMethod]
        public void Validate_PartTimeEmployee_2()
        {
            //assign 
            ParttimeEmployee tempObj = new ParttimeEmployee();
            tempObj.SetemployeeType('P');
            tempObj.SetlastName("Diaby");
            tempObj.SetfirstName("Sekou");
            tempObj.SetdateOfBirth("1992-03-16");
            tempObj.SetsocialInsuranceNumber("193-456-787");
            tempObj.SetdateOfHire("2005-03-16");
            tempObj.SetdateOfTermination("2008-03-26");
            tempObj.SethourlyRate((float)11.25);
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.Validate();
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Employee must be 14 years or older to start work");
            }
        }


        /**
        \details    Test identifier:       SethourlyRate_Float_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called SethourlyRate() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         hourlyRate: 11.25
                    Expected outputs:                   true for a valid rate.
                    Observed outputs:                   true for a valid rate.
         \endverbatim
         */
        [TestMethod]
        public void SethourlyRate_Float_1()
        {
            //assign 
            ParttimeEmployee tempObj = new ParttimeEmployee();
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.SethourlyRate((float)11.25);
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }


        /**
        \details    Test identifier:       SethourlyRate_Float_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called SethourlyRate() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         hourlyRate: -11.25
                    Expected outputs:                   Exception Message:The Hourly rate is invalid
                    Observed outputs:                   Exception Message:The Hourly rate is invalid
         \endverbatim
         */
        [TestMethod]
        public void SethourlyRate_Float_2()
        {
            //assign 
            ParttimeEmployee tempObj = new ParttimeEmployee();
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.SethourlyRate((float)(-11.25));
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                Assert.Fail("The Hourly rate is invalid");
                StringAssert.Contains(e.Message, "The Hourly rate is invalid");
            }
        }

        /**
        \details    Test identifier:       Validate_SeasonalEmployee_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the Seasonal class called validate() which validate all the attributes
                                                        of a Seasonal employee. It will test that the temporary employee is a valid one. 
                                                        the method recognises a valid Seasonal employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         firstname:Sekou
                                                        lastname:Diaby
                                                        dateOfBirth:1992-03-16
                                                        socialInsuranceNumber:193-456-787
                                                        Season: Fall
                                                        Piecepay: 1000
                    Expected outputs:                   a true valid Seasonal employee
                    Observed outputs:                   a true valid Seasonal employee
         \endverbatim
         */
        [TestMethod]
        public void Validate_SeasonalEmployee_1()
        {
            //assign 

            SeasonalEmployee tempObj = new SeasonalEmployee();
            tempObj.SetemployeeType('S');
            tempObj.SetlastName("Diaby");
            tempObj.SetfirstName("Sekou");
            tempObj.SetdateOfBirth("1992-03-16");
            tempObj.SetsocialInsuranceNumber("193-456-787");
            tempObj.SetpiecePay((float)1000);
            tempObj.Setseason("FALL");
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.Validate();
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }


        /**
        \details    Test identifier:       Validate_SeasonalEmployee_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the Seasonal class called validate() which validate all the attributes
                                                        of a Seasonal employee. It will test that the temporary employee is a valid one. 
                                                        the method recognises a valid Seasonal employee from an invalid one.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         firstname:Sekou
                                                        lastname:Diaby
                                                        dateOfBirth:1992-03-16
                                                        socialInsuranceNumber:193-456-787
                                                        Season: WIND
                                                        Piecepay: 1000
                    Expected outputs:                   Exception Message:The Season provide is not a valid season
                    Observed outputs:                   Exception Message:The Season provide is not a valid season
         \endverbatim
         */
        [TestMethod]
        public void Validate_SeasonalEmployee_2()
        {
            //assign 
            SeasonalEmployee tempObj = new SeasonalEmployee();
            tempObj.SetemployeeType('S');
            tempObj.SetlastName("Diaby");
            tempObj.SetfirstName("Sekou");
            tempObj.SetdateOfBirth("1992-03-16");
            tempObj.SetsocialInsuranceNumber("193-456-787");
            tempObj.SetpiecePay((float)1000);
            tempObj.Setseason("WIND");
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.Validate();
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The Season provide is not a valid season");
            }
        }


        /**
        \details    Test identifier:       SetpiecePay_Float_1
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called SetpiecePay() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Normal Test
                    Input data:                         firstname:Sekou
                                                        lastname:Diaby
                                                        dateOfBirth:1992-03-16
                                                        socialInsuranceNumber:193-456-787
                                                        Season: WIND
                                                        Piecepay: 1000
                    Expected outputs:                   Exception Message:The Season provide is not a valid season
                    Observed outputs:                   Exception Message:The Season provide is not a valid season
         \endverbatim
         */
        [TestMethod]
        public void SetpiecePay_Float_1()
        {
            //assign 
            SeasonalEmployee tempObj = new SeasonalEmployee();
            bool expected = true;
            bool actual;
            //act
            actual = tempObj.SetpiecePay((float)1000);
            // Assert
            Assert.AreEqual(expected, actual, testFailMessage);
        }


        /**
        \details    Test identifier:       SetpiecePay_Float_2
        \verbatim
                    Description:                        This function tests a method from 
                                                        the all employee class called SetpiecePay() which set the current 
                                                        employee's rate to the parameter passed in. It will test that 
                                                        the rate provided is a valid one. if not an exception is thrown.
                    Method of execution:                Automatic  
                    Test Type:                          Exception Test
                    Input data:                         Piecepay: -1000
                    Expected outputs:                   Exception Message:The piecePay is invalid
                    Observed outputs:                   Exception Message:The piecePay is invalid
         \endverbatim
         */
        [TestMethod]
        public void SetpiecePay_Float_2()
        {
            //assign 
            SeasonalEmployee tempObj = new SeasonalEmployee();
            bool expected = false;
            bool actual;
            try
            {
                //act
                actual = tempObj.SetpiecePay((float)(-1000));
                // Assert
                Assert.AreEqual(expected, actual, testFailMessage);
            }
            catch (Exception e)
            {
                Assert.Fail("The piecePay is invalid");
                StringAssert.Contains(e.Message, "The piecePay is invalid");
            }
        }

        ///
        /// \METHODS NOT TESTED IN Employee.cs : 
        /// \SetemployeeType(char empType)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetfirstName(string fName)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetlastName(string lName)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetdateOfBirth(string DOB)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetsocialInsuranceNumber(string SIN)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \GetfirstName()
        /// \Reason: It is an accessor that does not do basic validation.
        ///          
        /// \GetlastName()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \GetdateOfBirth()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \GetemployeeType()
        /// \Reason: It is an accessor that does not do basic validation.
        ///          
        /// \GetsocialInsuranceNumber()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \Details() 
        /// \Reason: This method has no parameters and return value and just prints to the console.
        /// 
        

        ///
        /// \METHODS NOT TESTED IN ContractEmployee.cs : 
        /// \SetcontractStartDate(string newConStartDate)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetcontractStopDate(string newConStopDate)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetfixedContractAmount(float newAmount)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \GetcontractStartDate()
        /// \Reason: It is an accessor that does not do basic validation.
        ///          
        /// \GetcontractStopDate()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \GetfixedContractAmount()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \Details() 
        /// \Reason: This method has no parameters and return value and just prints to the console.
        /// 


        ///
        /// \METHODS NOT TESTED IN PartTimeEmployee.cs : 
        /// \SetdateOfHire(string newDOH)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetdateOfTermination(string newDOT)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SethourlyRate(float newHourlyRate)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \GetdateOfHire()
        /// \Reason: It is an accessor that does not do basic validation.
        ///          
        /// \GetdateOfTermination()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \GethourlyRate()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \Details() 
        /// \Reason: This method has no parameters and return value and just prints to the console.
        /// 


        ///
        /// \METHODS NOT TESTED IN FullTimeEmployee.cs : 
        /// \SetdateOfHire(string newDOH)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetdateOfTermination(string newDOT)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \Setsalary(float salary)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \GetdateOfHire()
        /// \Reason: It is an accessor that does not do basic validation.
        ///          
        /// \GetdateOfTermination()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \Getsalary()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \Details() 
        /// \Reason: This method has no parameters and return value and just prints to the console.
        /// 


        ///
        /// \METHODS NOT TESTED IN SeasonalEmployee.cs : 
        /// \Setseason(string newSeason)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \SetpiecePay(float newPiecePay)
        /// \Reason: It is a mutator that does basic validation which is repeated in 
        ///          the validate function. To avoid repetition.
        ///          
        /// \Getseason()
        /// \Reason: It is an accessor that does not do basic validation.
        ///          
        /// \GetpiecePay()
        /// \Reason: It is an accessor that does not do basic validation.
        /// 
        /// \Details() 
        /// \Reason: This method has no parameters and return value and just prints to the console.
        /// 
    }


}
